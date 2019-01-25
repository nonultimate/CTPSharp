
#include "stdafx.h"
#include "TradeApi.h"
#include "..\api\x86\ThostFtdcUserApiDataType.h"
#include "..\api\x86\ThostFtdcUserApiStruct.h"

#pragma region 请求方法

//获取接口版本
TRADEAPI_API const char* GetApiVersion()
{
	return CThostFtdcTraderApi::GetApiVersion();
}

//创建回调类
TRADEAPI_API CTraderSpi* CreateSpi()
{
	CTraderSpi* pUserSpi = new CTraderSpi();
	return pUserSpi;
}

//连接
TRADEAPI_API CThostFtdcTraderApi* Connect(char *frontAddr, char *pszFlowPath, CTraderSpi* pUserSpi)
{
	// 初始化UserApi
	CThostFtdcTraderApi* pUserApi = CThostFtdcTraderApi::CreateFtdcTraderApi(pszFlowPath);			// 创建UserApi	
																									//CTraderSpi* pUserSpi = new CTraderSpi();
	pUserApi->RegisterSpi((CThostFtdcTraderSpi*)pUserSpi);			// 注册事件类
	pUserApi->SubscribePublicTopic(THOST_TERT_QUICK/*THOST_TERT_RESTART*/);					// 注册公有流
	pUserApi->SubscribePrivateTopic(THOST_TERT_QUICK/*THOST_TERT_RESTART*/);					// 注册私有流
	pUserApi->RegisterFront(frontAddr);							// connect
	pUserApi->Init();
	//pUserApi->Join();
	return pUserApi;
}

//断开
TRADEAPI_API void DisConnect(CThostFtdcTraderApi* pUserApi)
{
	if (pUserApi == NULL) return;

	pUserApi->RegisterSpi(NULL);
	pUserApi->Release();
	pUserApi = NULL;
}

//获取交易日
TRADEAPI_API const char* GetTradingDay(CThostFtdcTraderApi* pUserApi)
{
	if (pUserApi == NULL) return NULL;

	return pUserApi->GetTradingDay();
}

//客户端认证请求
TRADEAPI_API int ReqAuthenticate(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID, TThostFtdcProductInfoType productInfo, TThostFtdcAuthCodeType authCode)
{
	if (pUserApi == NULL) return NULL;

	CThostFtdcReqAuthenticateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.UserProductInfo, productInfo);
	strcpy_s(req.AuthCode, authCode);
	return pUserApi->ReqAuthenticate(&req, requestID);
}

//发送用户登录请求
TRADEAPI_API int ReqUserLogin(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID, TThostFtdcPasswordType password)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcReqUserLoginField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.Password, password);
	strcpy_s(req.UserProductInfo, "HF");
	return pUserApi->ReqUserLogin(&req, requestID);
}
//发送登出请求
TRADEAPI_API int ReqUserLogout(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType investorID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcUserLogoutField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, investorID);
	return pUserApi->ReqUserLogout(&req, requestID);
}
//更新用户口令
TRADEAPI_API int ReqUserPasswordUpdate(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID, TThostFtdcUserIDType oldPassword, TThostFtdcPasswordType newPassword)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcUserPasswordUpdateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.OldPassword, oldPassword);
	strcpy_s(req.NewPassword, newPassword);
	return pUserApi->ReqUserPasswordUpdate(&req, requestID);
}
//资金账户口令更新请求
TRADEAPI_API int ReqTradingAccountPasswordUpdate(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcAccountIDType accountID, TThostFtdcUserIDType oldPassword, TThostFtdcPasswordType newPassword)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcTradingAccountPasswordUpdateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.AccountID, accountID);
	strcpy_s(req.NewPassword, newPassword);
	strcpy_s(req.OldPassword, oldPassword);
	return pUserApi->ReqTradingAccountPasswordUpdate(&req, requestID);
}
//安全登录请求
TRADEAPI_API int ReqUserSafeLogin(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID, TThostFtdcPasswordType password)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcReqUserLoginField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.Password, password);
	strcpy_s(req.UserProductInfo, "HF");
	return pUserApi->ReqUserLogin2(&req, requestID);
}
//安全更新用户口令
TRADEAPI_API int ReqUserPasswordSafeUpdate(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID, TThostFtdcUserIDType oldPassword, TThostFtdcPasswordType newPassword)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcUserPasswordUpdateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.OldPassword, oldPassword);
	strcpy_s(req.NewPassword, newPassword);
	return pUserApi->ReqUserPasswordUpdate2(&req, requestID);
}
//报单录入请求
TRADEAPI_API int ReqOrderInsert(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcInputOrderField *pOrder)
{
	if (pUserApi == NULL) return -1;

	int siez = sizeof(CThostFtdcInputOrderField);
	strcpy_s(pOrder->BusinessUnit, "HF");
	return pUserApi->ReqOrderInsert(pOrder, requestID);
}
//报单操作请求
TRADEAPI_API int ReqOrderAction(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcInputOrderActionField *pOrder)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqOrderAction(pOrder, requestID);
}
//查询最大报单数量请求
TRADEAPI_API int ReqQueryMaxOrderVolume(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQueryMaxOrderVolumeField *pMaxOrderVolume)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQueryMaxOrderVolume(pMaxOrderVolume, requestID);
}
//投资者结算结果确认
TRADEAPI_API int ReqSettlementInfoConfirm(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	
	if (pUserApi == NULL) return -1;

	CThostFtdcSettlementInfoConfirmField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqSettlementInfoConfirm(&req, requestID);
}
//执行宣告录入请求
TRADEAPI_API int ReqExecOrderInsert(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcInputExecOrderField *pInputExecOrder)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqExecOrderInsert(pInputExecOrder, requestID);
}
//执行宣告操作请求
TRADEAPI_API int ReqExecOrderAction(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcInputExecOrderActionField *pInputExecOrderAction)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqExecOrderAction(pInputExecOrderAction, requestID);
}
//询价录入请求
TRADEAPI_API int ReqForQuoteInsert(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcInputForQuoteField *pInputForQuote)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqForQuoteInsert(pInputForQuote, requestID);
}
//报价录入请求
TRADEAPI_API int ReqQuoteInsert(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcInputQuoteField *pInputQuote)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQuoteInsert(pInputQuote, requestID);
}
//报价操作请求
TRADEAPI_API int ReqQuoteAction(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcInputQuoteActionField *pInputQuoteAction)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQuoteAction(pInputQuoteAction, requestID);
}
//批量报单操作请求
TRADEAPI_API int ReqBatchOrderAction(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcInputBatchOrderActionField *pInputBatchOrderAction)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqBatchOrderAction(pInputBatchOrderAction, requestID);
}
//期权自对冲录入请求
TRADEAPI_API int ReqOptionSelfCloseInsert(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcInputOptionSelfCloseField *pInputOptionSelfClose)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqOptionSelfCloseInsert(pInputOptionSelfClose, requestID);
}
//期权自对冲操作请求
TRADEAPI_API int ReqOptionSelfCloseAction(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcInputOptionSelfCloseActionField *pInputOptionSelfCloseAction)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqOptionSelfCloseAction(pInputOptionSelfCloseAction, requestID);
}
//申请组合录入请求
TRADEAPI_API int ReqCombActionInsert(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcInputCombActionField *pInputCombAction)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqCombActionInsert(pInputCombAction, requestID);
}
//请求查询报单
TRADEAPI_API int ReqQryOrder(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryOrderField *pQryOrder)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQryOrder(pQryOrder, requestID);
}
//请求查询成交
TRADEAPI_API int ReqQryTrade(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryTradeField *pQryTrade)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQryTrade(pQryTrade, requestID);
}
//请求查询投资者持仓
TRADEAPI_API int ReqQryInvestorPosition(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryInvestorPositionField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryInvestorPosition(&req, requestID);
}
//请求查询资金账户
TRADEAPI_API int ReqQryTradingAccount(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryTradingAccountField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQryTradingAccount(&req, requestID);
}
//请求查询投资者
TRADEAPI_API int ReqQryInvestor(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryInvestorField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQryInvestor(&req, requestID);
}
//请求查询交易编码
TRADEAPI_API int ReqQryTradingCode(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcClientIDType clientID, TThostFtdcExchangeIDType	exchangeID)
{
	if (pUserApi == NULL) return -1;

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
//请求查询合约保证金率
TRADEAPI_API int ReqQryInstrumentMarginRate(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcHedgeFlagType	hedgeFlag)
{
	if (pUserApi == NULL) return -1;

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
//请求查询合约手续费率
TRADEAPI_API int ReqQryInstrumentCommissionRate(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryInstrumentCommissionRateField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryInstrumentCommissionRate(&req, requestID);
}
//请求查询交易所
TRADEAPI_API int ReqQryExchange(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcExchangeIDType exchangeID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryExchangeField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.ExchangeID, exchangeID);
	return pUserApi->ReqQryExchange(&req, requestID);
}
//请求查询合约
TRADEAPI_API int ReqQryInstrument(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryInstrumentField req;
	memset(&req, 0, sizeof(req));
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryInstrument(&req, requestID);
}
//请求查询行情
TRADEAPI_API int ReqQryDepthMarketData(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryDepthMarketDataField req;
	memset(&req, 0, sizeof(req));
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryDepthMarketData(&req, requestID);
}
//请求查询投资者结算结果
TRADEAPI_API int ReqQrySettlementInfo(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcDateType	tradingDay)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQrySettlementInfoField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (tradingDay != NULL)
		strcpy_s(req.TradingDay, tradingDay);
	return pUserApi->ReqQrySettlementInfo(&req, requestID);
}
//查询持仓明细
TRADEAPI_API int ReqQryInvestorPositionDetail(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryInvestorPositionDetailField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryInvestorPositionDetail(&req, requestID);
}
//请求查询客户通知
TRADEAPI_API int ReqQryNotice(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryNoticeField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	return pUserApi->ReqQryNotice(&req, requestID);
}
//请求查询结算信息确认
TRADEAPI_API int ReqQrySettlementInfoConfirm(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQrySettlementInfoConfirmField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQrySettlementInfoConfirm(&req, requestID);
}
//请求查询**组合**持仓明细
TRADEAPI_API int ReqQryInvestorPositionCombineDetail(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryInvestorPositionCombineDetailField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.CombInstrumentID, instrumentID);
	return pUserApi->ReqQryInvestorPositionCombineDetail(&req, requestID);
}
//请求查询保证金监管系统经纪公司资金账户密钥
TRADEAPI_API int ReqQryCFMMCTradingAccountKey(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryCFMMCTradingAccountKeyField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQryCFMMCTradingAccountKey(&req, requestID);
}
//请求查询仓单折抵信息
TRADEAPI_API int ReqQryEWarrantOffset(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryEWarrantOffsetField *pQryEWarrantOffset)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQryEWarrantOffset(pQryEWarrantOffset, requestID);
}
//请求查询投资者品种/跨品种保证金
TRADEAPI_API int ReqQryInvestorProductGroupMargin(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryInvestorProductGroupMarginField *pQryInvestorProductGroupMargin)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQryInvestorProductGroupMargin(pQryInvestorProductGroupMargin, requestID);
}
//请求查询交易所保证金率
TRADEAPI_API int ReqQryExchangeMarginRate(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcHedgeFlagType hedgeFlag)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryExchangeMarginRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	req.HedgeFlag = hedgeFlag;
	return pUserApi->ReqQryExchangeMarginRate(&req, requestID);
}
//请求查询交易所调整保证金率
TRADEAPI_API int ReqQryExchangeMarginRateAdjust(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcHedgeFlagType hedgeFlag)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryExchangeMarginRateAdjustField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	req.HedgeFlag = hedgeFlag;
	return pUserApi->ReqQryExchangeMarginRateAdjust(&req, requestID);
}
//请求查询汇率
TRADEAPI_API int ReqQryExchangeRate(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryExchangeRateField *pQryExchangeRate)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQryExchangeRate(pQryExchangeRate, requestID);
}
//请求查询二级代理操作员银期权限
TRADEAPI_API int ReqQrySecAgentACIDMap(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQrySecAgentACIDMapField *pQrySecAgentACIDMap)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQrySecAgentACIDMap(pQrySecAgentACIDMap, requestID);
}
//请求查询产品报价汇率
TRADEAPI_API int ReqQryProductExchRate(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcInstrumentIDType productID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryProductExchRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.ProductID, productID);
	return pUserApi->ReqQryProductExchRate(&req, requestID);
}
//请求查询产品组
TRADEAPI_API int ReqQryProductGroup(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcInstrumentIDType productID, TThostFtdcExchangeIDType exchangeID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryProductGroupField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.ProductID, productID);
	strcpy_s(req.ExchangeID, exchangeID);
	return pUserApi->ReqQryProductGroup(&req, requestID);
}
//请求查询做市商合约手续费率
TRADEAPI_API int ReqQryMMInstrumentCommissionRate(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryMMInstrumentCommissionRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryMMInstrumentCommissionRate(&req, requestID);
}
//请求查询做市商期权合约手续费
TRADEAPI_API int ReqQryMMOptionInstrCommRate(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryMMOptionInstrCommRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryMMOptionInstrCommRate(&req, requestID);
}
//请求查询报单手续费
TRADEAPI_API int ReqQryInstrumentOrderCommRate(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryInstrumentOrderCommRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryInstrumentOrderCommRate(&req, requestID);
}
//请求查询资金账户
TRADEAPI_API int ReqQrySecAgentTradingAccount(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryTradingAccountField *pQryTradingAccount)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQrySecAgentTradingAccount(pQryTradingAccount, requestID);
}
//请求查询二级代理商资金校验模式
TRADEAPI_API int ReqQrySecAgentCheckMode(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQrySecAgentCheckModeField *pQrySecAgentCheckMode)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQrySecAgentCheckMode(pQrySecAgentCheckMode, requestID);
}
//请求查询期权交易成本
TRADEAPI_API int ReqQryOptionInstrTradeCost(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryOptionInstrTradeCostField *pQryOptionInstrTradeCost)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQryOptionInstrTradeCost(pQryOptionInstrTradeCost, requestID);
}
//请求查询期权合约手续费
TRADEAPI_API int ReqQryOptionInstrCommRate(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryOptionInstrCommRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pUserApi->ReqQryOptionInstrCommRate(&req, requestID);
}
//请求查询执行宣告
TRADEAPI_API int ReqQryExecOrder(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryExecOrderField *pQryExecOrder)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQryExecOrder(pQryExecOrder, requestID);
}
//请求查询询价
TRADEAPI_API int ReqQryForQuote(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryForQuoteField *pQryForQuote)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQryForQuote(pQryForQuote, requestID);
}
//请求查询报价
TRADEAPI_API int ReqQryQuote(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryQuoteField *pQryQuote)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQryQuote(pQryQuote, requestID);
}
//请求查询期权自对冲
TRADEAPI_API int ReqQryOptionSelfClose(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryOptionSelfCloseField *pQryOptionSelfClose)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQryOptionSelfClose(pQryOptionSelfClose, requestID);
}
//请求查询投资单元
TRADEAPI_API int ReqQryInvestUnit(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryInvestUnitField *pQryInvestUnit)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQryInvestUnit(pQryInvestUnit, requestID);
}
//请求查询组合合约安全系数
TRADEAPI_API int ReqQryCombInstrumentGuard(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryCombInstrumentGuardField *pQryCombInstrumentGuard)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQryCombInstrumentGuard(pQryCombInstrumentGuard, requestID);
}
//请求查询申请组合
TRADEAPI_API int ReqQryCombAction(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcQryCombActionField *pQryCombAction)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQryCombAction(pQryCombAction, requestID);
}
//请求查询交易通知
TRADEAPI_API int ReqQryTradingNotice(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryTradingNoticeField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQryTradingNotice(&req, requestID);
}
//请求查询经纪公司交易参数
TRADEAPI_API int ReqQryBrokerTradingParams(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryBrokerTradingParamsField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQryBrokerTradingParams(&req, requestID);
}
//请求查询经纪公司交易算法
TRADEAPI_API int ReqQryBrokerTradingAlgos(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcExchangeIDType exchangeID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryBrokerTradingAlgosField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	if (exchangeID != NULL)
		strcpy_s(req.ExchangeID, exchangeID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);

	return pUserApi->ReqQryBrokerTradingAlgos(&req, requestID);
}
//预埋单录入请求
TRADEAPI_API int ReqParkedOrderInsert(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcParkedOrderField *ParkedOrder)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqParkedOrderInsert(ParkedOrder, requestID);
}
//预埋撤单录入请求
TRADEAPI_API int ReqParkedOrderAction(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcParkedOrderActionField *ParkedOrderAction)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqParkedOrderAction(ParkedOrderAction, requestID);
}
//请求删除预埋单
TRADEAPI_API int ReqRemoveParkedOrder(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcParkedOrderIDType parkedOrderID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcRemoveParkedOrderField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	strcpy_s(req.ParkedOrderID, parkedOrderID);
	return pUserApi->ReqRemoveParkedOrder(&req, requestID);
}
//请求删除预埋撤单
TRADEAPI_API int ReqRemoveParkedOrderAction(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcParkedOrderActionIDType parkedOrderActionID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcRemoveParkedOrderActionField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	strcpy_s(req.ParkedOrderActionID, parkedOrderActionID);
	return pUserApi->ReqRemoveParkedOrderAction(&req, requestID);
}

//请求查询转帐银行
TRADEAPI_API int ReqQryTransferBank(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBankIDType bankID, TThostFtdcBankBrchIDType bankBrchID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryTransferBankField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BankID, bankID);
	strcpy_s(req.BankBrchID, bankBrchID);
	return pUserApi->ReqQryTransferBank(&req, requestID);
}
//请求查询转帐流水
TRADEAPI_API int ReqQryTransferSerial(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcAccountIDType accountID, TThostFtdcBankIDType bankID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryTransferSerialField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.AccountID, accountID);
	strcpy_s(req.BankID, bankID);
	return pUserApi->ReqQryTransferSerial(&req, requestID);
}
//请求查询银期签约关系
TRADEAPI_API int ReqQryAccountregister(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcAccountIDType accountID, TThostFtdcBankIDType bankID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryAccountregisterField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.AccountID, accountID);
	strcpy_s(req.BankID, bankID);
	return pUserApi->ReqQryAccountregister(&req, requestID);
}
//请求查询签约银行
TRADEAPI_API int ReqQryContractBank(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcBankIDType bankID, TThostFtdcBankBrchIDType bankBrchID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQryContractBankField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	if (bankID != NULL)
		strcpy_s(req.BankID, bankID);
	if (bankBrchID != NULL)
		strcpy_s(req.BankBrchID, bankBrchID);
	return pUserApi->ReqQryContractBank(&req, requestID);
}
//请求查询预埋单
TRADEAPI_API int ReqQryParkedOrder(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcExchangeIDType exchangeID)
{
	if (pUserApi == NULL) return -1;

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
//请求查询预埋撤单
TRADEAPI_API int ReqQryParkedOrderAction(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcExchangeIDType exchangeID)
{
	if (pUserApi == NULL) return -1;

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
//请求查询监控中心用户令牌
TRADEAPI_API int ReqQueryCFMMCTradingAccountToken(CThostFtdcTraderApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pUserApi == NULL) return -1;

	CThostFtdcQueryCFMMCTradingAccountTokenField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pUserApi->ReqQueryCFMMCTradingAccountToken(&req, requestID);
}
//期货发起银行资金转期货请求
TRADEAPI_API int ReqFromBankToFutureByFuture(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcReqTransferField *reqTransfer)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqFromBankToFutureByFuture(reqTransfer, requestID);
}
//期货发起期货资金转银行请求
TRADEAPI_API int ReqFromFutureToBankByFuture(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcReqTransferField *reqTransfer)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqFromFutureToBankByFuture(reqTransfer, requestID);
}
//期货发起查询银行余额请求
TRADEAPI_API int ReqQueryBankAccountMoneyByFuture(CThostFtdcTraderApi* pUserApi, int requestID, CThostFtdcReqQueryAccountField *reqQueryAccount)
{
	if (pUserApi == NULL) return -1;

	return pUserApi->ReqQueryBankAccountMoneyByFuture(reqQueryAccount, requestID);
}
#pragma endregion

#pragma region 回调函数
//========================================
//==================================== 回调函数 =======================================

TRADEAPI_API void WINAPI RegOnFrontConnected(CTraderSpi* pUserSpi, CBOnFrontConnected cb)		//当客户端与交易后台建立起通信连接时（还未登录前），该方法被调用。
{
	pUserSpi->cbOnFrontConnected = cb;
}

TRADEAPI_API void WINAPI RegOnFrontDisconnected(CTraderSpi* pUserSpi, CBOnFrontDisconnected cb)		//当客户端与交易后台通信连接断开时，该方法被调用。当发生这个情况后，API会自动重新连接，客户端可不做处理。
{
	pUserSpi->cbOnFrontDisconnected = cb;
}

TRADEAPI_API void WINAPI RegOnHeartBeatWarning(CTraderSpi* pUserSpi, CBOnHeartBeatWarning cb)		//心跳超时警告。当长时间未收到报文时，该方法被调用。
{
	pUserSpi->cbOnHeartBeatWarning = cb;
}

TRADEAPI_API void WINAPI RegRspAuthenticate(CTraderSpi* pUserSpi, CBRspAuthenticate cb)
{
	pUserSpi->cbRspAuthenticate = cb;
}

TRADEAPI_API void WINAPI RegRspUserLogin(CTraderSpi* pUserSpi, CBRspUserLogin cb)	//登录请求响应
{
	pUserSpi->cbRspUserLogin = cb;
}
TRADEAPI_API void WINAPI RegRspUserLogout(CTraderSpi* pUserSpi, CBRspUserLogout cb)	//登出请求响应
{
	pUserSpi->cbRspUserLogout = cb;
}
TRADEAPI_API void WINAPI RegRspUserPasswordUpdate(CTraderSpi* pUserSpi, CBRspUserPasswordUpdate cb)	//用户口令更新请求响应
{
	pUserSpi->cbRspUserPasswordUpdate = cb;
}
TRADEAPI_API void WINAPI RegRspTradingAccountPasswordUpdate(CTraderSpi* pUserSpi, CBRspTradingAccountPasswordUpdate cb)	//资金账户口令更新请求响应
{
	pUserSpi->cbRspTradingAccountPasswordUpdate = cb;
}
TRADEAPI_API void WINAPI RegRspOrderInsert(CTraderSpi* pUserSpi, CBRspOrderInsert cb)	//报单录入请求响应
{
	pUserSpi->cbRspOrderInsert = cb;
}
TRADEAPI_API void WINAPI RegRspParkedOrderInsert(CTraderSpi* pUserSpi, CBRspParkedOrderInsert cb)	//预埋单录入请求响应
{
	pUserSpi->cbRspParkedOrderInsert = cb;
}
TRADEAPI_API void WINAPI RegRspParkedOrderAction(CTraderSpi* pUserSpi, CBRspParkedOrderAction cb)	//预埋撤单录入请求响应
{
	pUserSpi->cbRspParkedOrderAction = cb;
}
TRADEAPI_API void WINAPI RegRspOrderAction(CTraderSpi* pUserSpi, CBRspOrderAction cb)	//报单操作请求响应
{
	pUserSpi->cbRspOrderAction = cb;
}
TRADEAPI_API void WINAPI RegRspQueryMaxOrderVolume(CTraderSpi* pUserSpi, CBRspQueryMaxOrderVolume cb)	//查询最大报单数量响应
{
	pUserSpi->cbRspQueryMaxOrderVolume = cb;
}
TRADEAPI_API void WINAPI RegRspSettlementInfoConfirm(CTraderSpi* pUserSpi, CBRspSettlementInfoConfirm cb)	//投资者结算结果确认响应
{
	pUserSpi->cbRspSettlementInfoConfirm = cb;
}
TRADEAPI_API void WINAPI RegRspRemoveParkedOrder(CTraderSpi* pUserSpi, CBRspRemoveParkedOrder cb)	//删除预埋单响应
{
	pUserSpi->cbRspRemoveParkedOrder = cb;
}
TRADEAPI_API void WINAPI RegRspRemoveParkedOrderAction(CTraderSpi* pUserSpi, CBRspRemoveParkedOrderAction cb)	//删除预埋撤单响应
{
	pUserSpi->cbRspRemoveParkedOrderAction = cb;
}
TRADEAPI_API void WINAPI RegBatchOrderAction(CTraderSpi* pUserSpi, CBRspBatchOrderAction cb)	//批量报单操作请求响应
{
	pUserSpi->cbRspBatchOrderAction = cb;
}
TRADEAPI_API void WINAPI RegCombActionInsert(CTraderSpi* pUserSpi, CBRspCombActionInsert cb)	//申请组合录入请求响应
{
	pUserSpi->cbRspCombActionInsert = cb;
}
TRADEAPI_API void WINAPI RegRspQryOrder(CTraderSpi* pUserSpi, CBRspQryOrder cb)	//请求查询报单响应
{
	pUserSpi->cbRspQryOrder = cb;
}
TRADEAPI_API void WINAPI RegRspQryTrade(CTraderSpi* pUserSpi, CBRspQryTrade cb)	//请求查询成交响应
{
	pUserSpi->cbRspQryTrade = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestorPosition(CTraderSpi* pUserSpi, CBRspQryInvestorPosition cb)	//请求查询投资者持仓响应
{
	pUserSpi->cbRspQryInvestorPosition = cb;
}
TRADEAPI_API void WINAPI RegRspQryTradingAccount(CTraderSpi* pUserSpi, CBRspQryTradingAccount cb)	//请求查询资金账户响应
{
	pUserSpi->cbRspQryTradingAccount = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestor(CTraderSpi* pUserSpi, CBRspQryInvestor cb)	//请求查询投资者响应
{
	pUserSpi->cbRspQryInvestor = cb;
}
TRADEAPI_API void WINAPI RegRspQryTradingCode(CTraderSpi* pUserSpi, CBRspQryTradingCode cb)	//请求查询交易编码响应
{
	pUserSpi->cbRspQryTradingCode = cb;
}
TRADEAPI_API void WINAPI RegRspQryInstrumentMarginRate(CTraderSpi* pUserSpi, CBRspQryInstrumentMarginRate cb)	//请求查询合约保证金率响应
{
	pUserSpi->cbRspQryInstrumentMarginRate = cb;
}
TRADEAPI_API void WINAPI RegRspQryInstrumentCommissionRate(CTraderSpi* pUserSpi, CBRspQryInstrumentCommissionRate cb)	//请求查询合约手续费率响应
{
	pUserSpi->cbRspQryInstrumentCommissionRate = cb;
}
TRADEAPI_API void WINAPI RegRspQryExchange(CTraderSpi* pUserSpi, CBRspQryExchange cb)	//请求查询交易所响应
{
	pUserSpi->cbRspQryExchange = cb;
}
TRADEAPI_API void WINAPI RegRspQryInstrument(CTraderSpi* pUserSpi, CBRspQryInstrument cb)	//请求查询合约响应
{
	pUserSpi->cbRspQryInstrument = cb;
}
TRADEAPI_API void WINAPI RegRspQryDepthMarketData(CTraderSpi* pUserSpi, CBRspQryDepthMarketData cb)	//请求查询行情响应
{
	pUserSpi->cbRspQryDepthMarketData = cb;
}
TRADEAPI_API void WINAPI RegRspQrySettlementInfo(CTraderSpi* pUserSpi, CBRspQrySettlementInfo cb)	//请求查询投资者结算结果响应
{
	pUserSpi->cbRspQrySettlementInfo = cb;
}
TRADEAPI_API void WINAPI RegRspQryTransferBank(CTraderSpi* pUserSpi, CBRspQryTransferBank cb)	//请求查询转帐银行响应
{
	pUserSpi->cbRspQryTransferBank = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestorPositionDetail(CTraderSpi* pUserSpi, CBRspQryInvestorPositionDetail cb)	//请求查询投资者持仓明细响应
{
	pUserSpi->cbRspQryInvestorPositionDetail = cb;
}
TRADEAPI_API void WINAPI RegRspQryNotice(CTraderSpi* pUserSpi, CBRspQryNotice cb)	//请求查询客户通知响应
{
	pUserSpi->cbRspQryNotice = cb;
}
TRADEAPI_API void WINAPI RegRspQrySettlementInfoConfirm(CTraderSpi* pUserSpi, CBRspQrySettlementInfoConfirm cb)	//请求查询结算信息确认响应
{
	pUserSpi->cbRspQrySettlementInfoConfirm = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestorPositionCombineDetail(CTraderSpi* pUserSpi, CBRspQryInvestorPositionCombineDetail cb)	//请求查询投资者持仓明细响应
{
	pUserSpi->cbRspQryInvestorPositionCombineDetail = cb;
}
TRADEAPI_API void WINAPI RegRspQryCFMMCTradingAccountKey(CTraderSpi* pUserSpi, CBRspQryCFMMCTradingAccountKey cb)	//查询保证金监管系统经纪公司资金账户密钥响应
{
	pUserSpi->cbRspQryCFMMCTradingAccountKey = cb;
}
TRADEAPI_API void WINAPI RegRspQryEWarrantOffset(CTraderSpi* pUserSpi, CBRspQryEWarrantOffset cb)	//请求查询仓单折抵信息
{
	pUserSpi->cbRspQryEWarrantOffset = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestorProductGroupMargin(CTraderSpi* pUserSpi, CBRspQryInvestorProductGroupMargin cb)	//请求查询投资者品种/跨品种保证金
{
	pUserSpi->cbRspQryInvestorProductGroupMargin = cb;
}
TRADEAPI_API void WINAPI RegRspQryExchangeMarginRate(CTraderSpi* pUserSpi, CBRspQryExchangeMarginRate cb)	//请求查询交易所保证金率
{
	pUserSpi->cbRspQryExchangeMarginRate = cb;
}
TRADEAPI_API void WINAPI RegRspQryExchangeMarginRateAdjust(CTraderSpi* pUserSpi, CBRspQryExchangeMarginRateAdjust cb)	//请求查询交易所调整保证金率
{
	pUserSpi->cbRspQryExchangeMarginRateAdjust = cb;
}
TRADEAPI_API void WINAPI RegRspQryTransferSerial(CTraderSpi* pUserSpi, CBRspQryTransferSerial cb)	//请求查询转帐流水响应
{
	pUserSpi->cbRspQryTransferSerial = cb;
}
TRADEAPI_API void WINAPI RegRspQryAccountregister(CTraderSpi* pUserSpi, CBRspQryAccountregister cb)	//请求查询银期签约关系响应
{
	pUserSpi->cbRspQryAccountregister = cb;
}
TRADEAPI_API void WINAPI RegRspError(CTraderSpi* pUserSpi, CBRspError cb)	//错误应答
{
	pUserSpi->cbRspError = cb;
}
TRADEAPI_API void WINAPI RegRtnOrder(CTraderSpi* pUserSpi, CBRtnOrder cb)	//报单通知
{
	pUserSpi->cbRtnOrder = cb;
}
TRADEAPI_API void WINAPI RegRtnTrade(CTraderSpi* pUserSpi, CBRtnTrade cb)	//成交通知
{
	pUserSpi->cbRtnTrade = cb;
}
TRADEAPI_API void WINAPI RegErrRtnOrderInsert(CTraderSpi* pUserSpi, CBErrRtnOrderInsert cb)	//报单录入错误回报
{
	pUserSpi->cbErrRtnOrderInsert = cb;
}
TRADEAPI_API void WINAPI RegErrRtnOrderAction(CTraderSpi* pUserSpi, CBErrRtnOrderAction cb)	//报单操作错误回报
{
	pUserSpi->cbErrRtnOrderAction = cb;
}
TRADEAPI_API void WINAPI RegRtnInstrumentStatus(CTraderSpi* pUserSpi, CBRtnInstrumentStatus cb)	//合约交易状态通知
{
	pUserSpi->cbRtnInstrumentStatus = cb;
}
TRADEAPI_API void WINAPI RegRtnTradingNotice(CTraderSpi* pUserSpi, CBRtnTradingNotice cb)	//交易通知
{
	pUserSpi->cbRtnTradingNotice = cb;
}
TRADEAPI_API void WINAPI RegRtnErrorConditionalOrder(CTraderSpi* pUserSpi, CBRtnErrorConditionalOrder cb)	//提示条件单校验错误
{
	pUserSpi->cbRtnErrorConditionalOrder = cb;
}
TRADEAPI_API void WINAPI RegRspQryContractBank(CTraderSpi* pUserSpi, CBRspQryContractBank cb)	//请求查询签约银行响应
{
	pUserSpi->cbRspQryContractBank = cb;
}
TRADEAPI_API void WINAPI RegRspQryParkedOrder(CTraderSpi* pUserSpi, CBRspQryParkedOrder cb)	//请求查询预埋单响应
{
	pUserSpi->cbRspQryParkedOrder = cb;
}
TRADEAPI_API void WINAPI RegRspQryParkedOrderAction(CTraderSpi* pUserSpi, CBRspQryParkedOrderAction cb)	//请求查询预埋撤单响应
{
	pUserSpi->cbRspQryParkedOrderAction = cb;
}
TRADEAPI_API void WINAPI RegRspQryTradingNotice(CTraderSpi* pUserSpi, CBRspQryTradingNotice cb)	//请求查询交易通知响应
{
	pUserSpi->cbRspQryTradingNotice = cb;
}
TRADEAPI_API void WINAPI RegRspQryBrokerTradingParams(CTraderSpi* pUserSpi, CBRspQryBrokerTradingParams cb)	//请求查询经纪公司交易参数响应
{
	pUserSpi->cbRspQryBrokerTradingParams = cb;
}
TRADEAPI_API void WINAPI RegRspQryBrokerTradingAlgos(CTraderSpi* pUserSpi, CBRspQryBrokerTradingAlgos cb)	//请求查询经纪公司交易算法响应
{
	pUserSpi->cbRspQryBrokerTradingAlgos = cb;
}
TRADEAPI_API void WINAPI RegRtnFromBankToFutureByBank(CTraderSpi* pUserSpi, CBRtnFromBankToFutureByBank cb)	//银行发起银行资金转期货通知
{
	pUserSpi->cbRtnFromBankToFutureByBank = cb;
}
TRADEAPI_API void WINAPI RegRtnFromFutureToBankByBank(CTraderSpi* pUserSpi, CBRtnFromFutureToBankByBank cb)	//银行发起期货资金转银行通知
{
	pUserSpi->cbRtnFromFutureToBankByBank = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromBankToFutureByBank(CTraderSpi* pUserSpi, CBRtnRepealFromBankToFutureByBank cb)	//银行发起冲正银行转期货通知
{
	pUserSpi->cbRtnRepealFromBankToFutureByBank = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromFutureToBankByBank(CTraderSpi* pUserSpi, CBRtnRepealFromFutureToBankByBank cb)	//银行发起冲正期货转银行通知
{
	pUserSpi->cbRtnRepealFromFutureToBankByBank = cb;
}
TRADEAPI_API void WINAPI RegRtnFromBankToFutureByFuture(CTraderSpi* pUserSpi, CBRtnFromBankToFutureByFuture cb)	//期货发起银行资金转期货通知
{
	pUserSpi->cbRtnFromBankToFutureByFuture = cb;
}
TRADEAPI_API void WINAPI RegRtnFromFutureToBankByFuture(CTraderSpi* pUserSpi, CBRtnFromFutureToBankByFuture cb)	//期货发起期货资金转银行通知
{
	pUserSpi->cbRtnFromFutureToBankByFuture = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromBankToFutureByFutureManual(CTraderSpi* pUserSpi, CBRtnRepealFromBankToFutureByFutureManual cb)	//系统运行时期货端手工发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
{
	pUserSpi->cbRtnRepealFromBankToFutureByFutureManual = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromFutureToBankByFutureManual(CTraderSpi* pUserSpi, CBRtnRepealFromFutureToBankByFutureManual cb)	//系统运行时期货端手工发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
{
	pUserSpi->cbRtnRepealFromFutureToBankByFutureManual = cb;
}
TRADEAPI_API void WINAPI RegRtnQueryBankBalanceByFuture(CTraderSpi* pUserSpi, CBRtnQueryBankBalanceByFuture cb)	//期货发起查询银行余额通知
{
	pUserSpi->cbRtnQueryBankBalanceByFuture = cb;
}
TRADEAPI_API void WINAPI RegErrRtnBankToFutureByFuture(CTraderSpi* pUserSpi, CBErrRtnBankToFutureByFuture cb)	//期货发起银行资金转期货错误回报
{
	pUserSpi->cbErrRtnBankToFutureByFuture = cb;
}
TRADEAPI_API void WINAPI RegErrRtnFutureToBankByFuture(CTraderSpi* pUserSpi, CBErrRtnFutureToBankByFuture cb)	//期货发起期货资金转银行错误回报
{
	pUserSpi->cbErrRtnFutureToBankByFuture = cb;
}
TRADEAPI_API void WINAPI RegErrRtnRepealBankToFutureByFutureManual(CTraderSpi* pUserSpi, CBErrRtnRepealBankToFutureByFutureManual cb)	//系统运行时期货端手工发起冲正银行转期货错误回报
{
	pUserSpi->cbErrRtnRepealBankToFutureByFutureManual = cb;
}
TRADEAPI_API void WINAPI RegErrRtnRepealFutureToBankByFutureManual(CTraderSpi* pUserSpi, CBErrRtnRepealFutureToBankByFutureManual cb)	//系统运行时期货端手工发起冲正期货转银行错误回报
{
	pUserSpi->cbErrRtnRepealFutureToBankByFutureManual = cb;
}
TRADEAPI_API void WINAPI RegErrRtnQueryBankBalanceByFuture(CTraderSpi* pUserSpi, CBErrRtnQueryBankBalanceByFuture cb)	//期货发起查询银行余额错误回报
{
	pUserSpi->cbErrRtnQueryBankBalanceByFuture = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromBankToFutureByFuture(CTraderSpi* pUserSpi, CBRtnRepealFromBankToFutureByFuture cb)	//期货发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
{
	pUserSpi->cbRtnRepealFromBankToFutureByFuture = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromFutureToBankByFuture(CTraderSpi* pUserSpi, CBRtnRepealFromFutureToBankByFuture cb)	//期货发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
{
	pUserSpi->cbRtnRepealFromFutureToBankByFuture = cb;
}
TRADEAPI_API void WINAPI RegRspFromBankToFutureByFuture(CTraderSpi* pUserSpi, CBRspFromBankToFutureByFuture cb)	//期货发起银行资金转期货应答
{
	pUserSpi->cbRspFromBankToFutureByFuture = cb;
}
TRADEAPI_API void WINAPI RegRspFromFutureToBankByFuture(CTraderSpi* pUserSpi, CBRspFromFutureToBankByFuture cb)	//期货发起期货资金转银行应答
{
	pUserSpi->cbRspFromFutureToBankByFuture = cb;
}
TRADEAPI_API void WINAPI RegRspQueryBankAccountMoneyByFuture(CTraderSpi* pUserSpi, CBRspQueryBankAccountMoneyByFuture cb)	//期货发起查询银行余额应答
{
	pUserSpi->cbRspQueryBankAccountMoneyByFuture = cb;
}
#pragma endregion