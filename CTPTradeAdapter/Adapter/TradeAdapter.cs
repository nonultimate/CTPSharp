using CTPCore;
using CTPTradeAdapter.Interface;
using CTPTradeAdapter.Model;
using CTPTradeApi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Timers;

namespace CTPTradeAdapter.Adapter
{
    /// <summary>
    /// CTP交易适配器，支持同时调用多个查询接口(利用任务队列解决CTP单个查询1秒限制)
    /// </summary>
    public class TradeAdapter : ITradeApi
    {
        #region 私有变量

        /// <summary>
        /// 交易接口类实例
        /// </summary>
        private TradeApi _api;

        private DataCollect _dataCollect;

        /// <summary>
        /// 回调方法字典
        /// </summary>
        private ConcurrentDictionary<int, object> _dataDict = new ConcurrentDictionary<int, object>();

        /// <summary>
        /// 数据回调字典
        /// </summary>
        private ConcurrentDictionary<int, object> _dataCallbackDict = new ConcurrentDictionary<int, object>();

        /// <summary>
        /// 查询方法字典
        /// </summary>
        private Dictionary<int, Action> _queryMethodDict = new Dictionary<int, Action>();

        /// <summary>
        /// 请求编号
        /// </summary>
        private int _requestID;

        /// <summary>
        /// 是否已连接
        /// </summary>
        private bool _isConnected;

        /// <summary>
        /// 上次查询时间
        /// </summary>
        private DateTime _queryTime = DateTime.MinValue;

        /// <summary>
        /// 执行失败次数
        /// </summary>
        private int _executeFailCount;

        /// <summary>
        /// 查询时间限制定时器
        /// </summary>
        private Timer _timer;

        #endregion

        #region 属性

        /// <summary>
        /// 网卡信息
        /// </summary>
        public string MacAddress
        {
            get { return _api.MacAddress; }
            set { _api.MacAddress = value; }
        }

        /// <summary>
        /// 产品信息
        /// </summary>
        public string ProductInfo
        {
            get { return _api.ProductInfo; }
            set { _api.ProductInfo = value; }
        }

        /// <summary>
        /// 接口信息
        /// </summary>
        public string InterfaceInfo
        {
            get { return _api.InterfaceInfo; }
            set { _api.InterfaceInfo = value; }
        }

        /// <summary>
        /// 协议信息
        /// </summary>
        public string ProtocolInfo
        {
            get { return _api.ProtocolInfo; }
            set { _api.ProtocolInfo = value; }
        }

        #endregion

        #region 构造方法

        /// <summary>
        /// 创建CTP交易适配器
        /// </summary>
        public TradeAdapter()
        {
            _api = new TradeApi();
            _dataCollect = new DataCollect();

            BindEvents();
        }

        /// <summary>
        /// 创建CTP交易适配器
        /// </summary>
        /// <param name="flowPath">存储订阅信息文件的目录</param>
        public TradeAdapter(string flowPath)
        {
            _api = new TradeApi("", "", flowPath);

            BindEvents();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void BindEvents()
        {
            _api.OnRspError += OnRspError;
            _api.OnFrontConnect += OnConnected;
            _api.OnDisconnected += OnDisconnected;
            _api.OnRspAuthenticate += OnRspAuthenticate;
            _api.OnRspUserLogin += OnRspUserLogin;
            _api.OnRspUserLogout += OnRspUserLogout;
            _api.OnRspUserPasswordUpdate += OnRspUserPasswordUpdate;
            _api.OnRspTradingAccountPasswordUpdate += OnTradingPasswordUpdate;
            _api.OnRspUserAuthMethod += OnUserAuthMethod;
            _api.OnRspGenUserCaptcha += OnGenUserCaptcha;
            _api.OnRspGenUserText += OnGenUserText;
            _api.OnRtnOrder += OnRtnOrder;
            _api.OnRtnTrade += OnRtnTrade;
            _api.OnRspOrderInsert += OnRspOrderInsert;
            _api.OnRspOrderAction += OnRspOrderAction;
            _api.OnRspQryOrder += OnRspQueryOrder;
            _api.OnRspQryTrade += OnRspQueryTrade;
            _api.OnRspQryTradingAccount += OnRspQueryTradingAccount;
            _api.OnRspQryInvestorPosition += OnRspQueryInvestorPosition;
            _api.OnRspParkedOrderInsert += OnRspParkedOrderInsert;
            _api.OnRspParkedOrderAction += OnRspParkedOrderAction;
            _api.OnRspQryParkedOrder += OnRspQueryParkedOrder;
            _api.OnRspQryParkedOrderAction += OnRspQueryParkedOrderAction;
            _api.OnRspQryInstrument += OnRspQueryInstrument;
            _api.OnRspQryInvestor += OnRspQueryInvestor;
            _api.OnRspQryInvestorPositionDetail += OnRspQueryInvestorPositionDetail;
            _api.OnRspQryNotice += OnRspQueryNotice;
            _api.OnRspQrySettlementInfo += OnRspQrySettlementInfo;
            _api.OnRspQrySettlementInfoConfirm += OnRspQrySettlementInfoConfirm;
            _api.OnRspSettlementInfoConfirm += OnRspSettlementInfoConfirm;

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += ExecuteMethod;
        }

        #endregion

        #region 回调事件

        /// <summary>
        /// 委托回报
        /// </summary>
        public event OrderReturnHandler OnReturnOrder;

        /// <summary>
        /// 成交回报
        /// </summary>
        public event TradeReturnHandler OnReturnTrade;

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
            _isConnected = false;
            _api.Disconnect();
        }

        /// <summary>
        /// 客户端认证
        /// </summary>
        /// <param name="callback">认证回调</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="productInfo">产品信息</param>
        /// <param name="authCode">认证代码</param>
        /// <param name="appID">应用代码</param>
        /// <returns></returns>
        public int Authenticate(DataCallback callback, string investorID, string productInfo, string authCode, string appID)
        {
            int requestID = AddCallback(callback, -5);
            return _api.Authenticate(requestID, investorID, productInfo, authCode, appID);
        }

        /// <summary>
        /// 需要在终端认证成功后，用户登录前调用该接口
        /// </summary>
        /// <param name="clientIP">终端IP</param>
        /// <param name="clientPort">终端端口</param>
        /// <param name="loginTime">登录时间</param>
        /// <returns></returns>
        public int RegisterUserSystemInfo(string clientIP, int clientPort, string loginTime)
        {
            int result = -1;
            var systemInfo = _dataCollect.GetSsystemInfo();
            if (systemInfo.Item2 > 0)
            {
                result = _api.RegisterUserSystemInfo(systemInfo.Item1, systemInfo.Item2, clientIP, clientPort, loginTime);
            }
            return result;
        }

        /// <summary>
        /// 上报用户终端信息，用于中继服务器操作员登录模式
        /// 操作员登录后，可以多次调用该接口上报客户信息
        /// </summary>
        /// <param name="clientIP">终端IP</param>
        /// <param name="clientPort">终端端口</param>
        /// <param name="loginTime">登录时间</param>
        /// <returns></returns>
        public int SubmitUserSystemInfo(string clientIP, int clientPort, string loginTime)
        {
            int result = -1;
            var systemInfo = _dataCollect.GetSsystemInfo();
            if (systemInfo.Item2 > 0)
            {
                result = _api.SubmitUserSystemInfo(systemInfo.Item1, systemInfo.Item2, clientIP, clientPort, loginTime);
            }
            return result;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="callback">登录回调</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="password">密码</param>
        /// <param name="oneTimePassword">动态密码</param>
        public int UserLogin(DataCallback callback, string investorID, string password, string oneTimePassword = null)
        {
            int requestID = AddCallback(callback, -3);

            return _api.UserLogin(requestID, investorID, password);
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="callback">登出回调</param>
        public int UserLogout(DataCallback callback)
        {
            int requestID = AddCallback(callback, -4);
            return _api.UserLogout(requestID);
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
        /// 更新用户口令
        /// </summary>
        /// <param name="callback">更新回调</param>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public int UpdateUserPassword(DataCallback callback, string oldPassword, string newPassword)
        {
            int requestID = AddCallback(callback);
            return _api.UserPasswordupdate(requestID, _api.InvestorID, oldPassword, newPassword);
        }

        /// <summary>
        /// 更新资金账号口令
        /// </summary>
        /// <param name="callback">更新回调</param>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public int UpdateTradingAccountPassword(DataCallback callback, string oldPassword, string newPassword)
        {
            int requestID = AddCallback(callback);
            return _api.TradingAccountPasswordUpdate(requestID, _api.InvestorID, oldPassword, newPassword);
        }

        /// <summary>
        /// 查询用户当前支持的认证模式
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="tradingDay">交易日</param>
        /// <returns></returns>
        public int UserAuthMethod(DataCallback callback, string investorID, string tradingDay)
        {
            int requestID = AddCallback(callback);
            return _api.UserAuthMethod(requestID, _api.BrokerID, investorID, tradingDay);
        }

        /// <summary>
        /// 用户发出获取图形验证码请求
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="tradingDay">交易日</param>
        /// <returns>当前可以用的认证模式
        /// 0代表无需认证模式
        /// A从低位开始最后一位代表图片验证码
        /// 倒数第二位代表动态口令
        /// 倒数第三位代表短信验证码类型</returns>
        public int GenUserCaptcha(DataCallback callback, string investorID, string tradingDay)
        {
            int requestID = AddCallback(callback);
            return _api.GenUserCaptcha(requestID, _api.BrokerID, investorID, tradingDay);
        }

        /// <summary>
        /// 用户发出获取短信验证码请求
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="tradingDay">交易日</param>
        /// <returns></returns>
        public int GenUserText(DataCallback callback, string investorID, string tradingDay)
        {
            int requestID = AddCallback(callback);
            return _api.GenUserText(requestID, _api.BrokerID, investorID, tradingDay);
        }

        /// <summary>
        /// 用户发出带有验证码的登录请求
        /// </summary>
        /// <param name="callback">登录回调</param>
        /// <param name="request">登录请求</param>
        /// <returns></returns>
        public int UserLoginWithCaptcha(DataCallback callback, LoginRequest request)
        {
            int requestID = AddCallback(callback);
            CThostFtdcReqUserLoginWithCaptchaField req = ConvertToLoginField(request);

            if (request.LoginType == CaptchaType.Captcha)
            {
                return _api.UserLoginWithCaptcha(requestID, req);
            }
            else if (request.LoginType == CaptchaType.Text)
            {
                return _api.UserLoginWithText(requestID, req);
            }
            else
            {
                return _api.UserLoginWithOTP(requestID, req);
            }
        }

        /// <summary>
        /// 报单
        /// </summary>
        /// <param name="callback">报单回调</param>
        /// <param name="parameter">报单参数</param>
        public int InsertOrder(DataCallback<OrderInfo> callback, OrderParameter parameter)
        {
            int requestID = AddCallback(callback);
            CThostFtdcInputOrderField req = ConvertToInputOrderField(parameter);
            req.RequestID = requestID;
            return _api.OrderInsert(requestID, req);
        }

        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="callback">撤单回调</param>
        /// <param name="parameter">撤单参数</param>
        public int CancelOrder(DataCallback<OrderInfo> callback, CancelOrderParameter parameter)
        {
            int requestID = AddCallback(callback);
            CThostFtdcInputOrderActionField req = ConvertToInputOrderActionField(parameter);
            req.RequestID = requestID;
            return _api.OrderAction(requestID, req);
        }

        /// <summary>
        /// 预埋单录入
        /// </summary>
        /// <param name="callback">报单回调</param>
        /// <param name="parameter">预埋单参数</param>
        public int InsertParkedOrder(DataCallback<ParkedOrderInfo> callback, OrderParameter parameter)
        {
            int requestID = AddCallback(callback);
            CThostFtdcParkedOrderField req = ConvertToParkedOrderField(parameter);
            req.RequestID = requestID;
            return _api.ParkedOrderInsert(requestID, req);
        }

        /// <summary>
        /// 预埋撤单
        /// </summary>
        /// <param name="callback">撤单回调</param>
        /// <param name="parameter">预埋单撤单参数</param>
        public int CancelParkedOrder(DataCallback<ParkedOrderInfo> callback, CancelOrderParameter parameter)
        {
            int requestID = AddCallback(callback);
            CThostFtdcParkedOrderActionField req = ConvertToParkedOrderActionField(parameter);
            req.RequestID = requestID;
            return _api.ParkedOrderAction(requestID, req);
        }

        /// <summary>
        /// 确认结算结果
        /// </summary>
        /// <param name="callback">结果回调</param>
        /// <returns></returns>
        public int SettlementInfoConfirm(DataCallback callback)
        {
            int requestID = AddCallback(callback);
            return _api.SettlementInfoConfirm(requestID);
        }

        /// <summary>
        /// 查询资金账户
        /// </summary>
        /// <param name="callback">查询回调</param>
        public void QueryAccount(DataCallback<AccountInfo> callback)
        {
            int requestID = AddCallback(callback);
            AddMethod(requestID, new Action(() =>
            {
                int ret = _api.QueryTradingAccount(requestID);
                RemoveMethod(requestID, ret);
            }));
        }

        /// <summary>
        /// 查询投资者
        /// </summary>
        /// <param name="callback">查询回调</param>
        public void QueryInvestor(DataCallback<InvestorInfo> callback)
        {
            int requestID = AddCallback(callback);
            AddMethod(requestID, new Action(() =>
            {
                int ret = _api.QueryInvestor(requestID);
                RemoveMethod(requestID, ret);
            }));
        }

        /// <summary>
        /// 查询持仓
        /// </summary>
        /// <param name="callback">查询回调</param>
        public void QueryPosition(DataListCallback<PositionInfo> callback)
        {
            int requestID = AddCallback(callback);
            AddMethod(requestID, new Action(() =>
            {
                int ret = _api.QueryInvestorPosition(requestID);
                RemoveMethod(requestID, ret);
            }));
        }

        /// <summary>
        /// 查询当日委托
        /// </summary>
        /// <param name="callback">查询回调</param>
        public void QueryOrder(DataListCallback<OrderInfo> callback)
        {
            int requestID = AddCallback(callback);
            AddMethod(requestID, new Action(() =>
            {
                int ret = _api.QueryOrder(requestID);
                RemoveMethod(requestID, ret);
            }));
        }

        /// <summary>
        /// 查询当日成交
        /// </summary>
        /// <param name="callback">查询回调</param>
        public void QueryTrade(DataListCallback<TradeInfo> callback)
        {
            int requestID = AddCallback(callback);
            AddMethod(requestID, new Action(() =>
            {
                int ret = _api.QueryTrade(requestID);
                RemoveMethod(requestID, ret);
            }));
        }

        /// <summary>
        /// 查询预埋单
        /// </summary>
        /// <param name="callback">查询回调</param>
        public void QueryParkedOrder(DataListCallback<ParkedOrderInfo> callback)
        {
            int requestID = AddCallback(callback);
            AddMethod(requestID, new Action(() =>
            {
                int ret = _api.QueryParkedOrder(requestID);
                RemoveMethod(requestID, ret);
            }));
        }

        /// <summary>
        /// 查询预埋撤单
        /// </summary>
        /// <param name="callback">查询回调</param>
        public void QueryParkedOrderAction(DataListCallback<ParkedCanelOrderInfo> callback)
        {
            int requestID = AddCallback(callback);
            AddMethod(requestID, new Action(() =>
            {
                int ret = _api.QueryParkedOrderAction(requestID);
                RemoveMethod(requestID, ret);
            }));
        }

        /// <summary>
        /// 查询合约列表
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <param name="instrumentID">指定合约</param>
        /// <returns></returns>
        public void QueryInstrument(DataListCallback<InstrumentInfo> callback, string instrumentID)
        {
            int requestID = AddCallback(callback);
            AddMethod(requestID, new Action(() =>
            {
                int ret = _api.QueryInstrument(requestID, instrumentID);
                RemoveMethod(requestID, ret);
            }));
        }

        /// <summary>
        /// 查询投资者持仓明细
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <param name="instrumentID">合约代码:不填-查所有</param>
        /// <returns></returns>
        public void QueryInvestorPositionDetail(DataListCallback<PositionDetailInfo> callback, string instrumentID)
        {
            int requestID = AddCallback(callback);
            AddMethod(requestID, new Action(() =>
            {
                int ret = _api.QueryInvestorPositionDetail(requestID, instrumentID);
                RemoveMethod(requestID, ret);
            }));
        }

        /// <summary>
        /// 请求查询客户通知
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <returns></returns>
        public void QueryNotice(DataListCallback<NoticeInfo> callback)
        {
            int requestID = AddCallback(callback);
            AddMethod(requestID, new Action(() =>
            {
                int ret = _api.QueryNotice(requestID);
                RemoveMethod(requestID, ret);
            }));
        }

        /// <summary>
        /// 查询结算结果
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <returns></returns>
        public void QuerySettlementInfo(DataCallback callback)
        {
            int requestID = AddCallback(callback);
            AddMethod(requestID, new Action(() =>
            {
                int ret = _api.QuerySettlementInfo(requestID);
                RemoveMethod(requestID, ret);
            }));
        }

        /// <summary>
        /// 查询结算信息确认
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <returns></returns>
        public void QuerySettlementInfoConfirm(DataCallback callback)
        {
            int requestID = AddCallback(callback);
            AddMethod(requestID, new Action(() =>
            {
                int ret = _api.QuerySettlementInfoConfirm(requestID);
                RemoveMethod(requestID, ret);
            }));
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
        /// 添加查询委托方法
        /// </summary>
        /// <param name="requestID"></param>
        /// <param name="action"></param>
        private void AddMethod(int requestID, Action action)
        {
            if ((DateTime.Now - _queryTime).TotalSeconds > 1)
            {
                action.Invoke();
                _queryTime = DateTime.Now;
            }
            else
            {
                lock (this)
                {
                    _queryMethodDict.Add(requestID, action);
                    if (!_timer.Enabled)
                    {
                        _timer.Start();
                    }
                }
            }
        }

        /// <summary>
        /// 执行委托方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteMethod(object sender, ElapsedEventArgs e)
        {
            if (_queryMethodDict.Count > 0)
            {
                var action = _queryMethodDict.Values.FirstOrDefault();
                action.Invoke();
                _queryTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 移除委托方法
        /// </summary>
        /// <param name="requestID"></param>
        /// <param name="ret"></param>
        private void RemoveMethod(int requestID, int ret)
        {
            lock (this)
            {
                if (ret == 0)
                {
                    _executeFailCount = 0;
                }
                else
                {
                    _executeFailCount++;
                }
                if (ret == 0 || _executeFailCount > 1)
                {
                    _queryMethodDict.Remove(requestID);
                    if (_queryMethodDict.Count == 0)
                    {
                        if (_timer != null && _timer.Enabled)
                        {
                            _timer.Stop();
                        }
                    }
                }
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
        /// 执行回调方法
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="requestID">请求编号</param>
        /// <param name="dataResult">返回结果</param>
        private void ExecuteCallback<T>(int requestID, DataResult<T> dataResult)
        {
            if (_dataCallbackDict.ContainsKey(requestID))
            {
                object callback;
                if (_dataCallbackDict.TryRemove(requestID, out callback))
                {
                    if (callback != null)
                    {
                        ((DataCallback<T>)callback)(dataResult);
                    }
                }
            }
        }

        /// <summary>
        /// 执行集合回调方法
        /// </summary>
        /// <typeparam name="T">结果列表对象类型</typeparam>
        /// <param name="requestID">请求编号</param>
        /// <param name="dataResult">返回列表结果</param>
        private void ExecuteCallback<T>(int requestID, DataListResult<T> dataResult)
        {
            if (_dataCallbackDict.ContainsKey(requestID))
            {
                object callback;
                if (_dataCallbackDict.TryRemove(requestID, out callback))
                {
                    if (callback != null)
                    {
                        ((DataListCallback<T>)callback)(dataResult);
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
        /// 设置错误信息
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="result">返回结果</param>
        /// <param name="pRspInfo">错误信息</param>
        private void SetError<T>(DataResult<T> result, CThostFtdcRspInfoField pRspInfo)
        {
            result.ErrorCode = pRspInfo.ErrorID.ToString();
            result.Error = pRspInfo.ErrorMsg;
            result.IsSuccess = false;
        }

        /// <summary>
        /// 设置错误信息
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="result">返回结果</param>
        /// <param name="pRspInfo">错误信息</param>
        private void SetError<T>(DataListResult<T> result, CThostFtdcRspInfoField pRspInfo)
        {
            result.ErrorCode = pRspInfo.ErrorID.ToString();
            result.Error = pRspInfo.ErrorMsg;
            result.IsSuccess = false;
        }

        /// <summary>
        /// 错误回调
        /// </summary>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
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
        private void OnConnected()
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
        /// <param name="reason">原因</param>
        private void OnDisconnected(int reason)
        {
            ExecuteCallback(-2, new DataResult()
            {
                IsSuccess = true
            });
            _isConnected = false;
        }

        /// <summary>
        /// 客户端认证回调
        /// </summary>
        /// <param name="pRspAuthenticate">客户端认证响应</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspAuthenticate(ref CThostFtdcRspAuthenticateField pRspAuthenticate,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
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
            ExecuteCallback(-5, result);
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
                AccountInfo account = new AccountInfo()
                {
                    TradingDay = pRspUserLogin.TradingDay,
                    LoginTime = pRspUserLogin.LoginTime,
                    InvestorID = pRspUserLogin.UserID,
                };
                _api.FrontID = pRspUserLogin.FrontID;
                _api.SessionID = pRspUserLogin.SessionID;
                _api.MaxOrderRef = pRspUserLogin.MaxOrderRef;
                result.Result = account;
                result.IsSuccess = true;
            }
            ExecuteCallback(nRequestID, result);
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
            ExecuteCallback(nRequestID, result);
        }

        /// <summary>
        /// 更新用户口令
        /// </summary>
        /// <param name="pUserPasswordUpdate">用户口令变更</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspUserPasswordUpdate(ref CThostFtdcUserPasswordUpdateField pUserPasswordUpdate,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
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
            ExecuteCallback(nRequestID, result);
        }

        /// <summary>
        /// 更新资金账户口令
        /// </summary>
        /// <param name="pTradingAccountPasswordUpdate">资金账户口令变更</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnTradingPasswordUpdate(ref CThostFtdcTradingAccountPasswordUpdateField pTradingAccountPasswordUpdate,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
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
            ExecuteCallback(nRequestID, result);
        }

        /// <summary>
        /// 查询认证方式
        /// </summary>
        /// <param name="pRspUserAuthMethod"></param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnUserAuthMethod(ref CThostFtdcRspUserAuthMethodField pRspUserAuthMethod,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            DataResult result = new DataResult();
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                result.IsSuccess = true;
                result.Result = pRspUserAuthMethod.UsableAuthMethod;
            }
            ExecuteCallback(nRequestID, result);
        }

        /// <summary>
        /// 生成图片验证码
        /// </summary>
        /// <param name="pRspGenUserCaptcha">图片验证码信息</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnGenUserCaptcha(ref CThostFtdcRspGenUserCaptchaField pRspGenUserCaptcha,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            DataResult result = new DataResult();
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                result.IsSuccess = true;
                if (pRspGenUserCaptcha.CaptchaInfoLen > 0)
                {
                    result.Result = pRspGenUserCaptcha.CaptchaInfo.Take(pRspGenUserCaptcha.CaptchaInfoLen).ToArray();
                }
            }
            ExecuteCallback(nRequestID, result);
        }

        /// <summary>
        /// 生成短信验证码
        /// </summary>
        /// <param name="pRspGenUserText">短信验证码</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnGenUserText(ref CThostFtdcRspGenUserTextField pRspGenUserText,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            DataResult result = new DataResult();
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                result.IsSuccess = true;
                result.Result = pRspGenUserText.UserTextSeq;
            }
            ExecuteCallback(nRequestID, result);
        }

        /// <summary>
        /// 报单通知
        /// </summary>
        private void OnRtnOrder(ref CThostFtdcOrderField pOrder)
        {
            DataResult<OrderInfo> result = new DataResult<OrderInfo>();
            result.IsSuccess = true;
            result.Result = ConvertToOrder(pOrder);
            ExecuteCallback<OrderInfo>(pOrder.RequestID, result);
            if (OnReturnOrder != null)
            {
                OnReturnOrder.Invoke(result.Result);
            }
        }

        /// <summary>
        /// 成交回报
        /// </summary>
        /// <param name="pTrade">成交信息</param>
        private void OnRtnTrade(ref CThostFtdcTradeField pTrade)
        {
            if (OnReturnTrade != null)
            {
                var tradeInfo = ConvertToTrade(pTrade);
                OnReturnTrade.Invoke(tradeInfo);
            }
        }

        /// <summary>
        /// 报单错误回调
        /// </summary>
        /// <param name="pInputOrder">报单信息</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspOrderInsert(ref CThostFtdcInputOrderField pInputOrder, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast)
        {
            DataResult<OrderInfo> result = new DataResult<OrderInfo>();
            SetError(result, pRspInfo);
            ExecuteCallback(nRequestID, result);
        }

        /// <summary>
        /// 撤单回调
        /// </summary>
        /// <param name="pInputOrderAction">撤单信息</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspOrderAction(ref CThostFtdcInputOrderActionField pInputOrderAction,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
        {
            DataResult result = new DataResult();
            SetError(result, pRspInfo);
            ExecuteCallback(nRequestID, result);
        }

        /// <summary>
        /// 查询报单回调
        /// </summary>
        /// <param name="pOrder">委托信息</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspQueryOrder(ref CThostFtdcOrderField pOrder, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast)
        {
            DataListResult<OrderInfo> result;
            if (_dataDict.ContainsKey(nRequestID))
            {
                result = (DataListResult<OrderInfo>)_dataDict[nRequestID];
            }
            else
            {
                result = new DataListResult<OrderInfo>();
                _dataDict.TryAdd(nRequestID, result);
            }
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                OrderInfo order = ConvertToOrder(pOrder);
                result.Result.Add(order);
                if (bIsLast == 1)
                {
                    result.IsSuccess = true;
                    ExecuteCallback(nRequestID, result);
                }
            }
        }

        /// <summary>
        /// 查询成交回调
        /// </summary>
        /// <param name="pTrade">成交信息</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspQueryTrade(ref CThostFtdcTradeField pTrade, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast)
        {
            DataListResult<TradeInfo> result;
            if (_dataDict.ContainsKey(nRequestID))
            {
                result = (DataListResult<TradeInfo>)_dataDict[nRequestID];
            }
            else
            {
                result = new DataListResult<TradeInfo>();
                _dataDict.TryAdd(nRequestID, result);
            }
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                TradeInfo trade = ConvertToTrade(pTrade);
                result.Result.Add(trade);
                if (bIsLast == 1)
                {
                    result.IsSuccess = true;
                    ExecuteCallback(nRequestID, result);
                }
            }
        }

        /// <summary>
        /// 查询资金账户
        /// </summary>
        /// <param name="pTradingAccount">资金账户信息</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspQueryTradingAccount(ref CThostFtdcTradingAccountField pTradingAccount,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
        {
            DataResult<AccountInfo> result = new DataResult<AccountInfo>();
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                result.Result = ConvertToAccount(pTradingAccount);
                result.IsSuccess = true;
                ExecuteCallback(nRequestID, result);
            }
        }

        /// <summary>
        /// 查询持仓
        /// </summary>
        /// <param name="pInvestorPosition">持仓信息</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspQueryInvestorPosition(ref CThostFtdcInvestorPositionField pInvestorPosition,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
        {
            DataListResult<PositionInfo> result;
            if (_dataDict.ContainsKey(nRequestID))
            {
                result = (DataListResult<PositionInfo>)_dataDict[nRequestID];
            }
            else
            {
                result = new DataListResult<PositionInfo>();
                _dataDict.TryAdd(nRequestID, result);
            }
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                PositionInfo position = ConvertToPosition(pInvestorPosition);
                result.Result.Add(position);
                if (bIsLast == 1)
                {
                    result.IsSuccess = true;
                    ExecuteCallback(nRequestID, result);
                }
            }
        }

        /// <summary>
        /// 预埋报单回调
        /// </summary>
        /// <param name="pParkedOrder">预埋单委托信息</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspParkedOrderInsert(ref CThostFtdcParkedOrderField pParkedOrder,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
        {
            DataResult<ParkedOrderInfo> result = new DataResult<ParkedOrderInfo>();
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                result.IsSuccess = true;
                result.Result = ConvertToParkedOrder(pParkedOrder);
            }
            ExecuteCallback(nRequestID, result);
        }

        /// <summary>
        /// 预埋撤单回调
        /// </summary>
        /// <param name="pParkedOrderAction">预埋单撤单信息</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspParkedOrderAction(ref CThostFtdcParkedOrderActionField pParkedOrderAction,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
        {
            DataResult<ParkedCanelOrderInfo> result = new DataResult<ParkedCanelOrderInfo>();
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                result.IsSuccess = true;
                result.Result = ConvertToParkedCancelOrder(pParkedOrderAction);
            }
            ExecuteCallback(nRequestID, result);
        }

        /// <summary>
        /// 结算确认回调
        /// </summary>
        /// <param name="pSettlementInfoConfirm"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
        private void OnRspSettlementInfoConfirm(ref CThostFtdcSettlementInfoConfirmField pSettlementInfoConfirm,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
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
            ExecuteCallback(nRequestID, result);
        }

        /// <summary>
        /// 查询预埋单回调
        /// </summary>
        /// <param name="pParkedOrder">预埋单委托信息</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspQueryParkedOrder(ref CThostFtdcParkedOrderField pParkedOrder,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
        {
            DataListResult<ParkedOrderInfo> result;
            if (_dataDict.ContainsKey(nRequestID))
            {
                result = (DataListResult<ParkedOrderInfo>)_dataDict[nRequestID];
            }
            else
            {
                result = new DataListResult<ParkedOrderInfo>();
                _dataDict.TryAdd(nRequestID, result);
            }
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                ParkedOrderInfo position = ConvertToParkedOrder(pParkedOrder);
                result.Result.Add(position);
                if (bIsLast == 1)
                {
                    result.IsSuccess = true;
                    ExecuteCallback(nRequestID, result);
                }
            }
        }

        /// <summary>
        /// 查询预埋单撤单回调
        /// </summary>
        /// <param name="pParkedOrderAction">预埋单撤单委托信息</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspQueryParkedOrderAction(ref CThostFtdcParkedOrderActionField pParkedOrderAction,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
        {
            DataListResult<ParkedCanelOrderInfo> result;
            if (_dataDict.ContainsKey(nRequestID))
            {
                result = (DataListResult<ParkedCanelOrderInfo>)_dataDict[nRequestID];
            }
            else
            {
                result = new DataListResult<ParkedCanelOrderInfo>();
                _dataDict.TryAdd(nRequestID, result);
            }
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                ParkedCanelOrderInfo position = ConvertToParkedCancelOrder(pParkedOrderAction);
                result.Result.Add(position);
                if (bIsLast == 1)
                {
                    result.IsSuccess = true;
                    ExecuteCallback(nRequestID, result);
                }
            }
        }

        /// <summary>
        /// 查询合约列表回调
        /// </summary>
        /// <param name="pInstrument">合约信息</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspQueryInstrument(ref CThostFtdcInstrumentField pInstrument,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
        {
            DataListResult<InstrumentInfo> result;
            if (_dataDict.ContainsKey(nRequestID))
            {
                result = (DataListResult<InstrumentInfo>)_dataDict[nRequestID];
            }
            else
            {
                result = new DataListResult<InstrumentInfo>();
                _dataDict.TryAdd(nRequestID, result);
            }
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                InstrumentInfo instrument = ConvertToInstrumentInfo(pInstrument);
                result.Result.Add(instrument);
                if (bIsLast == 1)
                {
                    result.IsSuccess = true;
                    ExecuteCallback(nRequestID, result);
                }
            }
        }

        /// <summary>
        /// 查询投资者
        /// </summary>
        /// <param name="pInvestor">投资者信息</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspQueryInvestor(ref CThostFtdcInvestorField pInvestor, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast)
        {
            DataResult<InvestorInfo> result = new DataResult<InvestorInfo>();
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                result.Result = ConvertToInvestor(pInvestor);
                result.IsSuccess = true;
                ExecuteCallback(nRequestID, result);
            }
        }

        /// <summary>
        /// 查询投资者持仓明细
        /// </summary>
        /// <param name="pInvestorPositionDetail">持仓明细</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspQueryInvestorPositionDetail(
            ref CThostFtdcInvestorPositionDetailField pInvestorPositionDetail, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast)
        {
            DataListResult<PositionDetailInfo> result;
            if (_dataDict.ContainsKey(nRequestID))
            {
                result = (DataListResult<PositionDetailInfo>)_dataDict[nRequestID];
            }
            else
            {
                result = new DataListResult<PositionDetailInfo>();
                _dataDict.TryAdd(nRequestID, result);
            }
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                PositionDetailInfo instrument = ConvertToPositionDetailInfo(pInvestorPositionDetail);
                result.Result.Add(instrument);
                if (bIsLast == 1)
                {
                    result.IsSuccess = true;
                    ExecuteCallback(nRequestID, result);
                }
            }
        }

        /// <summary>
        /// 查询客户通知
        /// </summary>
        /// <param name="pNotice">客户通知</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspQueryNotice(ref CThostFtdcNoticeField pNotice, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast)
        {
            DataListResult<NoticeInfo> result;
            if (_dataDict.ContainsKey(nRequestID))
            {
                result = (DataListResult<NoticeInfo>)_dataDict[nRequestID];
            }
            else
            {
                result = new DataListResult<NoticeInfo>();
                _dataDict.TryAdd(nRequestID, result);
            }
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                NoticeInfo notice = ConvertToNoticeInfo(pNotice);
                result.Result.Add(notice);
                if (bIsLast == 1)
                {
                    result.IsSuccess = true;
                    ExecuteCallback(nRequestID, result);
                }
            }
        }

        /// <summary>
        /// 查询结算结果回调
        /// </summary>
        /// <param name="pSettlementInfo">结算结果</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspQrySettlementInfo(ref CThostFtdcSettlementInfoField pSettlementInfo,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
        {
            DataResult result;
            if (_dataDict.ContainsKey(nRequestID))
            {
                result = (DataResult)_dataDict[nRequestID];
            }
            else
            {
                result = new DataResult();
                _dataDict.TryAdd(nRequestID, result);
            }
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                result.Result += pSettlementInfo.Content;
                if (bIsLast == 1)
                {
                    result.IsSuccess = true;
                    ExecuteCallback(nRequestID, result);
                }
            }
        }

        /// <summary>
        /// 查询结算信息确认回调
        /// </summary>
        /// <param name="pSettlementInfoConfirm">结算信息确认</param>
        /// <param name="pRspInfo">错误信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        private void OnRspQrySettlementInfoConfirm(ref CThostFtdcSettlementInfoConfirmField pSettlementInfoConfirm,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast)
        {
            DataResult result = new DataResult();
            if (pRspInfo.ErrorID > 0)
            {
                SetError(result, pRspInfo);
            }
            else
            {
                result.IsSuccess = !string.IsNullOrEmpty(pSettlementInfoConfirm.ConfirmDate);
            }
            ExecuteCallback(nRequestID, result);
        }

        #endregion

        #region 通用类型转特定类型

        /// <summary>
        /// 买卖方向类型转换
        /// </summary>
        /// <param name="direction">买卖方向</param>
        /// <returns></returns>
        private TThostFtdcDirectionType ConvertToDirectionType(DirectionType direction)
        {
            return (TThostFtdcDirectionType)Enum.Parse(typeof(TThostFtdcDirectionType),
                direction.ToString());
        }

        /// <summary>
        /// 报单价格类型转换
        /// </summary>
        /// <param name="orderPriceType">报单价格类型</param>
        /// <returns></returns>
        private TThostFtdcOrderPriceTypeType ConvertToOrderPriceType(OrderPriceType orderPriceType)
        {
            return (TThostFtdcOrderPriceTypeType)Enum.Parse(typeof(TThostFtdcOrderPriceTypeType),
                orderPriceType.ToString());
        }

        /// <summary>
        /// 组合开平仓标志类型转换
        /// </summary>
        /// <param name="openCloseFlag">开平仓标志</param>
        /// <returns></returns>
        private TThostFtdcOffsetFlagType ConvertToCombOffsetFlag(OpenCloseFlag openCloseFlag)
        {
            return (TThostFtdcOffsetFlagType)Enum.Parse(typeof(TThostFtdcOffsetFlagType), openCloseFlag.ToString());
        }

        /// <summary>
        /// 投机套保标志类型转换
        /// </summary>
        /// <param name="hedgeFlag">投机套保标志</param>
        /// <returns></returns>
        private TThostFtdcHedgeFlagType ConvertToCombHedgeFlag(HedgeFlag hedgeFlag)
        {
            return (TThostFtdcHedgeFlagType)Enum.Parse(typeof(TThostFtdcHedgeFlagType), hedgeFlag.ToString());
        }

        /// <summary>
        /// 成交量类型转换
        /// </summary>
        /// <param name="volumeCondition">成交量类型</param>
        /// <returns></returns>
        private TThostFtdcVolumeConditionType ConvertToVolumeCondition(VolumeConditionType volumeCondition)
        {
            return (TThostFtdcVolumeConditionType)Enum.Parse(typeof(TThostFtdcVolumeConditionType),
                volumeCondition.ToString());
        }

        /// <summary>
        /// 操作标志类型转换
        /// </summary>
        /// <param name="actionFlag">操作标志</param>
        /// <returns></returns>
        private TThostFtdcActionFlagType ConvertToActionFlag(ActionFlag actionFlag)
        {
            return (TThostFtdcActionFlagType)Enum.Parse(typeof(TThostFtdcActionFlagType), actionFlag.ToString());
        }

        /// <summary>
        /// 触发条件类型转换
        /// </summary>
        /// <param name="contingentCondition">触发条件类型</param>
        /// <returns></returns>
        private TThostFtdcContingentConditionType ConvertToContingentCondition(
            ContingentConditionType contingentCondition)
        {
            return (TThostFtdcContingentConditionType)Enum.Parse(typeof(TThostFtdcContingentConditionType),
                contingentCondition.ToString());
        }

        /// <summary>
        /// 预埋单状态转换
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private TThostFtdcParkedOrderStatusType ConvertToParkedOrderStatus(ParkedOrderStatusType status)
        {
            return (TThostFtdcParkedOrderStatusType)Enum.Parse(typeof(TThostFtdcParkedOrderStatusType),
                status.ToString());
        }

        /// <summary>
        /// 有效期类型转换
        /// </summary>
        /// <param name="timeCondition"></param>
        /// <returns></returns>
        private TThostFtdcTimeConditionType ConvertToTimeCondition(TimeConditionType timeCondition)
        {
            return (TThostFtdcTimeConditionType)Enum.Parse(typeof(TThostFtdcTimeConditionType),
                timeCondition.ToString());
        }

        /// <summary>
        /// 强平原因类型转换
        /// </summary>
        /// <param name="forceCloseReason"></param>
        /// <returns></returns>
        private TThostFtdcForceCloseReasonType ConvertToForceCloseReason(ForceCloseReasonType forceCloseReason)
        {
            return (TThostFtdcForceCloseReasonType)Enum.Parse(typeof(TThostFtdcForceCloseReasonType),
                forceCloseReason.ToString());
        }

        /// <summary>
        /// 将登录类型转换为结构体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private CThostFtdcReqUserLoginWithCaptchaField ConvertToLoginField(LoginRequest request)
        {
            CThostFtdcReqUserLoginWithCaptchaField result = new CThostFtdcReqUserLoginWithCaptchaField()
            {
                TradingDay = request.TradingDay,
                BrokerID = request.BrokerID,
                UserID = request.UserID,
                Password = request.Password,
                UserProductInfo = request.UserProductInfo,
                InterfaceProductInfo = request.InterfaceProductInfo,
                ProtocolInfo = request.ProtocolInfo,
                MacAddress = request.MacAddress,
                ClientIPAddress = request.ClientIPAddress,
                ClientIPPort = request.ClientIPPort,
                LoginRemark = request.LoginRemark,
                Captcha = request.Captcha
            };

            return result;
        }

        /// <summary>
        /// 报单转换
        /// </summary>
        /// <param name="parameter">报单参数</param>
        /// <returns></returns>
        private CThostFtdcInputOrderField ConvertToInputOrderField(OrderParameter parameter)
        {
            CThostFtdcInputOrderField result = new CThostFtdcInputOrderField()
            {
                BrokerID = _api.BrokerID,
                InvestorID = _api.InvestorID,
                UserID = _api.InvestorID,
                InstrumentID = parameter.InstrumentID,
                ExchangeID = parameter.ExchangeID,
                OrderRef = parameter.OrderRef,
                VolumeTotalOriginal = (int)parameter.Quantity,
                TimeCondition = ConvertToTimeCondition(parameter.TimeCondition),
                LimitPrice = (double)parameter.Price,
                StopPrice = (double)parameter.StopPrice,
                Direction = ConvertToDirectionType(parameter.Direction),
                OrderPriceType = ConvertToOrderPriceType(parameter.PriceType),
                CombOffsetFlag = ConvertToCombOffsetFlag(parameter.OpenCloseFlag),
                CombHedgeFlag = ConvertToCombHedgeFlag(parameter.HedgeFlag),
                VolumeCondition = ConvertToVolumeCondition(parameter.VolumeCondition),
                MinVolume = (int)parameter.MinVolume,
                ContingentCondition = ConvertToContingentCondition(parameter.ContingentCondition),
                ForceCloseReason = ConvertToForceCloseReason(parameter.ForceCloseReason),
                GTDDate = parameter.GTDDate,
                IsAutoSuspend = parameter.IsAutoSuspend,
                UserForceClose = parameter.UserForceClose
            };

            return result;
        }

        /// <summary>
        /// 撤单转换
        /// </summary>
        /// <param name="parameter">撤单参数</param>
        /// <returns></returns>
        private CThostFtdcInputOrderActionField ConvertToInputOrderActionField(CancelOrderParameter parameter)
        {
            CThostFtdcInputOrderActionField result = new CThostFtdcInputOrderActionField()
            {
                BrokerID = _api.BrokerID,
                UserID = _api.InvestorID,
                InvestorID = _api.InvestorID,
                InstrumentID = parameter.InstrumentID,
                ExchangeID = parameter.ExchangeID,
                OrderActionRef = parameter.OrderActionRef,
                OrderRef = parameter.OrderRef,
                OrderSysID = parameter.OrderSysID,
                LimitPrice = (double)parameter.Price,
                VolumeChange = (int)parameter.Quantity,
                ActionFlag = ConvertToActionFlag(parameter.ActionFlag),
                FrontID = _api.FrontID,
                SessionID = _api.SessionID,
            };

            return result;
        }

        /// <summary>
        /// 预埋单转换
        /// </summary>
        /// <param name="parameter">报单参数</param>
        /// <returns></returns>
        private CThostFtdcParkedOrderField ConvertToParkedOrderField(OrderParameter parameter)
        {
            CThostFtdcParkedOrderField result = new CThostFtdcParkedOrderField()
            {
                BrokerID = _api.BrokerID,
                InvestorID = _api.InvestorID,
                UserID = _api.InvestorID,
                InstrumentID = parameter.InstrumentID,
                ExchangeID = parameter.ExchangeID,
                OrderRef = parameter.OrderRef,
                VolumeTotalOriginal = (int)parameter.Quantity,
                LimitPrice = (double)parameter.Price,
                StopPrice = (double)parameter.StopPrice,
                Direction = ConvertToDirectionType(parameter.Direction),
                OrderPriceType = ConvertToOrderPriceType(parameter.PriceType),
                CombOffsetFlag = ConvertToCombOffsetFlag(parameter.OpenCloseFlag),
                CombHedgeFlag = ConvertToCombHedgeFlag(parameter.HedgeFlag),
                VolumeCondition = ConvertToVolumeCondition(parameter.VolumeCondition),
                MinVolume = (int)parameter.MinVolume,
                ContingentCondition = ConvertToContingentCondition(parameter.ContingentCondition),
                GTDDate = parameter.GTDDate,
                ParkedOrderID = parameter.ParkedOrderID,
                IsAutoSuspend = parameter.IsAutoSuspend,
                UserForceClose = parameter.UserForceClose,
            };

            return result;
        }


        /// <summary>
        /// 预埋撤单转换
        /// </summary>
        /// <param name="parameter">预埋单撤单参数</param>
        /// <returns></returns>
        private CThostFtdcParkedOrderActionField ConvertToParkedOrderActionField(CancelOrderParameter parameter)
        {
            CThostFtdcParkedOrderActionField result = new CThostFtdcParkedOrderActionField()
            {
                BrokerID = _api.BrokerID,
                UserID = _api.InvestorID,
                InstrumentID = parameter.InstrumentID,
                ExchangeID = parameter.ExchangeID,
                OrderRef = parameter.OrderRef,
                OrderActionRef = parameter.OrderActionRef,
                OrderSysID = parameter.OrderSysID,
                ParkedOrderActionID = parameter.ParkedOrderActionID,
                ActionFlag = ConvertToActionFlag(parameter.ActionFlag),
                Status = ConvertToParkedOrderStatus(parameter.Status),
                FrontID = _api.FrontID,
                SessionID = _api.SessionID,
            };

            return result;
        }

        #endregion

        #region 特定类型转通用类型

        /// <summary>
        /// 买卖方向类型转换
        /// </summary>
        /// <param name="direction">买卖方向</param>
        /// <returns></returns>
        private DirectionType ConvertToDirectionType(TThostFtdcDirectionType direction)
        {
            return (DirectionType)Enum.Parse(typeof(DirectionType), direction.ToString());
        }

        /// <summary>
        /// 开平仓标志转换
        /// </summary>
        /// <param name="offsetFlag">开平仓标志</param>
        /// <returns></returns>
        private OpenCloseFlag ConvertToOpenCloseFlag(TThostFtdcOffsetFlagType offsetFlag)
        {
            return (OpenCloseFlag)Enum.Parse(typeof(OpenCloseFlag), offsetFlag.ToString());
        }

        /// <summary>
        /// 投机套保标志转换
        /// </summary>
        /// <param name="hedgeFlag">投机套保标志</param>
        /// <returns></returns>
        private HedgeFlag ConvertToHedgeFlag(TThostFtdcHedgeFlagType hedgeFlag)
        {
            return (HedgeFlag)Enum.Parse(typeof(HedgeFlag), hedgeFlag.ToString());
        }

        /// <summary>
        /// 成交类型转换
        /// </summary>
        /// <param name="tradeType">成交类型</param>
        /// <returns></returns>
        private TradeType ConvertToTradeType(TThostFtdcTradeTypeType tradeType)
        {
            return (TradeType)Enum.Parse(typeof(TradeType), tradeType.ToString());
        }

        /// <summary>
        /// 成交价来源类型转换
        /// </summary>
        /// <param name="priceSource">成交价来源类型</param>
        /// <returns></returns>
        private PriceSourceType ConvertToPriceSource(TThostFtdcPriceSourceType priceSource)
        {
            return (PriceSourceType)Enum.Parse(typeof(PriceSourceType), priceSource.ToString());
        }

        /// <summary>
        /// 报单状态类型转换
        /// </summary>
        /// <param name="orderStatus">报单状态类型</param>
        /// <returns></returns>
        private OrderStatusType ConvertToOrderStatus(TThostFtdcOrderStatusType orderStatus)
        {
            return (OrderStatusType)Enum.Parse(typeof(OrderStatusType), orderStatus.ToString());
        }

        /// <summary>
        /// 报单价格类型转换
        /// </summary>
        /// <param name="priceType">报单价格类型</param>
        /// <returns></returns>
        private OrderPriceType ConvertToOrderPriceType(TThostFtdcOrderPriceTypeType priceType)
        {
            return (OrderPriceType)Enum.Parse(typeof(OrderPriceType), priceType.ToString());
        }

        /// <summary>
        /// 预埋单状态转换
        /// </summary>
        /// <param name="status">预埋单状态</param>
        /// <returns></returns>
        private ParkedOrderStatusType ConvertToParkedOrderStatus(TThostFtdcParkedOrderStatusType status)
        {
            return (ParkedOrderStatusType)Enum.Parse(typeof(ParkedOrderStatusType), status.ToString());
        }

        /// <summary>
        /// 持仓多空头方向类型转换
        /// </summary>
        /// <param name="posiDirection">持仓多空头方向类型</param>
        /// <returns></returns>
        private PositionDirectionType ConvertToPositionDirection(TThostFtdcPosiDirectionType posiDirection)
        {
            return (PositionDirectionType)Enum.Parse(typeof(PositionDirectionType), posiDirection.ToString());
        }

        /// <summary>
        /// 持仓日期类型转换
        /// </summary>
        /// <param name="positionDate">持仓日期类型</param>
        /// <returns></returns>
        private PositionDateType ConvertToPositionDate(TThostFtdcPositionDateType positionDate)
        {
            return (PositionDateType)Enum.Parse(typeof(PositionDateType), positionDate.ToString());
        }

        /// <summary>
        /// 合约产品类型转换
        /// </summary>
        /// <param name="productClass">合约产品类型</param>
        /// <returns></returns>
        private InstrumentProductClassType ConvertToInstrumentProductClass(TThostFtdcProductClassType productClass)
        {
            return (InstrumentProductClassType)Enum.Parse(typeof(InstrumentProductClassType), productClass.ToString());
        }

        /// <summary>
        /// 合约生命周期状态类型转换
        /// </summary>
        /// <param name="lifePhase"></param>
        /// <returns></returns>
        private InstrumentLifePhaseType ConvertToInstrumentLifePhase(TThostFtdcInstLifePhaseType lifePhase)
        {
            return (InstrumentLifePhaseType)Enum.Parse(typeof(InstrumentLifePhaseType), lifePhase.ToString());
        }

        /// <summary>
        /// 证件类型转换
        /// </summary>
        /// <param name="idCardType">持仓多空头方向类型</param>
        /// <returns></returns>
        private IdCardType ConvertToIdCardTypeType(TThostFtdcIdCardTypeType idCardType)
        {
            return (IdCardType)Enum.Parse(typeof(IdCardType), idCardType.ToString());
        }

        #endregion

        #region 结构体转通用实体

        /// <summary>
        /// 报单类型转换
        /// </summary>
        /// <param name="pOrder">报单结构体</param>
        /// <returns></returns>
        private OrderInfo ConvertToOrder(CThostFtdcOrderField pOrder)
        {
            OrderInfo result = new OrderInfo()
            {
                InvestorID = pOrder.InvestorID,
                InstrumentID = pOrder.InstrumentID,
                ExchangeID = pOrder.ExchangeID,
                OrderPrice = (decimal)pOrder.LimitPrice,
                OrderQuantity = pOrder.VolumeTotalOriginal,
                OrderRef = pOrder.OrderRef,
                OrderSysID = pOrder.OrderSysID,
                OrderLocalID = pOrder.OrderLocalID,
                OrderDate = pOrder.InsertDate,
                OrderTime = pOrder.InsertTime,
                SequenceNo = pOrder.SequenceNo,
                StatusMessage = pOrder.StatusMsg,
                OrderStatus = ConvertToOrderStatus(pOrder.OrderStatus),
                Direction = ConvertToDirectionType(pOrder.Direction),
            };
            return result;
        }

        /// <summary>
        /// 成交类型转换
        /// </summary>
        /// <param name="pTrade">成交结构体</param>
        /// <returns></returns>
        private TradeInfo ConvertToTrade(CThostFtdcTradeField pTrade)
        {
            TradeInfo result = new TradeInfo()
            {
                InvestorID = pTrade.InvestorID,
                InstrumentID = pTrade.InstrumentID,
                ExchangeID = pTrade.ExchangeID,
                OrderRef = pTrade.OrderRef,
                OrderSysID = pTrade.OrderSysID,
                OrderLocalID = pTrade.OrderLocalID,
                TradeID = pTrade.TradeID,
                TradePrice = (decimal)pTrade.Price,
                TradeQuantity = pTrade.Volume,
                TradeDate = pTrade.TradeDate,
                TradeTime = pTrade.TradeTime,
                SequenceNo = pTrade.SequenceNo,
                Direction = ConvertToDirectionType(pTrade.Direction),
                OpenCloseFlag = ConvertToOpenCloseFlag(pTrade.OffsetFlag),
                HedgeFlag = ConvertToHedgeFlag(pTrade.HedgeFlag),
                TradeType = ConvertToTradeType(pTrade.TradeType),
                PriceSource = ConvertToPriceSource(pTrade.PriceSource),
            };

            return result;
        }

        /// <summary>
        /// 资金账号转换
        /// </summary>
        /// <param name="pTradingAccount"></param>
        /// <returns></returns>
        private AccountInfo ConvertToAccount(CThostFtdcTradingAccountField pTradingAccount)
        {
            AccountInfo result = new AccountInfo()
            {
                InvestorID = pTradingAccount.AccountID,
                TradingDay = pTradingAccount.TradingDay,
                Deposit = (decimal)pTradingAccount.Deposit,
                Withdraw = (decimal)pTradingAccount.Withdraw,
                FrozenMargin = (decimal)pTradingAccount.FrozenMargin,
                FrozenCash = (decimal)pTradingAccount.FrozenCash,
                TotalMargin = (decimal)pTradingAccount.CurrMargin,
                CashIn = (decimal)pTradingAccount.CashIn,
                Commission = (decimal)pTradingAccount.Commission,
                CloseProfit = (decimal)pTradingAccount.CloseProfit,
                PositionProfit = (decimal)pTradingAccount.PositionProfit,
                Balance = (decimal)pTradingAccount.Balance,
                Available = (decimal)pTradingAccount.Available,
                WithdrawQuota = (decimal)pTradingAccount.WithdrawQuota,
            };

            return result;
        }

        /// <summary>
        /// 投资者账号转换
        /// </summary>
        /// <param name="pInvestor"></param>
        /// <returns></returns>
        private InvestorInfo ConvertToInvestor(CThostFtdcInvestorField pInvestor)
        {
            InvestorInfo result = new InvestorInfo()
            {
                InvestorID = pInvestor.InvestorID,
                BrokerID = pInvestor.BrokerID,
                InvestorGroupID = pInvestor.InvestorGroupID,
                InvestorName = pInvestor.InvestorName,
                IdentifiedCardType = ConvertToIdCardTypeType(pInvestor.IdentifiedCardType),
                IdentifiedCardNo = pInvestor.IdentifiedCardNo,
                IsActive = pInvestor.IsActive,
                Telephone = pInvestor.Telephone,
                Address = pInvestor.Address,
                OpenDate = pInvestor.OpenDate,
                Mobile = pInvestor.Mobile,
                CommModelID = pInvestor.CommModelID,
                MarginModelID = pInvestor.MarginModelID,
            };

            return result;
        }

        /// <summary>
        /// 持仓信息转换
        /// </summary>
        /// <param name="pInvestorPosition">持仓结构体</param>
        /// <returns></returns>
        private PositionInfo ConvertToPosition(CThostFtdcInvestorPositionField pInvestorPosition)
        {
            PositionInfo result = new PositionInfo()
            {
                BrokerID = pInvestorPosition.BrokerID,
                InvestorID = pInvestorPosition.InvestorID,
                InstrumentID = pInvestorPosition.InstrumentID,
                PositionDirection = ConvertToPositionDirection(pInvestorPosition.PosiDirection),
                HedgeFlag = ConvertToHedgeFlag(pInvestorPosition.HedgeFlag),
                PositionDate = ConvertToPositionDate(pInvestorPosition.PositionDate),
                PrePosition = pInvestorPosition.YdPosition,
                Position = pInvestorPosition.Position,
                LongFrozen = pInvestorPosition.LongFrozen,
                ShortFrozen = pInvestorPosition.ShortFrozen,
                LongFrozenAmount = (decimal)pInvestorPosition.LongFrozenAmount,
                ShortFrozenAmount = (decimal)pInvestorPosition.ShortFrozenAmount,
                OpenVolume = pInvestorPosition.OpenVolume,
                CloseAmount = pInvestorPosition.CloseVolume,
                OpenAmount = (decimal)pInvestorPosition.OpenAmount,
                CloseVolume = (decimal)pInvestorPosition.CloseAmount,
                PositionCost = (decimal)pInvestorPosition.PositionCost,
                Commission = (decimal)pInvestorPosition.Commission,
                PositionProfit = (decimal)pInvestorPosition.PositionProfit,
            };

            return result;
        }

        /// <summary>
        /// 预埋单类型转换
        /// </summary>
        /// <param name="pParkedOrder">预埋单结构体</param>
        /// <returns></returns>
        private ParkedOrderInfo ConvertToParkedOrder(CThostFtdcParkedOrderField pParkedOrder)
        {
            ParkedOrderInfo result = new ParkedOrderInfo()
            {
                InvestorID = pParkedOrder.InvestorID,
                InstrumentID = pParkedOrder.InstrumentID,
                ExchangeID = pParkedOrder.ExchangeID,
                OrderRef = pParkedOrder.OrderRef,
                ParkedOrderID = pParkedOrder.ParkedOrderID,
                Price = (decimal)pParkedOrder.LimitPrice,
                Quantity = pParkedOrder.VolumeTotalOriginal,
                Direction = ConvertToDirectionType(pParkedOrder.Direction),
                OpenCloseFlag = ConvertToOpenCloseFlag(pParkedOrder.CombOffsetFlag),
                HedgeFlag = ConvertToHedgeFlag(pParkedOrder.CombHedgeFlag),
                PriceType = ConvertToOrderPriceType(pParkedOrder.OrderPriceType),
            };

            return result;
        }

        /// <summary>
        /// 预埋单撤单类型转换
        /// </summary>
        /// <param name="pParkedOrderAction">预埋单撤单结构体</param>
        /// <returns></returns>
        private ParkedCanelOrderInfo ConvertToParkedCancelOrder(CThostFtdcParkedOrderActionField pParkedOrderAction)
        {
            ParkedCanelOrderInfo result = new ParkedCanelOrderInfo()
            {
                InvestorID = pParkedOrderAction.InvestorID,
                InstrumentID = pParkedOrderAction.InstrumentID,
                ExchangeID = pParkedOrderAction.ExchangeID,
                OrderRef = pParkedOrderAction.OrderRef,
                OrderActionRef = pParkedOrderAction.OrderActionRef,
                OrderSysID = pParkedOrderAction.OrderSysID,
                ParkedOrderActionID = pParkedOrderAction.ParkedOrderActionID,
                Status = ConvertToParkedOrderStatus(pParkedOrderAction.Status),
            };

            return result;
        }

        /// <summary>
        /// 合约类型转换
        /// </summary>
        /// <param name="pInstrument"></param>
        /// <returns></returns>
        private InstrumentInfo ConvertToInstrumentInfo(CThostFtdcInstrumentField pInstrument)
        {
            InstrumentInfo result = new InstrumentInfo()
            {
                InstrumentID = pInstrument.InstrumentID,
                InstrumentName = pInstrument.InstrumentName,
                ExchangeID = pInstrument.ExchangeID,
                ProductID = pInstrument.ProductID,
                ProductType = ConvertToInstrumentProductClass(pInstrument.ProductClass),
                MaxMarketOrderVolume = pInstrument.MaxMarketOrderVolume,
                MinMarketOrderVolume = pInstrument.MinMarketOrderVolume,
                MaxLimitOrderVolume = pInstrument.MaxLimitOrderVolume,
                MinLimitOrderVolume = pInstrument.MinLimitOrderVolume,
                VolumeMultiple = pInstrument.VolumeMultiple,
                PriceTick = (decimal)pInstrument.PriceTick,
                CreateDate = pInstrument.CreateDate,
                OpenDate = pInstrument.OpenDate,
                ExpireDate = pInstrument.ExpireDate,
                StartDelivDate = pInstrument.StartDelivDate,
                EndDelivDate = pInstrument.EndDelivDate,
                LifePhaseType = ConvertToInstrumentLifePhase(pInstrument.InstLifePhase),
                LongMarginRatio = Convert.ToDecimal(pInstrument.LongMarginRatio.ToString("0.000000")),
                ShortMarginRatio = Convert.ToDecimal(pInstrument.ShortMarginRatio.ToString("0.000000")),
            };

            return result;
        }

        /// <summary>
        /// 投资者持仓明细转换
        /// </summary>
        /// <param name="pInvestorPositionDetail">持仓明细结构体</param>
        /// <returns></returns>
        private PositionDetailInfo ConvertToPositionDetailInfo(CThostFtdcInvestorPositionDetailField pInvestorPositionDetail)
        {
            PositionDetailInfo result = new PositionDetailInfo()
            {
                BrokerID = pInvestorPositionDetail.BrokerID,
                InvestorID = pInvestorPositionDetail.InvestorID,
                InstrumentID = pInvestorPositionDetail.InstrumentID,
                HedgeFlag = ConvertToHedgeFlag(pInvestorPositionDetail.HedgeFlag),
                Direction = ConvertToDirectionType(pInvestorPositionDetail.Direction),
                OpenDate = pInvestorPositionDetail.OpenDate,
                TradeID = pInvestorPositionDetail.TradeID,
                Volume = pInvestorPositionDetail.Volume,
                OpenPrice = (decimal)pInvestorPositionDetail.OpenPrice,
                TradingDay = pInvestorPositionDetail.TradingDay,
                SettlementID = pInvestorPositionDetail.SettlementID,
                TradeType = ConvertToTradeType(pInvestorPositionDetail.TradeType),
                CombInstrumentID = pInvestorPositionDetail.CombInstrumentID,
                ExchangeID = pInvestorPositionDetail.ExchangeID,
                CloseProfitByDate = (decimal)pInvestorPositionDetail.CloseProfitByDate,
                CloseProfitByTrade = (decimal)pInvestorPositionDetail.CloseProfitByTrade,
                PositionProfitByDate = (decimal)pInvestorPositionDetail.PositionProfitByDate,
                PositionProfitByTrade = (decimal)pInvestorPositionDetail.PositionProfitByTrade,
                Margin = (decimal)pInvestorPositionDetail.Margin,
                ExchangeMargin = (decimal)pInvestorPositionDetail.ExchMargin,
                MarginRateByMoney = (decimal)pInvestorPositionDetail.MarginRateByMoney,
                MarginRateByVolume = (decimal)pInvestorPositionDetail.MarginRateByVolume,
                LastSettlementPrice = (decimal)pInvestorPositionDetail.LastSettlementPrice,
                SettlementPrice = (decimal)pInvestorPositionDetail.SettlementPrice,
                CloseVolume = pInvestorPositionDetail.CloseVolume,
                CloseAmount = (decimal)pInvestorPositionDetail.CloseAmount,
            };

            return result;
        }

        /// <summary>
        /// 客户通知转换
        /// </summary>
        /// <param name="pNotice">客户通知结构体</param>
        /// <returns></returns>
        private NoticeInfo ConvertToNoticeInfo(CThostFtdcNoticeField pNotice)
        {
            NoticeInfo result = new NoticeInfo()
            {
                BrokerID = pNotice.BrokerID,
                Content = pNotice.Content,
                ID = pNotice.SequenceLabel,
            };

            return result;
        }

        #endregion
    }
}
