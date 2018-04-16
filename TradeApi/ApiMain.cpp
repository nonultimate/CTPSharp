
#include "stdafx.h"
#include "TradeApi.h"
#include "..\api\x86\ThostFtdcUserApiDataType.h"
#include "..\api\x86\ThostFtdcUserApiStruct.h"

// UserApi对象
CThostFtdcTraderApi* pUserApi;

//回调函数
CBOnFrontConnected cbOnFrontConnected = 0;		///当客户端与交易后台建立起通信连接时（还未登录前），该方法被调用。
CBOnFrontDisconnected cbOnFrontDisconnected = 0;		///当客户端与交易后台通信连接断开时，该方法被调用。当发生这个情况后，API会自动重新连接，客户端可不做处理。
CBOnHeartBeatWarning cbOnHeartBeatWarning = 0;		///心跳超时警告。当长时间未收到报文时，该方法被调用。
CBRspUserLogin cbRspUserLogin = 0;	///登录请求响应
CBRspUserLogout cbRspUserLogout = 0;	///登出请求响应
CBRspUserPasswordUpdate cbRspUserPasswordUpdate = 0;	///用户口令更新请求响应
CBRspTradingAccountPasswordUpdate cbRspTradingAccountPasswordUpdate = 0;	///资金账户口令更新请求响应
CBRspOrderInsert cbRspOrderInsert = 0;	///报单录入请求响应
CBRspParkedOrderInsert cbRspParkedOrderInsert = 0;	///预埋单录入请求响应
CBRspParkedOrderAction cbRspParkedOrderAction = 0;	///预埋撤单录入请求响应
CBRspOrderAction cbRspOrderAction = 0;	///报单操作请求响应
CBRspQueryMaxOrderVolume cbRspQueryMaxOrderVolume = 0;	///查询最大报单数量响应
CBRspSettlementInfoConfirm cbRspSettlementInfoConfirm = 0;	///投资者结算结果确认响应
CBRspRemoveParkedOrder cbRspRemoveParkedOrder = 0;	///删除预埋单响应
CBRspRemoveParkedOrderAction cbRspRemoveParkedOrderAction = 0;	///删除预埋撤单响应
CBRspExecOrderInsert cbRspExecOrderInsert = 0;	///执行宣告录入请求响应
CBRspExecOrderAction cbRspExecOrderAction = 0;	///执行宣告操作请求响应
CBRspForQuoteInsert cbRspForQuoteInsert = 0;	///询价录入请求响应
CBRspQuoteInsert cbRspQuoteInsert = 0;	///报价录入请求响应
CBRspQuoteAction cbRspQuoteAction = 0;	///报价操作请求响应
CBRspBatchOrderAction cbRspBatchOrderAction = 0;	///批量报单操作请求响应
CBRspOptionSelfCloseInsert cbRspOptionSelfCloseInsert = 0;	///期权自对冲录入请求响应
CBRspOptionSelfCloseAction cbRspOptionSelfCloseAction = 0; ///期权自对冲操作请求响应
CBRspCombActionInsert cbRspCombActionInsert = 0;	///申请组合录入请求响应
CBRspQryOrder cbRspQryOrder = 0;	///请求查询报单响应
CBRspQryTrade cbRspQryTrade = 0;	///请求查询成交响应
CBRspQryInvestorPosition cbRspQryInvestorPosition = 0;	///请求查询投资者持仓响应
CBRspQryTradingAccount cbRspQryTradingAccount = 0;	///请求查询资金账户响应
CBRspQryInvestor cbRspQryInvestor = 0;	///请求查询投资者响应
CBRspQryTradingCode cbRspQryTradingCode = 0;	///请求查询交易编码响应
CBRspQryInstrumentMarginRate cbRspQryInstrumentMarginRate = 0;	///请求查询合约保证金率响应
CBRspQryInstrumentCommissionRate cbRspQryInstrumentCommissionRate = 0;	///请求查询合约手续费率响应
CBRspQryExchange cbRspQryExchange = 0;	///请求查询交易所响应
CBRspQryInstrument cbRspQryInstrument = 0;	///请求查询合约响应
CBRspQryDepthMarketData cbRspQryDepthMarketData = 0;	///请求查询行情响应
CBRspQrySettlementInfo cbRspQrySettlementInfo = 0;	///请求查询投资者结算结果响应
CBRspQryTransferBank cbRspQryTransferBank = 0;	///请求查询转帐银行响应
CBRspQryInvestorPositionDetail cbRspQryInvestorPositionDetail = 0;	///请求查询投资者持仓明细响应
CBRspQryNotice cbRspQryNotice = 0;	///请求查询客户通知响应
CBRspQrySettlementInfoConfirm cbRspQrySettlementInfoConfirm = 0;	///请求查询结算信息确认响应
CBRspQryInvestorPositionCombineDetail cbRspQryInvestorPositionCombineDetail = 0;	///请求查询投资者持仓明细响应
CBRspQryCFMMCTradingAccountKey cbRspQryCFMMCTradingAccountKey = 0;	///查询保证金监管系统经纪公司资金账户密钥响应
CBRspQryEWarrantOffset cbRspQryEWarrantOffset = 0;	///请求查询仓单折抵信息响应
CBRspQryInvestorProductGroupMargin cbRspQryInvestorProductGroupMargin = 0;	///请求查询投资者品种/跨品种保证金响应
CBRspQryExchangeMarginRate cbRspQryExchangeMarginRate = 0;	///请求查询交易所保证金率响应
CBRspQryExchangeMarginRateAdjust cbRspQryExchangeMarginRateAdjust = 0;	///请求查询交易所调整保证金率响应
CBRspQryExchangeRate cbRspQryExchangeRate = 0;	///请求查询汇率响应
CBRspQrySecAgentACIDMap cbRspQrySecAgentACIDMap = 0;	///请求查询二级代理操作员银期权限响应
CBRspQryProductExchRate cbRspQryProductExchRate = 0;	///请求查询产品报价汇率
CBRspQryProductGroup cbRspQryProductGroup = 0;	///请求查询产品组
CBRspQryMMInstrumentCommissionRate cbRspQryMMInstrumentCommissionRate = 0;	///请求查询做市商合约手续费率响应
CBRspQryMMOptionInstrCommRate cbRspQryMMOptionInstrCommRate = 0;	///请求查询做市商期权合约手续费响应
CBRspQryInstrumentOrderCommRate cbRspQryInstrumentOrderCommRate = 0;	///请求查询报单手续费响应
CBRspQrySecAgentTradingAccount cbRspQrySecAgentTradingAccount = 0;	///请求查询资金账户响应
CBRspQrySecAgentCheckMode cbRspQrySecAgentCheckMode = 0;	///请求查询二级代理商资金校验模式响应
CBRspQryOptionInstrTradeCost cbRspQryOptionInstrTradeCost = 0;	///请求查询期权交易成本响应
CBRspQryOptionInstrCommRate cbRspQryOptionInstrCommRate = 0;	///请求查询期权合约手续费响应
CBRspQryExecOrder cbRspQryExecOrder = 0;	///请求查询执行宣告响应
CBRspQryForQuote cbRspQryForQuote = 0;	///请求查询询价响应
CBRspQryQuote cbRspQryQuote = 0;	///请求查询报价响应
CBRspQryOptionSelfClose cbRspQryOptionSelfClose = 0;	///请求查询期权自对冲响应
CBRspQryInvestUnit cbRspQryInvestUnit = 0;	///请求查询投资单元响应
CBRspQryCombInstrumentGuard cbRspQryCombInstrumentGuard = 0;	///请求查询组合合约安全系数响应
CBRspQryCombAction cbRspQryCombAction = 0;	///请求查询申请组合响应
CBRspQryTransferSerial cbRspQryTransferSerial = 0;	///请求查询转帐流水响应
CBRspQryAccountregister cbRspQryAccountregister = 0;///请求查询银期签约关系响应
CBRspError cbRspError = 0;	///错误应答
CBRtnOrder cbRtnOrder = 0;	///报单通知
CBRtnTrade cbRtnTrade = 0;	///成交通知
CBErrRtnOrderInsert cbErrRtnOrderInsert = 0;	///报单录入错误回报
CBErrRtnOrderAction cbErrRtnOrderAction = 0;	///报单操作错误回报
CBRtnInstrumentStatus cbRtnInstrumentStatus = 0;	///合约交易状态通知
CBRtnTradingNotice cbRtnTradingNotice = 0;	///交易通知
CBRtnErrorConditionalOrder cbRtnErrorConditionalOrder = 0;	///提示条件单校验错误
CBRtnExecOrder cbRtnExecOrder = 0;	///执行宣告通知
CBErrRtnExecOrderInsert cbErrRtnExecOrderInsert = 0;	///执行宣告录入错误回报
CBErrRtnExecOrderAction cbErrRtnExecOrderAction = 0;	///执行宣告操作错误回报
CBErrRtnForQuoteInsert cbErrRtnForQuoteInsert = 0;	///询价录入错误回报
CBRtnQuote cbRtnQuote = 0;	///报价通知
CBErrRtnQuoteInsert cbErrRtnQuoteInsert = 0;	///报价录入错误回报
CBErrRtnQuoteAction cbErrRtnQuoteAction = 0;	///报价操作错误回报
CBRtnForQuoteRsp cbRtnForQuoteRsp = 0;	///询价通知
CBRtnCFMMCTradingAccountToken cbRtnCFMMCTradingAccountToken = 0;	///保证金监控中心用户令牌
CBErrRtnBatchOrderAction cbErrRtnBatchOrderAction = 0;	///批量报单操作错误回报
CBRtnOptionSelfClose cbRtnOptionSelfClose = 0;	///期权自对冲通知
CBErrRtnOptionSelfCloseInsert cbErrRtnOptionSelfCloseInsert = 0;	///期权自对冲录入错误回报
CBErrRtnOptionSelfCloseAction cbErrRtnOptionSelfCloseAction = 0;	///期权自对冲操作错误回报
CBRtnCombAction cbRtnCombAction = 0;	///申请组合通知
CBErrRtnCombActionInsert cbErrRtnCombActionInsert = 0;	///申请组合录入错误回报
CBRspQryContractBank cbRspQryContractBank = 0;	///请求查询签约银行响应
CBRspQryParkedOrder cbRspQryParkedOrder = 0;	///请求查询预埋单响应
CBRspQryParkedOrderAction cbRspQryParkedOrderAction = 0;	///请求查询预埋撤单响应
CBRspQryTradingNotice cbRspQryTradingNotice = 0;	///请求查询交易通知响应
CBRspQryBrokerTradingParams cbRspQryBrokerTradingParams = 0;	///请求查询经纪公司交易参数响应
CBRspQryBrokerTradingAlgos cbRspQryBrokerTradingAlgos = 0;	///请求查询经纪公司交易算法响应
CBRspQueryCFMMCTradingAccountToken cbRspQueryCFMMCTradingAccountToken = 0;	///请求查询监控中心用户令牌
CBRtnFromBankToFutureByBank cbRtnFromBankToFutureByBank = 0;	///银行发起银行资金转期货通知
CBRtnFromFutureToBankByBank cbRtnFromFutureToBankByBank = 0;	///银行发起期货资金转银行通知
CBRtnRepealFromBankToFutureByBank cbRtnRepealFromBankToFutureByBank = 0;	///银行发起冲正银行转期货通知
CBRtnRepealFromFutureToBankByBank cbRtnRepealFromFutureToBankByBank = 0;	///银行发起冲正期货转银行通知
CBRtnFromBankToFutureByFuture cbRtnFromBankToFutureByFuture = 0;	///期货发起银行资金转期货通知
CBRtnFromFutureToBankByFuture cbRtnFromFutureToBankByFuture = 0;	///期货发起期货资金转银行通知
CBRtnRepealFromBankToFutureByFutureManual cbRtnRepealFromBankToFutureByFutureManual = 0;	///系统运行时期货端手工发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
CBRtnRepealFromFutureToBankByFutureManual cbRtnRepealFromFutureToBankByFutureManual = 0;	///系统运行时期货端手工发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
CBRtnQueryBankBalanceByFuture cbRtnQueryBankBalanceByFuture = 0;	///期货发起查询银行余额通知
CBErrRtnBankToFutureByFuture cbErrRtnBankToFutureByFuture = 0;	///期货发起银行资金转期货错误回报
CBErrRtnFutureToBankByFuture cbErrRtnFutureToBankByFuture = 0;	///期货发起期货资金转银行错误回报
CBErrRtnRepealBankToFutureByFutureManual cbErrRtnRepealBankToFutureByFutureManual = 0;	///系统运行时期货端手工发起冲正银行转期货错误回报
CBErrRtnRepealFutureToBankByFutureManual cbErrRtnRepealFutureToBankByFutureManual = 0;	///系统运行时期货端手工发起冲正期货转银行错误回报
CBErrRtnQueryBankBalanceByFuture cbErrRtnQueryBankBalanceByFuture = 0;	///期货发起查询银行余额错误回报
CBRtnRepealFromBankToFutureByFuture cbRtnRepealFromBankToFutureByFuture = 0;	///期货发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
CBRtnRepealFromFutureToBankByFuture cbRtnRepealFromFutureToBankByFuture = 0;	///期货发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
CBRspFromBankToFutureByFuture cbRspFromBankToFutureByFuture = 0;	///期货发起银行资金转期货应答
CBRspFromFutureToBankByFuture cbRspFromFutureToBankByFuture = 0;	///期货发起期货资金转银行应答
CBRspQueryBankAccountMoneyByFuture cbRspQueryBankAccountMoneyByFuture = 0;	///期货发起查询银行余额应答
CBRtnOpenAccountByBank cbRtnOpenAccountByBank = 0;	///银行发起银期开户通知
CBRtnCancelAccountByBank cbRtnCancelAccountByBank = 0;	///银行发起银期销户通知
CBRtnChangeAccountByBank cbRtnChangeAccountByBank = 0;	///银行发起变更银行账号通知

//===============

//获取接口版本
TRADEAPI_API const char* GetApiVersion()
{
	return CThostFtdcTraderApi::GetApiVersion();
}

//连接
TRADEAPI_API void Connect(char *frontAddr, char *pszFlowPath)
{
	// 初始化UserApi
	pUserApi = CThostFtdcTraderApi::CreateFtdcTraderApi(pszFlowPath);			// 创建UserApi
	CTraderSpi* pUserSpi = new CTraderSpi();
	pUserApi->RegisterSpi((CThostFtdcTraderSpi*)pUserSpi);			// 注册事件类
	pUserApi->SubscribePublicTopic(THOST_TERT_QUICK/*THOST_TERT_RESTART*/);					// 注册公有流
	pUserApi->SubscribePrivateTopic(THOST_TERT_QUICK/*THOST_TERT_RESTART*/);					// 注册私有流
	pUserApi->RegisterFront(frontAddr);							// connect
	pUserApi->Init();
	//pUserApi->Join();
}

//断开
TRADEAPI_API void DisConnect()
{
	pUserApi->RegisterSpi(NULL);
	pUserApi->Release();
	pUserApi = NULL;
}

//获取交易日
TRADEAPI_API const char* GetTradingDay()
{
	return pUserApi->GetTradingDay();
}

//发送用户登录请求
TRADEAPI_API int ReqUserLogin(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID, TThostFtdcPasswordType password)
{
	CThostFtdcReqUserLoginField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.Password, password);
	strcpy_s(req.UserProductInfo, "HF");
	return pUserApi->ReqUserLogin(&req, requestID);
}
//发送登出请求
TRADEAPI_API int ReqUserLogout(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType investorID)
{
	CThostFtdcUserLogoutField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, investorID);
	return pUserApi->ReqUserLogout(&req, requestID);
}
//更新用户口令
TRADEAPI_API int ReqUserPasswordUpdate(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID, TThostFtdcUserIDType oldPassword, TThostFtdcPasswordType newPassword)
{
	CThostFtdcUserPasswordUpdateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.OldPassword, oldPassword);
	strcpy_s(req.NewPassword, newPassword);
	return pUserApi->ReqUserPasswordUpdate(&req, requestID);
}
///资金账户口令更新请求
TRADEAPI_API int ReqTradingAccountPasswordUpdate(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcAccountIDType accountID, TThostFtdcUserIDType oldPassword, TThostFtdcPasswordType newPassword)
{
	CThostFtdcTradingAccountPasswordUpdateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.AccountID, accountID);
	strcpy_s(req.NewPassword, newPassword);
	strcpy_s(req.OldPassword, oldPassword);
	return pUserApi->ReqTradingAccountPasswordUpdate(&req, requestID);
}
//安全登录请求
TRADEAPI_API int ReqUserSafeLogin(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID, TThostFtdcPasswordType password)
{
	CThostFtdcReqUserLoginField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.Password, password);
	strcpy_s(req.UserProductInfo, "HF");
	return pUserApi->ReqUserLogin2(&req, requestID);
}
//安全更新用户口令
TRADEAPI_API int ReqUserPasswordSafeUpdate(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID, TThostFtdcUserIDType oldPassword, TThostFtdcPasswordType newPassword)
{
	CThostFtdcUserPasswordUpdateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.OldPassword, oldPassword);
	strcpy_s(req.NewPassword, newPassword);
	return pUserApi->ReqUserPasswordUpdate2(&req, requestID);
}
//报单录入请求
TRADEAPI_API int ReqOrderInsert(int requestID, CThostFtdcInputOrderField *pOrder)
{
	int siez = sizeof(CThostFtdcInputOrderField);
	strcpy_s(pOrder->BusinessUnit, "HF");
	return pUserApi->ReqOrderInsert(pOrder, requestID);
}
//报单操作请求
TRADEAPI_API int ReqOrderAction(int requestID, CThostFtdcInputOrderActionField *pOrder)
{
	return pUserApi->ReqOrderAction(pOrder, requestID);
}
///查询最大报单数量请求
TRADEAPI_API int ReqQueryMaxOrderVolume(int requestID, CThostFtdcQueryMaxOrderVolumeField *pMaxOrderVolume)
{
	return pUserApi->ReqQueryMaxOrderVolume(pMaxOrderVolume, requestID);
}
//投资者结算结果确认
TRADEAPI_API int ReqSettlementInfoConfirm(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	CThostFtdcSettlementInfoConfirmField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqSettlementInfoConfirm(&req, requestID);
}
///执行宣告录入请求
TRADEAPI_API int ReqExecOrderInsert(int requestID, CThostFtdcInputExecOrderField *pInputExecOrder)
{
	return pUserApi->ReqExecOrderInsert(pInputExecOrder, requestID);
}
///执行宣告操作请求
TRADEAPI_API int ReqExecOrderAction(int requestID, CThostFtdcInputExecOrderActionField *pInputExecOrderAction)
{
	return pUserApi->ReqExecOrderAction(pInputExecOrderAction, requestID);
}
///询价录入请求
TRADEAPI_API int ReqForQuoteInsert(int requestID, CThostFtdcInputForQuoteField *pInputForQuote)
{
	return pUserApi->ReqForQuoteInsert(pInputForQuote, requestID);
}
///报价录入请求
TRADEAPI_API int ReqQuoteInsert(int requestID, CThostFtdcInputQuoteField *pInputQuote)
{
	return pUserApi->ReqQuoteInsert(pInputQuote, requestID);
}
///报价操作请求
TRADEAPI_API int ReqQuoteAction(int requestID, CThostFtdcInputQuoteActionField *pInputQuoteAction)
{
	return pUserApi->ReqQuoteAction(pInputQuoteAction, requestID);
}
///批量报单操作请求
TRADEAPI_API int ReqBatchOrderAction(int requestID, CThostFtdcInputBatchOrderActionField *pInputBatchOrderAction)
{
	return pUserApi->ReqBatchOrderAction(pInputBatchOrderAction, requestID);
}
///期权自对冲录入请求
TRADEAPI_API int ReqOptionSelfCloseInsert(int requestID, CThostFtdcInputOptionSelfCloseField *pInputOptionSelfClose)
{
	return pUserApi->ReqOptionSelfCloseInsert(pInputOptionSelfClose, requestID);
}
///期权自对冲操作请求
TRADEAPI_API int ReqOptionSelfCloseAction(int requestID, CThostFtdcInputOptionSelfCloseActionField *pInputOptionSelfCloseAction)
{
	return pUserApi->ReqOptionSelfCloseAction(pInputOptionSelfCloseAction, requestID);
}
///申请组合录入请求
TRADEAPI_API int ReqCombActionInsert(int requestID, CThostFtdcInputCombActionField *pInputCombAction)
{
	return pUserApi->ReqCombActionInsert(pInputCombAction, requestID);
}
///请求查询报单
TRADEAPI_API int ReqQryOrder(int requestID, CThostFtdcQryOrderField *pQryOrder)
{
	return pUserApi->ReqQryOrder(pQryOrder, requestID);
}
///请求查询成交
TRADEAPI_API int ReqQryTrade(int requestID, CThostFtdcQryTradeField *pQryTrade)
{
	return pUserApi->ReqQryTrade(pQryTrade, requestID);
}
//请求查询投资者持仓
TRADEAPI_API int ReqQryInvestorPosition(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	CThostFtdcQryInvestorPositionField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryInvestorPosition(&req, requestID);
}
//请求查询资金账户
TRADEAPI_API int ReqQryTradingAccount(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	CThostFtdcQryTradingAccountField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQryTradingAccount(&req, requestID);
}
///请求查询投资者
TRADEAPI_API int ReqQryInvestor(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	CThostFtdcQryInvestorField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQryInvestor(&req, requestID);
}
///请求查询交易编码
TRADEAPI_API int ReqQryTradingCode(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcClientIDType clientID, TThostFtdcExchangeIDType	exchangeID)
{
	CThostFtdcQryTradingCodeField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (clientID != NULL)
		strcpy_s(req.ClientID, clientID);
	if (exchangeID != NULL)
		strcpy_s(req.ExchangeID, exchangeID);
	return pUserApi->ReqQryTradingCode(&req, requestID);
}
///请求查询合约保证金率
TRADEAPI_API int ReqQryInstrumentMarginRate(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcHedgeFlagType	hedgeFlag)
{
	CThostFtdcQryInstrumentMarginRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	if (hedgeFlag != NULL)
		req.HedgeFlag = hedgeFlag;						//*不*能采用null进行所有查询
	return pUserApi->ReqQryInstrumentMarginRate(&req, requestID);
}
///请求查询合约手续费率
TRADEAPI_API int ReqQryInstrumentCommissionRate(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	CThostFtdcQryInstrumentCommissionRateField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryInstrumentCommissionRate(&req, requestID);
}
///请求查询交易所
TRADEAPI_API int ReqQryExchange(int requestID, TThostFtdcExchangeIDType exchangeID)
{
	CThostFtdcQryExchangeField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.ExchangeID, exchangeID);
	return pUserApi->ReqQryExchange(&req, requestID);
}
//请求查询合约
TRADEAPI_API int ReqQryInstrument(int requestID, TThostFtdcInstrumentIDType instrumentID)
{
	CThostFtdcQryInstrumentField req;
	memset(&req, 0, sizeof(req));
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryInstrument(&req, requestID);
}
//请求查询行情
TRADEAPI_API int ReqQryDepthMarketData(int requestID, TThostFtdcInstrumentIDType instrumentID)
{
	CThostFtdcQryDepthMarketDataField req;
	memset(&req, 0, sizeof(req));
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryDepthMarketData(&req, requestID);
}
///请求查询投资者结算结果
TRADEAPI_API int ReqQrySettlementInfo(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcDateType	tradingDay)
{
	CThostFtdcQrySettlementInfoField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (tradingDay != NULL)
		strcpy_s(req.TradingDay, tradingDay);
	return pUserApi->ReqQrySettlementInfo(&req, requestID);
}
//查询持仓明细
TRADEAPI_API int ReqQryInvestorPositionDetail(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	CThostFtdcQryInvestorPositionDetailField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryInvestorPositionDetail(&req, requestID);
}
///请求查询客户通知
TRADEAPI_API int ReqQryNotice(int requestID, TThostFtdcBrokerIDType brokerID)
{
	CThostFtdcQryNoticeField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	return pUserApi->ReqQryNotice(&req, requestID);
}
///请求查询结算信息确认
TRADEAPI_API int ReqQrySettlementInfoConfirm(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	CThostFtdcQrySettlementInfoConfirmField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQrySettlementInfoConfirm(&req, requestID);
}
///请求查询**组合**持仓明细
TRADEAPI_API int ReqQryInvestorPositionCombineDetail(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	CThostFtdcQryInvestorPositionCombineDetailField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.CombInstrumentID, instrumentID);
	return pUserApi->ReqQryInvestorPositionCombineDetail(&req, requestID);
}
///请求查询保证金监管系统经纪公司资金账户密钥
TRADEAPI_API int ReqQryCFMMCTradingAccountKey(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	CThostFtdcQryCFMMCTradingAccountKeyField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQryCFMMCTradingAccountKey(&req, requestID);
}
///请求查询仓单折抵信息
TRADEAPI_API int ReqQryEWarrantOffset(int requestID, CThostFtdcQryEWarrantOffsetField *pQryEWarrantOffset)
{
	return pUserApi->ReqQryEWarrantOffset(pQryEWarrantOffset, requestID);
}
///请求查询投资者品种/跨品种保证金
TRADEAPI_API int ReqQryInvestorProductGroupMargin(int requestID, CThostFtdcQryInvestorProductGroupMarginField *pQryInvestorProductGroupMargin)
{
	return pUserApi->ReqQryInvestorProductGroupMargin(pQryInvestorProductGroupMargin, requestID);
}
///请求查询交易所保证金率
TRADEAPI_API int ReqQryExchangeMarginRate(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcHedgeFlagType hedgeFlag)
{
	CThostFtdcQryExchangeMarginRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	req.HedgeFlag = hedgeFlag;
	return pUserApi->ReqQryExchangeMarginRate(&req, requestID);
}
///请求查询交易所调整保证金率
TRADEAPI_API int ReqQryExchangeMarginRateAdjust(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcHedgeFlagType hedgeFlag)
{
	CThostFtdcQryExchangeMarginRateAdjustField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	req.HedgeFlag = hedgeFlag;
	return pUserApi->ReqQryExchangeMarginRateAdjust(&req, requestID);
}
///请求查询汇率
TRADEAPI_API int ReqQryExchangeRate(int requestID, CThostFtdcQryExchangeRateField *pQryExchangeRate)
{
	return pUserApi->ReqQryExchangeRate(pQryExchangeRate, requestID);
}
///请求查询二级代理操作员银期权限
TRADEAPI_API int ReqQrySecAgentACIDMap(int requestID, CThostFtdcQrySecAgentACIDMapField *pQrySecAgentACIDMap)
{
	return pUserApi->ReqQrySecAgentACIDMap(pQrySecAgentACIDMap, requestID);
}
///请求查询产品报价汇率
TRADEAPI_API int ReqQryProductExchRate(int requestID, TThostFtdcInstrumentIDType productID)
{
	CThostFtdcQryProductExchRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.ProductID, productID);
	return pUserApi->ReqQryProductExchRate(&req, requestID);
}
///请求查询产品组
TRADEAPI_API int ReqQryProductGroup(int requestID, TThostFtdcInstrumentIDType productID, TThostFtdcExchangeIDType exchangeID)
{
	CThostFtdcQryProductGroupField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.ProductID, productID);
	strcpy_s(req.ExchangeID, exchangeID);
	return pUserApi->ReqQryProductGroup(&req, requestID);
}
///请求查询做市商合约手续费率
TRADEAPI_API int ReqQryMMInstrumentCommissionRate(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	CThostFtdcQryMMInstrumentCommissionRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryMMInstrumentCommissionRate(&req, requestID);
}
///请求查询做市商期权合约手续费
TRADEAPI_API int ReqQryMMOptionInstrCommRate(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	CThostFtdcQryMMOptionInstrCommRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryMMOptionInstrCommRate(&req, requestID);
}
///请求查询报单手续费
TRADEAPI_API int ReqQryInstrumentOrderCommRate(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	CThostFtdcQryInstrumentOrderCommRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryInstrumentOrderCommRate(&req, requestID);
}
///请求查询资金账户
TRADEAPI_API int ReqQrySecAgentTradingAccount(int requestID, CThostFtdcQryTradingAccountField *pQryTradingAccount)
{
	return pUserApi->ReqQrySecAgentTradingAccount(pQryTradingAccount, requestID);
}
///请求查询二级代理商资金校验模式
TRADEAPI_API int ReqQrySecAgentCheckMode(int requestID, CThostFtdcQrySecAgentCheckModeField *pQrySecAgentCheckMode)
{
	return pUserApi->ReqQrySecAgentCheckMode(pQrySecAgentCheckMode, requestID);
}
///请求查询期权交易成本
TRADEAPI_API int ReqQryOptionInstrTradeCost(int requestID, CThostFtdcQryOptionInstrTradeCostField *pQryOptionInstrTradeCost)
{
	return pUserApi->ReqQryOptionInstrTradeCost(pQryOptionInstrTradeCost, requestID);
}
///请求查询期权合约手续费
TRADEAPI_API int ReqQryOptionInstrCommRate(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	CThostFtdcQryOptionInstrCommRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryOptionInstrCommRate(&req, requestID);
}
///请求查询执行宣告
TRADEAPI_API int ReqQryExecOrder(int requestID, CThostFtdcQryExecOrderField *pQryExecOrder)
{
	return pUserApi->ReqQryExecOrder(pQryExecOrder, requestID);
}
///请求查询询价
TRADEAPI_API int ReqQryForQuote(int requestID, CThostFtdcQryForQuoteField *pQryForQuote)
{
	return pUserApi->ReqQryForQuote(pQryForQuote, requestID);
}
///请求查询报价
TRADEAPI_API int ReqQryQuote(int requestID, CThostFtdcQryQuoteField *pQryQuote)
{
	return pUserApi->ReqQryQuote(pQryQuote, requestID);
}
///请求查询期权自对冲
TRADEAPI_API int ReqQryOptionSelfClose(int requestID, CThostFtdcQryOptionSelfCloseField *pQryOptionSelfClose)
{
	return pUserApi->ReqQryOptionSelfClose(pQryOptionSelfClose, requestID);
}
///请求查询投资单元
TRADEAPI_API int ReqQryInvestUnit(int requestID, CThostFtdcQryInvestUnitField *pQryInvestUnit)
{
	return pUserApi->ReqQryInvestUnit(pQryInvestUnit, requestID);
}
///请求查询组合合约安全系数
TRADEAPI_API int ReqQryCombInstrumentGuard(int requestID, CThostFtdcQryCombInstrumentGuardField *pQryCombInstrumentGuard)
{
	return pUserApi->ReqQryCombInstrumentGuard(pQryCombInstrumentGuard, requestID);
}
///请求查询申请组合
TRADEAPI_API int ReqQryCombAction(int requestID, CThostFtdcQryCombActionField *pQryCombAction)
{
	return pUserApi->ReqQryCombAction(pQryCombAction, requestID);
}
///请求查询交易通知
TRADEAPI_API int ReqQryTradingNotice(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	CThostFtdcQryTradingNoticeField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQryTradingNotice(&req, requestID);
}
///请求查询经纪公司交易参数
TRADEAPI_API int ReqQryBrokerTradingParams(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	CThostFtdcQryBrokerTradingParamsField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQryBrokerTradingParams(&req, requestID);
}
///请求查询经纪公司交易算法
TRADEAPI_API int ReqQryBrokerTradingAlgos(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcExchangeIDType exchangeID, TThostFtdcInstrumentIDType instrumentID)
{
	CThostFtdcQryBrokerTradingAlgosField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	if (exchangeID != NULL)
		strcpy_s(req.ExchangeID, exchangeID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);

	return pUserApi->ReqQryBrokerTradingAlgos(&req, requestID);
}
///预埋单录入请求
TRADEAPI_API int ReqParkedOrderInsert(int requestID, CThostFtdcParkedOrderField *ParkedOrder)
{
	return pUserApi->ReqParkedOrderInsert(ParkedOrder, requestID);
}
///预埋撤单录入请求
TRADEAPI_API int ReqParkedOrderAction(int requestID, CThostFtdcParkedOrderActionField *ParkedOrderAction)
{
	return pUserApi->ReqParkedOrderAction(ParkedOrderAction, requestID);
}
///请求删除预埋单
TRADEAPI_API int ReqRemoveParkedOrder(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcParkedOrderIDType parkedOrderID)
{
	CThostFtdcRemoveParkedOrderField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	strcpy_s(req.ParkedOrderID, parkedOrderID);
	return pUserApi->ReqRemoveParkedOrder(&req, requestID);
}
///请求删除预埋撤单
TRADEAPI_API int ReqRemoveParkedOrderAction(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcParkedOrderActionIDType parkedOrderActionID)
{
	CThostFtdcRemoveParkedOrderActionField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	strcpy_s(req.ParkedOrderActionID, parkedOrderActionID);
	return pUserApi->ReqRemoveParkedOrderAction(&req, requestID);
}

///请求查询转帐银行
TRADEAPI_API int ReqQryTransferBank(int requestID, TThostFtdcBankIDType bankID, TThostFtdcBankBrchIDType bankBrchID)
{
	CThostFtdcQryTransferBankField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BankID, bankID);
	strcpy_s(req.BankBrchID, bankBrchID);
	return pUserApi->ReqQryTransferBank(&req, requestID);
}
///请求查询转帐流水
TRADEAPI_API int ReqQryTransferSerial(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcAccountIDType accountID, TThostFtdcBankIDType bankID)
{
	CThostFtdcQryTransferSerialField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.AccountID, accountID);
	strcpy_s(req.BankID, bankID);
	return pUserApi->ReqQryTransferSerial(&req, requestID);
}
///请求查询银期签约关系
TRADEAPI_API int ReqQryAccountregister(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcAccountIDType accountID, TThostFtdcBankIDType bankID)
{
	CThostFtdcQryAccountregisterField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.AccountID, accountID);
	strcpy_s(req.BankID, bankID);
	return pUserApi->ReqQryAccountregister(&req, requestID);
}
///请求查询签约银行
TRADEAPI_API int ReqQryContractBank(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcBankIDType bankID, TThostFtdcBankBrchIDType bankBrchID)
{
	CThostFtdcQryContractBankField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	if (bankID != NULL)
		strcpy_s(req.BankID, bankID);
	if (bankBrchID != NULL)
		strcpy_s(req.BankBrchID, bankBrchID);
	return pUserApi->ReqQryContractBank(&req, requestID);
}
///请求查询预埋单
TRADEAPI_API int ReqQryParkedOrder(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcExchangeIDType exchangeID)
{
	CThostFtdcQryParkedOrderField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	if (exchangeID != NULL)
		strcpy_s(req.ExchangeID, exchangeID);
	return pUserApi->ReqQryParkedOrder(&req, requestID);
}
///请求查询预埋撤单
TRADEAPI_API int ReqQryParkedOrderAction(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcExchangeIDType exchangeID)
{
	CThostFtdcQryParkedOrderActionField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	if (exchangeID != NULL)
		strcpy_s(req.ExchangeID, exchangeID);
	return pUserApi->ReqQryParkedOrderAction(&req, requestID);
}
///请求查询监控中心用户令牌
TRADEAPI_API int ReqQueryCFMMCTradingAccountToken(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	CThostFtdcQueryCFMMCTradingAccountTokenField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQueryCFMMCTradingAccountToken(&req, requestID);
}
///期货发起银行资金转期货请求
TRADEAPI_API int ReqFromBankToFutureByFuture(int requestID, CThostFtdcReqTransferField *reqTransfer)
{
	return pUserApi->ReqFromBankToFutureByFuture(reqTransfer, requestID);
}
///期货发起期货资金转银行请求
TRADEAPI_API int ReqFromFutureToBankByFuture(int requestID, CThostFtdcReqTransferField *reqTransfer)
{
	return pUserApi->ReqFromFutureToBankByFuture(reqTransfer, requestID);
}
///期货发起查询银行余额请求
TRADEAPI_API int ReqQueryBankAccountMoneyByFuture(int requestID, CThostFtdcReqQueryAccountField *reqQueryAccount)
{
	return pUserApi->ReqQueryBankAccountMoneyByFuture(reqQueryAccount, requestID);
}
//========================================
///==================================== 回调函数 =======================================
TRADEAPI_API void WINAPI RegOnFrontConnected(CBOnFrontConnected cb)		///当客户端与交易后台建立起通信连接时（还未登录前），该方法被调用。
{
	cbOnFrontConnected = cb;
}

TRADEAPI_API void WINAPI RegOnFrontDisconnected(CBOnFrontDisconnected cb)		///当客户端与交易后台通信连接断开时，该方法被调用。当发生这个情况后，API会自动重新连接，客户端可不做处理。
{
	cbOnFrontDisconnected = cb;
}

TRADEAPI_API void WINAPI RegOnHeartBeatWarning(CBOnHeartBeatWarning cb)		///心跳超时警告。当长时间未收到报文时，该方法被调用。
{
	cbOnHeartBeatWarning = cb;
}

TRADEAPI_API void WINAPI RegRspUserLogin(CBRspUserLogin cb)	///登录请求响应
{
	cbRspUserLogin = cb;
}
TRADEAPI_API void WINAPI RegRspUserLogout(CBRspUserLogout cb)	///登出请求响应
{
	cbRspUserLogout = cb;
}
TRADEAPI_API void WINAPI RegRspUserPasswordUpdate(CBRspUserPasswordUpdate cb)	///用户口令更新请求响应
{
	cbRspUserPasswordUpdate = cb;
}
TRADEAPI_API void WINAPI RegRspTradingAccountPasswordUpdate(CBRspTradingAccountPasswordUpdate cb)	///资金账户口令更新请求响应
{
	cbRspTradingAccountPasswordUpdate = cb;
}
TRADEAPI_API void WINAPI RegRspOrderInsert(CBRspOrderInsert cb)	///报单录入请求响应
{
	cbRspOrderInsert = cb;
}
TRADEAPI_API void WINAPI RegRspParkedOrderInsert(CBRspParkedOrderInsert cb)	///预埋单录入请求响应
{
	cbRspParkedOrderInsert = cb;
}
TRADEAPI_API void WINAPI RegRspParkedOrderAction(CBRspParkedOrderAction cb)	///预埋撤单录入请求响应
{
	cbRspParkedOrderAction = cb;
}
TRADEAPI_API void WINAPI RegRspOrderAction(CBRspOrderAction cb)	///报单操作请求响应
{
	cbRspOrderAction = cb;
}
TRADEAPI_API void WINAPI RegRspQueryMaxOrderVolume(CBRspQueryMaxOrderVolume cb)	///查询最大报单数量响应
{
	cbRspQueryMaxOrderVolume = cb;
}
TRADEAPI_API void WINAPI RegRspSettlementInfoConfirm(CBRspSettlementInfoConfirm cb)	///投资者结算结果确认响应
{
	cbRspSettlementInfoConfirm = cb;
}
TRADEAPI_API void WINAPI RegRspRemoveParkedOrder(CBRspRemoveParkedOrder cb)	///删除预埋单响应
{
	cbRspRemoveParkedOrder = cb;
}
TRADEAPI_API void WINAPI RegRspRemoveParkedOrderAction(CBRspRemoveParkedOrderAction cb)	///删除预埋撤单响应
{
	cbRspRemoveParkedOrderAction = cb;
}
TRADEAPI_API void WINAPI RegBatchOrderAction(CBRspBatchOrderAction cb)	///批量报单操作请求响应
{
	cbRspBatchOrderAction = cb;
}
TRADEAPI_API void WINAPI RegCombActionInsert(CBRspCombActionInsert cb)	///申请组合录入请求响应
{
	cbRspCombActionInsert = cb;
}
TRADEAPI_API void WINAPI RegRspQryOrder(CBRspQryOrder cb)	///请求查询报单响应
{
	cbRspQryOrder = cb;
}
TRADEAPI_API void WINAPI RegRspQryTrade(CBRspQryTrade cb)	///请求查询成交响应
{
	cbRspQryTrade = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestorPosition(CBRspQryInvestorPosition cb)	///请求查询投资者持仓响应
{
	cbRspQryInvestorPosition = cb;
}
TRADEAPI_API void WINAPI RegRspQryTradingAccount(CBRspQryTradingAccount cb)	///请求查询资金账户响应
{
	cbRspQryTradingAccount = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestor(CBRspQryInvestor cb)	///请求查询投资者响应
{
	cbRspQryInvestor = cb;
}
TRADEAPI_API void WINAPI RegRspQryTradingCode(CBRspQryTradingCode cb)	///请求查询交易编码响应
{
	cbRspQryTradingCode = cb;
}
TRADEAPI_API void WINAPI RegRspQryInstrumentMarginRate(CBRspQryInstrumentMarginRate cb)	///请求查询合约保证金率响应
{
	cbRspQryInstrumentMarginRate = cb;
}
TRADEAPI_API void WINAPI RegRspQryInstrumentCommissionRate(CBRspQryInstrumentCommissionRate cb)	///请求查询合约手续费率响应
{
	cbRspQryInstrumentCommissionRate = cb;
}
TRADEAPI_API void WINAPI RegRspQryExchange(CBRspQryExchange cb)	///请求查询交易所响应
{
	cbRspQryExchange = cb;
}
TRADEAPI_API void WINAPI RegRspQryInstrument(CBRspQryInstrument cb)	///请求查询合约响应
{
	cbRspQryInstrument = cb;
}
TRADEAPI_API void WINAPI RegRspQryDepthMarketData(CBRspQryDepthMarketData cb)	///请求查询行情响应
{
	cbRspQryDepthMarketData = cb;
}
TRADEAPI_API void WINAPI RegRspQrySettlementInfo(CBRspQrySettlementInfo cb)	///请求查询投资者结算结果响应
{
	cbRspQrySettlementInfo = cb;
}
TRADEAPI_API void WINAPI RegRspQryTransferBank(CBRspQryTransferBank cb)	///请求查询转帐银行响应
{
	cbRspQryTransferBank = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestorPositionDetail(CBRspQryInvestorPositionDetail cb)	///请求查询投资者持仓明细响应
{
	cbRspQryInvestorPositionDetail = cb;
}
TRADEAPI_API void WINAPI RegRspQryNotice(CBRspQryNotice cb)	///请求查询客户通知响应
{
	cbRspQryNotice = cb;
}
TRADEAPI_API void WINAPI RegRspQrySettlementInfoConfirm(CBRspQrySettlementInfoConfirm cb)	///请求查询结算信息确认响应
{
	cbRspQrySettlementInfoConfirm = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestorPositionCombineDetail(CBRspQryInvestorPositionCombineDetail cb)	///请求查询投资者持仓明细响应
{
	cbRspQryInvestorPositionCombineDetail = cb;
}
TRADEAPI_API void WINAPI RegRspQryCFMMCTradingAccountKey(CBRspQryCFMMCTradingAccountKey cb)	///查询保证金监管系统经纪公司资金账户密钥响应
{
	cbRspQryCFMMCTradingAccountKey = cb;
}
TRADEAPI_API void WINAPI RegRspQryEWarrantOffset(CBRspQryEWarrantOffset cb)	///请求查询仓单折抵信息
{
	cbRspQryEWarrantOffset = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestorProductGroupMargin(CBRspQryInvestorProductGroupMargin cb)	///请求查询投资者品种/跨品种保证金
{
	cbRspQryInvestorProductGroupMargin = cb;
}
TRADEAPI_API void WINAPI RegRspQryExchangeMarginRate(CBRspQryExchangeMarginRate cb)	///请求查询交易所保证金率
{
	cbRspQryExchangeMarginRate = cb;
}
TRADEAPI_API void WINAPI RegRspQryExchangeMarginRateAdjust(CBRspQryExchangeMarginRateAdjust cb)	///请求查询交易所调整保证金率
{
	cbRspQryExchangeMarginRateAdjust = cb;
}
TRADEAPI_API void WINAPI RegRspQryTransferSerial(CBRspQryTransferSerial cb)	///请求查询转帐流水响应
{
	cbRspQryTransferSerial = cb;
}
TRADEAPI_API void WINAPI RegRspQryAccountregister(CBRspQryAccountregister cb)	///请求查询银期签约关系响应
{
	cbRspQryAccountregister = cb;
}
TRADEAPI_API void WINAPI RegRspError(CBRspError cb)	///错误应答
{
	cbRspError = cb;
}
TRADEAPI_API void WINAPI RegRtnOrder(CBRtnOrder cb)	///报单通知
{
	cbRtnOrder = cb;
}
TRADEAPI_API void WINAPI RegRtnTrade(CBRtnTrade cb)	///成交通知
{
	cbRtnTrade = cb;
}
TRADEAPI_API void WINAPI RegErrRtnOrderInsert(CBErrRtnOrderInsert cb)	///报单录入错误回报
{
	cbErrRtnOrderInsert = cb;
}
TRADEAPI_API void WINAPI RegErrRtnOrderAction(CBErrRtnOrderAction cb)	///报单操作错误回报
{
	cbErrRtnOrderAction = cb;
}
TRADEAPI_API void WINAPI RegRtnInstrumentStatus(CBRtnInstrumentStatus cb)	///合约交易状态通知
{
	cbRtnInstrumentStatus = cb;
}
TRADEAPI_API void WINAPI RegRtnTradingNotice(CBRtnTradingNotice cb)	///交易通知
{
	cbRtnTradingNotice = cb;
}
TRADEAPI_API void WINAPI RegRtnErrorConditionalOrder(CBRtnErrorConditionalOrder cb)	///提示条件单校验错误
{
	cbRtnErrorConditionalOrder = cb;
}
TRADEAPI_API void WINAPI RegRspQryContractBank(CBRspQryContractBank cb)	///请求查询签约银行响应
{
	cbRspQryContractBank = cb;
}
TRADEAPI_API void WINAPI RegRspQryParkedOrder(CBRspQryParkedOrder cb)	///请求查询预埋单响应
{
	cbRspQryParkedOrder = cb;
}
TRADEAPI_API void WINAPI RegRspQryParkedOrderAction(CBRspQryParkedOrderAction cb)	///请求查询预埋撤单响应
{
	cbRspQryParkedOrderAction = cb;
}
TRADEAPI_API void WINAPI RegRspQryTradingNotice(CBRspQryTradingNotice cb)	///请求查询交易通知响应
{
	cbRspQryTradingNotice = cb;
}
TRADEAPI_API void WINAPI RegRspQryBrokerTradingParams(CBRspQryBrokerTradingParams cb)	///请求查询经纪公司交易参数响应
{
	cbRspQryBrokerTradingParams = cb;
}
TRADEAPI_API void WINAPI RegRspQryBrokerTradingAlgos(CBRspQryBrokerTradingAlgos cb)	///请求查询经纪公司交易算法响应
{
	cbRspQryBrokerTradingAlgos = cb;
}
TRADEAPI_API void WINAPI RegRtnFromBankToFutureByBank(CBRtnFromBankToFutureByBank cb)	///银行发起银行资金转期货通知
{
	cbRtnFromBankToFutureByBank = cb;
}
TRADEAPI_API void WINAPI RegRtnFromFutureToBankByBank(CBRtnFromFutureToBankByBank cb)	///银行发起期货资金转银行通知
{
	cbRtnFromFutureToBankByBank = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromBankToFutureByBank(CBRtnRepealFromBankToFutureByBank cb)	///银行发起冲正银行转期货通知
{
	cbRtnRepealFromBankToFutureByBank = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromFutureToBankByBank(CBRtnRepealFromFutureToBankByBank cb)	///银行发起冲正期货转银行通知
{
	cbRtnRepealFromFutureToBankByBank = cb;
}
TRADEAPI_API void WINAPI RegRtnFromBankToFutureByFuture(CBRtnFromBankToFutureByFuture cb)	///期货发起银行资金转期货通知
{
	cbRtnFromBankToFutureByFuture = cb;
}
TRADEAPI_API void WINAPI RegRtnFromFutureToBankByFuture(CBRtnFromFutureToBankByFuture cb)	///期货发起期货资金转银行通知
{
	cbRtnFromFutureToBankByFuture = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromBankToFutureByFutureManual(CBRtnRepealFromBankToFutureByFutureManual cb)	///系统运行时期货端手工发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
{
	cbRtnRepealFromBankToFutureByFutureManual = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromFutureToBankByFutureManual(CBRtnRepealFromFutureToBankByFutureManual cb)	///系统运行时期货端手工发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
{
	cbRtnRepealFromFutureToBankByFutureManual = cb;
}
TRADEAPI_API void WINAPI RegRtnQueryBankBalanceByFuture(CBRtnQueryBankBalanceByFuture cb)	///期货发起查询银行余额通知
{
	cbRtnQueryBankBalanceByFuture = cb;
}
TRADEAPI_API void WINAPI RegErrRtnBankToFutureByFuture(CBErrRtnBankToFutureByFuture cb)	///期货发起银行资金转期货错误回报
{
	cbErrRtnBankToFutureByFuture = cb;
}
TRADEAPI_API void WINAPI RegErrRtnFutureToBankByFuture(CBErrRtnFutureToBankByFuture cb)	///期货发起期货资金转银行错误回报
{
	cbErrRtnFutureToBankByFuture = cb;
}
TRADEAPI_API void WINAPI RegErrRtnRepealBankToFutureByFutureManual(CBErrRtnRepealBankToFutureByFutureManual cb)	///系统运行时期货端手工发起冲正银行转期货错误回报
{
	cbErrRtnRepealBankToFutureByFutureManual = cb;
}
TRADEAPI_API void WINAPI RegErrRtnRepealFutureToBankByFutureManual(CBErrRtnRepealFutureToBankByFutureManual cb)	///系统运行时期货端手工发起冲正期货转银行错误回报
{
	cbErrRtnRepealFutureToBankByFutureManual = cb;
}
TRADEAPI_API void WINAPI RegErrRtnQueryBankBalanceByFuture(CBErrRtnQueryBankBalanceByFuture cb)	///期货发起查询银行余额错误回报
{
	cbErrRtnQueryBankBalanceByFuture = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromBankToFutureByFuture(CBRtnRepealFromBankToFutureByFuture cb)	///期货发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
{
	cbRtnRepealFromBankToFutureByFuture = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromFutureToBankByFuture(CBRtnRepealFromFutureToBankByFuture cb)	///期货发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
{
	cbRtnRepealFromFutureToBankByFuture = cb;
}
TRADEAPI_API void WINAPI RegRspFromBankToFutureByFuture(CBRspFromBankToFutureByFuture cb)	///期货发起银行资金转期货应答
{
	cbRspFromBankToFutureByFuture = cb;
}
TRADEAPI_API void WINAPI RegRspFromFutureToBankByFuture(CBRspFromFutureToBankByFuture cb)	///期货发起期货资金转银行应答
{
	cbRspFromFutureToBankByFuture = cb;
}
TRADEAPI_API void WINAPI RegRspQueryBankAccountMoneyByFuture(CBRspQueryBankAccountMoneyByFuture cb)	///期货发起查询银行余额应答
{
	cbRspQueryBankAccountMoneyByFuture = cb;
}
