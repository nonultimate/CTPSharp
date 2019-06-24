using CTPCore;
using CTPTradeAdapter.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CTPTradeAdapter.Adapter.Tests
{
    /// <summary>
    /// CTP交易适配器测试用例
    /// </summary>
    [TestClass()]
    public class CTPTradeAdapterTest
    {
        /// <summary>
        /// 交易接口实例
        /// </summary>
        private TradeAdapter _adapter;

        /// <summary>
        /// 交易服务器地址
        /// 180.168.146.187:10000
        /// 180.168.146.187:10001
        /// 218.202.237.33:10002
        /// </summary>
        private string _frontAddr = "tcp://218.202.237.33:10002";

        /// <summary>
        /// 经纪商代码
        /// </summary>
        private string _brokerID = "9999";

        /// <summary>
        /// 投资者账号
        /// </summary>
        private string _investorID = "081081";

        /// <summary>
        /// 密码
        /// </summary>
        private string _password = "test1234";

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
            _adapter = new TradeAdapter();
            var connectCallback = new DataCallback((DataResult result) =>
            {
                if (result.IsSuccess)
                {
                    _isConnected = true;
                    var loginCallback = new DataCallback((DataResult loginResult) =>
                    {
                        if (loginResult.IsSuccess)
                        {
                            _isLogin = true;
                            _adapter.SettlementInfoConfirm(null);
                        }
                        else
                        {
                            Console.WriteLine("登录失败：{0}", loginResult.Error);
                        }
                    });
                    _adapter.UserLogin(loginCallback, _investorID, _password);
                    Thread.Sleep(100);
                }
                else
                {
                    Console.WriteLine("连接失败：{0}", result.Error);
                }
            });
            _adapter.Connect(connectCallback, _brokerID, _frontAddr);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 清理测试用例
        /// </summary>
        [TestCleanup()]
        public void Cleanup()
        {
            if (_isLogin)
            {
                var logoutCallback = new DataCallback((DataResult logoutResult) =>
                {
                    if (logoutResult.IsSuccess)
                    {
                        _isLogin = false;
                    }
                    else
                    {
                        Console.WriteLine("登出失败：{0}", logoutResult.Error);
                    }
                });
                _adapter.UserLogout(logoutCallback);
                Thread.Sleep(100);
            }
            else if (_isConnected)
            {
                var disconnectCallback = new DataCallback((DataResult disconnectResult) =>
                {
                    if (disconnectResult.IsSuccess)
                    {
                        _isConnected = false;
                    }
                    else
                    {
                        Console.WriteLine("登出失败：{0}", disconnectResult.Error);
                    }
                });
                _adapter.Disconnect(disconnectCallback);
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 测试获取交易日
        /// </summary>
        [TestMethod()]
        public void TestGetTradingDay()
        {
            string result = _adapter.GetTradingDay();
            Console.WriteLine("交易日：" +result);
            Assert.AreEqual(8, result.Length);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 测试更新用户口令
        /// </summary>
        [TestMethod()]
        public void TestUpdateUserPassword()
        {
            var updUserPasswCallback = new DataCallback((DataResult result) =>
            {
                if (result.IsSuccess)
                {
                    Console.WriteLine("更新用户口令成功");
                }
                Assert.IsFalse(result.IsSuccess);
            });
            string newPassword = "asde34562";
            _adapter.UpdateUserPassword(updUserPasswCallback, _password, newPassword);
            Thread.Sleep(100);
            _adapter.UpdateUserPassword(updUserPasswCallback, newPassword, _password);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 测试下单
        /// </summary>
        [TestMethod()]
        public void TestInsertOrder()
        {
            var insertOrderCallback = new DataCallback<OrderInfo>((DataResult<OrderInfo> result) =>
            {
                OrderInfo orderInfo = new OrderInfo();
                orderInfo = result.Result;
                if (result.IsSuccess)
                {
                    Console.WriteLine("下单成功, OrderRef：{0}, OrderSysID：{1}", orderInfo.OrderRef, orderInfo.OrderSysID);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            OrderParameter order = new OrderParameter();
            order.InstrumentID = "TF1809";
            order.OrderRef = "1";
            order.Direction = DirectionType.Buy;
            order.PriceType = OrderPriceType.LimitPrice;
            order.OpenCloseFlag = OpenCloseFlag.Open;
            order.HedgeFlag = HedgeFlag.Speculation;
            order.Price = 97.260M;
            order.Quantity = 1;
            order.TimeCondition = TimeConditionType.GFD;
            order.VolumeCondition = VolumeConditionType.AV;
            order.MinVolume = 1;
            order.ContingentCondition = ContingentConditionType.Immediately;
            order.ForceCloseReason = ForceCloseReasonType.NotForceClose;
            order.IsAutoSuspend = 0;
            order.UserForceClose = 0;

            _adapter.InsertOrder(insertOrderCallback, order);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 测试撤单
        /// </summary>
        [TestMethod()]
        public void TestCancelOrder()
        {
            var insertOrderCallback = new DataCallback<OrderInfo>((DataResult<OrderInfo> result) =>
            {
                OrderInfo orderInfo = new OrderInfo();
                orderInfo = result.Result;
                if (result.IsSuccess)
                {
                    var cancelOrderCallback = new DataCallback<OrderInfo>((DataResult<OrderInfo> cancelOrderResult) =>
                    {
                        orderInfo = cancelOrderResult.Result;
                        if (cancelOrderResult.IsSuccess)
                        {
                            Console.WriteLine("撤单成功，OrderRef：{0}, InstrumentID：{1}", orderInfo.OrderRef, orderInfo.InstrumentID);
                        }
                        Assert.IsTrue(cancelOrderResult.IsSuccess);
                    });
                    CancelOrderParameter field = new CancelOrderParameter();
                    field.ActionFlag = ActionFlag.Delete;
                    field.InstrumentID = "TF1809";
                    field.OrderRef = orderInfo.OrderRef;
                    field.ExchangeID = orderInfo.ExchangeID;
                    field.OrderSysID = new string('\0', 21 - orderInfo.OrderSysID.Length) + orderInfo.OrderSysID;

                    _adapter.CancelOrder(cancelOrderCallback, field);
                    Thread.Sleep(50);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            OrderParameter order = new OrderParameter();
            order.InstrumentID = "TF1809";
            order.OrderRef = "1";
            order.Direction = DirectionType.Buy;
            order.PriceType = OrderPriceType.LimitPrice;
            order.OpenCloseFlag = OpenCloseFlag.Open;
            order.HedgeFlag = HedgeFlag.Speculation;
            order.Price = 97.270M;
            order.Quantity = 1;
            order.TimeCondition = TimeConditionType.GFD;
            order.VolumeCondition = VolumeConditionType.AV;
            order.MinVolume = 1;
            order.ContingentCondition = ContingentConditionType.Immediately;
            order.ForceCloseReason = ForceCloseReasonType.NotForceClose;
            order.IsAutoSuspend = 0;
            order.UserForceClose = 0;

            _adapter.InsertOrder(insertOrderCallback, order);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询资金账户
        /// </summary>
        [TestMethod()]
        public void TestQueryAccount()
        {
            var queryAccountCallback = new DataCallback<AccountInfo>((DataResult<AccountInfo> result) =>
            {
                if (result.IsSuccess)
                {
                    Console.WriteLine("帐户资金查询成功, Available：{0}", result.Result.Available);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            _adapter.QueryAccount(queryAccountCallback);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 测试查询投资者持仓
        /// </summary>
        [TestMethod()]
        public void TestQueryPosition()
        {
            var queryPositionCallback = new DataListCallback<PositionInfo>((DataListResult<PositionInfo> result) =>
            {
                if (result.IsSuccess)
                {
                    Console.WriteLine("投资者持仓查询成功, 记录条数：{0}", result.Result.Count);
                }
                else
                {
                    Console.WriteLine("投资者持仓查询失败：{0}", result.Error);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            _adapter.QueryPosition(queryPositionCallback);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 测试查询报单
        /// </summary>
        [TestMethod()]
        public void TestQueryOrder()
        {
            var queryOrderCallback = new DataListCallback<OrderInfo>((DataListResult<OrderInfo> result) =>
            {
                if (result.IsSuccess)
                {
                    Console.WriteLine("报单查询成功, 记录条数：{0}", result.Result.Count);
                }
                else
                {
                    Console.WriteLine("报单查询失败：{0}", result.Error);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            _adapter.QueryOrder(queryOrderCallback);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 测试查询成交
        /// </summary>
        [TestMethod()]
        public void TestQueryTrade()
        {
            var queryPositionCallback = new DataListCallback<TradeInfo>((DataListResult<TradeInfo> result) =>
            {
                if (result.IsSuccess)
                {
                    Console.WriteLine("成交查询成功，记录条数：{0}", result.Result.Count);
                }
                else
                {
                    Console.WriteLine("成交查询失败：{0}", result.Error);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            _adapter.QueryTrade(queryPositionCallback);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 测试预埋单录入
        /// </summary>
        [TestMethod()]
        public void TestInsertParkedOrder()
        {
            var insertParkedOrderCallback = new DataCallback<ParkedOrderInfo>((DataResult<ParkedOrderInfo> result) =>
            {
                if (result.IsSuccess)
                {
                    Console.WriteLine("预埋单录入成功，ParkedOrderID：{0}", result.Result.ParkedOrderID);
                }
                else
                {
                    Console.WriteLine("预埋单录入失败：{0}", result.Error);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            OrderParameter field = new OrderParameter();
            field.InstrumentID = "TF1809";
            field.OrderRef = "1";
            field.Direction = DirectionType.Buy;
            field.PriceType = OrderPriceType.LimitPrice;
            field.OpenCloseFlag = OpenCloseFlag.Open;
            field.HedgeFlag = HedgeFlag.Speculation;
            field.Price = 97.010M;
            field.Quantity = 1;
            field.TimeCondition = TimeConditionType.GFD;
            field.VolumeCondition = VolumeConditionType.AV;
            field.MinVolume = 1;
            field.ContingentCondition = ContingentConditionType.Immediately;
            field.ForceCloseReason = ForceCloseReasonType.NotForceClose;
            field.IsAutoSuspend = 0;
            field.UserForceClose = 0;

            _adapter.InsertParkedOrder(insertParkedOrderCallback, field);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试预埋撤单录入
        /// </summary>
        [TestMethod()]
        public void TestCancelParkedOrder()
        {
            var insertParkedOrderCallback = new DataCallback<ParkedOrderInfo>((DataResult<ParkedOrderInfo> result) =>
            {
                ParkedOrderInfo pParkedOrder = new ParkedOrderInfo();
                pParkedOrder = result.Result;
                if (result.IsSuccess)
                {
                    var cancelParkedOrderCallback = new DataCallback<ParkedOrderInfo>((DataResult<ParkedOrderInfo> cancelParkedOrderResult) =>
                    {
                        pParkedOrder = cancelParkedOrderResult.Result;
                        if (cancelParkedOrderResult.IsSuccess)
                        {
                            Console.WriteLine("预埋撤单录入成功，ParkedOrderActionID：{0}", pParkedOrder.ParkedOrderActionID);
                        }
                        else
                        {
                            Console.WriteLine("预埋撤单录入失败：{0}", cancelParkedOrderResult.Error);
                        }
                        Assert.IsTrue(cancelParkedOrderResult.IsSuccess);
                    });
                    CancelOrderParameter fieldAction = new CancelOrderParameter();
                    fieldAction.ActionFlag = ActionFlag.Delete;
                    fieldAction.InstrumentID = pParkedOrder.InstrumentID;
                    fieldAction.OrderRef = pParkedOrder.OrderRef;
                    fieldAction.ExchangeID = pParkedOrder.ExchangeID;
                    fieldAction.OrderSysID = new string('\0', 21 - pParkedOrder.OrderSysID.Length) + pParkedOrder.OrderSysID;

                    _adapter.CancelParkedOrder(cancelParkedOrderCallback, fieldAction);
                    Thread.Sleep(50);
                }
                else
                {
                    Console.WriteLine("预埋单录入失败：", result.Error);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            OrderParameter field = new OrderParameter();
            field.InstrumentID = "TF1809";
            field.OrderRef = "1";
            field.Direction = DirectionType.Buy;
            field.PriceType = OrderPriceType.LimitPrice;
            field.OpenCloseFlag = OpenCloseFlag.Open;
            field.HedgeFlag = HedgeFlag.Speculation;
            field.Price = 97.010M;
            field.Quantity = 1;
            field.TimeCondition = TimeConditionType.GFD;
            field.VolumeCondition = VolumeConditionType.AV;
            field.MinVolume = 1;
            field.ContingentCondition = ContingentConditionType.Immediately;
            field.ForceCloseReason = ForceCloseReasonType.NotForceClose;
            field.IsAutoSuspend = 0;
            field.UserForceClose = 0;

            _adapter.InsertParkedOrder(insertParkedOrderCallback, field);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 测试查询预埋单
        /// </summary
        [TestMethod()]
        public void TestQueryParkedOrder()
        {
            var callback = new DataListCallback<ParkedOrderInfo>((DataListResult<ParkedOrderInfo> result) =>
            {
                if (result.IsSuccess)
                {
                    Console.WriteLine("预埋单条数：" + result.Result.Count);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            _adapter.QueryParkedOrder(callback);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 测试查询预埋撤单
        /// </summary
        [TestMethod()]
        public void TestQueryParkedOrderAction()
        {
            var callback = new DataListCallback<ParkedCanelOrderInfo>((DataListResult<ParkedCanelOrderInfo> result) =>
            {
                if (result.IsSuccess)
                {
                    Console.WriteLine("预埋撤单条数：" + result.Result.Count);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            _adapter.QueryParkedOrderAction(callback);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 测试查询合约列表
        /// </summary>
        [TestMethod()]
        public void TestQueryInstrument()
        {
            var callback = new DataListCallback<InstrumentInfo>((DataListResult<InstrumentInfo> result) => 
            {
                if (result.IsSuccess)
                {
                    Console.WriteLine("合约条数：" + result.Result.Count);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            _adapter.QueryInstrument(callback, null);
            Thread.Sleep(500);
        }

        /// <summary>
        /// 测试查询投资者
        /// </summary>
        [TestMethod()]
        public void TestQueryInvestor()
        {
            var queryInvestorInfoCallback = new DataCallback<InvestorInfo>((DataResult<InvestorInfo> result) =>
            {
                if (result.IsSuccess)
                {
                    Console.WriteLine("投资者查询成功，InvestorID：{0}", result.Result.InvestorID);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            _adapter.QueryInvestor(queryInvestorInfoCallback);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 查询投资者持仓明细
        /// </summary>
        [TestMethod()]
        public void TestQueryInvestorPositionDetail()
        {
            var queryInvestorPositionDetailCallback = new DataListCallback<PositionDetailInfo>((DataListResult<PositionDetailInfo> result) =>
            {
                if (result.IsSuccess)
                {
                    Console.WriteLine("投资者持仓明细条数：" + result.Result.Count);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            string InstrumentID = "TF1809";
            _adapter.QueryInvestorPositionDetail(queryInvestorPositionDetailCallback, InstrumentID);
            Thread.Sleep(200);
        }

        /// <summary>
        /// 查询客户通知
        /// </summary>
        [TestMethod()]
        public void TestQueryNotice()
        {
            var queryNoticeCallback = new DataListCallback<NoticeInfo>((DataListResult<NoticeInfo> result) =>
            {
                if (result.IsSuccess)
                {
                    Console.WriteLine("客户通知条数：" + result.Result.Count);
                }
                Assert.IsTrue(result.IsSuccess);
            });
            _adapter.QueryNotice(queryNoticeCallback);
            Thread.Sleep(200);
        }

    }
}