using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CTPTradeApi
{
    /// <summary>
    /// 交易接口类
    /// </summary>
    public class TradeApi
    {
        private const string dllName = "TradeApi.dll";

        /// <summary>
        /// TradeApi.dll/CTPTradeApi.dll/thosttraderapi.dll 放在主程序的执行文件夹中
        /// </summary>
        /// <param name="brokerID">经纪公司代码:2030-CTP模拟</param>
        /// <param name="frontAddr">前置地址，tcp://IP:Port</param>
        /// <param name="flowPath">存储订阅信息文件的目录，默认为当前目录</param>
        public TradeApi(string brokerID = "", string frontAddr = "", string flowPath = "")
        {
            this.FrontAddr = frontAddr;
            this.BrokerID = brokerID;
            this._flowPath = flowPath;
        }

        /// <summary>
        /// 前置地址
        /// </summary>
        public string FrontAddr { get; set; }

        /// <summary>
        /// 经纪公司代码ctp-2030;上期-4030;
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
        public int MaxOrderRef { get; set; }

        private string _password;
        private string _flowPath;

        /// <summary>
        /// 登录
        /// </summary>
        public void Connect() { connect(this.FrontAddr, this._flowPath); }
        [DllImport(dllName, EntryPoint = "?Connect@@YAXPAD0@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern void connect(string FrontAddr, string FlowPath);

        /// <summary>
        /// 断开
        /// </summary>
        public void DisConnect() { disConnect(); }
        [DllImport(dllName, EntryPoint = "?DisConnect@@YAXXZ", CallingConvention = CallingConvention.Cdecl)]
        static extern void disConnect();

        /// <summary>
        /// 获取交易日
        /// </summary>
        /// <returns></returns>
        public string GetTradingDay()
        { return getTradingDay(); }
        [DllImport(dllName, EntryPoint = "?GetTradingDay@@YAPBDXZ", CallingConvention = CallingConvention.Cdecl)]
        static extern string getTradingDay();

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
        
        [DllImport(dllName, EntryPoint = "?ReqUserLogin@@YAHHQAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqUserLogin(int requestID, string brokerID, string investorID, string password);

        /// <summary>
        /// 发送登出请求
        /// </summary>
        public int UserLogout(int requestID) { return reqUserLogout(requestID, this.BrokerID, this.InvestorID); }
        [DllImport(dllName, EntryPoint = "?ReqUserLogout@@YAHHQAD0@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqUserLogout(int requestID, string brokerID, string investorID);

        /// <summary>
        /// 更新用户口令
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="userID">投资者ID</param>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        public int UserPasswordupdate(int requestID, string userID, string oldPassword, string newPassword)
        {
            return reqUserPasswordUpdate(requestID, this.BrokerID, userID, oldPassword, newPassword);
        }

        [DllImport(dllName, EntryPoint = "?ReqUserPasswordUpdate@@YAHHQAD000@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqUserPasswordUpdate(int requestID, string brokerID, string userID, string oldPassword, string newPassword);

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

        [DllImport(dllName, EntryPoint = "?ReqTradingAccountPasswordUpdate@@YAHHQAD000@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqTradingAccountPasswordUpdate(int requestID, string brokerID, string accountID, string oldPassword, string newPassword);

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

        [DllImport(dllName, EntryPoint = "?ReqOrderInsert@@YAHHPAUCThostFtdcInputOrderField@@@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqOrderInsert(int requestID, ref CThostFtdcInputOrderField req);

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

        [DllImport(dllName, EntryPoint = "?ReqOrderAction@@YAHHPAUCThostFtdcInputOrderActionField@@@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqOrderAction(int requestID, ref CThostFtdcInputOrderActionField pOrder);

        /// <summary>
        /// 查询最大允许报单数量请求
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="pMaxOrderVolume">最大报单数量</param>
        public int QueryMaxOrderVolume(int requestID, CThostFtdcQueryMaxOrderVolumeField pMaxOrderVolume)
        { return reqQueryMaxOrderVolume(requestID, ref pMaxOrderVolume); }
        [DllImport(dllName, EntryPoint = "?ReqQueryMaxOrderVolume@@YAHHPAUCThostFtdcQueryMaxOrderVolumeField@@@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQueryMaxOrderVolume(int requestID, ref CThostFtdcQueryMaxOrderVolumeField pMaxOrderVolume);

        /// <summary>
        /// 确认结算结果
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int SettlementInfoConfirm(int requestID) { return reqSettlementInfoConfirm(requestID, this.BrokerID, this.InvestorID); }
        [DllImport(dllName, EntryPoint = "?ReqSettlementInfoConfirm@@YAHHQAD0@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqSettlementInfoConfirm(int requestID, string brokerID, string investorID);

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

        [DllImport(dllName, EntryPoint = "?ReqQryOrder@@YAHHPAUCThostFtdcQryOrderField@@@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryOrder(int requestID, ref CThostFtdcQryOrderField pQryOrder);

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

        [DllImport(dllName, EntryPoint = "?ReqQryTrade@@YAHHPAUCThostFtdcQryTradeField@@@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryTrade(int requestID, ref CThostFtdcQryTradeField pQryTrade);

        /// <summary>
        /// 查询投资者持仓
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="instrument">合约代码:不填-查所有</param>
        public int QueryInvestorPosition(int requestID, string instrument = null)
        {
            return reqQryInvestorPosition(requestID, this.BrokerID, this.InvestorID, instrument);
        }

        [DllImport(dllName, EntryPoint = "?ReqQryInvestorPosition@@YAHHQAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryInvestorPosition(int requestID, string brokerID, string investorID, string Instrument);

        /// <summary>
        /// 查询帐户资金请求
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryTradingAccount(int requestID) { return reqQryTradingAccount(requestID, this.BrokerID, this.InvestorID); }
        [DllImport(dllName, EntryPoint = "?ReqQryTradingAccount@@YAHHQAD0@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryTradingAccount(int requestID, string brokerID, string investorID);

        /// <summary>
        /// 请求查询投资者
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryInvestor(int requestID) { return reqQryInvestor(requestID, this.BrokerID, this.InvestorID); }
        [DllImport(dllName, EntryPoint = "?ReqQryInvestor@@YAHHQAD0@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryInvestor(int requestID, string brokerID, string investorID);

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

        [DllImport(dllName, EntryPoint = "?ReqQryTradingCode@@YAHHQAD000@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryTradingCode(int requestID, string brokerID, string investorID, string clientID, string exchangeID);

        /// <summary>
        /// 请求查询合约保证金率:能为null;每次只能查一个合约
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="hedgeFlag">投机/套保</param>
        /// <param name="instrumentID">合约代码:不填-查所有</param>
        /// <returns></returns>
        public int QueryInstrumentMarginRate(int requestID, string instrumentID, TThostFtdcHedgeFlagType hedgeFlag = TThostFtdcHedgeFlagType.Speculation)
        {
            return reqQryInstrumentMarginRate(requestID, this.BrokerID, this.InvestorID, instrumentID, hedgeFlag);
        }

        [DllImport(dllName, EntryPoint = "?ReqQryInstrumentMarginRate@@YAHHQAD00D@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryInstrumentMarginRate(int requestID, string brokerID, string investorID, string instrumentID, TThostFtdcHedgeFlagType HEDGE_FLAG);

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

        [DllImport(dllName, EntryPoint = "?ReqQryInstrumentCommissionRate@@YAHHQAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryInstrumentCommissionRate(int requestID, string brokerID, string investorID, string instrumentID);

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

        [DllImport(dllName, EntryPoint = "?ReqQryExchange@@YAHHQAD@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryExchange(int requestID, string exchangeID);

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

        [DllImport(dllName, EntryPoint = "?ReqQryInstrument@@YAHHQAD@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryInstrument(int requestID, string instrumentID);

        /// <summary>
        /// 查询行情
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="instrumentID">合约代码</param>
        /// <returns></returns>
        public int QryDepthMarketData(int requestID, string instrumentID)
        { return reqQryDepthMarketData(requestID, instrumentID); }

        [DllImport(dllName, EntryPoint = "?ReqQryDepthMarketData@@YAHHQAD@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryDepthMarketData(int requestID, string Instrument);

        /// <summary>
        /// 请求查询投资者结算结果
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="date">查询日期，格式yyyyMMdd</param>
        /// <returns></returns>
        public int QuerySettlementInfo(int requestID, string date = null)
        { return reqQrySettlementInfo(requestID, this.BrokerID, this.InvestorID, date); }// tradingDay.ToString("yyyyMMdd")); }
        [DllImport(dllName, EntryPoint = "?ReqQrySettlementInfo@@YAHHQAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQrySettlementInfo(int requestID, string brokerID, string investorID, string TRADING_DAY);

        /// <summary>
        /// 查询投资者持仓明细
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="instrumentID">合约代码:不填-查所有</param>
        /// <returns></returns>
        public int QueryInvestorPositionDetail(int requestID, string instrumentID = null)
        { return reqQryInvestorPositionDetail(requestID, this.BrokerID, this.InvestorID, instrumentID); }
        [DllImport(dllName, EntryPoint = "?ReqQryInvestorPositionDetail@@YAHHQAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryInvestorPositionDetail(int requestID, string brokerID, string investorID, string Instrument);

        /// <summary>
        /// 请求查询客户通知
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryNotice(int requestID) { return reqQryNotice(requestID, this.BrokerID); }
        [DllImport(dllName, EntryPoint = "?ReqQryNotice@@YAHHQAD@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryNotice(int requestID, string BROKERID);

        /// <summary>
        /// 请求查询结算信息确认
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QuerySettlementInfoConfirm(int requestID)
        { return reqQrySettlementInfoConfirm(requestID, this.BrokerID, this.InvestorID); }
        [DllImport(dllName, EntryPoint = "?ReqQrySettlementInfoConfirm@@YAHHQAD0@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQrySettlementInfoConfirm(int requestID, string brokerID, string investorID);

        /// <summary>
        /// 请求查询**组合**持仓明细
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="instrumentID">合约代码:不填-查所有</param>
        /// <returns></returns>
        public int QueryInvestorPositionCombinaDetail(int requestID, string instrumentID = null)
        { return reqQryInvestorPositionCombineDetail(requestID, this.BrokerID, this.InvestorID, instrumentID); }
        [DllImport(dllName, EntryPoint = "?ReqQryInvestorPositionCombineDetail@@YAHHQAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryInvestorPositionCombineDetail(int requestID, string brokerID, string investorID, string instrumentID);

        /// <summary>
        /// 请求查询保证金监管系统经纪公司资金账户密钥
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryCFMMCTradingAccountKey(int requestID)
        { return reqQryCFMMCTradingAccountKey(requestID, this.BrokerID, this.InvestorID); }
        [DllImport(dllName, EntryPoint = "?ReqQryCFMMCTradingAccountKey@@YAHHQAD0@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryCFMMCTradingAccountKey(int requestID, string brokerID, string investorID);

        /// <summary>
        /// 请求查询交易通知
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryTradingNotice(int requestID)
        { return reqQryTradingNotice(requestID, this.BrokerID, this.InvestorID); }
        [DllImport(dllName, EntryPoint = "?ReqQryTradingNotice@@YAHHQAD0@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryTradingNotice(int requestID, string brokerID, string investorID);

        /// <summary>
        /// 请求查询经纪公司交易参数
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryBrokerTradingParams(int requestID)
        { return reqQryBrokerTradingParams(requestID, this.BrokerID, this.InvestorID); }
        [DllImport(dllName, EntryPoint = "?ReqQryBrokerTradingParams@@YAHHQAD0@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryBrokerTradingParams(int requestID, string brokerID, string investorID);

        /// <summary>
        /// 请求查询经纪公司交易算法
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="exchangeID">交易所代码</param>
        /// <param name="instrumentID">合约代码</param>
        /// <returns></returns>
        public int QueryBrokerTradingAlgos(int requestID, string exchangeID, string instrumentID)
        { return reqQryBrokerTradingAlgos(requestID, this.BrokerID, exchangeID, instrumentID); }
        [DllImport(dllName, EntryPoint = "?ReqQryBrokerTradingAlgos@@YAHHQAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryBrokerTradingAlgos(int requestID, string brokerID, string exchangeID, string instrumentID);

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
        public int ParkedOrderInsert(int requestID, string InstrumentID, TThostFtdcOffsetFlagType OffsetFlag, TThostFtdcDirectionType Direction, double Price, int Volume, string orderRef = "")
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

        [DllImport(dllName, EntryPoint = "?ReqParkedOrderInsert@@YAHHPAUCThostFtdcParkedOrderField@@@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqParkedOrderInsert(int requestID, ref CThostFtdcParkedOrderField pField);

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

        [DllImport(dllName, EntryPoint = "?ReqParkedOrderAction@@YAHHPAUCThostFtdcParkedOrderActionField@@@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqParkedOrderAction(int requestID, ref CThostFtdcParkedOrderActionField pField);

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

        [DllImport(dllName, EntryPoint = "?ReqRemoveParkedOrder@@YAHHQAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqRemoveParkedOrder(int requestID, string brokerID, string investorID, string parkedOrderID);

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

        [DllImport(dllName, EntryPoint = "?ReqRemoveParkedOrderAction@@YAHHQAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqRemoveParkedOrderAction(int requestID, string brokerID, string investorID, string parkedOrderActionID);

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

        [DllImport(dllName, EntryPoint = "?ReqQryTransferBank@@YAHHQAD0@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryTransferBank(int requestID, string bankID, string bankBranchID);

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

        [DllImport(dllName, EntryPoint = "?ReqQryTransferSerial@@YAHHQAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryTransferSerial(int requestID, string brokerID, string accountID, string bankID);

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

        [DllImport(dllName, EntryPoint = "?ReqQryAccountregister@@YAHHQAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryAccountregister(int requestID, string brokerID, string accountID, string bankID);

        /// <summary>
        /// 请求查询签约银行
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryContractBank(int requestID)
        {
            return reqQryContractBank(requestID, this.BrokerID, null, null);
        }

        [DllImport(dllName, EntryPoint = "?ReqQryContractBank@@YAHHQAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryContractBank(int requestID, string brokerID, string bankID, string bankBranchID);

        /// <summary>
        /// 请求查询预埋单
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryParkedOrder(int requestID)
        {
            return reqQryParkedOrder(requestID, this.BrokerID, this.InvestorID, null, null);
        }

        [DllImport(dllName, EntryPoint = "?ReqQryParkedOrder@@YAHHQAD000@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryParkedOrder(int requestID, string brokerID, string investorID, string instrumentID, string exchangeID);

        /// <summary>
        /// 请求查询预埋撤单
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <returns></returns>
        public int QueryParkedOrderAction(int requestID)
        {
            return reqQryParkedOrderAction(requestID, this.BrokerID, this.InvestorID, null, null);
        }

        [DllImport(dllName, EntryPoint = "?ReqQryParkedOrderAction@@YAHHQAD000@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQryParkedOrderAction(int requestID, string brokerID, string investorID, string instrumentID, string exchangeID);

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

        [DllImport(dllName, EntryPoint = "?ReqFromBankToFutureByFuture@@YAHHPAUCThostFtdcReqTransferField@@@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqFromBankToFutureByFuture(int requestID, ref CThostFtdcReqTransferField pField);

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

        [DllImport(dllName, EntryPoint = "?ReqFromFutureToBankByFuture@@YAHHPAUCThostFtdcReqTransferField@@@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqFromFutureToBankByFuture(int requestID, ref CThostFtdcReqTransferField pField);

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

        [DllImport(dllName, EntryPoint = "?ReqQueryBankAccountMoneyByFuture@@YAHHPAUCThostFtdcReqQueryAccountField@@@Z", CallingConvention = CallingConvention.Cdecl)]
        static extern int reqQueryBankAccountMoneyByFuture(int requestID, ref CThostFtdcReqQueryAccountField pField);

        //回调函数 =====================================================================================================================
        [DllImport(dllName, EntryPoint = "?RegOnFrontConnected@@YGXP6GHXZ@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regOnFrontConnected(FrontConnect fc);
        FrontConnect frontConnect;
        /// <summary>
        /// 
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

        [DllImport(dllName, EntryPoint = "?RegOnFrontDisconnected@@YGXP6GHH@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regOnDisConnected(DisConnected dc);
        DisConnected disConnected;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reason"></param>
        public delegate void DisConnected(int reason);
        /// <summary>
        /// 当客户端与交易后台通信连接断开时，该方法被调用。当发生这个情况后，API会自动重新连接，客户端可不做处理。
        /// </summary>
        public event DisConnected OnDisConnected
        {
            add { disConnected += value; regOnDisConnected(disConnected); }
            remove { disConnected -= value; regOnDisConnected(disConnected); }
        }

        [DllImport(dllName, EntryPoint = "?RegOnHeartBeatWarning@@YGXP6GHH@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regOnHeartBeatWarning(HeartBeatWarning hbw);
        HeartBeatWarning heartBeatWarning;
        /// <summary>
        /// 
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

        ///期货发起银行资金转期货错误回报
        [DllImport(dllName, EntryPoint = "?RegErrRtnBankToFutureByFuture@@YGXP6GHPAUCThostFtdcReqTransferField@@PAUCThostFtdcRspInfoField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regErrRtnBankToFutureByFuture(ErrRtnBankToFutureByFuture cb);
        ErrRtnBankToFutureByFuture errRtnBankToFutureByFuture;

        /// <summary>
        /// 期货发起银行资金转期货错误回报委托
        /// </summary>
        /// <param name="pReqTransfer"></param>
        /// <param name="pRspInfo"></param>
		public delegate void ErrRtnBankToFutureByFuture(ref CThostFtdcReqTransferField pReqTransfer, ref CThostFtdcRspInfoField pRspInfo);
        /// <summary>
		/// 期货发起银行资金转期货错误回报
		/// </summary>
		public event ErrRtnBankToFutureByFuture OnErrRtnBankToFutureByFuture
        {
            add { errRtnBankToFutureByFuture += value; regErrRtnBankToFutureByFuture(errRtnBankToFutureByFuture); }
            remove { errRtnBankToFutureByFuture -= value; regErrRtnBankToFutureByFuture(errRtnBankToFutureByFuture); }
        }

        ///期货发起期货资金转银行错误回报
        [DllImport(dllName, EntryPoint = "?RegErrRtnFutureToBankByFuture@@YGXP6GHPAUCThostFtdcReqTransferField@@PAUCThostFtdcRspInfoField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regErrRtnFutureToBankByFuture(ErrRtnFutureToBankByFuture cb);
        ErrRtnFutureToBankByFuture errRtnFutureToBankByFuture;
        /// <summary>
        /// 期货发起期货资金转银行错误回报委托
        /// </summary>
        /// <param name="pReqTransfer"></param>
        /// <param name="pRspInfo"></param>
		public delegate void ErrRtnFutureToBankByFuture(ref CThostFtdcReqTransferField pReqTransfer, ref CThostFtdcRspInfoField pRspInfo);
        /// <summary>
        /// 期货发起期货资金转银行错误回报
        /// </summary>
        public event ErrRtnFutureToBankByFuture OnErrRtnFutureToBankByFuture
        {
            add { errRtnFutureToBankByFuture += value; regErrRtnFutureToBankByFuture(errRtnFutureToBankByFuture); }
            remove { errRtnFutureToBankByFuture -= value; regErrRtnFutureToBankByFuture(errRtnFutureToBankByFuture); }
        }

        ///报单操作错误回报
        [DllImport(dllName, EntryPoint = "?RegErrRtnOrderAction@@YGXP6GHPAUCThostFtdcOrderActionField@@PAUCThostFtdcRspInfoField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regErrRtnOrderAction(ErrRtnOrderAction cb);
        ErrRtnOrderAction errRtnOrderAction;
        /// <summary>
        /// 报单操作错误回报委托
        /// </summary>
        /// <param name="pOrderAction"></param>
        /// <param name="pRspInfo"></param>
		public delegate void ErrRtnOrderAction(ref CThostFtdcOrderActionField pOrderAction, ref CThostFtdcRspInfoField pRspInfo);
        /// <summary>
        /// 报单操作错误回报
        /// </summary>
        public event ErrRtnOrderAction OnErrRtnOrderAction
        {
            add { errRtnOrderAction += value; regErrRtnOrderAction(errRtnOrderAction); }
            remove { errRtnOrderAction -= value; regErrRtnOrderAction(errRtnOrderAction); }
        }

        ///报单录入错误回报
        [DllImport(dllName, EntryPoint = "?RegErrRtnOrderInsert@@YGXP6GHPAUCThostFtdcInputOrderField@@PAUCThostFtdcRspInfoField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regErrRtnOrderInsert(ErrRtnOrderInsert cb);
        ErrRtnOrderInsert errRtnOrderInsert;
        /// <summary>
        /// 报单录入错误回报委托
        /// </summary>
        /// <param name="pInputOrder"></param>
        /// <param name="pRspInfo"></param>
		public delegate void ErrRtnOrderInsert(ref CThostFtdcInputOrderField pInputOrder, ref CThostFtdcRspInfoField pRspInfo);
        /// <summary>
        /// 报单录入错误回报
        /// </summary>
        public event ErrRtnOrderInsert OnErrRtnOrderInsert
        {
            add { errRtnOrderInsert += value; regErrRtnOrderInsert(errRtnOrderInsert); }
            remove { errRtnOrderInsert -= value; regErrRtnOrderInsert(errRtnOrderInsert); }
        }

        ///期货发起查询银行余额错误回报
        [DllImport(dllName, EntryPoint = "?RegErrRtnQueryBankBalanceByFuture@@YGXP6GHPAUCThostFtdcReqQueryAccountField@@PAUCThostFtdcRspInfoField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regErrRtnQueryBankBalanceByFuture(ErrRtnQueryBankBalanceByFuture cb);
        ErrRtnQueryBankBalanceByFuture errRtnQueryBankBalanceByFuture;
        /// <summary>
        /// 期货发起查询银行余额错误回报委托
        /// </summary>
        /// <param name="pReqQueryAccount"></param>
        /// <param name="pRspInfo"></param>
		public delegate void ErrRtnQueryBankBalanceByFuture(ref CThostFtdcReqQueryAccountField pReqQueryAccount, ref CThostFtdcRspInfoField pRspInfo);
        /// <summary>
        /// 期货发起查询银行余额错误回报
        /// </summary>
        public event ErrRtnQueryBankBalanceByFuture OnErrRtnQueryBankBalanceByFuture
        {
            add { errRtnQueryBankBalanceByFuture += value; regErrRtnQueryBankBalanceByFuture(errRtnQueryBankBalanceByFuture); }
            remove { errRtnQueryBankBalanceByFuture -= value; regErrRtnQueryBankBalanceByFuture(errRtnQueryBankBalanceByFuture); }
        }

        ///系统运行时期货端手工发起冲正银行转期货错误回报
        [DllImport(dllName, EntryPoint = "?RegErrRtnRepealBankToFutureByFutureManual@@YGXP6GHPAUCThostFtdcReqRepealField@@PAUCThostFtdcRspInfoField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regErrRtnRepealBankToFutureByFutureManual(ErrRtnRepealBankToFutureByFutureManual cb);
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
            add { errRtnRepealBankToFutureByFutureManual += value; regErrRtnRepealBankToFutureByFutureManual(errRtnRepealBankToFutureByFutureManual); }
            remove { errRtnRepealBankToFutureByFutureManual -= value; regErrRtnRepealBankToFutureByFutureManual(errRtnRepealBankToFutureByFutureManual); }
        }

        ///系统运行时期货端手工发起冲正期货转银行错误回报
        [DllImport(dllName, EntryPoint = "?RegErrRtnRepealFutureToBankByFutureManual@@YGXP6GHPAUCThostFtdcReqRepealField@@PAUCThostFtdcRspInfoField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regErrRtnRepealFutureToBankByFutureManual(ErrRtnRepealFutureToBankByFutureManual cb);
        ErrRtnRepealFutureToBankByFutureManual errRtnRepealFutureToBankByFutureManual;
        /// <summary>
        /// 系统运行时期货端手工发起冲正期货转银行错误回报委托
        /// </summary>
        /// <param name="pReqRepeal"></param>
        /// <param name="pRspInfo"></param>
		public delegate void ErrRtnRepealFutureToBankByFutureManual(ref CThostFtdcReqRepealField pReqRepeal, ref CThostFtdcRspInfoField pRspInfo);
        /// <summary>
        /// 系统运行时期货端手工发起冲正期货转银行错误回报
        /// </summary>
        public event ErrRtnRepealFutureToBankByFutureManual OnErrRtnRepealFutureToBankByFutureManual
        {
            add { errRtnRepealFutureToBankByFutureManual += value; regErrRtnRepealFutureToBankByFutureManual(errRtnRepealFutureToBankByFutureManual); }
            remove { errRtnRepealFutureToBankByFutureManual -= value; regErrRtnRepealFutureToBankByFutureManual(errRtnRepealFutureToBankByFutureManual); }
        }

        ///错误应答
        [DllImport(dllName, EntryPoint = "?RegRspError@@YGXP6GHPAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspError(RspError cb);
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
            add { rspError += value; regRspError(rspError); }
            remove { rspError -= value; regRspError(rspError); }
        }

        ///期货发起银行资金转期货应答
        [DllImport(dllName, EntryPoint = "?RegRspFromBankToFutureByFuture@@YGXP6GHPAUCThostFtdcReqTransferField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspFromBankToFutureByFuture(RspFromBankToFutureByFuture cb);
        RspFromBankToFutureByFuture rspFromBankToFutureByFuture;
        /// <summary>
        /// 期货发起银行资金转期货应答委托
        /// </summary>
        /// <param name="pReqTransfer"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspFromBankToFutureByFuture(ref CThostFtdcReqTransferField pReqTransfer, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 期货发起银行资金转期货应答
        /// </summary>
        public event RspFromBankToFutureByFuture OnRspFromBankToFutureByFuture
        {
            add { rspFromBankToFutureByFuture += value; regRspFromBankToFutureByFuture(rspFromBankToFutureByFuture); }
            remove { rspFromBankToFutureByFuture -= value; regRspFromBankToFutureByFuture(rspFromBankToFutureByFuture); }
        }

        ///期货发起期货资金转银行应答
        [DllImport(dllName, EntryPoint = "?RegRspFromFutureToBankByFuture@@YGXP6GHPAUCThostFtdcReqTransferField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspFromFutureToBankByFuture(RspFromFutureToBankByFuture cb);
        RspFromFutureToBankByFuture rspFromFutureToBankByFuture;
        /// <summary>
        /// 期货发起期货资金转银行应答委托
        /// </summary>
        /// <param name="pReqTransfer"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspFromFutureToBankByFuture(ref CThostFtdcReqTransferField pReqTransfer, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 期货发起期货资金转银行应答
        /// </summary>
        public event RspFromFutureToBankByFuture OnRspFromFutureToBankByFuture
        {
            add { rspFromFutureToBankByFuture += value; regRspFromFutureToBankByFuture(rspFromFutureToBankByFuture); }
            remove { rspFromFutureToBankByFuture -= value; regRspFromFutureToBankByFuture(rspFromFutureToBankByFuture); }
        }

        ///报单操作请求响应
        [DllImport(dllName, EntryPoint = "?RegRspOrderAction@@YGXP6GHPAUCThostFtdcInputOrderActionField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspOrderAction(RspOrderAction cb);
        RspOrderAction rspOrderAction;
        /// <summary>
        /// 报单操作请求响应委托
        /// </summary>
        /// <param name="pInputOrderAction"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspOrderAction(ref CThostFtdcInputOrderActionField pInputOrderAction, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 报单操作请求响应
        /// </summary>
        public event RspOrderAction OnRspOrderAction
        {
            add { rspOrderAction += value; regRspOrderAction(rspOrderAction); }
            remove { rspOrderAction -= value; regRspOrderAction(rspOrderAction); }
        }

        ///报单录入请求响应
        [DllImport(dllName, EntryPoint = "?RegRspOrderInsert@@YGXP6GHPAUCThostFtdcInputOrderField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspOrderInsert(RspOrderInsert cb);
        RspOrderInsert rspOrderInsert;
        /// <summary>
        /// 报单录入请求响应委托
        /// </summary>
        /// <param name="pInputOrder"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspOrderInsert(ref CThostFtdcInputOrderField pInputOrder, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 报单录入请求响应
        /// </summary>
        public event RspOrderInsert OnRspOrderInsert
        {
            add { rspOrderInsert += value; regRspOrderInsert(rspOrderInsert); }
            remove { rspOrderInsert -= value; regRspOrderInsert(rspOrderInsert); }
        }

        ///预埋撤单录入请求响应
        [DllImport(dllName, EntryPoint = "?RegRspParkedOrderAction@@YGXP6GHPAUCThostFtdcParkedOrderActionField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspParkedOrderAction(RspParkedOrderAction cb);
        RspParkedOrderAction rspParkedOrderAction;
        /// <summary>
        /// 预埋撤单录入请求响应委托
        /// </summary>
        /// <param name="pParkedOrderAction"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspParkedOrderAction(ref CThostFtdcParkedOrderActionField pParkedOrderAction, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 预埋撤单录入请求响应
        /// </summary>
        public event RspParkedOrderAction OnRspParkedOrderAction
        {
            add { rspParkedOrderAction += value; regRspParkedOrderAction(rspParkedOrderAction); }
            remove { rspParkedOrderAction -= value; regRspParkedOrderAction(rspParkedOrderAction); }
        }

        ///预埋单录入请求响应
        [DllImport(dllName, EntryPoint = "?RegRspParkedOrderInsert@@YGXP6GHPAUCThostFtdcParkedOrderField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspParkedOrderInsert(RspParkedOrderInsert cb);
        RspParkedOrderInsert rspParkedOrderInsert;
        /// <summary>
        /// 预埋单录入请求响应委托
        /// </summary>
        /// <param name="pParkedOrder"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspParkedOrderInsert(ref CThostFtdcParkedOrderField pParkedOrder, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 预埋单录入请求响应
        /// </summary>
        public event RspParkedOrderInsert OnRspParkedOrderInsert
        {
            add { rspParkedOrderInsert += value; regRspParkedOrderInsert(rspParkedOrderInsert); }
            remove { rspParkedOrderInsert -= value; regRspParkedOrderInsert(rspParkedOrderInsert); }
        }

        ///请求查询经纪公司交易算法响应
        [DllImport(dllName, EntryPoint = "?RegRspQryBrokerTradingAlgos@@YGXP6GHPAUCThostFtdcBrokerTradingAlgosField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryBrokerTradingAlgos(RspQryBrokerTradingAlgos cb);
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
            add { rspQryBrokerTradingAlgos += value; regRspQryBrokerTradingAlgos(rspQryBrokerTradingAlgos); }
            remove { rspQryBrokerTradingAlgos -= value; regRspQryBrokerTradingAlgos(rspQryBrokerTradingAlgos); }
        }

        ///请求查询经纪公司交易参数响应
        [DllImport(dllName, EntryPoint = "?RegRspQryBrokerTradingParams@@YGXP6GHPAUCThostFtdcBrokerTradingParamsField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryBrokerTradingParams(RspQryBrokerTradingParams cb);
        RspQryBrokerTradingParams rspQryBrokerTradingParams;
        /// <summary>
        /// 请求查询经纪公司交易参数响应委托
        /// </summary>
        /// <param name="pBrokerTradingParams"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryBrokerTradingParams(ref CThostFtdcBrokerTradingParamsField pBrokerTradingParams, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询经纪公司交易参数响应
        /// </summary>
        public event RspQryBrokerTradingParams OnRspQryBrokerTradingParams
        {
            add { rspQryBrokerTradingParams += value; regRspQryBrokerTradingParams(rspQryBrokerTradingParams); }
            remove { rspQryBrokerTradingParams -= value; regRspQryBrokerTradingParams(rspQryBrokerTradingParams); }
        }

        ///查询保证金监管系统经纪公司资金账户密钥响应
        [DllImport(dllName, EntryPoint = "?RegRspQryCFMMCTradingAccountKey@@YGXP6GHPAUCThostFtdcCFMMCTradingAccountKeyField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryCFMMCTradingAccountKey(RspQryCFMMCTradingAccountKey cb);
        RspQryCFMMCTradingAccountKey rspQryCFMMCTradingAccountKey;
        /// <summary>
        /// 查询保证金监管系统经纪公司资金账户密钥响应委托
        /// </summary>
        /// <param name="pCFMMCTradingAccountKey"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryCFMMCTradingAccountKey(ref CThostFtdcCFMMCTradingAccountKeyField pCFMMCTradingAccountKey, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 查询保证金监管系统经纪公司资金账户密钥响应
        /// </summary>
        public event RspQryCFMMCTradingAccountKey OnRspQryCFMMCTradingAccountKey
        {
            add { rspQryCFMMCTradingAccountKey += value; regRspQryCFMMCTradingAccountKey(rspQryCFMMCTradingAccountKey); }
            remove { rspQryCFMMCTradingAccountKey -= value; regRspQryCFMMCTradingAccountKey(rspQryCFMMCTradingAccountKey); }
        }

        ///请求查询签约银行响应
        [DllImport(dllName, EntryPoint = "?RegRspQryContractBank@@YGXP6GHPAUCThostFtdcContractBankField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryContractBank(RspQryContractBank cb);
        RspQryContractBank rspQryContractBank;
        /// <summary>
        /// 请求查询签约银行响应委托
        /// </summary>
        /// <param name="pContractBank"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryContractBank(ref CThostFtdcContractBankField pContractBank, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询签约银行响应
        /// </summary>
        public event RspQryContractBank OnRspQryContractBank
        {
            add { rspQryContractBank += value; regRspQryContractBank(rspQryContractBank); }
            remove { rspQryContractBank -= value; regRspQryContractBank(rspQryContractBank); }
        }

        ///请求查询行情响应
        [DllImport(dllName, EntryPoint = "?RegRspQryDepthMarketData@@YGXP6GHPAUCThostFtdcDepthMarketDataField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryDepthMarketData(RspQryDepthMarketData cb);
        RspQryDepthMarketData rspQryDepthMarketData;
        /// <summary>
        /// 请求查询行情响应委托
        /// </summary>
        /// <param name="pDepthMarketData"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryDepthMarketData(ref CThostFtdcDepthMarketDataField pDepthMarketData, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询行情响应
        /// </summary>
        public event RspQryDepthMarketData OnRspQryDepthMarketData
        {
            add { rspQryDepthMarketData += value; regRspQryDepthMarketData(rspQryDepthMarketData); }
            remove { rspQryDepthMarketData -= value; regRspQryDepthMarketData(rspQryDepthMarketData); }
        }

        ///请求查询交易所响应
        [DllImport(dllName, EntryPoint = "?RegRspQryExchange@@YGXP6GHPAUCThostFtdcExchangeField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryExchange(RspQryExchange cb);
        RspQryExchange rspQryExchange;
        /// <summary>
        /// 请求查询交易所响应委托
        /// </summary>
        /// <param name="pExchange"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryExchange(ref CThostFtdcExchangeField pExchange, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询交易所响应
        /// </summary>
        public event RspQryExchange OnRspQryExchange
        {
            add { rspQryExchange += value; regRspQryExchange(rspQryExchange); }
            remove { rspQryExchange -= value; regRspQryExchange(rspQryExchange); }
        }

        ///请求查询合约响应
        [DllImport(dllName, EntryPoint = "?RegRspQryInstrument@@YGXP6GHPAUCThostFtdcInstrumentField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryInstrument(RspQryInstrument cb);
        RspQryInstrument rspQryInstrument;
        /// <summary>
        /// 请求查询合约响应委托
        /// </summary>
        /// <param name="pInstrument"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInstrument(ref CThostFtdcInstrumentField pInstrument, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询合约响应
        /// </summary>
        public event RspQryInstrument OnRspQryInstrument
        {
            add { rspQryInstrument += value; regRspQryInstrument(rspQryInstrument); }
            remove { rspQryInstrument -= value; regRspQryInstrument(rspQryInstrument); }
        }

        ///请求查询合约手续费率响应
        [DllImport(dllName, EntryPoint = "?RegRspQryInstrumentCommissionRate@@YGXP6GHPAUCThostFtdcInstrumentCommissionRateField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryInstrumentCommissionRate(RspQryInstrumentCommissionRate cb);
        RspQryInstrumentCommissionRate rspQryInstrumentCommissionRate;
        /// <summary>
        /// 请求查询合约手续费率响应委托
        /// </summary>
        /// <param name="pInstrumentCommissionRate"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInstrumentCommissionRate(ref CThostFtdcInstrumentCommissionRateField pInstrumentCommissionRate, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询合约手续费率响应
        /// </summary>
        public event RspQryInstrumentCommissionRate OnRspQryInstrumentCommissionRate
        {
            add { rspQryInstrumentCommissionRate += value; regRspQryInstrumentCommissionRate(rspQryInstrumentCommissionRate); }
            remove { rspQryInstrumentCommissionRate -= value; regRspQryInstrumentCommissionRate(rspQryInstrumentCommissionRate); }
        }

        ///请求查询合约保证金率响应
        [DllImport(dllName, EntryPoint = "?RegRspQryInstrumentMarginRate@@YGXP6GHPAUCThostFtdcInstrumentMarginRateField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryInstrumentMarginRate(RspQryInstrumentMarginRate cb);
        RspQryInstrumentMarginRate rspQryInstrumentMarginRate;
        /// <summary>
        /// 请求查询合约保证金率响应委托
        /// </summary>
        /// <param name="pInstrumentMarginRate"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInstrumentMarginRate(ref CThostFtdcInstrumentMarginRateField pInstrumentMarginRate, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询合约保证金率响应
        /// </summary>
        public event RspQryInstrumentMarginRate OnRspQryInstrumentMarginRate
        {
            add { rspQryInstrumentMarginRate += value; regRspQryInstrumentMarginRate(rspQryInstrumentMarginRate); }
            remove { rspQryInstrumentMarginRate -= value; regRspQryInstrumentMarginRate(rspQryInstrumentMarginRate); }
        }

        ///请求查询投资者响应
        [DllImport(dllName, EntryPoint = "?RegRspQryInvestor@@YGXP6GHPAUCThostFtdcInvestorField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryInvestor(RspQryInvestor cb);
        RspQryInvestor rspQryInvestor;
        /// <summary>
        /// 请求查询投资者响应委托
        /// </summary>
        /// <param name="pInvestor"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInvestor(ref CThostFtdcInvestorField pInvestor, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询投资者响应
        /// </summary>
        public event RspQryInvestor OnRspQryInvestor
        {
            add { rspQryInvestor += value; regRspQryInvestor(rspQryInvestor); }
            remove { rspQryInvestor -= value; regRspQryInvestor(rspQryInvestor); }
        }

        ///请求查询投资者持仓响应
        [DllImport(dllName, EntryPoint = "?RegRspQryInvestorPosition@@YGXP6GHPAUCThostFtdcInvestorPositionField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryInvestorPosition(RspQryInvestorPosition cb);
        RspQryInvestorPosition rspQryInvestorPosition;
        /// <summary>
        /// 请求查询投资者持仓响应委托
        /// </summary>
        /// <param name="pInvestorPosition"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInvestorPosition(ref CThostFtdcInvestorPositionField pInvestorPosition, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询投资者持仓响应
        /// </summary>
        public event RspQryInvestorPosition OnRspQryInvestorPosition
        {
            add { rspQryInvestorPosition += value; regRspQryInvestorPosition(rspQryInvestorPosition); }
            remove { rspQryInvestorPosition -= value; regRspQryInvestorPosition(rspQryInvestorPosition); }
        }

        ///请求查询投资者持仓明细响应
        [DllImport(dllName, EntryPoint = "?RegRspQryInvestorPositionCombineDetail@@YGXP6GHPAUCThostFtdcInvestorPositionCombineDetailField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryInvestorPositionCombineDetail(RspQryInvestorPositionCombineDetail cb);
        RspQryInvestorPositionCombineDetail rspQryInvestorPositionCombineDetail;
        /// <summary>
        /// 请求查询投资者持仓明细响应委托
        /// </summary>
        /// <param name="pInvestorPositionCombineDetail"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInvestorPositionCombineDetail(ref CThostFtdcInvestorPositionCombineDetailField pInvestorPositionCombineDetail, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询投资者持仓明细响应
        /// </summary>
        public event RspQryInvestorPositionCombineDetail OnRspQryInvestorPositionCombineDetail
        {
            add { rspQryInvestorPositionCombineDetail += value; regRspQryInvestorPositionCombineDetail(rspQryInvestorPositionCombineDetail); }
            remove { rspQryInvestorPositionCombineDetail -= value; regRspQryInvestorPositionCombineDetail(rspQryInvestorPositionCombineDetail); }
        }

        ///请求查询投资者持仓明细响应
        [DllImport(dllName, EntryPoint = "?RegRspQryInvestorPositionDetail@@YGXP6GHPAUCThostFtdcInvestorPositionDetailField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryInvestorPositionDetail(RspQryInvestorPositionDetail cb);
        RspQryInvestorPositionDetail rspQryInvestorPositionDetail;
        /// <summary>
        /// 请求查询投资者持仓明细响应委托
        /// </summary>
        /// <param name="pInvestorPositionDetail"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryInvestorPositionDetail(ref CThostFtdcInvestorPositionDetailField pInvestorPositionDetail, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询投资者持仓明细响应
        /// </summary>
        public event RspQryInvestorPositionDetail OnRspQryInvestorPositionDetail
        {
            add { rspQryInvestorPositionDetail += value; regRspQryInvestorPositionDetail(rspQryInvestorPositionDetail); }
            remove { rspQryInvestorPositionDetail -= value; regRspQryInvestorPositionDetail(rspQryInvestorPositionDetail); }
        }

        ///请求查询客户通知响应
        [DllImport(dllName, EntryPoint = "?RegRspQryNotice@@YGXP6GHPAUCThostFtdcNoticeField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryNotice(RspQryNotice cb);
        RspQryNotice rspQryNotice;
        /// <summary>
        /// 请求查询客户通知响应委托
        /// </summary>
        /// <param name="pNotice"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryNotice(ref CThostFtdcNoticeField pNotice, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询客户通知响应
        /// </summary>
        public event RspQryNotice OnRspQryNotice
        {
            add { rspQryNotice += value; regRspQryNotice(rspQryNotice); }
            remove { rspQryNotice -= value; regRspQryNotice(rspQryNotice); }
        }

        ///请求查询报单响应
        [DllImport(dllName, EntryPoint = "?RegRspQryOrder@@YGXP6GHPAUCThostFtdcOrderField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryOrder(RspQryOrder cb);
        RspQryOrder rspQryOrder;
        /// <summary>
        /// 请求查询报单响应委托
        /// </summary>
        /// <param name="pOrder"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryOrder(ref CThostFtdcOrderField pOrder, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询报单响应
        /// </summary>
        public event RspQryOrder OnRspQryOrder
        {
            add { rspQryOrder += value; regRspQryOrder(rspQryOrder); }
            remove { rspQryOrder -= value; regRspQryOrder(rspQryOrder); }
        }

        ///请求查询预埋单响应
        [DllImport(dllName, EntryPoint = "?RegRspQryParkedOrder@@YGXP6GHPAUCThostFtdcParkedOrderField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryParkedOrder(RspQryParkedOrder cb);
        RspQryParkedOrder rspQryParkedOrder;
        /// <summary>
        /// 请求查询预埋单响应委托
        /// </summary>
        /// <param name="pParkedOrder"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryParkedOrder(ref CThostFtdcParkedOrderField pParkedOrder, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询预埋单响应
        /// </summary>
        public event RspQryParkedOrder OnRspQryParkedOrder
        {
            add { rspQryParkedOrder += value; regRspQryParkedOrder(rspQryParkedOrder); }
            remove { rspQryParkedOrder -= value; regRspQryParkedOrder(rspQryParkedOrder); }
        }

        ///请求查询预埋撤单响应
        [DllImport(dllName, EntryPoint = "?RegRspQryParkedOrderAction@@YGXP6GHPAUCThostFtdcParkedOrderActionField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryParkedOrderAction(RspQryParkedOrderAction cb);
        RspQryParkedOrderAction rspQryParkedOrderAction;
        /// <summary>
        /// 请求查询预埋撤单响应委托
        /// </summary>
        /// <param name="pParkedOrderAction"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryParkedOrderAction(ref CThostFtdcParkedOrderActionField pParkedOrderAction, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询预埋撤单响应
        /// </summary>
        public event RspQryParkedOrderAction OnRspQryParkedOrderAction
        {
            add { rspQryParkedOrderAction += value; regRspQryParkedOrderAction(rspQryParkedOrderAction); }
            remove { rspQryParkedOrderAction -= value; regRspQryParkedOrderAction(rspQryParkedOrderAction); }
        }

        ///请求查询投资者结算结果响应
        [DllImport(dllName, EntryPoint = "?RegRspQrySettlementInfo@@YGXP6GHPAUCThostFtdcSettlementInfoField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQrySettlementInfo(RspQrySettlementInfo cb);
        RspQrySettlementInfo rspQrySettlementInfo;
        /// <summary>
        /// 请求查询投资者结算结果响应委托
        /// </summary>
        /// <param name="pSettlementInfo"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQrySettlementInfo(ref CThostFtdcSettlementInfoField pSettlementInfo, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询投资者结算结果响应
        /// </summary>
        public event RspQrySettlementInfo OnRspQrySettlementInfo
        {
            add { rspQrySettlementInfo += value; regRspQrySettlementInfo(rspQrySettlementInfo); }
            remove { rspQrySettlementInfo -= value; regRspQrySettlementInfo(rspQrySettlementInfo); }
        }

        ///请求查询结算信息确认响应
        [DllImport(dllName, EntryPoint = "?RegRspQrySettlementInfoConfirm@@YGXP6GHPAUCThostFtdcSettlementInfoConfirmField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQrySettlementInfoConfirm(RspQrySettlementInfoConfirm cb);
        RspQrySettlementInfoConfirm rspQrySettlementInfoConfirm;
        /// <summary>
        /// 请求查询结算信息确认响应委托
        /// </summary>
        /// <param name="pSettlementInfoConfirm"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQrySettlementInfoConfirm(ref CThostFtdcSettlementInfoConfirmField pSettlementInfoConfirm, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询结算信息确认响应
        /// </summary>
        public event RspQrySettlementInfoConfirm OnRspQrySettlementInfoConfirm
        {
            add { rspQrySettlementInfoConfirm += value; regRspQrySettlementInfoConfirm(rspQrySettlementInfoConfirm); }
            remove { rspQrySettlementInfoConfirm -= value; regRspQrySettlementInfoConfirm(rspQrySettlementInfoConfirm); }
        }

        ///请求查询成交响应
        [DllImport(dllName, EntryPoint = "?RegRspQryTrade@@YGXP6GHPAUCThostFtdcTradeField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryTrade(RspQryTrade cb);
        RspQryTrade rspQryTrade;
        /// <summary>
        /// 请求查询成交响应委托
        /// </summary>
        /// <param name="pTrade"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryTrade(ref CThostFtdcTradeField pTrade, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询成交响应
        /// </summary>
        public event RspQryTrade OnRspQryTrade
        {
            add { rspQryTrade += value; regRspQryTrade(rspQryTrade); }
            remove { rspQryTrade -= value; regRspQryTrade(rspQryTrade); }
        }

        ///请求查询资金账户响应
        [DllImport(dllName, EntryPoint = "?RegRspQryTradingAccount@@YGXP6GHPAUCThostFtdcTradingAccountField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryTradingAccount(RspQryTradingAccount cb);
        RspQryTradingAccount rspQryTradingAccount;
        /// <summary>
        /// 请求查询资金账户响应委托
        /// </summary>
        /// <param name="pTradingAccount"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryTradingAccount(ref CThostFtdcTradingAccountField pTradingAccount, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询资金账户响应
        /// </summary>
        public event RspQryTradingAccount OnRspQryTradingAccount
        {
            add { rspQryTradingAccount += value; regRspQryTradingAccount(rspQryTradingAccount); }
            remove { rspQryTradingAccount -= value; regRspQryTradingAccount(rspQryTradingAccount); }
        }

        ///请求查询交易编码响应
        [DllImport(dllName, EntryPoint = "?RegRspQryTradingCode@@YGXP6GHPAUCThostFtdcTradingCodeField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryTradingCode(RspQryTradingCode cb);
        RspQryTradingCode rspQryTradingCode;
        /// <summary>
        /// 请求查询交易编码响应委托
        /// </summary>
        /// <param name="pTradingCode"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryTradingCode(ref CThostFtdcTradingCodeField pTradingCode, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询交易编码响应
        /// </summary>
        public event RspQryTradingCode OnRspQryTradingCode
        {
            add { rspQryTradingCode += value; regRspQryTradingCode(rspQryTradingCode); }
            remove { rspQryTradingCode -= value; regRspQryTradingCode(rspQryTradingCode); }
        }

        ///请求查询交易通知响应
        [DllImport(dllName, EntryPoint = "?RegRspQryTradingNotice@@YGXP6GHPAUCThostFtdcTradingNoticeField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryTradingNotice(RspQryTradingNotice cb);
        RspQryTradingNotice rspQryTradingNotice;
        /// <summary>
        /// 请求查询交易通知响应委托
        /// </summary>
        /// <param name="pTradingNotice"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryTradingNotice(ref CThostFtdcTradingNoticeField pTradingNotice, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询交易通知响应
        /// </summary>
        public event RspQryTradingNotice OnRspQryTradingNotice
        {
            add { rspQryTradingNotice += value; regRspQryTradingNotice(rspQryTradingNotice); }
            remove { rspQryTradingNotice -= value; regRspQryTradingNotice(rspQryTradingNotice); }
        }

        ///请求查询转帐银行响应
        [DllImport(dllName, EntryPoint = "?RegRspQryTransferBank@@YGXP6GHPAUCThostFtdcTransferBankField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryTransferBank(RspQryTransferBank cb);
        RspQryTransferBank rspQryTransferBank;
        /// <summary>
        /// 请求查询转帐银行响应委托
        /// </summary>
        /// <param name="pTransferBank"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryTransferBank(ref CThostFtdcTransferBankField pTransferBank, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询转帐银行响应
        /// </summary>
        public event RspQryTransferBank OnRspQryTransferBank
        {
            add { rspQryTransferBank += value; regRspQryTransferBank(rspQryTransferBank); }
            remove { rspQryTransferBank -= value; regRspQryTransferBank(rspQryTransferBank); }
        }

        ///请求查询转帐流水响应
        [DllImport(dllName, EntryPoint = "?RegRspQryTransferSerial@@YGXP6GHPAUCThostFtdcTransferSerialField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryTransferSerial(RspQryTransferSerial cb);
        RspQryTransferSerial rspQryTransferSerial;
        /// <summary>
        /// 请求查询转帐流水响应委托
        /// </summary>
        /// <param name="pTransferSerial"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQryTransferSerial(ref CThostFtdcTransferSerialField pTransferSerial, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询转帐流水响应
        /// </summary>
        public event RspQryTransferSerial OnRspQryTransferSerial
        {
            add { rspQryTransferSerial += value; regRspQryTransferSerial(rspQryTransferSerial); }
            remove { rspQryTransferSerial -= value; regRspQryTransferSerial(rspQryTransferSerial); }
        }

        ///请求查询银期签约关系响应
        [DllImport(dllName, EntryPoint = "?RegRspQryAccountregister@@YGXP6GHPAUCThostFtdcAccountregisterField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQryAccountregister(RspQryAccountregister cb);
        RspQryAccountregister rspQryAccountregister;
        /// <summary>
        /// 请求查询银期签约关系响应委托
        /// </summary>
        /// <param name="pAccountregister"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
        public delegate void RspQryAccountregister(ref CThostFtdcAccountregisterField pAccountregister, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 请求查询转帐流水响应
        /// </summary>
        public event RspQryAccountregister OnRspQryAccountregister
        {
            add { rspQryAccountregister += value; regRspQryAccountregister(rspQryAccountregister); }
            remove { rspQryAccountregister -= value; regRspQryAccountregister(rspQryAccountregister); }
        }


        ///期货发起查询银行余额应答
        [DllImport(dllName, EntryPoint = "?RegRspQueryBankAccountMoneyByFuture@@YGXP6GHPAUCThostFtdcReqQueryAccountField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQueryBankAccountMoneyByFuture(RspQueryBankAccountMoneyByFuture cb);
        RspQueryBankAccountMoneyByFuture rspQueryBankAccountMoneyByFuture;
        /// <summary>
        /// 期货发起查询银行余额应答委托
        /// </summary>
        /// <param name="pReqQueryAccount"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQueryBankAccountMoneyByFuture(ref CThostFtdcReqQueryAccountField pReqQueryAccount, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 期货发起查询银行余额应答
        /// </summary>
        public event RspQueryBankAccountMoneyByFuture OnRspQueryBankAccountMoneyByFuture
        {
            add { rspQueryBankAccountMoneyByFuture += value; regRspQueryBankAccountMoneyByFuture(rspQueryBankAccountMoneyByFuture); }
            remove { rspQueryBankAccountMoneyByFuture -= value; regRspQueryBankAccountMoneyByFuture(rspQueryBankAccountMoneyByFuture); }
        }

        ///查询最大报单数量响应
        [DllImport(dllName, EntryPoint = "?RegRspQueryMaxOrderVolume@@YGXP6GHPAUCThostFtdcQueryMaxOrderVolumeField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspQueryMaxOrderVolume(RspQueryMaxOrderVolume cb);
        RspQueryMaxOrderVolume rspQueryMaxOrderVolume;
        /// <summary>
        /// 查询最大报单数量响应委托
        /// </summary>
        /// <param name="pQueryMaxOrderVolume"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspQueryMaxOrderVolume(ref CThostFtdcQueryMaxOrderVolumeField pQueryMaxOrderVolume, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 查询最大报单数量响应
        /// </summary>
        public event RspQueryMaxOrderVolume OnRspQueryMaxOrderVolume
        {
            add { rspQueryMaxOrderVolume += value; regRspQueryMaxOrderVolume(rspQueryMaxOrderVolume); }
            remove { rspQueryMaxOrderVolume -= value; regRspQueryMaxOrderVolume(rspQueryMaxOrderVolume); }
        }

        ///删除预埋单响应
        [DllImport(dllName, EntryPoint = "?RegRspRemoveParkedOrder@@YGXP6GHPAUCThostFtdcRemoveParkedOrderField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspRemoveParkedOrder(RspRemoveParkedOrder cb);
        RspRemoveParkedOrder rspRemoveParkedOrder;
        /// <summary>
        /// 删除预埋单响应委托
        /// </summary>
        /// <param name="pRemoveParkedOrder"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspRemoveParkedOrder(ref CThostFtdcRemoveParkedOrderField pRemoveParkedOrder, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 删除预埋单响应
        /// </summary>
        public event RspRemoveParkedOrder OnRspRemoveParkedOrder
        {
            add { rspRemoveParkedOrder += value; regRspRemoveParkedOrder(rspRemoveParkedOrder); }
            remove { rspRemoveParkedOrder -= value; regRspRemoveParkedOrder(rspRemoveParkedOrder); }
        }

        ///删除预埋撤单响应
        [DllImport(dllName, EntryPoint = "?RegRspRemoveParkedOrderAction@@YGXP6GHPAUCThostFtdcRemoveParkedOrderActionField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspRemoveParkedOrderAction(RspRemoveParkedOrderAction cb);
        RspRemoveParkedOrderAction rspRemoveParkedOrderAction;
        /// <summary>
        /// 删除预埋撤单响应委托
        /// </summary>
        /// <param name="pRemoveParkedOrderAction"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspRemoveParkedOrderAction(ref CThostFtdcRemoveParkedOrderActionField pRemoveParkedOrderAction, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 删除预埋撤单响应
        /// </summary>
        public event RspRemoveParkedOrderAction OnRspRemoveParkedOrderAction
        {
            add { rspRemoveParkedOrderAction += value; regRspRemoveParkedOrderAction(rspRemoveParkedOrderAction); }
            remove { rspRemoveParkedOrderAction -= value; regRspRemoveParkedOrderAction(rspRemoveParkedOrderAction); }
        }

        ///投资者结算结果确认响应
        [DllImport(dllName, EntryPoint = "?RegRspSettlementInfoConfirm@@YGXP6GHPAUCThostFtdcSettlementInfoConfirmField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspSettlementInfoConfirm(RspSettlementInfoConfirm cb);
        RspSettlementInfoConfirm rspSettlementInfoConfirm;
        /// <summary>
        /// 投资者结算结果确认响应委托
        /// </summary>
        /// <param name="pSettlementInfoConfirm"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspSettlementInfoConfirm(ref CThostFtdcSettlementInfoConfirmField pSettlementInfoConfirm, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 投资者结算结果确认响应
        /// </summary>
        public event RspSettlementInfoConfirm OnRspSettlementInfoConfirm
        {
            add { rspSettlementInfoConfirm += value; regRspSettlementInfoConfirm(rspSettlementInfoConfirm); }
            remove { rspSettlementInfoConfirm -= value; regRspSettlementInfoConfirm(rspSettlementInfoConfirm); }
        }

        ///资金账户口令更新请求响应
        [DllImport(dllName, EntryPoint = "?RegRspTradingAccountPasswordUpdate@@YGXP6GHPAUCThostFtdcTradingAccountPasswordUpdateField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspTradingAccountPasswordUpdate(RspTradingAccountPasswordUpdate cb);
        RspTradingAccountPasswordUpdate rspTradingAccountPasswordUpdate;
        /// <summary>
        /// 资金账户口令更新请求响应委托
        /// </summary>
        /// <param name="pTradingAccountPasswordUpdate"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspTradingAccountPasswordUpdate(ref CThostFtdcTradingAccountPasswordUpdateField pTradingAccountPasswordUpdate, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 资金账户口令更新请求响应
        /// </summary>
        public event RspTradingAccountPasswordUpdate OnRspTradingAccountPasswordUpdate
        {
            add { rspTradingAccountPasswordUpdate += value; regRspTradingAccountPasswordUpdate(rspTradingAccountPasswordUpdate); }
            remove { rspTradingAccountPasswordUpdate -= value; regRspTradingAccountPasswordUpdate(rspTradingAccountPasswordUpdate); }
        }

        ///登录请求响应
        [DllImport(dllName, EntryPoint = "?RegRspUserLogin@@YGXP6GHPAUCThostFtdcRspUserLoginField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspUserLogin(RspUserLogin cb);
        RspUserLogin rspUserLogin;
        /// <summary>
        /// 登录请求响应委托
        /// </summary>
        /// <param name="pRspUserLogin"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspUserLogin(ref CThostFtdcRspUserLoginField pRspUserLogin, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 登录请求响应
        /// </summary>
        public event RspUserLogin OnRspUserLogin
        {
            add { rspUserLogin += value; regRspUserLogin(rspUserLogin); }
            remove { rspUserLogin -= value; regRspUserLogin(rspUserLogin); }
        }

        ///登出请求响应
        [DllImport(dllName, EntryPoint = "?RegRspUserLogout@@YGXP6GHPAUCThostFtdcUserLogoutField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspUserLogout(RspUserLogout cb);
        RspUserLogout rspUserLogout;
        /// <summary>
        /// 登出请求响应委托
        /// </summary>
        /// <param name="pUserLogout"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspUserLogout(ref CThostFtdcUserLogoutField pUserLogout, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 登出请求响应
        /// </summary>
        public event RspUserLogout OnRspUserLogout
        {
            add { rspUserLogout += value; regRspUserLogout(rspUserLogout); }
            remove { rspUserLogout -= value; regRspUserLogout(rspUserLogout); }
        }

        ///用户口令更新请求响应
        [DllImport(dllName, EntryPoint = "?RegRspUserPasswordUpdate@@YGXP6GHPAUCThostFtdcUserPasswordUpdateField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRspUserPasswordUpdate(RspUserPasswordUpdate cb);
        RspUserPasswordUpdate rspUserPasswordUpdate;
        /// <summary>
        /// 用户口令更新请求响应委托
        /// </summary>
        /// <param name="pUserPasswordUpdate"></param>
        /// <param name="pRspInfo"></param>
        /// <param name="nRequestID"></param>
        /// <param name="bIsLast"></param>
		public delegate void RspUserPasswordUpdate(ref CThostFtdcUserPasswordUpdateField pUserPasswordUpdate, ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        /// <summary>
        /// 用户口令更新请求响应
        /// </summary>
        public event RspUserPasswordUpdate OnRspUserPasswordUpdate
        {
            add { rspUserPasswordUpdate += value; regRspUserPasswordUpdate(rspUserPasswordUpdate); }
            remove { rspUserPasswordUpdate -= value; regRspUserPasswordUpdate(rspUserPasswordUpdate); }
        }

        ///提示条件单校验错误
        [DllImport(dllName, EntryPoint = "?RegRtnErrorConditionalOrder@@YGXP6GHPAUCThostFtdcErrorConditionalOrderField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnErrorConditionalOrder(RtnErrorConditionalOrder cb);
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
            add { rtnErrorConditionalOrder += value; regRtnErrorConditionalOrder(rtnErrorConditionalOrder); }
            remove { rtnErrorConditionalOrder -= value; regRtnErrorConditionalOrder(rtnErrorConditionalOrder); }
        }

        ///银行发起银行资金转期货通知
        [DllImport(dllName, EntryPoint = "?RegRtnFromBankToFutureByBank@@YGXP6GHPAUCThostFtdcRspTransferField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnFromBankToFutureByBank(RtnFromBankToFutureByBank cb);
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
            add { rtnFromBankToFutureByBank += value; regRtnFromBankToFutureByBank(rtnFromBankToFutureByBank); }
            remove { rtnFromBankToFutureByBank -= value; regRtnFromBankToFutureByBank(rtnFromBankToFutureByBank); }
        }

        ///期货发起银行资金转期货通知
        [DllImport(dllName, EntryPoint = "?RegRtnFromBankToFutureByFuture@@YGXP6GHPAUCThostFtdcRspTransferField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnFromBankToFutureByFuture(RtnFromBankToFutureByFuture cb);
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
            add { rtnFromBankToFutureByFuture += value; regRtnFromBankToFutureByFuture(rtnFromBankToFutureByFuture); }
            remove { rtnFromBankToFutureByFuture -= value; regRtnFromBankToFutureByFuture(rtnFromBankToFutureByFuture); }
        }

        ///银行发起期货资金转银行通知
        [DllImport(dllName, EntryPoint = "?RegRtnFromFutureToBankByBank@@YGXP6GHPAUCThostFtdcRspTransferField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnFromFutureToBankByBank(RtnFromFutureToBankByBank cb);
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
            add { rtnFromFutureToBankByBank += value; regRtnFromFutureToBankByBank(rtnFromFutureToBankByBank); }
            remove { rtnFromFutureToBankByBank -= value; regRtnFromFutureToBankByBank(rtnFromFutureToBankByBank); }
        }

        ///期货发起期货资金转银行通知
        [DllImport(dllName, EntryPoint = "?RegRtnFromFutureToBankByFuture@@YGXP6GHPAUCThostFtdcRspTransferField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnFromFutureToBankByFuture(RtnFromFutureToBankByFuture cb);
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
            add { rtnFromFutureToBankByFuture += value; regRtnFromFutureToBankByFuture(rtnFromFutureToBankByFuture); }
            remove { rtnFromFutureToBankByFuture -= value; regRtnFromFutureToBankByFuture(rtnFromFutureToBankByFuture); }
        }

        ///合约交易状态通知
        [DllImport(dllName, EntryPoint = "?RegRtnInstrumentStatus@@YGXP6GHPAUCThostFtdcInstrumentStatusField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnInstrumentStatus(RtnInstrumentStatus cb);
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
            add { rtnInstrumentStatus += value; regRtnInstrumentStatus(rtnInstrumentStatus); }
            remove { rtnInstrumentStatus -= value; regRtnInstrumentStatus(rtnInstrumentStatus); }
        }

        ///报单通知
        [DllImport(dllName, EntryPoint = "?RegRtnOrder@@YGXP6GHPAUCThostFtdcOrderField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnOrder(RtnOrder cb);
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
            add { rtnOrder += value; regRtnOrder(rtnOrder); }
            remove { rtnOrder -= value; regRtnOrder(rtnOrder); }
        }

        ///期货发起查询银行余额通知
        [DllImport(dllName, EntryPoint = "?RegRtnQueryBankBalanceByFuture@@YGXP6GHPAUCThostFtdcNotifyQueryAccountField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnQueryBankBalanceByFuture(RtnQueryBankBalanceByFuture cb);
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
            add { rtnQueryBankBalanceByFuture += value; regRtnQueryBankBalanceByFuture(rtnQueryBankBalanceByFuture); }
            remove { rtnQueryBankBalanceByFuture -= value; regRtnQueryBankBalanceByFuture(rtnQueryBankBalanceByFuture); }
        }

        ///银行发起冲正银行转期货通知
        [DllImport(dllName, EntryPoint = "?RegRtnRepealFromBankToFutureByBank@@YGXP6GHPAUCThostFtdcRspRepealField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnRepealFromBankToFutureByBank(RtnRepealFromBankToFutureByBank cb);
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
            add { rtnRepealFromBankToFutureByBank += value; regRtnRepealFromBankToFutureByBank(rtnRepealFromBankToFutureByBank); }
            remove { rtnRepealFromBankToFutureByBank -= value; regRtnRepealFromBankToFutureByBank(rtnRepealFromBankToFutureByBank); }
        }

        ///期货发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
        [DllImport(dllName, EntryPoint = "?RegRtnRepealFromBankToFutureByFuture@@YGXP6GHPAUCThostFtdcRspRepealField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnRepealFromBankToFutureByFuture(RtnRepealFromBankToFutureByFuture cb);
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
            add { rtnRepealFromBankToFutureByFuture += value; regRtnRepealFromBankToFutureByFuture(rtnRepealFromBankToFutureByFuture); }
            remove { rtnRepealFromBankToFutureByFuture -= value; regRtnRepealFromBankToFutureByFuture(rtnRepealFromBankToFutureByFuture); }
        }

        ///系统运行时期货端手工发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
        [DllImport(dllName, EntryPoint = "?RegRtnRepealFromBankToFutureByFutureManual@@YGXP6GHPAUCThostFtdcRspRepealField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnRepealFromBankToFutureByFutureManual(RtnRepealFromBankToFutureByFutureManual cb);
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
            add { rtnRepealFromBankToFutureByFutureManual += value; regRtnRepealFromBankToFutureByFutureManual(rtnRepealFromBankToFutureByFutureManual); }
            remove { rtnRepealFromBankToFutureByFutureManual -= value; regRtnRepealFromBankToFutureByFutureManual(rtnRepealFromBankToFutureByFutureManual); }
        }

        ///银行发起冲正期货转银行通知
        [DllImport(dllName, EntryPoint = "?RegRtnRepealFromFutureToBankByBank@@YGXP6GHPAUCThostFtdcRspRepealField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnRepealFromFutureToBankByBank(RtnRepealFromFutureToBankByBank cb);
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
            add { rtnRepealFromFutureToBankByBank += value; regRtnRepealFromFutureToBankByBank(rtnRepealFromFutureToBankByBank); }
            remove { rtnRepealFromFutureToBankByBank -= value; regRtnRepealFromFutureToBankByBank(rtnRepealFromFutureToBankByBank); }
        }

        ///期货发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
        [DllImport(dllName, EntryPoint = "?RegRtnRepealFromFutureToBankByFuture@@YGXP6GHPAUCThostFtdcRspRepealField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnRepealFromFutureToBankByFuture(RtnRepealFromFutureToBankByFuture cb);
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
            add { rtnRepealFromFutureToBankByFuture += value; regRtnRepealFromFutureToBankByFuture(rtnRepealFromFutureToBankByFuture); }
            remove { rtnRepealFromFutureToBankByFuture -= value; regRtnRepealFromFutureToBankByFuture(rtnRepealFromFutureToBankByFuture); }
        }

        ///系统运行时期货端手工发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
        [DllImport(dllName, EntryPoint = "?RegRtnRepealFromFutureToBankByFutureManual@@YGXP6GHPAUCThostFtdcRspRepealField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnRepealFromFutureToBankByFutureManual(RtnRepealFromFutureToBankByFutureManual cb);
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
            add { rtnRepealFromFutureToBankByFutureManual += value; regRtnRepealFromFutureToBankByFutureManual(rtnRepealFromFutureToBankByFutureManual); }
            remove { rtnRepealFromFutureToBankByFutureManual -= value; regRtnRepealFromFutureToBankByFutureManual(rtnRepealFromFutureToBankByFutureManual); }
        }

        ///成交通知
        [DllImport(dllName, EntryPoint = "?RegRtnTrade@@YGXP6GHPAUCThostFtdcTradeField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnTrade(RtnTrade cb);
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
            add { rtnTrade += value; regRtnTrade(rtnTrade); }
            remove { rtnTrade -= value; regRtnTrade(rtnTrade); }
        }

        ///交易通知
        [DllImport(dllName, EntryPoint = "?RegRtnTradingNotice@@YGXP6GHPAUCThostFtdcTradingNoticeInfoField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        static extern void regRtnTradingNotice(RtnTradingNotice cb);
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
            add { rtnTradingNotice += value; regRtnTradingNotice(rtnTradingNotice); }
            remove { rtnTradingNotice -= value; regRtnTradingNotice(rtnTradingNotice); }
        }
    }
}
