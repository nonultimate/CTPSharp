using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CTPMarketApi;
using System.Threading;

namespace CTPMarketApi.Tests
{
    /// <summary>
    /// CTP行情接口测试用例
    /// </summary>
    [TestClass()]
    public class MarketApiTest
    {
        /// <summary>
        /// 行情接口实例
        /// </summary>
        private MarketApi _api;

        /// <summary>
        /// 连接地址
        /// </summary>
        private string _frontAddr = "tcp://180.168.146.187:10010";

        /// <summary>
        /// 经纪商代码
        /// </summary>
        private string _brokerID = "9999";

        /// <summary>
        /// 投资者账号
        /// </summary>
        private string _investor = "081081";

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
            _api = new MarketApi(_brokerID, _frontAddr);
            _api.OnRspError += new MarketApi.RspError((ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                Console.WriteLine("ErrorID: {0}, ErrorMsg: {1}", pRspInfo.ErrorID, pRspInfo.ErrorMsg);
            });
            _api.OnFrontConnected += new MarketApi.FrontConnected(() =>
            {
                _isConnected = true;
                _api.UserLogin(-3, _investor, _password);
            });
            _api.OnRspUserLogin += new MarketApi.RspUserLogin((ref CThostFtdcRspUserLoginField pRspUserLogin,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                _isLogin = true;
            });
            _api.OnRspUserLogout += new MarketApi.RspUserLogout((ref CThostFtdcUserLogoutField pRspUserLogout,
                ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                _isLogin = false;
                _api.Disconnect();
            });
            _api.OnFrontDisconnected += new MarketApi.FrontDisconnected((int nReasion) =>
            {
                _isConnected = false;
            });

            _api.Connect();
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
                _api.UserLogout(-4);
            }
            else if (_isConnected)
            {
                _api.Disconnect();
            }
            Thread.Sleep(100);
        }

        /// <summary>
        /// 测试获取接口版本
        /// </summary>
        [TestMethod()]
        public void TestGetApiVersion()
        {
            string result = _api.GetApiVersion();
            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }

        /// <summary>
        /// 测试获取交易日
        /// </summary>
        [TestMethod()]
        public void TestGetTradingDay()
        {
            string result = _api.GetTradingDay();
            Assert.AreEqual(8, result.Length);
        }

        /// <summary>
        /// 测试订阅行情
        /// </summary>
        [TestMethod()]
        public void TestSubscribeMarketData()
        {
            string instrumentID = "IF1709";
            _api.OnRspSubMarketData += new MarketApi.RspSubMarketData((ref CThostFtdcSpecificInstrumentField pSpecificInstrument,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                Console.WriteLine("订阅{0}成功", instrumentID);
                Assert.AreEqual(instrumentID, pSpecificInstrument.InstrumentID);

                //退订行情
                _api.UnsubscribeMarketData(instrumentID);
                Thread.Sleep(50);
            });
            _api.OnRspUnSubMarketData += new MarketApi.RspUnSubMarketData((ref CThostFtdcSpecificInstrumentField pSpecificInstrument,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast) =>
            {
                Console.WriteLine("退订{0}成功", instrumentID);
                Assert.AreEqual(instrumentID, pSpecificInstrument.InstrumentID);
            });
            _api.OnRtnDepthMarketData += new MarketApi.RtnDepthMarketData((ref CThostFtdcDepthMarketDataField pDepthMarketData) =>
            {
                Console.WriteLine("昨收价：{0}，现价：{1}", pDepthMarketData.PreClosePrice, pDepthMarketData.LastPrice);
                Assert.AreEqual(instrumentID, pDepthMarketData.InstrumentID);
            });

            //订阅行情
            _api.SubscribeMarketData(instrumentID);
            Thread.Sleep(50);
        }
    }
}
