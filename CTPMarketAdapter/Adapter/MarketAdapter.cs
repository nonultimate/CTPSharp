using CTPCore;
using CTPMarketAdapter.Interface;
using CTPMarketAdapter.Model;
using CTPMarketApi;
using System;
using System.Collections.Concurrent;

namespace CTPMarketAdapter.Adapter
{
    /// <summary>
    /// CTP行情适配器
    /// </summary>
    public class MarketAdapter : IMarketApi
    {
        #region 私有变量

        /// <summary>
        /// 行情接口类实例
        /// </summary>
        private MarketApi _api;

        /// <summary>
        /// 回调方法字典
        /// </summary>
        private ConcurrentDictionary<int, object> _dataDict = new ConcurrentDictionary<int, object>();

        /// <summary>
        /// 数据回调字典
        /// </summary>
        private ConcurrentDictionary<int, object> _dataCallbackDict = new ConcurrentDictionary<int, object>();

        /// <summary>
        /// 请求编号
        /// </summary>
        private int _requestID;

        /// <summary>
        /// 是否已连接
        /// </summary>
        private bool _isConnected;

        #endregion

        /// <summary>
        /// 创建CTP行情适配器
        /// </summary>
        /// <param name="flowPath">存储订阅信息文件的目录</param>
        public MarketAdapter(string flowPath = "")
        {
            _api = new MarketApi("", "", flowPath);
            _api.OnRspError += OnRspError;
            _api.OnFrontConnected += OnFrontConnected;
            _api.OnFrontDisconnected += OnFrontDisconnected;
            _api.OnRspUserLogin += OnRspUserLogin;
            _api.OnRspUserLogout += OnRspUserLogout;
            _api.OnRtnDepthMarketData += OnRtnDepthMarketData;
        }

        #region 回调事件

        /// <summary>
        /// 行情数据改变事件
        /// </summary>
        public event MarketDataChangedHandler OnMarketDataChanged;

        #endregion

        #region 接口方法

        /// <summary>
        /// 是否已连接
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return _isConnected;
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="callback">连接服务器回调</param>
        /// <param name="brokerID">经纪商代码</param>
        /// <param name="frontAddress">前置服务器地址，tcp://IP:Port</param>
        public void Connect(DataCallback callback, string brokerID, string frontAddress)
        {
            _api.BrokerID = brokerID;
            _api.FrontAddr = frontAddress;

            AddCallback(callback, -1);
            _api.Connect();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="callback">断开连接回调</param>
        public void Disconnect(DataCallback callback)
        {
            AddCallback(callback, -2);
            _api.Disconnect();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="callback">登录回调</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="password">密码</param>
        public void UserLogin(DataCallback callback, string investorID, string password)
        {
            int requestID = AddCallback(callback, -3);
            _api.UserLogin(requestID, investorID, password);
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="callback">登出回调</param>
        public void UserLogout(DataCallback callback)
        {
            int requestID = AddCallback(callback, -4);
            _api.UserLogout(requestID);
        }

        /// <summary>
        /// 获取交易日
        /// </summary>
        /// <returns></returns>
        public string GetTradingDay()
        {
            return _api.GetTradingDay();
        }

        /// <summary>
        /// 订阅行情
        /// </summary>
        /// <param name="instruments">合约列表，传空订阅所有</param>
        public void SubscribeMarket(params string[] instruments)
        {
            _api.SubscribeMarketData(instruments);
        }

        /// <summary>
        /// 退订行情
        /// </summary>
        /// <param name="instruments">合约列表，传空退订所有</param>
        public void UnsubscribeMarket(params string[] instruments)
        {
            _api.UnsubscribeMarketData(instruments);
        }

        #endregion

        #region 回调方法

        /// <summary>
        /// 获取请求ID
        /// </summary>
        /// <returns></returns>
        private int GetRequestID()
        {
            lock (_api)
            {
                _requestID += 1;
                return _requestID;
            }
        }

        /// <summary>
        /// 添加回调方法
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <param name="requestID">请求编号</param>
        private int AddCallback(object callback, int requestID = 0)
        {
            if (requestID == 0)
            {
                requestID = GetRequestID();
            }
            if (callback != null)
            {
                if (_dataCallbackDict.ContainsKey(requestID))
                {
                    object tmp;
                    _dataCallbackDict.TryRemove(requestID, out tmp);
                }
                _dataCallbackDict.TryAdd(requestID, callback);
            }
            return requestID;
        }

        /// <summary>
        /// 执行回调方法
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="dataResult">返回结果</param>
        private void ExecuteCallback(int requestID, DataResult dataResult)
        {
            if (_dataCallbackDict.ContainsKey(requestID))
            {
                object callback;
                if (_dataCallbackDict.TryRemove(requestID, out callback))
                {
                    if (callback != null)
                    {
                        ((DataCallback)callback)(dataResult);
                    }
                }
            }
        }

        /// <summary>
        /// 设置错误信息
        /// </summary>
        /// <param name="result">返回结果</param>
        /// <param name="pRspInfo">错误信息</param>
        private void SetError(DataResult result, CThostFtdcRspInfoField pRspInfo)
        {
            result.ErrorCode = pRspInfo.ErrorID.ToString();
            result.Error = pRspInfo.ErrorMsg;
            result.IsSuccess = false;
        }

        /// <summary>
        /// 错误回调
        /// </summary>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspError(ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
        {
            if (_dataCallbackDict.ContainsKey(nRequestID))
            {
                var callback = _dataCallbackDict[nRequestID];
                if (callback != null)
                {
                    DataResult result = new DataResult();
                    SetError(result, pRspInfo);
                    ExecuteCallback(nRequestID, result);
                }
            }
        }

        /// <summary>
        /// 连接成功回调
        /// </summary>
        private void OnFrontConnected()
        {
            ExecuteCallback(-1, new DataResult()
            {
                IsSuccess = true
            });
            _isConnected = true;
        }

        /// <summary>
        /// 断开连接回调
        /// </summary>
        /// <param name="nReason">原因</param>
        private void OnFrontDisconnected(int nReason)
        {
            ExecuteCallback(-2, new DataResult()
            {
                IsSuccess = true
            });
            _isConnected = false;
        }

        /// <summary>
        /// 登录回调
        /// </summary>
        /// <param name="pRspUserLogin">登录返回结果</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspUserLogin(ref CThostFtdcRspUserLoginField pRspUserLogin, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast)
        {
            DataResult result = new DataResult();
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                _api.FrontID = pRspUserLogin.FrontID;
                _api.SessionID = pRspUserLogin.SessionID;
                _api.MaxOrderRef = pRspUserLogin.MaxOrderRef;
                result.IsSuccess = true;
            }
            ExecuteCallback(-3, result);
        }

        /// <summary>
        /// 登出回调
        /// </summary>
        /// <param name="pUserLogout">登出返回结果</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspUserLogout(ref CThostFtdcUserLogoutField pUserLogout, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast)
        {
            DataResult result = new DataResult();
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                result.IsSuccess = true;
            }
            ExecuteCallback(-4, result);
        }

        /// <summary>
        /// 行情数据回调
        /// </summary>
        /// <param name="pDepthMarketData">深度行情</param>
        private void OnRtnDepthMarketData(ref CThostFtdcDepthMarketDataField pDepthMarketData)
        {
            CTPMarketData marketData = new CTPMarketData()
            {
                InstrmentID = pDepthMarketData.InstrumentID,
                ExchangeID = pDepthMarketData.ExchangeInstID,
                LastPrice = ConvertToDecimal(pDepthMarketData.LastPrice),
                PreSettlementPrice = ConvertToDecimal(pDepthMarketData.PreSettlementPrice),
                PreClosePrice = ConvertToDecimal(pDepthMarketData.PreClosePrice),
                PreOpenInterest = ConvertToDecimal(pDepthMarketData.PreOpenInterest),
                OpenPrice = ConvertToDecimal(pDepthMarketData.OpenPrice),
                HighestPrice = ConvertToDecimal(pDepthMarketData.HighestPrice),
                LowestPrice = ConvertToDecimal(pDepthMarketData.LowestPrice),
                Volume = pDepthMarketData.Volume,
                Turnover = ConvertToDecimal(pDepthMarketData.Turnover),
                OpenInterest = ConvertToDecimal(pDepthMarketData.OpenInterest),
                ClosePrice = ConvertToDecimal(pDepthMarketData.ClosePrice),
                SettlementPrice = ConvertToDecimal(pDepthMarketData.SettlementPrice),
                UpperLimitPrice = ConvertToDecimal(pDepthMarketData.UpperLimitPrice),
                LowerLimitPrice = ConvertToDecimal(pDepthMarketData.LowerLimitPrice),
                BidPrice1 = ConvertToDecimal(pDepthMarketData.BidPrice1),
                BidPrice2 = ConvertToDecimal(pDepthMarketData.BidPrice2),
                BidPrice3 = ConvertToDecimal(pDepthMarketData.BidPrice3),
                BidPrice4 = ConvertToDecimal(pDepthMarketData.BidPrice4),
                BidPrice5 = ConvertToDecimal(pDepthMarketData.BidPrice5),
                BidVolume1 = pDepthMarketData.BidVolume1,
                BidVolume2 = pDepthMarketData.BidVolume2,
                BidVolume3 = pDepthMarketData.BidVolume3,
                BidVolume4 = pDepthMarketData.BidVolume4,
                BidVolume5 = pDepthMarketData.BidVolume5,
                AskPrice1 = ConvertToDecimal(pDepthMarketData.AskPrice1),
                AskPrice2 = ConvertToDecimal(pDepthMarketData.AskPrice2),
                AskPrice3 = ConvertToDecimal(pDepthMarketData.AskPrice3),
                AskPrice4 = ConvertToDecimal(pDepthMarketData.AskPrice4),
                AskPrice5 = ConvertToDecimal(pDepthMarketData.AskPrice5),
                AskVolume1 = pDepthMarketData.AskVolume1,
                AskVolume2 = pDepthMarketData.AskVolume2,
                AskVolume3 = pDepthMarketData.AskVolume3,
                AskVolume4 = pDepthMarketData.AskVolume4,
                AskVolume5 = pDepthMarketData.AskVolume5,
            };
            if (marketData.LastPrice > 0 && marketData.PreClosePrice > 0)
            {
                marketData.AdvanceDeclineAmount = marketData.LastPrice - marketData.PreClosePrice;
                marketData.AdvanceDecline = Math.Round(marketData.AdvanceDeclineAmount / marketData.PreClosePrice, 4,
                    MidpointRounding.AwayFromZero);
            }
            if (OnMarketDataChanged != null)
            {
                OnMarketDataChanged.Invoke(marketData);
            }
        }

        /// <summary>
        /// 将double类型转为decimal类型
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns></returns>
        private decimal ConvertToDecimal(double value)
        {
            decimal result = 0;
            decimal.TryParse(value.ToString(), out result);

            return result;
        }

        #endregion
    }
}
