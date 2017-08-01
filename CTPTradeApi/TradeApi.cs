using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using CTPCore;
using System.IO;

namespace CTPTradeApi
{
    /// <summary>
    /// CTP交易接口类
    /// </summary>
    public class TradeApi
    {
        #region 属性

        /// <summary>
        /// DLL名称
        /// </summary>
        private const string dllName = "TradeApi.dll";

        /// <summary>
        /// 前置地址
        /// </summary>
        public string FrontAddr { get; set; }

        /// <summary>
        /// 经纪公司代码
        /// </summary>
        public string BrokerID { get; set; }

        /// <summary>
        /// 投资者代码
        /// </summary>
        public string InvestorID { get; set; }

        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID { get; set; }

        /// <summary>
        /// 会话编号
        /// </summary>
        public int SessionID { get; set; }

        /// <summary>
        /// 最大报单引用
        /// </summary>
        public string MaxOrderRef { get; set; }

        private string _password;
        private string _flowPath;

        /// <summary>
        /// 类库加载类
        /// </summary>
        private LibraryWrapper _wrapper;

        /// <summary>
        /// 方法入口列表
        /// </summary>
        private List<string> _entryList = new List<string>();

        #endregion

        #region 委托定义

        delegate IntPtr DelegateGetString();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DelegateConnect(string frontAddr, string flowPath);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DelegateDisconnect();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqQryExchange(int requestID, string exchangeID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqAccount(int requestID, string brokerID, string investorID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqUser(int requestID, string brokerID, string investorID, string password);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqUserUpdate(int requestID, string brokerID, string userID, string oldPassword,
            string newPassword);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqOrderInsert(int requestID, ref CThostFtdcInputOrderField req);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqOrderAction(int requestID, ref CThostFtdcInputOrderActionField pOrder);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqQueryMaxOrderVolume(int requestID,
            ref CThostFtdcQueryMaxOrderVolumeField pMaxOrderVolume);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqQryOrder(int requestID, ref CThostFtdcQryOrderField pQryOrder);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqQryTrade(int requestID, ref CThostFtdcQryTradeField pQryTrade);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqQryInstrumentMarginRate(int requestID, string brokerID, string investorID,
            string instrumentID, TThostFtdcHedgeFlagType hedgeFlag);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqParkedOrderInsert(int requestID, ref CThostFtdcParkedOrderField pField);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqParkedOrderAction(int requestID, ref CThostFtdcParkedOrderActionField pField);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqBankAndFuture(int requestID, ref CThostFtdcReqTransferField pField);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateReqQueryBankAccountMoney(int requestID, ref CThostFtdcReqQueryAccountField pField);

        // 回调委托
        delegate void DelegateRegOnFrontConnected(FrontConnect fc);
        delegate void DelegateRegOnDisconnected(Disconnected dc);
        delegate void DelegateRegOnHeartBeatWarning(HeartBeatWarning hbw);
        delegate void DelegateRegErrRtnBankToFutureByFuture(ErrRtnBankToFutureByFuture cb);
        delegate void DelegateRegErrRtnFutureToBankByFuture(ErrRtnFutureToBankByFuture cb);
        delegate void DelegateRegErrRtnOrderAction(ErrRtnOrderAction cb);
        delegate void DelegateRegErrRtnOrderInsert(ErrRtnOrderInsert cb);
        delegate void DelegateRegErrRtnQueryBankBalanceByFuture(ErrRtnQueryBankBalanceByFuture cb);
        delegate void DelegateRegErrRtnRepealBankToFutureByFutureManual(ErrRtnRepealBankToFutureByFutureManual cb);
        delegate void DelegateRegErrRtnRepealFutureToBankByFutureManual(ErrRtnRepealFutureToBankByFutureManual cb);
        delegate void DelegateRegRspError(RspError cb);
        delegate void DelegateRegRspFromBankToFutureByFuture(RspFromBankToFutureByFuture cb);
        delegate void DelegateRegRspFromFutureToBankByFuture(RspFromFutureToBankByFuture cb);
        delegate void DelegateRegRspOrderAction(RspOrderAction cb);
        delegate void DelegateRegRspOrderInsert(RspOrderInsert cb);
        delegate void DelegateRegRspParkedOrderAction(RspParkedOrderAction cb);
        delegate void DelegateRegRspParkedOrderInsert(RspParkedOrderInsert cb);
        delegate void DelegateRegRspQryBrokerTradingAlgos(RspQryBrokerTradingAlgos cb);
        delegate void DelegateRegRspQryBrokerTradingParams(RspQryBrokerTradingParams cb);
        delegate void DelegateRegRspQryCFMMCTradingAccountKey(RspQryCFMMCTradingAccountKey cb);
        delegate void DelegateRegRspQryContractBank(RspQryContractBank cb);
        delegate void DelegateRegRspQryDepthMarketData(RspQryDepthMarketData cb);
        delegate void DelegateRegRspQryExchange(RspQryExchange cb);
        delegate void DelegateRegRspQryInstrument(RspQryInstrument cb);
        delegate void DelegateRegRspQryInstrumentCommissionRate(RspQryInstrumentCommissionRate cb);
        delegate void DelegateRegRspQryInstrumentMarginRate(RspQryInstrumentMarginRate cb);
        delegate void DelegateRegRspQryInvestor(RspQryInvestor cb);
        delegate void DelegateRegRspQryInvestorPosition(RspQryInvestorPosition cb);
        delegate void DelegateRegRspQryInvestorPositionCombineDetail(RspQryInvestorPositionCombineDetail cb);
        delegate void DelegateRegRspQryInvestorPositionDetail(RspQryInvestorPositionDetail cb);
        delegate void DelegateRegRspQryNotice(RspQryNotice cb);
        delegate void DelegateRegRspQryOrder(RspQryOrder cb);
        delegate void DelegateRegRspQryParkedOrder(RspQryParkedOrder cb);
        delegate void DelegateRegRspQryParkedOrderAction(RspQryParkedOrderAction cb);
        delegate void DelegateRegRspQrySettlementInfo(RspQrySettlementInfo cb);
        delegate void DelegateRegRspQrySettlementInfoConfirm(RspQrySettlementInfoConfirm cb);
        delegate void DelegateRegRspQryTrade(RspQryTrade cb);
        delegate void DelegateRegRspQryTradingAccount(RspQryTradingAccount cb);
        delegate void DelegateRegRspQryTradingCode(RspQryTradingCode cb);
        delegate void DelegateRegRspQryTradingNotice(RspQryTradingNotice cb);
        delegate void DelegateRegRspQryTransferBank(RspQryTransferBank cb);
        delegate void DelegateRegRspQryTransferSerial(RspQryTransferSerial cb);
        delegate void DelegateRegRspQryAccountregister(RspQryAccountregister cb);
        delegate void DelegateRegRspQueryBankAccountMoneyByFuture(RspQueryBankAccountMoneyByFuture cb);
        delegate void DelegateRegRspQueryMaxOrderVolume(RspQueryMaxOrderVolume cb);
        delegate void DelegateRegRspRemoveParkedOrder(RspRemoveParkedOrder cb);
        delegate void DelegateRegRspRemoveParkedOrderAction(RspRemoveParkedOrderAction cb);
        delegate void DelegateRegRspSettlementInfoConfirm(RspSettlementInfoConfirm cb);
        delegate void DelegateRegRspTradingAccountPasswordUpdate(RspTradingAccountPasswordUpdate cb);
        delegate void DelegateRegRspUserLogin(RspUserLogin cb);
        delegate void DelegateRegRspUserLogout(RspUserLogout cb);
        delegate void DelegateRegRspUserPasswordUpdate(RspUserPasswordUpdate cb);
        delegate void DelegateRegRtnErrorConditionalOrder(RtnErrorConditionalOrder cb);
        delegate void DelegateRegRtnFromBankToFutureByBank(RtnFromBankToFutureByBank cb);
        delegate void DelegateRegRtnFromBankToFutureByFuture(RtnFromBankToFutureByFuture cb);
        delegate void DelegateRegRtnFromFutureToBankByBank(RtnFromFutureToBankByBank cb);
        delegate void DelegateRegRtnFromFutureToBankByFuture(RtnFromFutureToBankByFuture cb);
        delegate void DelegateRegRtnInstrumentStatus(RtnInstrumentStatus cb);
        delegate void DelegateRegRtnOrder(RtnOrder cb);
        delegate void DelegateRegRtnQueryBankBalanceByFuture(RtnQueryBankBalanceByFuture cb);
        delegate void DelegateRegRtnRepealFromBankToFutureByBank(RtnRepealFromBankToFutureByBank cb);
        delegate void DelegateRegRtnRepealFromBankToFutureByFuture(RtnRepealFromBankToFutureByFuture cb);
        delegate void DelegateRegRtnRepealFromBankToFutureByFutureManual(RtnRepealFromBankToFutureByFutureManual cb);
        delegate void DelegateRegRtnRepealFromFutureToBankByBank(RtnRepealFromFutureToBankByBank cb);
        delegate void DelegateRegRtnRepealFromFutureToBankByFuture(RtnRepealFromFutureToBankByFuture cb);
        delegate void DelegateRegRtnRepealFromFutureToBankByFutureManual(RtnRepealFromFutureToBankByFutureManual cb);
        delegate void DelegateRegRtnTrade(RtnTrade cb);
        delegate void DelegateRegRtnTradingNotice(RtnTradingNotice cb);

        DelegateGetString getApiVersion;
        DelegateConnect connect;
        DelegateDisconnect disconnect;
        DelegateGetString getTradingDay;
        DelegateReqUser reqUserLogin;
        DelegateReqAccount reqUserLogout;
        DelegateReqUserUpdate reqUserPasswordUpdate;
        DelegateReqUserUpdate reqTradingAccountPasswordUpdate;
        DelegateReqOrderInsert reqOrderInsert;
        DelegateReqOrderAction reqOrderAction;
        DelegateReqQueryMaxOrderVolume reqQueryMaxOrderVolume;
        DelegateReqAccount reqSettlementInfoConfirm;
        DelegateReqQryOrder reqQryOrder;
        DelegateReqQryTrade reqQryTrade;
        DelegateReqUser reqQryInvestorPosition;
        DelegateReqAccount reqQryTradingAccount;
        DelegateReqAccount reqQryInvestor;
        DelegateReqUserUpdate reqQryTradingCode;
        DelegateReqQryInstrumentMarginRate reqQryInstrumentMarginRate;
        DelegateReqUser reqQryInstrumentCommissionRate;
        DelegateReqQryExchange reqQryExchange;
        DelegateReqQryExchange reqQryInstrument;
        DelegateReqQryExchange reqQryDepthMarketData;
        DelegateReqUser reqQrySettlementInfo;
        DelegateReqUser reqQryInvestorPositionDetail;
        DelegateReqQryExchange reqQryNotice;
        DelegateReqAccount reqQrySettlementInfoConfirm;
        DelegateReqUser reqQryInvestorPositionCombineDetail;
        DelegateReqAccount reqQryCFMMCTradingAccountKey;
        DelegateReqAccount reqQryTradingNotice;
        DelegateReqAccount reqQryBrokerTradingParams;
        DelegateReqUser reqQryBrokerTradingAlgos;
        DelegateReqParkedOrderInsert reqParkedOrderInsert;
        DelegateReqParkedOrderAction reqParkedOrderAction;
        DelegateReqUser reqRemoveParkedOrder;
        DelegateReqUser reqRemoveParkedOrderAction;
        DelegateReqAccount reqQryTransferBank;
        DelegateReqUser reqQryTransferSerial;
        DelegateReqUser reqQryAccountregister;
        DelegateReqUser reqQryContractBank;
        DelegateReqUserUpdate reqQryParkedOrder;
        DelegateReqUserUpdate reqQryParkedOrderAction;
        DelegateReqBankAndFuture reqFromBankToFutureByFuture;
        DelegateReqBankAndFuture reqFromFutureToBankByFuture;
        DelegateReqQueryBankAccountMoney reqQueryBankAccountMoneyByFuture;

        DelegateRegOnFrontConnected regOnFrontConnected;
        DelegateRegOnDisconnected regOnDisConnected;
        DelegateRegOnHeartBeatWarning regOnHeartBeatWarning;
        DelegateRegErrRtnBankToFutureByFuture regErrRtnBankToFutureByFuture;
        DelegateRegErrRtnFutureToBankByFuture regErrRtnFutureToBankByFuture;
        DelegateRegErrRtnOrderAction regErrRtnOrderAction;
        DelegateRegErrRtnOrderInsert regErrRtnOrderInsert;
        DelegateRegErrRtnQueryBankBalanceByFuture regErrRtnQueryBankBalanceByFuture;
        DelegateRegErrRtnRepealBankToFutureByFutureManual regErrRtnRepealBankToFutureByFutureManual;
        DelegateRegErrRtnRepealFutureToBankByFutureManual regErrRtnRepealFutureToBankByFutureManual;
        DelegateRegRspError regRspError;
        DelegateRegRspFromBankToFutureByFuture regRspFromBankToFutureByFuture;
        DelegateRegRspFromFutureToBankByFuture regRspFromFutureToBankByFuture;
        DelegateRegRspOrderAction regRspOrderAction;
        DelegateRegRspOrderInsert regRspOrderInsert;
        DelegateRegRspParkedOrderAction regRspParkedOrderAction;
        DelegateRegRspParkedOrderInsert regRspParkedOrderInsert;
        DelegateRegRspQryBrokerTradingAlgos regRspQryBrokerTradingAlgos;
        DelegateRegRspQryBrokerTradingParams regRspQryBrokerTradingParams;
        DelegateRegRspQryCFMMCTradingAccountKey regRspQryCFMMCTradingAccountKey;
        DelegateRegRspQryContractBank regRspQryContractBank;
        DelegateRegRspQryDepthMarketData regRspQryDepthMarketData;
        DelegateRegRspQryExchange regRspQryExchange;
        DelegateRegRspQryInstrument regRspQryInstrument;
        DelegateRegRspQryInstrumentCommissionRate regRspQryInstrumentCommissionRate;
        DelegateRegRspQryInstrumentMarginRate regRspQryInstrumentMarginRate;
        DelegateRegRspQryInvestor regRspQryInvestor;
        DelegateRegRspQryInvestorPosition regRspQryInvestorPosition;
        DelegateRegRspQryInvestorPositionCombineDetail regRspQryInvestorPositionCombineDetail;
        DelegateRegRspQryInvestorPositionDetail regRspQryInvestorPositionDetail;
        DelegateRegRspQryNotice regRspQryNotice;
        DelegateRegRspQryOrder regRspQryOrder;
        DelegateRegRspQryParkedOrder regRspQryParkedOrder;
        DelegateRegRspQryParkedOrderAction regRspQryParkedOrderAction;
        DelegateRegRspQrySettlementInfo regRspQrySettlementInfo;
        DelegateRegRspQrySettlementInfoConfirm regRspQrySettlementInfoConfirm;
        DelegateRegRspQryTrade regRspQryTrade;
        DelegateRegRspQryTradingAccount regRspQryTradingAccount;
        DelegateRegRspQryTradingCode regRspQryTradingCode;
        DelegateRegRspQryTradingNotice regRspQryTradingNotice;
        DelegateRegRspQryTransferBank regRspQryTransferBank;
        DelegateRegRspQryTransferSerial regRspQryTransferSerial;
        DelegateRegRspQryAccountregister regRspQryAccountregister;
        DelegateRegRspQueryBankAccountMoneyByFuture regRspQueryBankAccountMoneyByFuture;
        DelegateRegRspQueryMaxOrderVolume regRspQueryMaxOrderVolume;
        DelegateRegRspRemoveParkedOrder regRspRemoveParkedOrder;
        DelegateRegRspRemoveParkedOrderAction regRspRemoveParkedOrderAction;
        DelegateRegRspSettlementInfoConfirm regRspSettlementInfoConfirm;
        DelegateRegRspTradingAccountPasswordUpdate regRspTradingAccountPasswordUpdate;
        DelegateRegRspUserLogin regRspUserLogin;
        DelegateRegRspUserLogout regRspUserLogout;
        DelegateRegRspUserPasswordUpdate regRspUserPasswordUpdate;
        DelegateRegRtnErrorConditionalOrder regRtnErrorConditionalOrder;
        DelegateRegRtnFromBankToFutureByBank regRtnFromBankToFutureByBank;
        DelegateRegRtnFromBankToFutureByFuture regRtnFromBankToFutureByFuture;
        DelegateRegRtnFromFutureToBankByBank regRtnFromFutureToBankByBank;
        DelegateRegRtnFromFutureToBankByFuture regRtnFromFutureToBankByFuture;
        DelegateRegRtnInstrumentStatus regRtnInstrumentStatus;
        DelegateRegRtnOrder regRtnOrder;
        DelegateRegRtnQueryBankBalanceByFuture regRtnQueryBankBalanceByFuture;
        DelegateRegRtnRepealFromBankToFutureByBank regRtnRepealFromBankToFutureByBank;
        DelegateRegRtnRepealFromBankToFutureByFuture regRtnRepealFromBankToFutureByFuture;
        DelegateRegRtnRepealFromBankToFutureByFutureManual regRtnRepealFromBankToFutureByFutureManual;
        DelegateRegRtnRepealFromFutureToBankByBank regRtnRepealFromFutureToBankByBank;
        DelegateRegRtnRepealFromFutureToBankByFuture regRtnRepealFromFutureToBankByFuture;
        DelegateRegRtnRepealFromFutureToBankByFutureManual regRtnRepealFromFutureToBankByFutureManual;
        DelegateRegRtnTrade regRtnTrade;
        DelegateRegRtnTradingNotice regRtnTradingNotice;

        #endregion

        /// <summary>
        /// TradeApi.dll,CTPTradeApi.dll,thosttraderapi.dll 放在主程序的执行文件夹中
        /// </summary>
        /// <param name="brokerID">经纪公司代码</param>
        /// <param name="frontAddr">前置地址，tcp://IP:Port</param>
        /// <param name="flowPath">存储订阅信息文件的目录，默认为当前目录</param>
        public TradeApi(string brokerID = "", string frontAddr = "", string flowPath = "")
        {
            this.FrontAddr = frontAddr;
            this.BrokerID = brokerID;
            this._flowPath = flowPath;

            try
            {
                string path = Path.GetFullPath(string.Format("{0}\\{1}", LibraryWrapper.ProcessorArchitecture,
                    dllName));
                _wrapper = new LibraryWrapper(path, "thosttraderapi.dll");

                #region 读取方法入口列表

                string resourceName = string.Format("CTPTradeApi.Entry{0}.txt", LibraryWrapper.IsAmd64 ? "64" : "32");
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream(resourceName);
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string text = reader.ReadToEnd();
                        string entryStart = "ordinal hint";
                        int start = text.IndexOf(entryStart);
                        if (start > -1)
                        {
                            string[] arr = text.Substring(start).Split('\n');
                            if (arr.Length > 1)
                            {
                                for (int i = 1; i < arr.Length; i++)
                                {
                                    string[] list = arr[i].Split(new char[] { ' ', '\t' },
                                        StringSplitOptions.RemoveEmptyEntries);
                                    if (list.Length > 4)
                                    {
                                        _entryList.Add(list[3]);
                                    }
                                }
                            }
                        }
                    }
                    stream.Close();
                }
                if (_entryList.Count == 0)
                {
                    throw new Exception(string.Format("Cannot find entry point form resource {0}", resourceName));
                }

                #endregion

                #region 获取非托管方法

                getApiVersion = GetDelegate<DelegateGetString>("?GetApiVersion");
                getTradingDay = GetDelegate<DelegateGetString>("?GetTradingDay");
                connect = GetDelegate<DelegateConnect>("?Connect");
                disconnect = GetDelegate<DelegateDisconnect>("?DisConnect");
                reqUserLogin = GetDelegate<DelegateReqUser>("?ReqUserLogin");
                reqUserLogout = GetDelegate<DelegateReqAccount>("?ReqUserLogout");
                reqUserPasswordUpdate = GetDelegate<DelegateReqUserUpdate>("?ReqUserPasswordUpdate");
                reqTradingAccountPasswordUpdate = GetDelegate<DelegateReqUserUpdate>("?ReqTradingAccountPasswordUpdate");
                reqOrderInsert = GetDelegate<DelegateReqOrderInsert>("?ReqOrderInsert");
                reqOrderAction = GetDelegate<DelegateReqOrderAction>("?ReqOrderAction");
                reqQueryMaxOrderVolume = GetDelegate<DelegateReqQueryMaxOrderVolume>("?ReqQueryMaxOrderVolume");
                reqSettlementInfoConfirm = GetDelegate<DelegateReqAccount>("?ReqSettlementInfoConfirm");
                reqQryOrder = GetDelegate<DelegateReqQryOrder>("?ReqQryOrder");
                reqQryTrade = GetDelegate<DelegateReqQryTrade>("?ReqQryTrade");
                reqQryInvestorPosition = GetDelegate<DelegateReqUser>("?ReqQryInvestorPosition");
                reqQryTradingAccount = GetDelegate<DelegateReqAccount>("?ReqQryTradingAccount");
                reqQryInvestor = GetDelegate<DelegateReqAccount>("?ReqQryInvestor");
                reqQryTradingCode = GetDelegate<DelegateReqUserUpdate>("?ReqQryTradingCode");
                reqQryInstrumentMarginRate = GetDelegate<DelegateReqQryInstrumentMarginRate>("?ReqQryInstrumentMarginRate");
                reqQryInstrumentCommissionRate = GetDelegate<DelegateReqUser>("?ReqQryInstrumentCommissionRate");
                reqQryExchange = GetDelegate<DelegateReqQryExchange>("?ReqQryExchange");
                reqQryInstrument = GetDelegate<DelegateReqQryExchange>("?ReqQryInstrument");
                reqQryDepthMarketData = GetDelegate<DelegateReqQryExchange>("?ReqQryDepthMarketData");
                reqQrySettlementInfo = GetDelegate<DelegateReqUser>("?ReqQrySettlementInfo");
                reqQryInvestorPositionDetail = GetDelegate<DelegateReqUser>("?ReqQryInvestorPositionDetail");
                reqQryNotice = GetDelegate<DelegateReqQryExchange>("?ReqQryNotice");
                reqQrySettlementInfoConfirm = GetDelegate<DelegateReqAccount>("?ReqQrySettlementInfoConfirm");
                reqQryInvestorPositionCombineDetail = GetDelegate<DelegateReqUser>("?ReqQryInvestorPositionCombineDetail");
                reqQryCFMMCTradingAccountKey = GetDelegate<DelegateReqAccount>("?ReqQryCFMMCTradingAccountKey");
                reqQryTradingNotice = GetDelegate<DelegateReqAccount>("?ReqQryTradingNotice");
                reqQryBrokerTradingParams = GetDelegate<DelegateReqAccount>("?ReqQryBrokerTradingParams");
                reqQryBrokerTradingAlgos = GetDelegate<DelegateReqUser>("?ReqQryBrokerTradingAlgos");
                reqParkedOrderInsert = GetDelegate<DelegateReqParkedOrderInsert>("?ReqParkedOrderInsert");
                reqParkedOrderAction = GetDelegate<DelegateReqParkedOrderAction>("?ReqParkedOrderAction");
                reqRemoveParkedOrder = GetDelegate<DelegateReqUser>("?ReqRemoveParkedOrder");
                reqRemoveParkedOrderAction = GetDelegate<DelegateReqUser>("?ReqRemoveParkedOrderAction");
                reqQryTransferBank = GetDelegate<DelegateReqAccount>("?ReqQryTransferBank");
                reqQryTransferSerial = GetDelegate<DelegateReqUser>("?ReqQryTransferSerial");
                reqQryAccountregister = GetDelegate<DelegateReqUser>("?ReqQryAccountregister");
                reqQryContractBank = GetDelegate<DelegateReqUser>("?ReqQryContractBank");
                reqQryParkedOrder = GetDelegate<DelegateReqUserUpdate>("?ReqQryParkedOrder");
                reqQryParkedOrderAction = GetDelegate<DelegateReqUserUpdate>("?ReqQryParkedOrderAction");
                reqFromBankToFutureByFuture = GetDelegate<DelegateReqBankAndFuture>("?ReqFromBankToFutureByFuture");
                reqFromFutureToBankByFuture = GetDelegate<DelegateReqBankAndFuture>("?ReqFromFutureToBankByFuture");
                reqQueryBankAccountMoneyByFuture = GetDelegate<DelegateReqQueryBankAccountMoney>("?ReqQueryBankAccountMoneyByFuture");

                regOnFrontConnected = GetDelegate<DelegateRegOnFrontConnected>("?RegOnFrontConnected");
                regOnDisConnected = GetDelegate<DelegateRegOnDisconnected>("?RegOnFrontDisconnected");
                regOnHeartBeatWarning = GetDelegate<DelegateRegOnHeartBeatWarning>("?RegOnHeartBeatWarning");
                regErrRtnBankToFutureByFuture = GetDelegate<DelegateRegErrRtnBankToFutureByFuture>("?RegErrRtnBankToFutureByFuture");
                regErrRtnFutureToBankByFuture = GetDelegate<DelegateRegErrRtnFutureToBankByFuture>("?RegErrRtnFutureToBankByFuture");
                regErrRtnOrderAction = GetDelegate<DelegateRegErrRtnOrderAction>("?RegErrRtnOrderAction");
                regErrRtnOrderInsert = GetDelegate<DelegateRegErrRtnOrderInsert>("?RegErrRtnOrderInsert");
                regErrRtnQueryBankBalanceByFuture = GetDelegate<DelegateRegErrRtnQueryBankBalanceByFuture>("?RegErrRtnQueryBankBalanceByFuture");
                regErrRtnRepealBankToFutureByFutureManual = GetDelegate<DelegateRegErrRtnRepealBankToFutureByFutureManual>("?RegErrRtnRepealBankToFutureByFutureManual");
                regErrRtnRepealFutureToBankByFutureManual = GetDelegate<DelegateRegErrRtnRepealFutureToBankByFutureManual>("?RegErrRtnRepealFutureToBankByFutureManual");
                regRspError = GetDelegate<DelegateRegRspError>("?RegRspError");
                regRspFromBankToFutureByFuture = GetDelegate<DelegateRegRspFromBankToFutureByFuture>("?RegRspFromBankToFutureByFuture");
                regRspFromFutureToBankByFuture = GetDelegate<DelegateRegRspFromFutureToBankByFuture>("?RegRspFromFutureToBankByFuture");
                regRspOrderAction = GetDelegate<DelegateRegRspOrderAction>("?RegRspOrderAction");
                regRspOrderInsert = GetDelegate<DelegateRegRspOrderInsert>("?RegRspOrderInsert");
                regRspParkedOrderAction = GetDelegate<DelegateRegRspParkedOrderAction>("?RegRspParkedOrderAction");
                regRspParkedOrderInsert = GetDelegate<DelegateRegRspParkedOrderInsert>("?RegRspParkedOrderInsert");
                regRspQryBrokerTradingAlgos = GetDelegate<DelegateRegRspQryBrokerTradingAlgos>("?RegRspQryBrokerTradingAlgos");
                regRspQryBrokerTradingParams = GetDelegate<DelegateRegRspQryBrokerTradingParams>("?RegRspQryBrokerTradingParams");
                regRspQryCFMMCTradingAccountKey = GetDelegate<DelegateRegRspQryCFMMCTradingAccountKey>("?RegRspQryCFMMCTradingAccountKey");
                regRspQryContractBank = GetDelegate<DelegateRegRspQryContractBank>("?RegRspQryContractBank");
                regRspQryDepthMarketData = GetDelegate<DelegateRegRspQryDepthMarketData>("?RegRspQryDepthMarketData");
                regRspQryExchange = GetDelegate<DelegateRegRspQryExchange>("?RegRspQryExchange");
                regRspQryInstrument = GetDelegate<DelegateRegRspQryInstrument>("?RegRspQryInstrument");
                regRspQryInstrumentCommissionRate = GetDelegate<DelegateRegRspQryInstrumentCommissionRate>("?RegRspQryInstrumentCommissionRate");
                regRspQryInstrumentMarginRate = GetDelegate<DelegateRegRspQryInstrumentMarginRate>("?RegRspQryInstrumentMarginRate");
                regRspQryInvestor = GetDelegate<DelegateRegRspQryInvestor>("?RegRspQryInvestor");
                regRspQryInvestorPosition = GetDelegate<DelegateRegRspQryInvestorPosition>("?RegRspQryInvestorPosition");
                regRspQryInvestorPositionCombineDetail = GetDelegate<DelegateRegRspQryInvestorPositionCombineDetail>("?RegRspQryInvestorPositionCombineDetail");
                regRspQryInvestorPositionDetail = GetDelegate<DelegateRegRspQryInvestorPositionDetail>("?RegRspQryInvestorPositionDetail");
                regRspQryNotice = GetDelegate<DelegateRegRspQryNotice>("?RegRspQryNotice");
                regRspQryOrder = GetDelegate<DelegateRegRspQryOrder>("?RegRspQryOrder");
                regRspQryParkedOrder = GetDelegate<DelegateRegRspQryParkedOrder>("?RegRspQryParkedOrder");
                regRspQryParkedOrderAction = GetDelegate<DelegateRegRspQryParkedOrderAction>("?RegRspQryParkedOrderAction");
                regRspQrySettlementInfo = GetDelegate<DelegateRegRspQrySettlementInfo>("?RegRspQrySettlementInfo");
                regRspQrySettlementInfoConfirm = GetDelegate<DelegateRegRspQrySettlementInfoConfirm>("?RegRspQrySettlementInfoConfirm");
                regRspQryTrade = GetDelegate<DelegateRegRspQryTrade>("?RegRspQryTrade");
                regRspQryTradingAccount = GetDelegate<DelegateRegRspQryTradingAccount>("?RegRspQryTradingAccount");
                regRspQryTradingCode = GetDelegate<DelegateRegRspQryTradingCode>("?RegRspQryTradingCode");
                regRspQryTradingNotice = GetDelegate<DelegateRegRspQryTradingNotice>("?RegRspQryTradingNotice");
                regRspQryTransferBank = GetDelegate<DelegateRegRspQryTransferBank>("?RegRspQryTransferBank");
                regRspQryTransferSerial = GetDelegate<DelegateRegRspQryTransferSerial>("?RegRspQryTransferSerial");
                regRspQryAccountregister = GetDelegate<DelegateRegRspQryAccountregister>("?RegRspQryAccountregister");
                regRspQueryBankAccountMoneyByFuture = GetDelegate<DelegateRegRspQueryBankAccountMoneyByFuture>("?RegRspQueryBankAccountMoneyByFuture");
                regRspQueryMaxOrderVolume = GetDelegate<DelegateRegRspQueryMaxOrderVolume>("?RegRspQueryMaxOrderVolume");
                regRspRemoveParkedOrder = GetDelegate<DelegateRegRspRemoveParkedOrder>("?RegRspRemoveParkedOrder");
                regRspRemoveParkedOrderAction = GetDelegate<DelegateRegRspRemoveParkedOrderAction>("?RegRspRemoveParkedOrderAction");
                regRspSettlementInfoConfirm = GetDelegate<DelegateRegRspSettlementInfoConfirm>("?RegRspSettlementInfoConfirm");
                regRspTradingAccountPasswordUpdate = GetDelegate<DelegateRegRspTradingAccountPasswordUpdate>("?RegRspTradingAccountPasswordUpdate");
                regRspUserLogin = GetDelegate<DelegateRegRspUserLogin>("?RegRspUserLogin");
                regRspUserLogout = GetDelegate<DelegateRegRspUserLogout>("?RegRspUserLogout");
                regRspUserPasswordUpdate = GetDelegate<DelegateRegRspUserPasswordUpdate>("?RegRspUserPasswordUpdate");
                regRtnErrorConditionalOrder = GetDelegate<DelegateRegRtnErrorConditionalOrder>("?RegRtnErrorConditionalOrder");
                regRtnFromBankToFutureByBank = GetDelegate<DelegateRegRtnFromBankToFutureByBank>("?RegRtnFromBankToFutureByBank");
                regRtnFromBankToFutureByFuture = GetDelegate<DelegateRegRtnFromBankToFutureByFuture>("?RegRtnFromBankToFutureByFuture");
                regRtnFromFutureToBankByBank = GetDelegate<DelegateRegRtnFromFutureToBankByBank>("?RegRtnFromFutureToBankByBank");
                regRtnFromFutureToBankByFuture = GetDelegate<DelegateRegRtnFromFutureToBankByFuture>("?RegRtnFromFutureToBankByFuture");
                regRtnInstrumentStatus = GetDelegate<DelegateRegRtnInstrumentStatus>("?RegRtnInstrumentStatus");
                regRtnOrder = GetDelegate<DelegateRegRtnOrder>("?RegRtnOrder");
                regRtnQueryBankBalanceByFuture = GetDelegate<DelegateRegRtnQueryBankBalanceByFuture>("?RegRtnQueryBankBalanceByFuture");
                regRtnRepealFromBankToFutureByBank = GetDelegate<DelegateRegRtnRepealFromBankToFutureByBank>("?RegRtnRepealFromBankToFutureByBank");
                regRtnRepealFromBankToFutureByFuture = GetDelegate<DelegateRegRtnRepealFromBankToFutureByFuture>("?RegRtnRepealFromBankToFutureByFuture");
                regRtnRepealFromBankToFutureByFutureManual = GetDelegate<DelegateRegRtnRepealFromBankToFutureByFutureManual>("?RegRtnRepealFromBankToFutureByFutureManual");
                regRtnRepealFromFutureToBankByBank = GetDelegate<DelegateRegRtnRepealFromFutureToBankByBank>("?RegRtnRepealFromFutureToBankByBank");
                regRtnRepealFromFutureToBankByFuture = GetDelegate<DelegateRegRtnRepealFromFutureToBankByFuture>("?RegRtnRepealFromFutureToBankByFuture");
                regRtnRepealFromFutureToBankByFutureManual = GetDelegate<DelegateRegRtnRepealFromFutureToBankByFutureManual>("?RegRtnRepealFromFutureToBankByFutureManual");
                regRtnTrade = GetDelegate<DelegateRegRtnTrade>("?RegRtnTrade");
                regRtnTradingNotice = GetDelegate<DelegateRegRtnTradingNotice>("?RegRtnTradingNotice");

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 从列表中查找入口并返回非托管方法委托
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private T GetDelegate<T>(string name) where T : class
        {
            string entryName = _entryList.FirstOrDefault(p => p.StartsWith(name));
            if (!string.IsNullOrEmpty(entryName))
            {
                return _wrapper.GetUnmanagedFunction<T>(entryName);
            }
            throw new Exception(string.Format("Failed to get entry point for \"{0}\"", name));
        }

        /// <summary>
        /// 获取接口版本号
        /// </summary>
        /// <returns></returns>
        public string GetApiVersion()
        {
            IntPtr ptr = getApiVersion();

            return Marshal.PtrToStringAnsi(ptr);
        }

        /// <summary>
        /// 获取交易日（登录成功后调用）
        /// </summary>
        /// <returns></returns>
        public string GetTradingDay()
        {
            IntPtr ptr = getTradingDay();

            return Marshal.PtrToStringAnsi(ptr);
        }

        /// <summary>
        /// 连接
        /// </summary>
        public void Connect()
        {
            connect(this.FrontAddr, this._flowPath);
        }

        /// <summary>
        /// 断开
        /// </summary>
        public void Disconnect()
        {
            disconnect();
        }

        /// <summary>
        /// 登入请求
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public int UserLogin(int requestID, string investorID, string password)
        {
            this.InvestorID = investorID;
            this._password = password;
            return reqUserLogin(requestID, this.BrokerID, this.InvestorID, this._password);
        }

        /// <summary>
        /// 发送登出请求
        /// </summary>
        public int UserLogout(int requestID)
        {
            return reqUserLogout(requestID, this.BrokerID, this.InvestorID);
        }

        /// <summary>
        /// 更新用户口令
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="userID">投资者账号</param>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        public int UserPasswordupdate(int requestID, string userID, string oldPassword, string newPassword)
        {
            return reqUserPasswordUpdate(requestID, this.BrokerID, userID, oldPassword, newPassword);
        }

        /// <summary>
        /// 资金账户口令更新请求
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="accountID">资金账号</param>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        public int TradingAccountPasswordUpdate(int requestID, string accountID, string oldPassword, string newPassword)
        {
            return reqTradingAccountPasswordUpdate(requestID, this.BrokerID, accountID, oldPassword, newPassword);
        }

        /// <summary>
        /// 下单:录入报单
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="order">输入的报单</param>
        public int OrderInsert(int requestID, CThostFtdcInputOrderField order)
        {
            return reqOrderInsert(requestID, ref order);
        }

        /// <summary>
        /// 开平仓:限价单
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="InstrumentID">合约代码</param>
        /// <param name="OffsetFlag">平仓:仅上期所平今时使用CloseToday/其它情况均使用Close</param>
        /// <param name="Direction">买卖</param>
        /// <param name="Price">价格</param>
        /// <param name="Volume">手数</param>
        /// <param name="orderRef">报单引用</param>
        public int OrderInsert(int requestID, string InstrumentID, TThostFtdcOffsetFlagType OffsetFlag,
            TThostFtdcDirectionType Direction, double Price, int Volume, string orderRef = "")
        {
            CThostFtdcInputOrderField tmp = new CThostFtdcInputOrderField();
            tmp.BrokerID = this.BrokerID;
            tmp.BusinessUnit = null;
            tmp.ContingentCondition = TThostFtdcContingentConditionType.Immediately;
            tmp.ForceCloseReason = TThostFtdcForceCloseReasonType.NotForceClose;
            tmp.InvestorID = this.InvestorID;
            tmp.IsAutoSuspend = (int)TThostFtdcBoolType.No;
            tmp.MinVolume = 1;
            tmp.OrderPriceType = TThostFtdcOrderPriceTypeType.LimitPrice;
            tmp.OrderRef = orderRef;
            tmp.TimeCondition = TThostFtdcTimeConditionType.GFD;    //当日有效
            tmp.UserForceClose = (int)TThostFtdcBoolType.No;
            tmp.UserID = this.InvestorID;
            tmp.VolumeCondition = TThostFtdcVolumeConditionType.AV;
            tmp.CombHedgeFlag = TThostFtdcHedgeFlagType.Speculation;

            tmp.InstrumentID = InstrumentID;
            tmp.CombOffsetFlag = OffsetFlag;
            tmp.Direction = Direction;
            tmp.LimitPrice = Price;
            tmp.VolumeTotalOriginal = Volume;
            return reqOrderInsert(requestID, ref tmp);
        }

        /// <summary>
        /// 开平仓:市价单
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="InstrumentID"></param>
        /// <param name="OffsetFlag">平仓:仅上期所平今时使用CloseToday/其它情况均使用Close</param>
        /// <param name="Direction"></param>
        /// <param name="Volume"></param>
        /// <param name="orderRef">报单引用</param>
        public int OrderInsert(int requestID, string InstrumentID, TThostFtdcOffsetFlagType OffsetFlag,
            TThostFtdcDirectionType Direction, int Volume, string orderRef = "")
        {
            CThostFtdcInputOrderField tmp = new CThostFtdcInputOrderField();
            tmp.BrokerID = this.BrokerID;
            tmp.BusinessUnit = null;
            tmp.ContingentCondition = TThostFtdcContingentConditionType.Immediately;
            tmp.ForceCloseReason = TThostFtdcForceCloseReasonType.NotForceClose;
            tmp.InvestorID = this.InvestorID;
            tmp.IsAutoSuspend = (int)TThostFtdcBoolType.No;
            tmp.MinVolume = 1;
            tmp.OrderPriceType = TThostFtdcOrderPriceTypeType.AnyPrice;
            tmp.OrderRef = orderRef;
            tmp.TimeCondition = TThostFtdcTimeConditionType.IOC;    //立即完成,否则撤单
            tmp.UserForceClose = (int)TThostFtdcBoolType.No;
            tmp.UserID = this.InvestorID;
            tmp.VolumeCondition = TThostFtdcVolumeConditionType.AV;
            tmp.CombHedgeFlag = TThostFtdcHedgeFlagType.Speculation;

            tmp.InstrumentID = InstrumentID;
            tmp.CombOffsetFlag = OffsetFlag;
            tmp.Direction = Direction;
            tmp.LimitPrice = 0;
            tmp.VolumeTotalOriginal = Volume;
            return reqOrderInsert(requestID, ref tmp);
        }

        /// <summary>
        /// 开平仓:触发单
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="InstrumentID"></param>
        /// <param name="ConditionType">触发单类型</param>
        /// <param name="ConditionPrice">触发价格</param>
        /// <param name="OffsetFlag">平仓:仅上期所平今时使用CloseToday/其它情况均使用Close</param>
        /// <param name="Direction"></param>
        /// <param name="PriceType">下单类型</param>
        /// <param name="Price">下单价格:仅当下单类型为LimitPrice时有效</param>
        /// <param name="Volume"></param>
        /// <param name="orderRef">报单引用</param>
        public int OrderInsert(int requestID, string InstrumentID, TThostFtdcContingentConditionType ConditionType,
            double ConditionPrice, TThostFtdcOffsetFlagType OffsetFlag, TThostFtdcDirectionType Direction,
            TThostFtdcOrderPriceTypeType PriceType, double Price, int Volume, string orderRef = "")
        {
            CThostFtdcInputOrderField tmp = new CThostFtdcInputOrderField();
            tmp.BrokerID = this.BrokerID;
            tmp.BusinessUnit = null;
            tmp.ForceCloseReason = TThostFtdcForceCloseReasonType.NotForceClose;
            tmp.InvestorID = this.InvestorID;
            tmp.IsAutoSuspend = (int)TThostFtdcBoolType.No;
            tmp.MinVolume = 1;
            tmp.OrderRef = orderRef;
            tmp.TimeCondition = TThostFtdcTimeConditionType.GFD;
            tmp.UserForceClose = (int)TThostFtdcBoolType.No;
            tmp.UserID = this.InvestorID;
            tmp.VolumeCondition = TThostFtdcVolumeConditionType.AV;
            tmp.CombHedgeFlag = TThostFtdcHedgeFlagType.Speculation;

            tmp.InstrumentID = InstrumentID;
            tmp.CombOffsetFlag = OffsetFlag;
            tmp.Direction = Direction;
            tmp.ContingentCondition = ConditionType;    //触发类型
            tmp.StopPrice = Price;                      //触发价格
            tmp.OrderPriceType = PriceType;             //下单类型
            tmp.LimitPrice = Price;                     //下单价格:Price = LimitPrice 时有效
            tmp.VolumeTotalOriginal = Volume;
            return reqOrderInsert(requestID, ref tmp);
        }

        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="field">输入报单结构体</param>
        public int OrderAction(int requestID, CThostFtdcInputOrderActionField field)
        {
            return reqOrderAction(requestID, ref field);
        }

        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="instrumentID">合约代码</param>
        /// <param name="frontID">前置编号</param>
        /// <param name="sessionID">会话ID</param>
        /// <param name="orderRef">报单引用</param>
        /// <param name="exchangeID">交易所代码</param>
        /// <param name="orderSysID">报单编号</param>
        public int OrderAction(int requestID, string instrumentID, int frontID = 0, int sessionID = 0,
            string orderRef = "", string exchangeID = null, string orderSysID = null)
        {
            CThostFtdcInputOrderActionField tmp = new CThostFtdcInputOrderActionField();
            tmp.ActionFlag = TThostFtdcActionFlagType.Delete;
            tmp.BrokerID = this.BrokerID;
            tmp.InvestorID = this.InvestorID;
            //tmp.UserID = this.InvestorID;
            tmp.InstrumentID = instrumentID;
            //tmp.VolumeChange = int.Parse(lvi.SubItems["VolumeTotalOriginal"].Text);
            if (frontID != 0)
                tmp.FrontID = frontID;
            if (sessionID != 0)
                tmp.SessionID = sessionID;
            if (orderRef != "")
                tmp.OrderRef = orderRef;
            tmp.ExchangeID = exchangeID;
            if (orderSysID != null)
                tmp.OrderSysID = new string('\0', 21 - orderSysID.Length) + orderSysID; //OrderSysID右对齐
            return reqOrderAction(requestID, ref tmp);
        }

        /// <summary>
        /// 查询最大允许报单数量请求
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="pMaxOrderVolume">最大报单数量</param>
        public int QueryMaxOrderVolume(int requestID, CThostFtdcQueryMaxOrderVolumeField pMaxOrderVolume)
        {
            return reqQueryMaxOrderVolume(requestID, ref pMaxOrderVolume);
        }

        /// <summary>
        /// 确认结算结果
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int SettlementInfoConfirm(int requestID)
        {
            return reqSettlementInfoConfirm(requestID, this.BrokerID, this.InvestorID);
        }

        /// <summary>
        /// 请求查询报单:不填-查所有
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="exchangeID">交易所代码</param>
        /// <param name="timeStart">起始时间</param>
        /// <param name="timeEnd">结束时间</param>
        /// <param name="instrumentID">合约代码</param>
        /// <param name="orderSysID">报单编号</param>
        public int QueryOrder(int requestID, string exchangeID = null, string timeStart = null, string timeEnd = null,
            string instrumentID = null, string orderSysID = null)
        {
            CThostFtdcQryOrderField tmp = new CThostFtdcQryOrderField();
            tmp.BrokerID = this.BrokerID;
            tmp.InvestorID = this.InvestorID;
            tmp.ExchangeID = exchangeID;
            tmp.InsertTimeStart = timeStart;
            tmp.InsertTimeEnd = timeEnd;
            tmp.InstrumentID = instrumentID;
            tmp.OrderSysID = orderSysID;
            return reqQryOrder(requestID, ref tmp);
        }

        /// <summary>
        /// 请求查询成交:不填-查所有
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="timeStart"></param>
        /// <param name="timeEnd"></param>
        /// <param name="instrumentID"></param>
        /// <param name="exchangeID"></param>
        /// <param name="tradeID"></param>
        /// <returns></returns>
        public int QueryTrade(int requestID, string timeStart = null, string timeEnd = null,
            string instrumentID = null, string exchangeID = null, string tradeID = null)
        {
            CThostFtdcQryTradeField tmp = new CThostFtdcQryTradeField();
            tmp.BrokerID = this.BrokerID;
            tmp.InvestorID = this.InvestorID;
            tmp.ExchangeID = exchangeID;
            tmp.TradeTimeStart = timeStart;
            tmp.TradeTimeEnd = timeEnd;
            tmp.InstrumentID = instrumentID;
            tmp.TradeID = tradeID;
            return reqQryTrade(requestID, ref tmp);
        }

        /// <summary>
        /// 查询投资者持仓
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="instrument">合约代码:不填-查所有</param>
        public int QueryInvestorPosition(int requestID, string instrument = null)
        {
            return reqQryInvestorPosition(requestID, this.BrokerID, this.InvestorID, instrument);
        }

        /// <summary>
        /// 查询帐户资金请求
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryTradingAccount(int requestID)
        {
            return reqQryTradingAccount(requestID, this.BrokerID, this.InvestorID);
        }

        /// <summary>
        /// 请求查询投资者
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryInvestor(int requestID)
        {
            return reqQryInvestor(requestID, this.BrokerID, this.InvestorID);
        }

        /// <summary>
        /// 请求查询交易编码:参数不填-查所有
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="clientID">客户ID</param>
        /// <param name="exchangeID">交易所ID</param>
        /// <returns></returns>
        public int QueryTradingCode(int requestID, string clientID = null, string exchangeID = null)
        {
            return reqQryTradingCode(requestID, this.BrokerID, this.InvestorID, clientID, exchangeID);
        }

        /// <summary>
        /// 请求查询合约保证金率:能为null;每次只能查一个合约
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="hedgeFlag">投机/套保</param>
        /// <param name="instrumentID">合约代码:不填-查所有</param>
        /// <returns></returns>
        public int QueryInstrumentMarginRate(int requestID, string instrumentID,
            TThostFtdcHedgeFlagType hedgeFlag = TThostFtdcHedgeFlagType.Speculation)
        {
            return reqQryInstrumentMarginRate(requestID, this.BrokerID, this.InvestorID, instrumentID, hedgeFlag);
        }

        /// <summary>
        /// 请求查询合约手续费率
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="instrumentID">合约代码</param>
        /// <returns></returns>
        public int QueryInstrumentCommissionRate(int requestID, string instrumentID)
        {
            return reqQryInstrumentCommissionRate(requestID, this.BrokerID, this.InvestorID, instrumentID);
        }

        /// <summary>
        /// 请求查询交易所
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="exchangeID"></param>
        /// <returns></returns>
        public int QueryExchange(int requestID, string exchangeID)
        {
            return reqQryExchange(requestID, exchangeID);
        }

        /// <summary>
        /// 查询合约
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="instrumentID">合约代码:不填-查所有</param>
        /// <returns></returns>
        public int QueryInstrument(int requestID, string instrumentID = null)
        {
            return reqQryInstrument(requestID, instrumentID);
        }

        /// <summary>
        /// 查询行情
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="instrumentID">合约代码</param>
        /// <returns></returns>
        public int QryDepthMarketData(int requestID, string instrumentID)
        {
            return reqQryDepthMarketData(requestID, instrumentID);
        }

        /// <summary>
        /// 请求查询投资者结算结果
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="date">查询日期，格式yyyyMMdd</param>
        /// <returns></returns>
        public int QuerySettlementInfo(int requestID, string date = null)
        {
            return reqQrySettlementInfo(requestID, this.BrokerID, this.InvestorID, date);
        }

        /// <summary>
        /// 查询投资者持仓明细
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="instrumentID">合约代码:不填-查所有</param>
        /// <returns></returns>
        public int QueryInvestorPositionDetail(int requestID, string instrumentID = null)
        {
            return reqQryInvestorPositionDetail(requestID, this.BrokerID, this.InvestorID, instrumentID);
        }

        /// <summary>
        /// 请求查询客户通知
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryNotice(int requestID)
        {
            return reqQryNotice(requestID, this.BrokerID);
        }

        /// <summary>
        /// 请求查询结算信息确认
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QuerySettlementInfoConfirm(int requestID)
        {
            return reqQrySettlementInfoConfirm(requestID, this.BrokerID, this.InvestorID);
        }

        /// <summary>
        /// 请求查询**组合**持仓明细
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="instrumentID">合约代码:不填-查所有</param>
        /// <returns></returns>
        public int QueryInvestorPositionCombinaDetail(int requestID, string instrumentID = null)
        {
            return reqQryInvestorPositionCombineDetail(requestID, this.BrokerID, this.InvestorID, instrumentID);
        }

        /// <summary>
        /// 请求查询保证金监管系统经纪公司资金账户密钥
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryCFMMCTradingAccountKey(int requestID)
        {
            return reqQryCFMMCTradingAccountKey(requestID, this.BrokerID, this.InvestorID);
        }

        /// <summary>
        /// 请求查询交易通知
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryTradingNotice(int requestID)
        {
            return reqQryTradingNotice(requestID, this.BrokerID, this.InvestorID);
        }

        /// <summary>
        /// 请求查询经纪公司交易参数
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryBrokerTradingParams(int requestID)
        {
            return reqQryBrokerTradingParams(requestID, this.BrokerID, this.InvestorID);
        }

        /// <summary>
        /// 请求查询经纪公司交易算法
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="exchangeID">交易所代码</param>
        /// <param name="instrumentID">合约代码</param>
        /// <returns></returns>
        public int QueryBrokerTradingAlgos(int requestID, string exchangeID, string instrumentID)
        {
            return reqQryBrokerTradingAlgos(requestID, this.BrokerID, exchangeID, instrumentID);
        }

        /// <summary>
        /// 预埋单录入请求
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="field"></param>
        /// <returns></returns>
        public int ParkedOrderInsert(int requestID, CThostFtdcParkedOrderField field)
        {
            return reqParkedOrderInsert(requestID, ref field);
        }

        /// <summary>
        /// 预埋单录入请求
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="InstrumentID"></param>
        /// <param name="OffsetFlag"></param>
        /// <param name="Direction"></param>
        /// <param name="Price"></param>
        /// <param name="Volume"></param>
        /// <param name="orderRef"></param>
        /// <returns></returns>
        public int ParkedOrderInsert(int requestID, string InstrumentID, TThostFtdcOffsetFlagType OffsetFlag,
            TThostFtdcDirectionType Direction, double Price, int Volume, string orderRef = "")
        {
            CThostFtdcParkedOrderField tmp = new CThostFtdcParkedOrderField();
            tmp.BrokerID = this.BrokerID;
            tmp.BusinessUnit = null;
            tmp.ContingentCondition = TThostFtdcContingentConditionType.ParkedOrder;
            tmp.ForceCloseReason = TThostFtdcForceCloseReasonType.NotForceClose;
            tmp.InvestorID = this.InvestorID;
            tmp.IsAutoSuspend = (int)TThostFtdcBoolType.No;
            tmp.MinVolume = 1;
            tmp.OrderPriceType = TThostFtdcOrderPriceTypeType.LimitPrice;
            tmp.OrderRef = orderRef;
            tmp.TimeCondition = TThostFtdcTimeConditionType.GFD;
            tmp.UserForceClose = (int)TThostFtdcBoolType.No;
            tmp.UserID = this.InvestorID;
            tmp.VolumeCondition = TThostFtdcVolumeConditionType.AV;
            tmp.CombHedgeFlag = TThostFtdcHedgeFlagType.Speculation;

            tmp.InstrumentID = InstrumentID;
            tmp.CombOffsetFlag = OffsetFlag;
            tmp.Direction = Direction;
            tmp.LimitPrice = Price;
            tmp.VolumeTotalOriginal = Volume;
            return reqParkedOrderInsert(requestID, ref tmp);
        }

        /// <summary>
        /// 预埋撤单录入请求
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="field"></param>
        /// <returns></returns>
        public int ParkedOrderAction(int requestID, CThostFtdcParkedOrderActionField field)
        {
            return reqParkedOrderAction(requestID, ref field);
        }

        /// <summary>
        /// 预埋撤单录入请求
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="InstrumentID"></param>
        /// <param name="FrontID"></param>
        /// <param name="SessionID"></param>
        /// <param name="OrderRef"></param>
        /// <param name="ExchangeID"></param>
        /// <param name="OrderSysID"></param>
        /// <returns></returns>
		public int ParkedOrderAction(int requestID, string InstrumentID, int FrontID, int SessionID, string OrderRef,
            string ExchangeID = null, string OrderSysID = null)
        {
            CThostFtdcParkedOrderActionField tmp = new CThostFtdcParkedOrderActionField();
            tmp.ActionFlag = TThostFtdcActionFlagType.Delete;
            tmp.BrokerID = this.BrokerID;
            tmp.InvestorID = this.InvestorID;
            //tmp.UserID = this.InvestorID;
            tmp.InstrumentID = InstrumentID;
            //tmp.VolumeChange = int.Parse(lvi.SubItems["VolumeTotalOriginal"].Text);

            tmp.FrontID = FrontID;
            tmp.SessionID = SessionID;
            tmp.OrderRef = OrderRef;
            tmp.ExchangeID = ExchangeID;
            if (OrderSysID != null)
                tmp.OrderSysID = new string('\0', 21 - OrderSysID.Length) + OrderSysID; //OrderSysID右对齐
            return reqParkedOrderAction(requestID, ref tmp);
        }

        /// <summary>
        /// 请求删除预埋单
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="parkedOrderID">预埋单编号</param>
        /// <returns></returns>
        public int RemoveParkedOrder(int requestID, string parkedOrderID)
        {
            return reqRemoveParkedOrder(requestID, this.BrokerID, this.InvestorID, parkedOrderID);
        }

        /// <summary>
        /// 请求删除预埋撤单
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="parkedOrderActionID">预埋撤单编号</param>
        /// <returns></returns>
        public int RemoveParkedOrderAction(int requestID, string parkedOrderActionID)
        {
            return reqRemoveParkedOrderAction(requestID, this.BrokerID, this.InvestorID, parkedOrderActionID);
        }

        /// <summary>
        /// 请求查询转帐银行
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="bankID">银行编号</param>
        /// <param name="bankBranchID">银行分支编号</param>
        /// <returns></returns>
        public int QueryTransferBank(int requestID, string bankID, string bankBranchID)
        {
            return reqQryTransferBank(requestID, bankID, bankBranchID);
        }

        /// <summary>
        /// 请求查询转帐流水
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="bankID"></param>
        /// <returns></returns>
        public int QueryTransferSerial(int requestID, string bankID)
        {
            return reqQryTransferSerial(requestID, this.BrokerID, this.InvestorID, bankID);
        }

        /// <summary>
        /// 请求查询银期签约关系
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="bankID"></param>
        /// <returns></returns>
        public int QueryAccountregister(int requestID, string bankID)
        {
            return reqQryAccountregister(requestID, this.BrokerID, this.InvestorID, bankID);
        }

        /// <summary>
        /// 请求查询签约银行
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryContractBank(int requestID)
        {
            return reqQryContractBank(requestID, this.BrokerID, null, null);
        }

        /// <summary>
        /// 请求查询预埋单
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="instrumentID">合约代码</param>
        /// <param name="exchangeID">交易所代码</param>
        /// <returns></returns>
        public int QueryParkedOrder(int requestID, string instrumentID = null, string exchangeID = null)
        {
            return reqQryParkedOrder(requestID, this.BrokerID, this.InvestorID, instrumentID, exchangeID);
        }

        /// <summary>
        /// 请求查询预埋撤单
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="instrumentID">合约代码</param>
        /// <param name="exchangeID">交易所代码</param>
        /// <returns></returns>
        public int QueryParkedOrderAction(int requestID, string instrumentID = null, string exchangeID = null)
        {
            return reqQryParkedOrderAction(requestID, this.BrokerID, this.InvestorID, instrumentID, exchangeID);
        }

        /// <summary>
        /// 期货发起银行资金转期货请求
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="field"></param>
        /// <returns></returns>
        public int FromBankToFutureByFuture(int requestID, CThostFtdcReqTransferField field)
        {
            return reqFromBankToFutureByFuture(requestID, ref field);
        }

        /// <summary>
        /// 期货发起期货资金转银行请求
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="field"></param>
        /// <returns></returns>
        public int FromFutureToBankByFuture(int requestID, CThostFtdcReqTransferField field)
        {
            return reqFromFutureToBankByFuture(requestID, ref field);
        }

        /// <summary>
        /// 期货发起查询银行余额请求
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="field"></param>
        /// <returns></returns>
        public int QueryBankAccountMoneyByFuture(int requestID, CThostFtdcReqQueryAccountField field)
        {
            return reqQueryBankAccountMoneyByFuture(requestID, ref field);
        }

        #region 连接响应

        FrontConnect frontConnect;

        /// <summary>
        /// 建立连接委托
        /// </summary>
        public delegate void FrontConnect();

        /// <summary>
        /// 当客户端与交易后台建立起通信连接时（还未登录前），该方法被调用。
        /// </summary>
        public event FrontConnect OnFrontConnect
        {
            add { frontConnect += value; regOnFrontConnected(frontConnect); }
            remove { frontConnect -= value; regOnFrontConnected(frontConnect); }
        }

        #endregion

        #region 断开连接响应

        Disconnected disConnected;

        /// <summary>
        /// 断开连接委托
        /// </summary>
        /// <param name="reason"></param>
        public delegate void Disconnected(int reason);

        /// <summary>
        /// 当客户端与交易后台通信连接断开时，该方法被调用。当发生这个情况后，API会自动重新连接，客户端可不做处理。
        /// </summary>
        public event Disconnected OnDisconnected
        {
            add { disConnected += value; regOnDisConnected(disConnected); }
            remove { disConnected -= value; regOnDisConnected(disConnected); }
        }

        #endregion

        #region 心跳响应

        HeartBeatWarning heartBeatWarning;

        /// <summary>
        /// 心跳超时警告委托
        /// </summary>
        /// <param name="pTimeLapes"></param>
        public delegate void HeartBeatWarning(int pTimeLapes);

        /// <summary>
        /// 心跳超时警告。当长时间未收到报文时，该方法被调用。
        /// </summary>
        public event HeartBeatWarning OnHeartBeatWarning
        {
            add { heartBeatWarning += value; regOnHeartBeatWarning(heartBeatWarning); }
            remove { heartBeatWarning -= value; regOnHeartBeatWarning(heartBeatWarning); }
        }

        #endregion

        #region 期货发起银行资金转期货错误回报

        ErrRtnBankToFutureByFuture errRtnBankToFutureByFuture;

        /// <summary>
        /// 期货发起银行资金转期货错误回报委托
        /// </summary>
        /// <param name="pReqTransfer"></param>
        /// <param name="pRspInfo"></param>
		public delegate void ErrRtnBankToFutureByFuture(ref CThostFtdcReqTransferField pReqTransfer,
            ref CThostFtdcRspInfoField pRspInfo);

        /// <summary>
		/// 期货发起银行资金转期货错误回报
		/// </summary>
		public event ErrRtnBankToFutureByFuture OnErrRtnBankToFutureByFuture
        {
            add { errRtnBankToFutureByFuture += value; regErrRtnBankToFutureByFuture(errRtnBankToFutureByFuture); }
            remove { errRtnBankToFutureByFuture -= value; regErrRtnBankToFutureByFuture(errRtnBankToFutureByFuture); }
        }

        #endregion

        #region 期货发起期货资金转银行错误回报

        ErrRtnFutureToBankByFuture errRtnFutureToBankByFuture;

        /// <summary>
        /// 期货发起期货资金转银行错误回报委托
        /// </summary>
        /// <param name="pReqTransfer"></param>
        /// <param name="pRspInfo"></param>
		public delegate void ErrRtnFutureToBankByFuture(ref CThostFtdcReqTransferField pReqTransfer,
            ref CThostFtdcRspInfoField pRspInfo);

        /// <summary>
        /// 期货发起期货资金转银行错误回报
        /// </summary>
        public event ErrRtnFutureToBankByFuture OnErrRtnFutureToBankByFuture
        {
            add { errRtnFutureToBankByFuture += value; regErrRtnFutureToBankByFuture(errRtnFutureToBankByFuture); }
            remove { errRtnFutureToBankByFuture -= value; regErrRtnFutureToBankByFuture(errRtnFutureToBankByFuture); }
        }

        #endregion

        #region 报单操作错误回报

        ErrRtnOrderAction errRtnOrderAction;

        /// <summary>
        /// 报单操作错误回报委托
        /// </summary>
        /// <param name="pOrderAction"></param>
        /// <param name="pRspInfo"></param>
		public delegate void ErrRtnOrderAction(ref CThostFtdcOrderActionField pOrderAction,
            ref CThostFtdcRspInfoField pRspInfo);

        /// <summary>
        /// 报单操作错误回报
        /// </summary>
        public event ErrRtnOrderAction OnErrRtnOrderAction
        {
            add { errRtnOrderAction += value; regErrRtnOrderAction(errRtnOrderAction); }
            remove { errRtnOrderAction -= value; regErrRtnOrderAction(errRtnOrderAction); }
        }

        #endregion

        #region 报单录入错误回报

        ErrRtnOrderInsert errRtnOrderInsert;

        /// <summary>
        /// 报单录入错误回报委托
        /// </summary>
        /// <param name="pInputOrder"></param>
        /// <param name="pRspInfo"></param>
		public delegate void ErrRtnOrderInsert(ref CThostFtdcInputOrderField pInputOrder,
            ref CThostFtdcRspInfoField pRspInfo);

        /// <summary>
        /// 报单录入错误回报
        /// </summary>
        public event ErrRtnOrderInsert OnErrRtnOrderInsert
        {
            add { errRtnOrderInsert += value; regErrRtnOrderInsert(errRtnOrderInsert); }
            remove { errRtnOrderInsert -= value; regErrRtnOrderInsert(errRtnOrderInsert); }
        }

        #endregion

        #region 期货发起查询银行余额错误回报

        ErrRtnQueryBankBalanceByFuture errRtnQueryBankBalanceByFuture;

        /// <summary>
        /// 期货发起查询银行余额错误回报委托
        /// </summary>
        /// <param name="pReqQueryAccount"></param>
        /// <param name="pRspInfo"></param>
		public delegate void ErrRtnQueryBankBalanceByFuture(ref CThostFtdcReqQueryAccountField pReqQueryAccount,
            ref CThostFtdcRspInfoField pRspInfo);

        /// <summary>
        /// 期货发起查询银行余额错误回报
        /// </summary>
        public event ErrRtnQueryBankBalanceByFuture OnErrRtnQueryBankBalanceByFuture
        {
            add
            {
                errRtnQueryBankBalanceByFuture += value;
                regErrRtnQueryBankBalanceByFuture(errRtnQueryBankBalanceByFuture);
            }
            remove
            {
                errRtnQueryBankBalanceByFuture -= value;
                regErrRtnQueryBankBalanceByFuture(errRtnQueryBankBalanceByFuture);
            }
        }

        #endregion

        #region 期货端手工发起冲正银行转期货错误回报

        ErrRtnRepealBankToFutureByFutureManual errRtnRepealBankToFutureByFutureManual;

        /// <summary>
        /// 系统运行时期货端手工发起冲正银行转期货错误回报委托
        /// </summary>
        /// <param name="pReqRepeal"></param>
        /// <param name="pRspInfo"></param>
		public delegate void ErrRtnRepealBankToFutureByFutureManual(ref CThostFtdcReqRepealField pReqRepeal, ref CThostFtdcRspInfoField pRspInfo);

        /// <summary>
        /// 系统运行时期货端手工发起冲正银行转期货错误回报
        /// </summary>
        public event ErrRtnRepealBankToFutureByFutureManual OnErrRtnRepealBankToFutureByFutureManual
        {
            add
            {
                errRtnRepealBankToFutureByFutureManual += value;
                regErrRtnRepealBankToFutureByFutureManual(errRtnRepealBankToFutureByFutureManual);
            }
            remove
            {
                errRtnRepealBankToFutureByFutureManual -= value;
                regErrRtnRepealBankToFutureByFutureManual(errRtnRepealBankToFutureByFutureManual);
            }
        }

        #endregion

        #region 期货端手工发起冲正期货转银行错误回报

        ErrRtnRepealFutureToBankByFutureManual errRtnRepealFutureToBankByFutureManual;

        /// <summary>
        /// 系统运行时期货端手工发起冲正期货转银行错误回报委托
        /// </summary>
        /// <param name="pReqRepeal"></param>
        /// <param name="pRspInfo"></param>
		public delegate void ErrRtnRepealFutureToBankByFutureManual(ref CThostFtdcReqRepealField pReqRepeal,
            ref CThostFtdcRspInfoField pRspInfo);

        /// <summary>
        /// 系统运行时期货端手工发起冲正期货转银行错误回报
        /// </summary>
        public event ErrRtnRepealFutureToBankByFutureManual OnErrRtnRepealFutureToBankByFutureManual
        {
            add
            {
                errRtnRepealFutureToBankByFutureManual += value;
                regErrRtnRepealFutureToBankByFutureManual(errRtnRepealFutureToBankByFutureManual);
            }
            remove
            {
                errRtnRepealFutureToBankByFutureManual -= value;
                regErrRtnRepealFutureToBankByFutureManual(errRtnRepealFutureToBankByFutureManual);
            }
        }

        #endregion

        #region 错误应答

        RspError rspError;

        /// <summary>
        /// 错误应答委托
        /// </summary>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspError(ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 错误应答
        /// </summary>
        public event RspError OnRspError
        {
            add
            {
                rspError += value;
                regRspError(rspError);
            }
            remove
            {
                rspError -= value;
                regRspError(rspError);
            }
        }

        #endregion

        #region 期货发起银行资金转期货应答

        RspFromBankToFutureByFuture rspFromBankToFutureByFuture;

        /// <summary>
        /// 期货发起银行资金转期货应答委托
        /// </summary>
        /// <param name="pReqTransfer"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspFromBankToFutureByFuture(ref CThostFtdcReqTransferField pReqTransfer,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 期货发起银行资金转期货应答
        /// </summary>
        public event RspFromBankToFutureByFuture OnRspFromBankToFutureByFuture
        {
            add
            {
                rspFromBankToFutureByFuture += value;
                regRspFromBankToFutureByFuture(rspFromBankToFutureByFuture);
            }
            remove
            {
                rspFromBankToFutureByFuture -= value;
                regRspFromBankToFutureByFuture(rspFromBankToFutureByFuture);
            }
        }

        #endregion

        #region 期货发起期货资金转银行应答

        RspFromFutureToBankByFuture rspFromFutureToBankByFuture;

        /// <summary>
        /// 期货发起期货资金转银行应答委托
        /// </summary>
        /// <param name="pReqTransfer"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspFromFutureToBankByFuture(ref CThostFtdcReqTransferField pReqTransfer,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 期货发起期货资金转银行应答
        /// </summary>
        public event RspFromFutureToBankByFuture OnRspFromFutureToBankByFuture
        {
            add
            {
                rspFromFutureToBankByFuture += value;
                regRspFromFutureToBankByFuture(rspFromFutureToBankByFuture);
            }
            remove
            {
                rspFromFutureToBankByFuture -= value;
                regRspFromFutureToBankByFuture(rspFromFutureToBankByFuture);
            }
        }

        #endregion

        #region 报单操作请求响应

        RspOrderAction rspOrderAction;

        /// <summary>
        /// 报单操作请求响应委托
        /// </summary>
        /// <param name="pInputOrderAction"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspOrderAction(ref CThostFtdcInputOrderActionField pInputOrderAction,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 报单操作请求响应
        /// </summary>
        public event RspOrderAction OnRspOrderAction
        {
            add
            {
                rspOrderAction += value;
                regRspOrderAction(rspOrderAction);
            }
            remove
            {
                rspOrderAction -= value;
                regRspOrderAction(rspOrderAction);
            }
        }

        #endregion

        #region 报单录入请求响应

        RspOrderInsert rspOrderInsert;

        /// <summary>
        /// 报单录入请求响应委托
        /// </summary>
        /// <param name="pInputOrder"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspOrderInsert(ref CThostFtdcInputOrderField pInputOrder, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast);

        /// <summary>
        /// 报单录入请求响应
        /// </summary>
        public event RspOrderInsert OnRspOrderInsert
        {
            add
            {
                rspOrderInsert += value;
                regRspOrderInsert(rspOrderInsert);
            }
            remove
            {
                rspOrderInsert -= value;
                regRspOrderInsert(rspOrderInsert);
            }
        }

        #endregion

        #region 预埋撤单录入请求响应

        RspParkedOrderAction rspParkedOrderAction;

        /// <summary>
        /// 预埋撤单录入请求响应委托
        /// </summary>
        /// <param name="pParkedOrderAction"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspParkedOrderAction(ref CThostFtdcParkedOrderActionField pParkedOrderAction,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 预埋撤单录入请求响应
        /// </summary>
        public event RspParkedOrderAction OnRspParkedOrderAction
        {
            add
            {
                rspParkedOrderAction += value;
                regRspParkedOrderAction(rspParkedOrderAction);
            }
            remove
            {
                rspParkedOrderAction -= value;
                regRspParkedOrderAction(rspParkedOrderAction);
            }
        }

        #endregion

        #region 预埋单录入请求响应

        RspParkedOrderInsert rspParkedOrderInsert;

        /// <summary>
        /// 预埋单录入请求响应委托
        /// </summary>
        /// <param name="pParkedOrder"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspParkedOrderInsert(ref CThostFtdcParkedOrderField pParkedOrder,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 预埋单录入请求响应
        /// </summary>
        public event RspParkedOrderInsert OnRspParkedOrderInsert
        {
            add
            {
                rspParkedOrderInsert += value;
                regRspParkedOrderInsert(rspParkedOrderInsert);
            }
            remove
            {
                rspParkedOrderInsert -= value;
                regRspParkedOrderInsert(rspParkedOrderInsert);
            }
        }

        #endregion

        #region 请求查询经纪公司交易算法响应

        RspQryBrokerTradingAlgos rspQryBrokerTradingAlgos;

        /// <summary>
        /// 请求查询经纪公司交易算法响应委托
        /// </summary>
        /// <param name="pBrokerTradingAlgos"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryBrokerTradingAlgos(ref CThostFtdcBrokerTradingAlgosField pBrokerTradingAlgos, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询经纪公司交易算法响应
        /// </summary>
        public event RspQryBrokerTradingAlgos OnRspQryBrokerTradingAlgos
        {
            add
            {
                rspQryBrokerTradingAlgos += value;
                regRspQryBrokerTradingAlgos(rspQryBrokerTradingAlgos);
            }
            remove
            {
                rspQryBrokerTradingAlgos -= value;
                regRspQryBrokerTradingAlgos(rspQryBrokerTradingAlgos);
            }
        }

        #endregion

        #region 请求查询经纪公司交易参数响应

        RspQryBrokerTradingParams rspQryBrokerTradingParams;

        /// <summary>
        /// 请求查询经纪公司交易参数响应委托
        /// </summary>
        /// <param name="pBrokerTradingParams"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryBrokerTradingParams(ref CThostFtdcBrokerTradingParamsField pBrokerTradingParams,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询经纪公司交易参数响应
        /// </summary>
        public event RspQryBrokerTradingParams OnRspQryBrokerTradingParams
        {
            add
            {
                rspQryBrokerTradingParams += value;
                regRspQryBrokerTradingParams(rspQryBrokerTradingParams);
            }
            remove
            {
                rspQryBrokerTradingParams -= value;
                regRspQryBrokerTradingParams(rspQryBrokerTradingParams);
            }
        }

        #endregion

        #region 查询保证金监管系统经纪公司资金账户密钥响应

        RspQryCFMMCTradingAccountKey rspQryCFMMCTradingAccountKey;

        /// <summary>
        /// 查询保证金监管系统经纪公司资金账户密钥响应委托
        /// </summary>
        /// <param name="pCFMMCTradingAccountKey"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryCFMMCTradingAccountKey(ref CThostFtdcCFMMCTradingAccountKeyField pCFMMCTradingAccountKey,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 查询保证金监管系统经纪公司资金账户密钥响应
        /// </summary>
        public event RspQryCFMMCTradingAccountKey OnRspQryCFMMCTradingAccountKey
        {
            add
            {
                rspQryCFMMCTradingAccountKey += value;
                regRspQryCFMMCTradingAccountKey(rspQryCFMMCTradingAccountKey);
            }
            remove
            {
                rspQryCFMMCTradingAccountKey -= value;
                regRspQryCFMMCTradingAccountKey(rspQryCFMMCTradingAccountKey);
            }
        }

        #endregion

        #region 请求查询签约银行响应

        RspQryContractBank rspQryContractBank;

        /// <summary>
        /// 请求查询签约银行响应委托
        /// </summary>
        /// <param name="pContractBank"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryContractBank(ref CThostFtdcContractBankField pContractBank,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询签约银行响应
        /// </summary>
        public event RspQryContractBank OnRspQryContractBank
        {
            add
            {
                rspQryContractBank += value;
                regRspQryContractBank(rspQryContractBank);
            }
            remove
            {
                rspQryContractBank -= value;
                regRspQryContractBank(rspQryContractBank);
            }
        }

        #endregion

        #region 请求查询行情响应

        RspQryDepthMarketData rspQryDepthMarketData;

        /// <summary>
        /// 请求查询行情响应委托
        /// </summary>
        /// <param name="pDepthMarketData"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryDepthMarketData(ref CThostFtdcDepthMarketDataField pDepthMarketData,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询行情响应
        /// </summary>
        public event RspQryDepthMarketData OnRspQryDepthMarketData
        {
            add
            {
                rspQryDepthMarketData += value;
                regRspQryDepthMarketData(rspQryDepthMarketData);
            }
            remove
            {
                rspQryDepthMarketData -= value;
                regRspQryDepthMarketData(rspQryDepthMarketData);
            }
        }

        #endregion

        #region 请求查询交易所响应

        RspQryExchange rspQryExchange;

        /// <summary>
        /// 请求查询交易所响应委托
        /// </summary>
        /// <param name="pExchange"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryExchange(ref CThostFtdcExchangeField pExchange, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询交易所响应
        /// </summary>
        public event RspQryExchange OnRspQryExchange
        {
            add
            {
                rspQryExchange += value;
                regRspQryExchange(rspQryExchange);
            }
            remove
            {
                rspQryExchange -= value;
                regRspQryExchange(rspQryExchange);
            }
        }

        #endregion

        #region 请求查询合约响应

        RspQryInstrument rspQryInstrument;

        /// <summary>
        /// 请求查询合约响应委托
        /// </summary>
        /// <param name="pInstrument"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInstrument(ref CThostFtdcInstrumentField pInstrument,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询合约响应
        /// </summary>
        public event RspQryInstrument OnRspQryInstrument
        {
            add
            {
                rspQryInstrument += value;
                regRspQryInstrument(rspQryInstrument);
            }
            remove
            {
                rspQryInstrument -= value;
                regRspQryInstrument(rspQryInstrument);
            }
        }

        #endregion

        #region 请求查询合约手续费率响应

        RspQryInstrumentCommissionRate rspQryInstrumentCommissionRate;

        /// <summary>
        /// 请求查询合约手续费率响应委托
        /// </summary>
        /// <param name="pInstrumentCommissionRate"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInstrumentCommissionRate(
            ref CThostFtdcInstrumentCommissionRateField pInstrumentCommissionRate, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询合约手续费率响应
        /// </summary>
        public event RspQryInstrumentCommissionRate OnRspQryInstrumentCommissionRate
        {
            add
            {
                rspQryInstrumentCommissionRate += value;
                regRspQryInstrumentCommissionRate(rspQryInstrumentCommissionRate);
            }
            remove
            {
                rspQryInstrumentCommissionRate -= value;
                regRspQryInstrumentCommissionRate(rspQryInstrumentCommissionRate);
            }
        }

        #endregion

        #region 请求查询合约保证金率响应

        RspQryInstrumentMarginRate rspQryInstrumentMarginRate;

        /// <summary>
        /// 请求查询合约保证金率响应委托
        /// </summary>
        /// <param name="pInstrumentMarginRate"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInstrumentMarginRate(ref CThostFtdcInstrumentMarginRateField pInstrumentMarginRate,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询合约保证金率响应
        /// </summary>
        public event RspQryInstrumentMarginRate OnRspQryInstrumentMarginRate
        {
            add
            {
                rspQryInstrumentMarginRate += value;
                regRspQryInstrumentMarginRate(rspQryInstrumentMarginRate);
            }
            remove
            {
                rspQryInstrumentMarginRate -= value;
                regRspQryInstrumentMarginRate(rspQryInstrumentMarginRate);
            }
        }

        #endregion

        #region 请求查询投资者响应

        RspQryInvestor rspQryInvestor;

        /// <summary>
        /// 请求查询投资者响应委托
        /// </summary>
        /// <param name="pInvestor"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInvestor(ref CThostFtdcInvestorField pInvestor, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询投资者响应
        /// </summary>
        public event RspQryInvestor OnRspQryInvestor
        {
            add
            {
                rspQryInvestor += value;
                regRspQryInvestor(rspQryInvestor);
            }
            remove
            {
                rspQryInvestor -= value;
                regRspQryInvestor(rspQryInvestor);
            }
        }

        #endregion

        #region 请求查询投资者持仓响应

        RspQryInvestorPosition rspQryInvestorPosition;

        /// <summary>
        /// 请求查询投资者持仓响应委托
        /// </summary>
        /// <param name="pInvestorPosition"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInvestorPosition(ref CThostFtdcInvestorPositionField pInvestorPosition,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询投资者持仓响应
        /// </summary>
        public event RspQryInvestorPosition OnRspQryInvestorPosition
        {
            add
            {
                rspQryInvestorPosition += value;
                regRspQryInvestorPosition(rspQryInvestorPosition);
            }
            remove
            {
                rspQryInvestorPosition -= value;
                regRspQryInvestorPosition(rspQryInvestorPosition);
            }
        }

        #endregion

        #region 请求查询投资者持仓明细响应

        RspQryInvestorPositionCombineDetail rspQryInvestorPositionCombineDetail;

        /// <summary>
        /// 请求查询投资者持仓明细响应委托
        /// </summary>
        /// <param name="pInvestorPositionCombineDetail"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInvestorPositionCombineDetail(
            ref CThostFtdcInvestorPositionCombineDetailField pInvestorPositionCombineDetail,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询投资者持仓明细响应
        /// </summary>
        public event RspQryInvestorPositionCombineDetail OnRspQryInvestorPositionCombineDetail
        {
            add
            {
                rspQryInvestorPositionCombineDetail += value;
                regRspQryInvestorPositionCombineDetail(rspQryInvestorPositionCombineDetail);
            }
            remove
            {
                rspQryInvestorPositionCombineDetail -= value;
                regRspQryInvestorPositionCombineDetail(rspQryInvestorPositionCombineDetail);
            }
        }

        #endregion

        #region 请求查询投资者持仓明细响应

        RspQryInvestorPositionDetail rspQryInvestorPositionDetail;

        /// <summary>
        /// 请求查询投资者持仓明细响应委托
        /// </summary>
        /// <param name="pInvestorPositionDetail"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInvestorPositionDetail(
            ref CThostFtdcInvestorPositionDetailField pInvestorPositionDetail, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询投资者持仓明细响应
        /// </summary>
        public event RspQryInvestorPositionDetail OnRspQryInvestorPositionDetail
        {
            add
            {
                rspQryInvestorPositionDetail += value;
                regRspQryInvestorPositionDetail(rspQryInvestorPositionDetail);
            }
            remove
            {
                rspQryInvestorPositionDetail -= value;
                regRspQryInvestorPositionDetail(rspQryInvestorPositionDetail);
            }
        }

        #endregion

        #region 请求查询客户通知响应

        RspQryNotice rspQryNotice;

        /// <summary>
        /// 请求查询客户通知响应委托
        /// </summary>
        /// <param name="pNotice"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryNotice(ref CThostFtdcNoticeField pNotice, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询客户通知响应
        /// </summary>
        public event RspQryNotice OnRspQryNotice
        {
            add
            {
                rspQryNotice += value;
                regRspQryNotice(rspQryNotice);
            }
            remove
            {
                rspQryNotice -= value;
                regRspQryNotice(rspQryNotice);
            }
        }

        #endregion

        #region 请求查询报单响应

        RspQryOrder rspQryOrder;

        /// <summary>
        /// 请求查询报单响应委托
        /// </summary>
        /// <param name="pOrder"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryOrder(ref CThostFtdcOrderField pOrder, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询报单响应
        /// </summary>
        public event RspQryOrder OnRspQryOrder
        {
            add
            {
                rspQryOrder += value;
                regRspQryOrder(rspQryOrder);
            }
            remove
            {
                rspQryOrder -= value;
                regRspQryOrder(rspQryOrder);
            }
        }

        #endregion

        #region 请求查询预埋单响应

        RspQryParkedOrder rspQryParkedOrder;

        /// <summary>
        /// 请求查询预埋单响应委托
        /// </summary>
        /// <param name="pParkedOrder"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryParkedOrder(ref CThostFtdcParkedOrderField pParkedOrder,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询预埋单响应
        /// </summary>
        public event RspQryParkedOrder OnRspQryParkedOrder
        {
            add
            {
                rspQryParkedOrder += value;
                regRspQryParkedOrder(rspQryParkedOrder);
            }
            remove
            {
                rspQryParkedOrder -= value;
                regRspQryParkedOrder(rspQryParkedOrder);
            }
        }

        #endregion

        #region 请求查询预埋撤单响应

        RspQryParkedOrderAction rspQryParkedOrderAction;

        /// <summary>
        /// 请求查询预埋撤单响应委托
        /// </summary>
        /// <param name="pParkedOrderAction"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryParkedOrderAction(ref CThostFtdcParkedOrderActionField pParkedOrderAction,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询预埋撤单响应
        /// </summary>
        public event RspQryParkedOrderAction OnRspQryParkedOrderAction
        {
            add
            {
                rspQryParkedOrderAction += value;
                regRspQryParkedOrderAction(rspQryParkedOrderAction);
            }
            remove
            {
                rspQryParkedOrderAction -= value;
                regRspQryParkedOrderAction(rspQryParkedOrderAction);
            }
        }

        #endregion

        #region 请求查询投资者结算结果响应

        RspQrySettlementInfo rspQrySettlementInfo;

        /// <summary>
        /// 请求查询投资者结算结果响应委托
        /// </summary>
        /// <param name="pSettlementInfo"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQrySettlementInfo(ref CThostFtdcSettlementInfoField pSettlementInfo,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询投资者结算结果响应
        /// </summary>
        public event RspQrySettlementInfo OnRspQrySettlementInfo
        {
            add
            {
                rspQrySettlementInfo += value;
                regRspQrySettlementInfo(rspQrySettlementInfo);
            }
            remove
            {
                rspQrySettlementInfo -= value;
                regRspQrySettlementInfo(rspQrySettlementInfo);
            }
        }

        #endregion

        #region 请求查询结算信息确认响应

        RspQrySettlementInfoConfirm rspQrySettlementInfoConfirm;

        /// <summary>
        /// 请求查询结算信息确认响应委托
        /// </summary>
        /// <param name="pSettlementInfoConfirm"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQrySettlementInfoConfirm(
            ref CThostFtdcSettlementInfoConfirmField pSettlementInfoConfirm, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询结算信息确认响应
        /// </summary>
        public event RspQrySettlementInfoConfirm OnRspQrySettlementInfoConfirm
        {
            add
            {
                rspQrySettlementInfoConfirm += value;
                regRspQrySettlementInfoConfirm(rspQrySettlementInfoConfirm);
            }
            remove
            {
                rspQrySettlementInfoConfirm -= value;
                regRspQrySettlementInfoConfirm(rspQrySettlementInfoConfirm);
            }
        }

        #endregion

        #region 请求查询成交响应

        RspQryTrade rspQryTrade;
        /// <summary>
        /// 请求查询成交响应委托
        /// </summary>
        /// <param name="pTrade"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryTrade(ref CThostFtdcTradeField pTrade, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询成交响应
        /// </summary>
        public event RspQryTrade OnRspQryTrade
        {
            add
            {
                rspQryTrade += value;
                regRspQryTrade(rspQryTrade);
            }
            remove
            {
                rspQryTrade -= value;
                regRspQryTrade(rspQryTrade);
            }
        }

        #endregion

        #region 请求查询资金账户响应
        RspQryTradingAccount rspQryTradingAccount;

        /// <summary>
        /// 请求查询资金账户响应委托
        /// </summary>
        /// <param name="pTradingAccount"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryTradingAccount(ref CThostFtdcTradingAccountField pTradingAccount,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询资金账户响应
        /// </summary>
        public event RspQryTradingAccount OnRspQryTradingAccount
        {
            add
            {
                rspQryTradingAccount += value;
                regRspQryTradingAccount(rspQryTradingAccount);
            }
            remove
            {
                rspQryTradingAccount -= value;
                regRspQryTradingAccount(rspQryTradingAccount);
            }
        }

        #endregion

        #region 请求查询交易编码响应

        RspQryTradingCode rspQryTradingCode;

        /// <summary>
        /// 请求查询交易编码响应委托
        /// </summary>
        /// <param name="pTradingCode"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryTradingCode(ref CThostFtdcTradingCodeField pTradingCode,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询交易编码响应
        /// </summary>
        public event RspQryTradingCode OnRspQryTradingCode
        {
            add
            {
                rspQryTradingCode += value;
                regRspQryTradingCode(rspQryTradingCode);
            }
            remove
            {
                rspQryTradingCode -= value;
                regRspQryTradingCode(rspQryTradingCode);
            }
        }

        #endregion

        #region 请求查询交易通知响应

        RspQryTradingNotice rspQryTradingNotice;
        /// <summary>
        /// 请求查询交易通知响应委托
        /// </summary>
        /// <param name="pTradingNotice"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryTradingNotice(ref CThostFtdcTradingNoticeField pTradingNotice,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询交易通知响应
        /// </summary>
        public event RspQryTradingNotice OnRspQryTradingNotice
        {
            add
            {
                rspQryTradingNotice += value;
                regRspQryTradingNotice(rspQryTradingNotice);
            }
            remove
            {
                rspQryTradingNotice -= value;
                regRspQryTradingNotice(rspQryTradingNotice);
            }
        }

        #endregion

        #region 请求查询转帐银行响应

        RspQryTransferBank rspQryTransferBank;
        /// <summary>
        /// 请求查询转帐银行响应委托
        /// </summary>
        /// <param name="pTransferBank"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryTransferBank(ref CThostFtdcTransferBankField pTransferBank,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询转帐银行响应
        /// </summary>
        public event RspQryTransferBank OnRspQryTransferBank
        {
            add
            {
                rspQryTransferBank += value;
                regRspQryTransferBank(rspQryTransferBank);
            }
            remove
            {
                rspQryTransferBank -= value;
                regRspQryTransferBank(rspQryTransferBank);
            }
        }

        #endregion

        #region 请求查询转帐流水响应

        RspQryTransferSerial rspQryTransferSerial;

        /// <summary>
        /// 请求查询转帐流水响应委托
        /// </summary>
        /// <param name="pTransferSerial"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryTransferSerial(ref CThostFtdcTransferSerialField pTransferSerial,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询转帐流水响应
        /// </summary>
        public event RspQryTransferSerial OnRspQryTransferSerial
        {
            add
            {
                rspQryTransferSerial += value;
                regRspQryTransferSerial(rspQryTransferSerial);
            }
            remove
            {
                rspQryTransferSerial -= value;
                regRspQryTransferSerial(rspQryTransferSerial);
            }
        }

        #endregion

        #region 请求查询银期签约关系响应

        RspQryAccountregister rspQryAccountregister;

        /// <summary>
        /// 请求查询银期签约关系响应委托
        /// </summary>
        /// <param name="pAccountregister"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
        public delegate void RspQryAccountregister(ref CThostFtdcAccountregisterField pAccountregister,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 请求查询转帐流水响应
        /// </summary>
        public event RspQryAccountregister OnRspQryAccountregister
        {
            add
            {
                rspQryAccountregister += value;
                regRspQryAccountregister(rspQryAccountregister);
            }
            remove
            {
                rspQryAccountregister -= value;
                regRspQryAccountregister(rspQryAccountregister);
            }
        }

        #endregion

        #region 期货发起查询银行余额应答

        RspQueryBankAccountMoneyByFuture rspQueryBankAccountMoneyByFuture;

        /// <summary>
        /// 期货发起查询银行余额应答委托
        /// </summary>
        /// <param name="pReqQueryAccount"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQueryBankAccountMoneyByFuture(ref CThostFtdcReqQueryAccountField pReqQueryAccount,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 期货发起查询银行余额应答
        /// </summary>
        public event RspQueryBankAccountMoneyByFuture OnRspQueryBankAccountMoneyByFuture
        {
            add
            {
                rspQueryBankAccountMoneyByFuture += value;
                regRspQueryBankAccountMoneyByFuture(rspQueryBankAccountMoneyByFuture);
            }
            remove
            {
                rspQueryBankAccountMoneyByFuture -= value;
                regRspQueryBankAccountMoneyByFuture(rspQueryBankAccountMoneyByFuture);
            }
        }

        #endregion

        #region 查询最大报单数量响应

        RspQueryMaxOrderVolume rspQueryMaxOrderVolume;

        /// <summary>
        /// 查询最大报单数量响应委托
        /// </summary>
        /// <param name="pQueryMaxOrderVolume"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQueryMaxOrderVolume(ref CThostFtdcQueryMaxOrderVolumeField pQueryMaxOrderVolume,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 查询最大报单数量响应
        /// </summary>
        public event RspQueryMaxOrderVolume OnRspQueryMaxOrderVolume
        {
            add
            {
                rspQueryMaxOrderVolume += value;
                regRspQueryMaxOrderVolume(rspQueryMaxOrderVolume);
            }
            remove
            {
                rspQueryMaxOrderVolume -= value;
                regRspQueryMaxOrderVolume(rspQueryMaxOrderVolume);
            }
        }

        #endregion

        #region 删除预埋单响应

        RspRemoveParkedOrder rspRemoveParkedOrder;

        /// <summary>
        /// 删除预埋单响应委托
        /// </summary>
        /// <param name="pRemoveParkedOrder"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspRemoveParkedOrder(ref CThostFtdcRemoveParkedOrderField pRemoveParkedOrder,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 删除预埋单响应
        /// </summary>
        public event RspRemoveParkedOrder OnRspRemoveParkedOrder
        {
            add
            {
                rspRemoveParkedOrder += value;
                regRspRemoveParkedOrder(rspRemoveParkedOrder);
            }
            remove
            {
                rspRemoveParkedOrder -= value;
                regRspRemoveParkedOrder(rspRemoveParkedOrder);
            }
        }

        #endregion

        #region 删除预埋撤单响应

        RspRemoveParkedOrderAction rspRemoveParkedOrderAction;

        /// <summary>
        /// 删除预埋撤单响应委托
        /// </summary>
        /// <param name="pRemoveParkedOrderAction"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspRemoveParkedOrderAction(
            ref CThostFtdcRemoveParkedOrderActionField pRemoveParkedOrderAction, ref CThostFtdcRspInfoField pRspInfo,
            int nRequestID, byte bIsLast);

        /// <summary>
        /// 删除预埋撤单响应
        /// </summary>
        public event RspRemoveParkedOrderAction OnRspRemoveParkedOrderAction
        {
            add
            {
                rspRemoveParkedOrderAction += value;
                regRspRemoveParkedOrderAction(rspRemoveParkedOrderAction);
            }
            remove
            {
                rspRemoveParkedOrderAction -= value;
                regRspRemoveParkedOrderAction(rspRemoveParkedOrderAction);
            }
        }

        #endregion

        #region 投资者结算结果确认响应

        RspSettlementInfoConfirm rspSettlementInfoConfirm;

        /// <summary>
        /// 投资者结算结果确认响应委托
        /// </summary>
        /// <param name="pSettlementInfoConfirm"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspSettlementInfoConfirm(ref CThostFtdcSettlementInfoConfirmField pSettlementInfoConfirm,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 投资者结算结果确认响应
        /// </summary>
        public event RspSettlementInfoConfirm OnRspSettlementInfoConfirm
        {
            add
            {
                rspSettlementInfoConfirm += value;
                regRspSettlementInfoConfirm(rspSettlementInfoConfirm);
            }
            remove
            {
                rspSettlementInfoConfirm -= value;
                regRspSettlementInfoConfirm(rspSettlementInfoConfirm);
            }
        }

        #endregion

        #region 资金账户口令更新请求响应

        RspTradingAccountPasswordUpdate rspTradingAccountPasswordUpdate;

        /// <summary>
        /// 资金账户口令更新请求响应委托
        /// </summary>
        /// <param name="pTradingAccountPasswordUpdate"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspTradingAccountPasswordUpdate(
            ref CThostFtdcTradingAccountPasswordUpdateField pTradingAccountPasswordUpdate,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 资金账户口令更新请求响应
        /// </summary>
        public event RspTradingAccountPasswordUpdate OnRspTradingAccountPasswordUpdate
        {
            add
            {
                rspTradingAccountPasswordUpdate += value;
                regRspTradingAccountPasswordUpdate(rspTradingAccountPasswordUpdate);
            }
            remove
            {
                rspTradingAccountPasswordUpdate -= value;
                regRspTradingAccountPasswordUpdate(rspTradingAccountPasswordUpdate);
            }
        }

        #endregion

        #region 登录请求响应

        RspUserLogin rspUserLogin;

        /// <summary>
        /// 登录请求响应委托
        /// </summary>
        /// <param name="pRspUserLogin"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspUserLogin(ref CThostFtdcRspUserLoginField pRspUserLogin,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 登录请求响应
        /// </summary>
        public event RspUserLogin OnRspUserLogin
        {
            add
            {
                rspUserLogin += value;
                regRspUserLogin(rspUserLogin);
            }
            remove
            {
                rspUserLogin -= value;
                regRspUserLogin(rspUserLogin);
            }
        }

        #endregion

        #region 登出请求响应

        RspUserLogout rspUserLogout;

        /// <summary>
        /// 登出请求响应委托
        /// </summary>
        /// <param name="pUserLogout"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspUserLogout(ref CThostFtdcUserLogoutField pUserLogout,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 登出请求响应
        /// </summary>
        public event RspUserLogout OnRspUserLogout
        {
            add
            {
                rspUserLogout += value;
                regRspUserLogout(rspUserLogout);
            }
            remove
            {
                rspUserLogout -= value;
                regRspUserLogout(rspUserLogout);
            }
        }

        #endregion

        #region 用户口令更新请求响应

        RspUserPasswordUpdate rspUserPasswordUpdate;

        /// <summary>
        /// 用户口令更新请求响应委托
        /// </summary>
        /// <param name="pUserPasswordUpdate"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspUserPasswordUpdate(ref CThostFtdcUserPasswordUpdateField pUserPasswordUpdate,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 用户口令更新请求响应
        /// </summary>
        public event RspUserPasswordUpdate OnRspUserPasswordUpdate
        {
            add
            {
                rspUserPasswordUpdate += value;
                regRspUserPasswordUpdate(rspUserPasswordUpdate);
            }
            remove
            {
                rspUserPasswordUpdate -= value;
                regRspUserPasswordUpdate(rspUserPasswordUpdate);
            }
        }

        #endregion

        #region 提示条件单校验错误

        RtnErrorConditionalOrder rtnErrorConditionalOrder;

        /// <summary>
        /// 提示条件单校验错误委托
        /// </summary>
        /// <param name="pErrorConditionalOrder"></param>
		public delegate void RtnErrorConditionalOrder(ref CThostFtdcErrorConditionalOrderField pErrorConditionalOrder);

        /// <summary>
        /// 提示条件单校验错误
        /// </summary>
        public event RtnErrorConditionalOrder OnRtnErrorConditionalOrder
        {
            add
            {
                rtnErrorConditionalOrder += value;
                regRtnErrorConditionalOrder(rtnErrorConditionalOrder);
            }
            remove
            {
                rtnErrorConditionalOrder -= value;
                regRtnErrorConditionalOrder(rtnErrorConditionalOrder);
            }
        }

        #endregion

        #region 银行发起银行资金转期货通知

        RtnFromBankToFutureByBank rtnFromBankToFutureByBank;

        /// <summary>
        /// 银行发起银行资金转期货通知委托
        /// </summary>
        /// <param name="pRspTransfer"></param>
		public delegate void RtnFromBankToFutureByBank(ref CThostFtdcRspTransferField pRspTransfer);

        /// <summary>
        /// 银行发起银行资金转期货通知
        /// </summary>
        public event RtnFromBankToFutureByBank OnRtnFromBankToFutureByBank
        {
            add
            {
                rtnFromBankToFutureByBank += value;
                regRtnFromBankToFutureByBank(rtnFromBankToFutureByBank);
            }
            remove
            {
                rtnFromBankToFutureByBank -= value;
                regRtnFromBankToFutureByBank(rtnFromBankToFutureByBank);
            }
        }

        #endregion

        #region 期货发起银行资金转期货通知

        RtnFromBankToFutureByFuture rtnFromBankToFutureByFuture;

        /// <summary>
        /// 期货发起银行资金转期货通知委托
        /// </summary>
        /// <param name="pRspTransfer"></param>
		public delegate void RtnFromBankToFutureByFuture(ref CThostFtdcRspTransferField pRspTransfer);

        /// <summary>
        /// 期货发起银行资金转期货通知
        /// </summary>
        public event RtnFromBankToFutureByFuture OnRtnFromBankToFutureByFuture
        {
            add
            {
                rtnFromBankToFutureByFuture += value;
                regRtnFromBankToFutureByFuture(rtnFromBankToFutureByFuture);
            }
            remove
            {
                rtnFromBankToFutureByFuture -= value;
                regRtnFromBankToFutureByFuture(rtnFromBankToFutureByFuture);
            }
        }

        #endregion

        #region 银行发起期货资金转银行通知

        RtnFromFutureToBankByBank rtnFromFutureToBankByBank;

        /// <summary>
        /// 银行发起期货资金转银行通知委托
        /// </summary>
        /// <param name="pRspTransfer"></param>
		public delegate void RtnFromFutureToBankByBank(ref CThostFtdcRspTransferField pRspTransfer);

        /// <summary>
        /// 银行发起期货资金转银行通知
        /// </summary>
        public event RtnFromFutureToBankByBank OnRtnFromFutureToBankByBank
        {
            add
            {
                rtnFromFutureToBankByBank += value;
                regRtnFromFutureToBankByBank(rtnFromFutureToBankByBank);
            }
            remove
            {
                rtnFromFutureToBankByBank -= value;
                regRtnFromFutureToBankByBank(rtnFromFutureToBankByBank);
            }
        }

        #endregion

        #region 期货发起期货资金转银行通知

        RtnFromFutureToBankByFuture rtnFromFutureToBankByFuture;

        /// <summary>
        /// 期货发起期货资金转银行通知委托
        /// </summary>
        /// <param name="pRspTransfer"></param>
		public delegate void RtnFromFutureToBankByFuture(ref CThostFtdcRspTransferField pRspTransfer);

        /// <summary>
        /// 期货发起期货资金转银行通知
        /// </summary>
        public event RtnFromFutureToBankByFuture OnRtnFromFutureToBankByFuture
        {
            add
            {
                rtnFromFutureToBankByFuture += value;
                regRtnFromFutureToBankByFuture(rtnFromFutureToBankByFuture);
            }
            remove
            {
                rtnFromFutureToBankByFuture -= value;
                regRtnFromFutureToBankByFuture(rtnFromFutureToBankByFuture);
            }
        }

        #endregion

        #region 合约交易状态通知

        RtnInstrumentStatus rtnInstrumentStatus;

        /// <summary>
        /// 合约交易状态通知委托
        /// </summary>
        /// <param name="pInstrumentStatus"></param>
		public delegate void RtnInstrumentStatus(ref CThostFtdcInstrumentStatusField pInstrumentStatus);

        /// <summary>
        /// 合约交易状态通知
        /// </summary>
        public event RtnInstrumentStatus OnRtnInstrumentStatus
        {
            add
            {
                rtnInstrumentStatus += value;
                regRtnInstrumentStatus(rtnInstrumentStatus);
            }
            remove
            {
                rtnInstrumentStatus -= value;
                regRtnInstrumentStatus(rtnInstrumentStatus);
            }
        }

        #endregion

        #region 报单通知

        RtnOrder rtnOrder;

        /// <summary>
        /// 报单通知委托
        /// </summary>
        /// <param name="pOrder"></param>
		public delegate void RtnOrder(ref CThostFtdcOrderField pOrder);

        /// <summary>
        /// 报单通知
        /// </summary>
        public event RtnOrder OnRtnOrder
        {
            add
            {
                rtnOrder += value;
                regRtnOrder(rtnOrder);
            }
            remove
            {
                rtnOrder -= value;
                regRtnOrder(rtnOrder);
            }
        }

        #endregion

        #region 期货发起查询银行余额通知

        RtnQueryBankBalanceByFuture rtnQueryBankBalanceByFuture;

        /// <summary>
        /// 期货发起查询银行余额通知委托
        /// </summary>
        /// <param name="pNotifyQueryAccount"></param>
		public delegate void RtnQueryBankBalanceByFuture(ref CThostFtdcNotifyQueryAccountField pNotifyQueryAccount);

        /// <summary>
        /// 期货发起查询银行余额通知
        /// </summary>
        public event RtnQueryBankBalanceByFuture OnRtnQueryBankBalanceByFuture
        {
            add
            {
                rtnQueryBankBalanceByFuture += value;
                regRtnQueryBankBalanceByFuture(rtnQueryBankBalanceByFuture);
            }
            remove
            {
                rtnQueryBankBalanceByFuture -= value;
                regRtnQueryBankBalanceByFuture(rtnQueryBankBalanceByFuture);
            }
        }

        #endregion

        #region 银行发起冲正银行转期货通知

        RtnRepealFromBankToFutureByBank rtnRepealFromBankToFutureByBank;

        /// <summary>
        /// 银行发起冲正银行转期货通知委托
        /// </summary>
        /// <param name="pRspRepeal"></param>
		public delegate void RtnRepealFromBankToFutureByBank(ref CThostFtdcRspRepealField pRspRepeal);

        /// <summary>
        /// 银行发起冲正银行转期货通知
        /// </summary>
        public event RtnRepealFromBankToFutureByBank OnRtnRepealFromBankToFutureByBank
        {
            add
            {
                rtnRepealFromBankToFutureByBank += value;
                regRtnRepealFromBankToFutureByBank(rtnRepealFromBankToFutureByBank);
            }
            remove
            {
                rtnRepealFromBankToFutureByBank -= value;
                regRtnRepealFromBankToFutureByBank(rtnRepealFromBankToFutureByBank);
            }
        }

        #endregion

        #region 期货发起冲正银行转期货请求通知

        RtnRepealFromBankToFutureByFuture rtnRepealFromBankToFutureByFuture;

        /// <summary>
		/// 期货发起冲正银行转期货请求，银行处理完毕后报盘发回的通知委托
		/// </summary>
		public delegate void RtnRepealFromBankToFutureByFuture(ref CThostFtdcRspRepealField pRspRepeal);

        /// <summary>
        /// 期货发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
        /// </summary>
        public event RtnRepealFromBankToFutureByFuture OnRtnRepealFromBankToFutureByFuture
        {
            add
            {
                rtnRepealFromBankToFutureByFuture += value;
                regRtnRepealFromBankToFutureByFuture(rtnRepealFromBankToFutureByFuture);
            }
            remove
            {
                rtnRepealFromBankToFutureByFuture -= value;
                regRtnRepealFromBankToFutureByFuture(rtnRepealFromBankToFutureByFuture);
            }
        }

        #endregion

        #region 期货端手工发起冲正银行转期货请求通知
        RtnRepealFromBankToFutureByFutureManual rtnRepealFromBankToFutureByFutureManual;

        /// <summary>
		/// 系统运行时期货端手工发起冲正银行转期货请求，银行处理完毕后报盘发回的通知委托
		/// </summary>
		public delegate void RtnRepealFromBankToFutureByFutureManual(ref CThostFtdcRspRepealField pRspRepeal);

        /// <summary>
        /// 系统运行时期货端手工发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
        /// </summary>
        public event RtnRepealFromBankToFutureByFutureManual OnRtnRepealFromBankToFutureByFutureManual
        {
            add
            {
                rtnRepealFromBankToFutureByFutureManual += value;
                regRtnRepealFromBankToFutureByFutureManual(rtnRepealFromBankToFutureByFutureManual);
            }
            remove
            {
                rtnRepealFromBankToFutureByFutureManual -= value;
                regRtnRepealFromBankToFutureByFutureManual(rtnRepealFromBankToFutureByFutureManual);
            }
        }

        #endregion

        #region 银行发起冲正期货转银行通知

        RtnRepealFromFutureToBankByBank rtnRepealFromFutureToBankByBank;

        /// <summary>
		/// 银行发起冲正期货转银行通知委托
		/// </summary>
		public delegate void RtnRepealFromFutureToBankByBank(ref CThostFtdcRspRepealField pRspRepeal);

        /// <summary>
        /// 银行发起冲正期货转银行通知
        /// </summary>
        public event RtnRepealFromFutureToBankByBank OnRtnRepealFromFutureToBankByBank
        {
            add
            {
                rtnRepealFromFutureToBankByBank += value;
                regRtnRepealFromFutureToBankByBank(rtnRepealFromFutureToBankByBank);
            }
            remove
            {
                rtnRepealFromFutureToBankByBank -= value;
                regRtnRepealFromFutureToBankByBank(rtnRepealFromFutureToBankByBank);
            }
        }

        #endregion

        #region 期货发起冲正期货转银行请求通知

        RtnRepealFromFutureToBankByFuture rtnRepealFromFutureToBankByFuture;

        /// <summary>
		/// 期货发起冲正期货转银行请求，银行处理完毕后报盘发回的通知委托
		/// </summary>
		public delegate void RtnRepealFromFutureToBankByFuture(ref CThostFtdcRspRepealField pRspRepeal);

        /// <summary>
        /// 期货发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
        /// </summary>
        public event RtnRepealFromFutureToBankByFuture OnRtnRepealFromFutureToBankByFuture
        {
            add
            {
                rtnRepealFromFutureToBankByFuture += value;
                regRtnRepealFromFutureToBankByFuture(rtnRepealFromFutureToBankByFuture);
            }
            remove
            {
                rtnRepealFromFutureToBankByFuture -= value;
                regRtnRepealFromFutureToBankByFuture(rtnRepealFromFutureToBankByFuture);
            }
        }

        #endregion

        #region 期货端手工发起冲正期货转银行请求通知

        RtnRepealFromFutureToBankByFutureManual rtnRepealFromFutureToBankByFutureManual;

        /// <summary>
		/// 系统运行时期货端手工发起冲正期货转银行请求，银行处理完毕后报盘发回的通知委托
		/// </summary>
		public delegate void RtnRepealFromFutureToBankByFutureManual(ref CThostFtdcRspRepealField pRspRepeal);

        /// <summary>
        /// 系统运行时期货端手工发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
        /// </summary>
        public event RtnRepealFromFutureToBankByFutureManual OnRtnRepealFromFutureToBankByFutureManual
        {
            add
            {
                rtnRepealFromFutureToBankByFutureManual += value;
                regRtnRepealFromFutureToBankByFutureManual(rtnRepealFromFutureToBankByFutureManual);
            }
            remove
            {
                rtnRepealFromFutureToBankByFutureManual -= value;
                regRtnRepealFromFutureToBankByFutureManual(rtnRepealFromFutureToBankByFutureManual);
            }
        }

        #endregion

        #region 成交通知

        RtnTrade rtnTrade;

        /// <summary>
		/// 成交通知委托
		/// </summary>
		public delegate void RtnTrade(ref CThostFtdcTradeField pTrade);

        /// <summary>
        /// 成交通知
        /// </summary>
        public event RtnTrade OnRtnTrade
        {
            add
            {
                rtnTrade += value;
                regRtnTrade(rtnTrade);
            }
            remove
            {
                rtnTrade -= value;
                regRtnTrade(rtnTrade);
            }
        }

        #endregion

        #region 交易通知

        RtnTradingNotice rtnTradingNotice;

        /// <summary>
		/// 交易通知委托
		/// </summary>
		public delegate void RtnTradingNotice(ref CThostFtdcTradingNoticeInfoField pTradingNoticeInfo);

        /// <summary>
        /// 交易通知
        /// </summary>
        public event RtnTradingNotice OnRtnTradingNotice
        {
            add
            {
                rtnTradingNotice += value;
                regRtnTradingNotice(rtnTradingNotice);
            }
            remove
            {
                rtnTradingNotice -= value;
                regRtnTradingNotice(rtnTradingNotice);
            }
        }

        #endregion
    }
}
