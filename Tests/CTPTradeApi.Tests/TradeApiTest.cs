using CTPTradeApi;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace CTPTradeApi.Tests
{
    /// <summary>
    /// CTP交易接口测试用例
    /// </summary>
    [TestClass()]
    public class TradeApiTest
    {
        /// <summary>
        /// 交易接口实例
        /// </summary>
        private TradeApi _api;

        /// <summary>
        /// 交易服务器地址
        /// 180.168.146.187:10000
        /// 180.168.146.187:10001
        /// 218.202.237.33:10002
        /// </summary>
        private string _frontAddr = "tcp://180.169.50.131:42205";

        /// <summary>
        /// 经纪商代码
        /// </summary>
        private string _brokerID = "2071";

        /// <summary>
        /// 投资者账号
        /// </summary>
        private string _investorID = "10000020";

        /// <summary>
        /// 密码
        /// </summary>
        private string _password = "123456test";

        /// <summary>
        /// 是否连接
        /// </summary>
        private bool _isConnected;

        /// <summary>
        /// 是否登录
        /// </summary>
        private bool _isLogin;

        /// <summary>
        /// 初始化测试用例
        /// </summary>
        [TestInitialize()]
        public void Initialize()
        {
            _api = new TradeApi(_brokerID, _frontAddr);
            _api.OnRspError += new TradeApi.RspError((ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                Console.WriteLine("ErrorID: {0}, ErrorMsg: {1}", pRspInfo.ErrorID, pRspInfo.ErrorMsg);
            });
            _api.OnFrontConnect += new TradeApi.FrontConnect(() =>
            {
                _isConnected = true;
                _api.Authenticate(-5, _investorID, "", "20190627GTJA0001", "ZCXX_gtjaAmm_v1.3");
            });
            _api.OnRspAuthenticate += new TradeApi.RspAuthenticate((ref CThostFtdcRspAuthenticateField pRspAuthenticate,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    _api.UserLogin(-3, _investorID, _password);
                }
                else
                {
                    Console.WriteLine("Authenticate error: " + pRspInfo.ErrorMsg);
                    throw new Exception("Authenticate error:" + pRspInfo.ErrorMsg);
                }
            });
            _api.OnRspUserLogin += new TradeApi.RspUserLogin((ref CThostFtdcRspUserLoginField pRspUserLogin,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                _isLogin = true;
                _api.SettlementInfoConfirm(-4);
            });
            _api.OnDisconnected += new TradeApi.Disconnected((int nReasion) =>
            {
                _isConnected = false;
            });
            _api.OnRspUserLogout += new TradeApi.RspUserLogout((ref CThostFtdcUserLogoutField pUserLogout,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                _isLogin = false;
                _api.Disconnect();
            });

            _api.Connect();
            Thread.Sleep(500);
        }

        /// <summary>
        /// 清理测试用例
        /// </summary>
        [TestCleanup()]
        public void Cleanup()
        {
            if (_isLogin)
            {
                _api.UserLogout(-5);
            }
            else if (_isConnected)
            {
                _api.Disconnect();
            }
            Thread.Sleep(100);
        }

        /// <summary>
        /// 测试获取接口版本号
        /// </summary>
        [TestMethod()]
        public void TestGetApiVersion()
        {
            string result = _api.GetApiVersion();
            Console.WriteLine("Api version: " + result);
            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }

        /// <summary>
        /// 测试获取交易日
        /// </summary>
        [TestMethod()]
        public void TestGetTradingDay()
        {
            string result = _api.GetTradingDay();
            Console.WriteLine("交易日：" + result);
            Assert.AreEqual(8, result.Length);
        }

        /// <summary>
        /// 测试更新用户口令
        /// </summary>
        [TestMethod()]
        public void TestUserPasswordupdate()
        {
            _api.OnRspUserPasswordUpdate += new TradeApi.RspUserPasswordUpdate((
                ref CThostFtdcUserPasswordUpdateField pUserPasswordUpdate, ref CThostFtdcRspInfoField pRspInfo,
                int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("更新用户口令成功，旧口令：{0}, 新口令：{1}", pUserPasswordUpdate.OldPassword,
                        pUserPasswordUpdate.NewPassword);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                if (nRequestID == 1)
                {
                    Assert.IsFalse(pRspInfo.ErrorID == 0);
                }
                else if (nRequestID == 2 || nRequestID == 3)
                {
                    Assert.IsTrue(pRspInfo.ErrorID == 0);
                }
            });
            string newPassword = "asde34562";
            _api.UserPasswordupdate(1, _investorID, newPassword, newPassword);
            Thread.Sleep(100);

            _api.UserPasswordupdate(2, _investorID, _password, newPassword);
            Thread.Sleep(100);

            _api.UserPasswordupdate(3, _investorID, newPassword, _password);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 测试更新资金账户口令
        /// </summary>
        [TestMethod()]
        public void TestTradingAccountPasswordUpdate()
        {
            _api.OnRspTradingAccountPasswordUpdate += new TradeApi.RspTradingAccountPasswordUpdate((
                ref CThostFtdcTradingAccountPasswordUpdateField pTradingAccountPasswordUpdate,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("更新资金账户口令成功，旧口令：{0}, 新口令：{1}",
                        pTradingAccountPasswordUpdate.OldPassword, pTradingAccountPasswordUpdate.NewPassword);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                if (nRequestID == 1)
                {
                    Assert.IsFalse(pRspInfo.ErrorID == 0);
                }
                else if (nRequestID == 2 || nRequestID == 3)
                {
                    Assert.IsTrue(pRspInfo.ErrorID == 0);
                }
            });
            string newPassword = "asde34562";
            _api.TradingAccountPasswordUpdate(1, _investorID, newPassword, newPassword);
            Thread.Sleep(100);

            _api.TradingAccountPasswordUpdate(2, _investorID, _password, newPassword);
            Thread.Sleep(100);

            _api.TradingAccountPasswordUpdate(3, _investorID, newPassword, _password);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 测试下单
        /// </summary>
        [TestMethod()]
        public void TestOrderInsert()
        {
            _api.OnRtnOrder += new TradeApi.RtnOrder((ref CThostFtdcOrderField pOrder) =>
            {
                Console.WriteLine("下单成功, 合约代码：{0}，价格：{1}", pOrder.InstrumentID, pOrder.LimitPrice);
            });
            _api.OnRspOrderInsert += new TradeApi.RspOrderInsert((ref CThostFtdcInputOrderField pInputOrder,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID != 0)
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                    Assert.IsTrue(pRspInfo.ErrorID == 0);
                }
            });
            CThostFtdcInputOrderField order = new CThostFtdcInputOrderField();
            order.BrokerID = _brokerID;
            order.InvestorID = _investorID;
            order.InstrumentID = "TF1909";
            order.OrderRef = "1";
            order.UserID = _investorID;
            order.OrderPriceType = TThostFtdcOrderPriceTypeType.LimitPrice;
            order.Direction = TThostFtdcDirectionType.Sell;
            order.CombOffsetFlag = TThostFtdcOffsetFlagType.Open;
            order.CombHedgeFlag = TThostFtdcHedgeFlagType.Speculation;
            order.LimitPrice = 97.550;
            order.VolumeTotalOriginal = 1;
            order.TimeCondition = TThostFtdcTimeConditionType.GFD;
            order.VolumeCondition = TThostFtdcVolumeConditionType.AV;
            order.MinVolume = 1;
            order.ContingentCondition = TThostFtdcContingentConditionType.Immediately;
            order.ForceCloseReason = TThostFtdcForceCloseReasonType.NotForceClose;
            order.IsAutoSuspend = (int)TThostFtdcBoolType.No;
            order.BusinessUnit = null;
            order.UserForceClose = (int)TThostFtdcBoolType.No;
            _api.OrderInsert(1, order);
            Thread.Sleep(500);
        }

        /// <summary>
        /// 测试撤单
        /// </summary>
        [TestMethod()]
        public void TestOrderAction()
        {
            _api.OnRspOrderAction += new TradeApi.RspOrderAction((ref CThostFtdcInputOrderActionField pInputOrderAction,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID != 0)
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                    Assert.IsTrue(pRspInfo.ErrorID == 0);
                }
            });
            _api.OnRtnOrder += new TradeApi.RtnOrder((ref CThostFtdcOrderField pOrder) =>
            {
                if (pOrder.OrderStatus == TThostFtdcOrderStatusType.Canceled)
                {
                    Console.WriteLine("撤单成功, 合约代码：{0}", pOrder.BrokerID, pOrder.InstrumentID);
                }
                else
                {
                    Console.WriteLine("下单成功, 合约代码：{0}", pOrder.BrokerID, pOrder.InstrumentID);
                    CThostFtdcInputOrderActionField field = new CThostFtdcInputOrderActionField();
                    field.ActionFlag = TThostFtdcActionFlagType.Delete;
                    field.BrokerID = _brokerID;
                    field.InvestorID = _investorID;
                    field.InstrumentID = "TF1909";
                    if (pOrder.FrontID != 0)
                        field.FrontID = pOrder.FrontID;
                    if (pOrder.SessionID != 0)
                        field.SessionID = pOrder.SessionID;
                    if (pOrder.OrderRef != "")
                        field.OrderRef = pOrder.OrderRef;
                    field.ExchangeID = pOrder.ExchangeID;
                    if (pOrder.OrderSysID != null)
                        field.OrderSysID = new string('\0', 21 - pOrder.OrderSysID.Length) + pOrder.OrderSysID;
                    _api.OrderAction(1, field);
                    Thread.Sleep(50);
                }
                Thread.Sleep(50);
            });
            _api.OnRspOrderInsert += new TradeApi.RspOrderInsert((ref CThostFtdcInputOrderField pInputOrder,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID != 0)
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                    Assert.IsTrue(pRspInfo.ErrorID == 0);
                }
            });
            CThostFtdcInputOrderField order = new CThostFtdcInputOrderField();
            order.BrokerID = _brokerID;
            order.InvestorID = _investorID;
            order.InstrumentID = "TF1909";
            order.OrderRef = "1";
            order.UserID = _investorID;
            order.OrderPriceType = TThostFtdcOrderPriceTypeType.LimitPrice;
            order.Direction = TThostFtdcDirectionType.Buy;
            order.CombOffsetFlag = TThostFtdcOffsetFlagType.Open;
            order.CombHedgeFlag = TThostFtdcHedgeFlagType.Speculation;
            order.LimitPrice = 97.150;
            order.VolumeTotalOriginal = 1;
            order.TimeCondition = TThostFtdcTimeConditionType.GFD;
            order.VolumeCondition = TThostFtdcVolumeConditionType.AV;
            order.MinVolume = 1;
            order.ContingentCondition = TThostFtdcContingentConditionType.Immediately;
            order.ForceCloseReason = TThostFtdcForceCloseReasonType.NotForceClose;
            order.IsAutoSuspend = (int)TThostFtdcBoolType.No;
            order.BusinessUnit = null;
            order.UserForceClose = (int)TThostFtdcBoolType.No;
            _api.OrderInsert(1, order);
            Thread.Sleep(500);
        }

        /// <summary>
        /// 测试查询最大允许报单数量
        /// </summary>
        [TestMethod()]
        public void TestQueryMaxOrderVolume()
        {
            _api.OnRspQueryMaxOrderVolume += new TradeApi.RspQueryMaxOrderVolume((
                ref CThostFtdcQueryMaxOrderVolumeField pQueryMaxOrderVolume, ref CThostFtdcRspInfoField pRspInfo,
                int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("查询成功, MaxVolume: {0}", pQueryMaxOrderVolume.MaxVolume);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            CThostFtdcQueryMaxOrderVolumeField pMaxOrderVolume = new CThostFtdcQueryMaxOrderVolumeField();
            pMaxOrderVolume.BrokerID = _brokerID;
            pMaxOrderVolume.InvestorID = _investorID;
            pMaxOrderVolume.InstrumentID = "bu1712";
            pMaxOrderVolume.Direction = TThostFtdcDirectionType.Buy;
            pMaxOrderVolume.OffsetFlag = TThostFtdcOffsetFlagType.Close;
            pMaxOrderVolume.HedgeFlag = TThostFtdcHedgeFlagType.Arbitrage;
            pMaxOrderVolume.MaxVolume = 1000;
            _api.QueryMaxOrderVolume(7, pMaxOrderVolume);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试确认结算结果
        /// </summary>
        [TestMethod()]
        public void TestSettlementInfoConfirm()
        {
            _api.OnRspSettlementInfoConfirm += new TradeApi.RspSettlementInfoConfirm((
                ref CThostFtdcSettlementInfoConfirmField pSettlementInfoConfirm, ref CThostFtdcRspInfoField pRspInfo,
                int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("确认结算结果, ConfirmDate: {0}", pSettlementInfoConfirm.ConfirmDate);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.SettlementInfoConfirm(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询报单
        /// </summary>
        [TestMethod()]
        public void TestQueryOrder()
        {
            _api.OnRspQryOrder += new TradeApi.RspQryOrder((ref CThostFtdcOrderField pOrder,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("报单查询成功, 合约代码：{0}，价格：{1}", pOrder.InstrumentID, pOrder.LimitPrice);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryOrder(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询成交
        /// </summary>
        [TestMethod()]
        public void TestQueryTrade()
        {
            _api.OnRspQryTrade += new TradeApi.RspQryTrade((ref CThostFtdcTradeField pTrade,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("成交查询成功, TradeID: {0}", pTrade.TradeID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryTrade(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询投资者持仓
        /// </summary>
        [TestMethod()]
        public void TestQueryInvestorPosition()
        {
            _api.OnRspQryInvestorPosition += new TradeApi.RspQryInvestorPosition((
                ref CThostFtdcInvestorPositionField pInvestorPosition, ref CThostFtdcRspInfoField pRspInfo,
                int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("投资者持仓查询成功, 合约代码：{0}", pInvestorPosition.InstrumentID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryInvestorPosition(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询帐户资金
        /// </summary>
        [TestMethod()]
        public void TestQueryTradingAccount()
        {
            _api.OnRspQryTradingAccount += new TradeApi.RspQryTradingAccount((
                ref CThostFtdcTradingAccountField pTradingAccount, ref CThostFtdcRspInfoField pRspInfo,
                int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("帐户资金查询成功, Available: {0}", pTradingAccount.Available);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryTradingAccount(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询投资者
        /// </summary>
        [TestMethod()]
        public void TestQueryInvestor()
        {
            _api.OnRspQryInvestor += new TradeApi.RspQryInvestor((ref CThostFtdcInvestorField pInvestor,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("投资者查询成功, InvestorID: {0}", pInvestor.InvestorID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryInvestor(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询交易编码
        /// </summary>
        [TestMethod()]
        public void TestQueryTradingCode()
        {
            _api.OnRspQryTradingCode += new TradeApi.RspQryTradingCode((ref CThostFtdcTradingCodeField pTradingCode,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("交易编码查询成功, InvestorID: {0}", pTradingCode.InvestorID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryTradingCode(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询合约保证金率
        /// </summary>
        [TestMethod()]
        public void TestQueryInstrumentMarginRate()
        {
            _api.OnRspQryInstrumentMarginRate += new TradeApi.RspQryInstrumentMarginRate((
                ref CThostFtdcInstrumentMarginRateField pInstrumentMarginRate, ref CThostFtdcRspInfoField pRspInfo,
                int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("合约保证金率查询成功, 合约代码：{0}", pInstrumentMarginRate.InstrumentID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryInstrumentMarginRate(1, "bu1712");
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询合约手续费率
        /// </summary>
        [TestMethod()]
        public void TestQueryInstrumentCommissionRate()
        {
            string instrumentID = "bu1712";
            _api.OnRspQryInstrumentCommissionRate += new TradeApi.RspQryInstrumentCommissionRate((
                ref CThostFtdcInstrumentCommissionRateField pInstrumentCommissionRate,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("合约手续费率查询成功, 合约代码：{0}", pInstrumentCommissionRate.InstrumentID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryInstrumentCommissionRate(1, instrumentID);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询交易所
        /// </summary>
        [TestMethod()]
        public void TestQueryExchange()
        {
            _api.OnRspQryExchange += new TradeApi.RspQryExchange((ref CThostFtdcExchangeField pExchange,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("交易所查询成功, ExchangeID: {0}", pExchange.ExchangeID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryExchange(1, "SHFE");
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询合约
        /// </summary>
        [TestMethod()]
        public void TestQueryInstrument()
        {
            _api.OnRspQryInstrument += new TradeApi.RspQryInstrument((ref CThostFtdcInstrumentField pInstrument,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("合约查询成功, 合约代码：{0}", pInstrument.InstrumentID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryInstrument(1, "bu1712");
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询行情
        /// </summary>
        [TestMethod()]
        public void TestQryDepthMarketData()
        {
            _api.OnRspQryDepthMarketData += new TradeApi.RspQryDepthMarketData((
                ref CThostFtdcDepthMarketDataField pDepthMarketData, ref CThostFtdcRspInfoField pRspInfo,
                int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("行情查询成功, 合约代码：{0}", pDepthMarketData.InstrumentID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryMarketData(1, "bu1712");
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询投资者结算结果
        /// </summary>
        [TestMethod()]
        public void TestQuerySettlementInfo()
        {
            _api.OnRspQrySettlementInfo += new TradeApi.RspQrySettlementInfo((
                ref CThostFtdcSettlementInfoField pSettlementInfo, ref CThostFtdcRspInfoField pRspInfo,
                int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("投资者结算结果查询成功, SettlementID: {0}", pSettlementInfo.SettlementID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QuerySettlementInfo(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询投资者持仓明细
        /// </summary>
        [TestMethod()]
        public void TestQueryInvestorPositionDetail()
        {
            _api.OnRspQryInvestorPositionDetail += new TradeApi.RspQryInvestorPositionDetail((
                ref CThostFtdcInvestorPositionDetailField pInvestorPositionDetail, ref CThostFtdcRspInfoField pRspInfo,
                int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("投资者持仓明细查询成功, 合约代码：{0}", pInvestorPositionDetail.InstrumentID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryInvestorPositionDetail(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询客户通知
        /// </summary>
        [TestMethod()]
        public void TestQueryNotice()
        {
            _api.OnRspQryNotice += new TradeApi.RspQryNotice((ref CThostFtdcNoticeField pNotice,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("客户通知查询成功, Content: {0}", pNotice.Content);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryNotice(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询结算信息确认
        /// </summary>
        [TestMethod()]
        public void TestQuerySettlementInfoConfirm()
        {
            _api.OnRspQrySettlementInfoConfirm += new TradeApi.RspQrySettlementInfoConfirm((
                ref CThostFtdcSettlementInfoConfirmField pSettlementInfoConfirm, ref CThostFtdcRspInfoField pRspInfo,
                int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("结算信息确认查询成功, ConfirmDate: {0}", pSettlementInfoConfirm.ConfirmDate);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QuerySettlementInfoConfirm(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询**组合**持仓明细
        /// </summary>
        [TestMethod()]
        public void TestQueryInvestorPositionCombinaDetail()
        {
            _api.OnRspQryInvestorPositionCombineDetail += new TradeApi.RspQryInvestorPositionCombineDetail((
                ref CThostFtdcInvestorPositionCombineDetailField pInvestorPositionCombineDetail,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("持仓明细查询成功, TradingDay: {0}", pInvestorPositionCombineDetail.TradingDay);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryInvestorPositionCombineDetail(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询保证金监管系统经纪公司资金账户密钥
        /// </summary>
        [TestMethod()]
        public void TestQueryCFMMCTradingAccountKey()
        {
            _api.OnRspQryCFMMCTradingAccountKey += new TradeApi.RspQryCFMMCTradingAccountKey((
                ref CThostFtdcCFMMCTradingAccountKeyField pCFMMCTradingAccountKey,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("账户密钥查询成功, KeyID: {0}", pCFMMCTradingAccountKey.KeyID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryCFMMCTradingAccountKey(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询交易通知
        /// </summary>
        [TestMethod()]
        public void TestQueryTradingNotice()
        {
            _api.OnRspQryTradingNotice += new TradeApi.RspQryTradingNotice((
                ref CThostFtdcTradingNoticeField pTradingNotice, ref CThostFtdcRspInfoField pRspInfo,
                int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("交易通知查询成功, FieldContent: {0}", pTradingNotice.FieldContent);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryTradingNotice(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询经纪公司交易参数
        /// </summary>
        [TestMethod()]
        public void TestQueryBrokerTradingParams()
        {
            _api.OnRspQryBrokerTradingParams += new TradeApi.RspQryBrokerTradingParams((
                ref CThostFtdcBrokerTradingParamsField pBrokerTradingParams,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("交易参数查询成功, BrokerID: {0}", pBrokerTradingParams.BrokerID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryBrokerTradingParams(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询经纪公司交易算法
        /// </summary>
        [TestMethod()]
        public void TestQueryBrokerTradingAlgos()
        {
            _api.OnRspQryBrokerTradingAlgos += new TradeApi.RspQryBrokerTradingAlgos((
                ref CThostFtdcBrokerTradingAlgosField pBrokerTradingAlgos,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("交易算法查询成功, BrokerID: {0}", pBrokerTradingAlgos.BrokerID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryBrokerTradingAlgos(1, "SHFE", "bu1712");
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试预埋单录入
        /// </summary>
        [TestMethod()]
        public void TestParkedOrderInsert()
        {
            _api.OnRspParkedOrderInsert += new TradeApi.RspParkedOrderInsert((ref CThostFtdcParkedOrderField pParkedOrder,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("预埋单录入成功, ParkedOrderID: {0}", pParkedOrder.ParkedOrderID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            CThostFtdcParkedOrderField field = new CThostFtdcParkedOrderField();
            field.BrokerID = _brokerID;
            field.BusinessUnit = null;
            field.ContingentCondition = TThostFtdcContingentConditionType.ParkedOrder;
            field.ForceCloseReason = TThostFtdcForceCloseReasonType.NotForceClose;
            field.InvestorID = _investorID;
            field.IsAutoSuspend = (int)TThostFtdcBoolType.No;
            field.MinVolume = 1;
            field.OrderPriceType = TThostFtdcOrderPriceTypeType.LimitPrice;
            field.OrderRef = "1";
            field.TimeCondition = TThostFtdcTimeConditionType.GFD;
            field.UserForceClose = (int)TThostFtdcBoolType.No;
            field.UserID = _investorID;
            field.VolumeCondition = TThostFtdcVolumeConditionType.AV;
            field.CombHedgeFlag = TThostFtdcHedgeFlagType.Speculation;
            field.InstrumentID = "TF1909";
            field.CombOffsetFlag = TThostFtdcOffsetFlagType.Open;
            field.Direction = TThostFtdcDirectionType.Buy;
            field.LimitPrice = 97.080;
            field.VolumeTotalOriginal = 1;
            _api.ParkedOrderInsert(1, field);
            Thread.Sleep(500);
        }

        /// <summary>
        /// 测试预埋撤单录入
        /// </summary>
        [TestMethod()]
        public void TestParkedOrderAction()
        {
            _api.OnRspParkedOrderAction += new TradeApi.RspParkedOrderAction((
                ref CThostFtdcParkedOrderActionField pParkedOrderAction, ref CThostFtdcRspInfoField pRspInfo,
                int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("预埋撤单录入成功, ParkedOrderActionID: {0}", pParkedOrderAction.ParkedOrderActionID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            CThostFtdcParkedOrderActionField field = new CThostFtdcParkedOrderActionField();
            field.BrokerID = _brokerID;
            field.InvestorID = _investorID;
            field.InstrumentID = "TF1909";
            field.ActionFlag = TThostFtdcActionFlagType.Delete;
            field.FrontID = 1;
            field.SessionID = 574058695;
            field.OrderRef = "1";
            field.ExchangeID = "CFFEX";
            string OrderSysID = "132984";
            field.OrderSysID = new string('\0', 21 - OrderSysID.Length) + OrderSysID;
            _api.ParkedOrderAction(1, field);
            Thread.Sleep(500);
        }

        /// <summary>
        /// 测试删除预埋单
        /// </summary>
        [TestMethod()]
        public void TestRemoveParkedOrder()
        {
            _api.OnRspRemoveParkedOrder += new TradeApi.RspRemoveParkedOrder((
                ref CThostFtdcRemoveParkedOrderField pRemoveParkedOrder,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("删除预埋单成功, ParkedOrderID: {0}", pRemoveParkedOrder.ParkedOrderID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.OnRspParkedOrderInsert += new TradeApi.RspParkedOrderInsert((
                ref CThostFtdcParkedOrderField pParkedOrder, ref CThostFtdcRspInfoField pRspInfo,
                int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("预埋单录入成功, ParkedOrderID: {0}", pParkedOrder.ParkedOrderID);
                    _api.RemoveParkedOrder(1, pParkedOrder.ParkedOrderID);
                    Thread.Sleep(50);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            CThostFtdcParkedOrderField field = new CThostFtdcParkedOrderField();
            field.BrokerID = _brokerID;
            field.InvestorID = _investorID;
            field.InstrumentID = "TF1909";
            field.OrderRef = "";
            field.UserID = _investorID;
            field.OrderPriceType = TThostFtdcOrderPriceTypeType.LimitPrice;
            field.Direction = TThostFtdcDirectionType.Buy;
            field.CombOffsetFlag = TThostFtdcOffsetFlagType.Open;
            field.CombHedgeFlag = TThostFtdcHedgeFlagType.Speculation;
            field.LimitPrice = 96.88;
            field.VolumeTotalOriginal = 1;
            field.TimeCondition = TThostFtdcTimeConditionType.GFD;
            field.VolumeCondition = TThostFtdcVolumeConditionType.AV;
            field.MinVolume = 1;
            field.ContingentCondition = TThostFtdcContingentConditionType.ParkedOrder;
            field.ForceCloseReason = TThostFtdcForceCloseReasonType.NotForceClose;
            field.IsAutoSuspend = (int)TThostFtdcBoolType.No;
            field.UserForceClose = (int)TThostFtdcBoolType.No;
            _api.ParkedOrderInsert(1, field);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试删除预埋撤单
        /// </summary>
        [TestMethod()]
        public void TestRemoveParkedOrderAction()
        {
            _api.OnRspRemoveParkedOrderAction += new TradeApi.RspRemoveParkedOrderAction((
                ref CThostFtdcRemoveParkedOrderActionField pRemoveParkedOrderAction,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("删除预埋撤单成功, ParkedOrderActionID: {0}",
                        pRemoveParkedOrderAction.ParkedOrderActionID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.OnRspParkedOrderAction += new TradeApi.RspParkedOrderAction((
                ref CThostFtdcParkedOrderActionField pParkedOrderAction,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("预埋撤单录入成功, ParkedOrderActionID: {0}", pParkedOrderAction.ParkedOrderActionID);
                    _api.RemoveParkedOrderAction(1, pParkedOrderAction.ParkedOrderActionID);
                    Thread.Sleep(50);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            CThostFtdcParkedOrderActionField field = new CThostFtdcParkedOrderActionField();
            field.ActionFlag = TThostFtdcActionFlagType.Delete;
            field.BrokerID = _brokerID;
            field.InvestorID = _investorID;
            field.InstrumentID = "TF1909";
            field.FrontID = 1;
            field.SessionID = -1253843411;
            field.OrderRef = "1";
            field.ExchangeID = "CFFEX";
            string OrderSysID = "132984";
            field.OrderSysID = new string('\0', 21 - OrderSysID.Length) + OrderSysID;
            _api.ParkedOrderAction(1, field);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询转帐银行
        /// </summary
        [TestMethod()]
        public void TestQueryTransferBank()
        {
            _api.OnRspQryTransferBank += new TradeApi.RspQryTransferBank((ref CThostFtdcTransferBankField pTransferBank,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("转帐银行查询成功, BankID: {0}", pTransferBank.BankID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryTransferBank(1, "101", "1011");
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询转帐流水
        /// </summary
        [TestMethod()]
        public void TestQueryTransferSerial()
        {
            _api.OnRspQryTransferSerial += new TradeApi.RspQryTransferSerial((
                ref CThostFtdcTransferSerialField pTransferSerial,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("转帐流水查询成功, PlateSerial: {0}", pTransferSerial.PlateSerial);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryTransferSerial(1, "101");
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询银期签约关系
        /// </summary
        [TestMethod()]
        public void TestQueryAccountregister()
        {
            _api.OnRspQryAccountregister += new TradeApi.RspQryAccountregister((
                ref CThostFtdcAccountregisterField pAccountregister,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("银期签约关系查询成功, TradeDay: {0}", pAccountregister.TradeDay);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryAccountregister(1, "101");
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询签约银行
        /// </summary
        [TestMethod()]
        public void TestQueryContractBank()
        {
            _api.OnRspQryContractBank += new TradeApi.RspQryContractBank((ref CThostFtdcContractBankField pContractBank,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("签约银行查询成功, BankID: {0}", pContractBank.BankID);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryContractBank(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询预埋单
        /// </summary
        [TestMethod()]
        public void TestQueryParkedOrder()
        {
            _api.OnRspQryParkedOrder += new TradeApi.RspQryParkedOrder((ref CThostFtdcParkedOrderField pParkedOrder,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("预埋单查询成功, ParkedOrderID: {0},Status: {1}", pParkedOrder.ParkedOrderID,
                        pParkedOrder.Status);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryParkedOrder(1);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询预埋撤单
        /// </summary
        [TestMethod()]
        public void TestQueryParkedOrderAction()
        {
            _api.OnRspQryParkedOrderAction += new TradeApi.RspQryParkedOrderAction((
                ref CThostFtdcParkedOrderActionField pParkedOrderAction,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                if (pRspInfo.ErrorID == 0)
                {
                    Console.WriteLine("预埋撤单查询成功, parkedOrderActionID: {0}, Status: {1}",
                        pParkedOrderAction.ParkedOrderActionID, pParkedOrderAction.Status);
                }
                else
                {
                    Console.WriteLine(pRspInfo.ErrorMsg);
                }
                Assert.IsTrue(pRspInfo.ErrorID == 0);
            });
            _api.QueryParkedOrderAction(1);
            Thread.Sleep(200);
        }
    }
}
