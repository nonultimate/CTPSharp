
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
	CTraderSpi* pSpi = new CTraderSpi();
	return pSpi;
}

//连接
TRADEAPI_API CThostFtdcTraderApi* Connect(char *frontAddr, char *pszFlowPath, CTraderSpi* pSpi)
{
	// 初始化UserApi
	CThostFtdcTraderApi* pApi = CThostFtdcTraderApi::CreateFtdcTraderApi(pszFlowPath);			// 创建UserApi	
																									//CTraderSpi* pSpi = new CTraderSpi();
	pApi->RegisterSpi((CThostFtdcTraderSpi*)pSpi);			// 注册事件类
	pApi->SubscribePublicTopic(THOST_TERT_QUICK/*THOST_TERT_RESTART*/);					// 注册公有流
	pApi->SubscribePrivateTopic(THOST_TERT_QUICK/*THOST_TERT_RESTART*/);					// 注册私有流
	pApi->RegisterFront(frontAddr);							// connect
	pApi->Init();
	//pApi->Join();
	return pApi;
}

//断开
TRADEAPI_API void DisConnect(CThostFtdcTraderApi* pApi)
{
	if (pApi == NULL) return;

	pApi->RegisterSpi(NULL);
	pApi->Release();
	pApi = NULL;
}

//获取交易日
TRADEAPI_API const char* GetTradingDay(CThostFtdcTraderApi* pApi)
{
	if (pApi == NULL) return NULL;

	return pApi->GetTradingDay();
}

//客户端认证请求
TRADEAPI_API int ReqAuthenticate(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID,
	TThostFtdcProductInfoType productInfo, TThostFtdcAuthCodeType authCode, TThostFtdcAppIDType	appID)
{
	if (pApi == NULL) return -1;

	CThostFtdcReqAuthenticateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.UserProductInfo, productInfo);
	strcpy_s(req.AuthCode, authCode);
	strcpy_s(req.AppID, appID);

	return pApi->ReqAuthenticate(&req, requestID);
}

//需要在终端认证成功后，用户登录前调用该接口
TRADEAPI_API int RegisterUserSystemInfo(CThostFtdcTraderApi* pApi, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID,
	TThostFtdcClientSystemInfoType clientSystemInfo, TThostFtdcSystemInfoLenType clientSystemInfoLen, TThostFtdcIPAddressType clientPublicIP,
	TThostFtdcIPPortType clientIPPort, TThostFtdcTimeType clientLoginTime, TThostFtdcAppIDType clientAppID)
{
	if (pApi == NULL) return -1;
	CThostFtdcUserSystemInfoField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.ClientSystemInfo, clientSystemInfo);
	strcpy_s(req.ClientPublicIP, clientPublicIP);
	strcpy_s(req.ClientLoginTime, clientLoginTime);
	strcpy_s(req.ClientAppID, clientAppID);
	req.ClientSystemInfoLen = clientSystemInfoLen;
	req.ClientIPPort = clientIPPort;

	return pApi->RegisterUserSystemInfo(&req);
}

//上报用户终端信息，用于中继服务器操作员登录模式
//操作员登录后，可以多次调用该接口上报客户信息
TRADEAPI_API int SubmitUserSystemInfo(CThostFtdcTraderApi* pApi, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID,
	TThostFtdcClientSystemInfoType clientSystemInfo, TThostFtdcSystemInfoLenType clientSystemInfoLen, TThostFtdcIPAddressType clientPublicIP,
	TThostFtdcIPPortType clientIPPort, TThostFtdcTimeType clientLoginTime, TThostFtdcAppIDType clientAppID)
{
	if (pApi == NULL) return -1;
	CThostFtdcUserSystemInfoField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.ClientSystemInfo, clientSystemInfo);
	strcpy_s(req.ClientPublicIP, clientPublicIP);
	strcpy_s(req.ClientLoginTime, clientLoginTime);
	strcpy_s(req.ClientAppID, clientAppID);
	req.ClientSystemInfoLen = clientSystemInfoLen;
	req.ClientIPPort = clientIPPort;

	pApi->SubmitUserSystemInfo(&req);
}

//发送用户登录请求
TRADEAPI_API int ReqUserLogin(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID,
	TThostFtdcPasswordType password, TThostFtdcPasswordType oneTimePassword, TThostFtdcMacAddressType macAddress, TThostFtdcProductInfoType productInfo,
	TThostFtdcProductInfoType interfaceInfo, TThostFtdcProtocolInfoType	protocolInfo)
{
	if (pApi == NULL) return -1;

	CThostFtdcReqUserLoginField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.Password, password);
	if (oneTimePassword != NULL) strcpy_s(req.OneTimePassword, oneTimePassword);
	if (macAddress != NULL) strcpy_s(req.MacAddress, macAddress);
	if (productInfo != NULL) strcpy_s(req.UserProductInfo, productInfo);
	if (interfaceInfo != NULL) strcpy_s(req.InterfaceProductInfo, interfaceInfo);
	if (protocolInfo != NULL) strcpy_s(req.ProtocolInfo, protocolInfo);

	return pApi->ReqUserLogin(&req, requestID);
}
//发送登出请求
TRADEAPI_API int ReqUserLogout(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType investorID)
{
	if (pApi == NULL) return -1;

	CThostFtdcUserLogoutField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, investorID);
	return pApi->ReqUserLogout(&req, requestID);
}
//更新用户口令
TRADEAPI_API int ReqUserPasswordUpdate(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID,
	TThostFtdcUserIDType oldPassword, TThostFtdcPasswordType newPassword)
{
	if (pApi == NULL) return -1;

	CThostFtdcUserPasswordUpdateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.OldPassword, oldPassword);
	strcpy_s(req.NewPassword, newPassword);
	return pApi->ReqUserPasswordUpdate(&req, requestID);
}
//资金账户口令更新请求
TRADEAPI_API int ReqTradingAccountPasswordUpdate(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID,
	TThostFtdcAccountIDType accountID, TThostFtdcUserIDType oldPassword, TThostFtdcPasswordType newPassword)
{
	if (pApi == NULL) return -1;

	CThostFtdcTradingAccountPasswordUpdateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.AccountID, accountID);
	strcpy_s(req.NewPassword, newPassword);
	strcpy_s(req.OldPassword, oldPassword);
	return pApi->ReqTradingAccountPasswordUpdate(&req, requestID);
}
//查询用户当前支持的认证模式
TRADEAPI_API int ReqUserAuthMethod(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID, TThostFtdcDateType tradingDay)
{
	if (pApi == NULL) return -1;

	CThostFtdcReqUserAuthMethodField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.TradingDay, tradingDay);

	return pApi->ReqUserAuthMethod(&req, requestID);
}
//用户发出获取图形验证码请求
TRADEAPI_API int ReqGenUserCaptcha(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID, TThostFtdcDateType tradingDay)
{
	if (pApi == NULL) return -1;

	CThostFtdcReqGenUserCaptchaField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.TradingDay, tradingDay);

	return pApi->ReqGenUserCaptcha(&req, requestID);
}
//用户发出获取短信验证码请求
TRADEAPI_API int ReqGenUserText(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcUserIDType userID, TThostFtdcDateType tradingDay)
{
	if (pApi == NULL) return -1;

	CThostFtdcReqGenUserTextField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, userID);
	strcpy_s(req.TradingDay, tradingDay);

	return pApi->ReqGenUserText(&req, requestID);
}
//用户发出带有图片验证码的登陆请求
TRADEAPI_API int ReqUserLoginWithCaptcha(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcReqUserLoginWithCaptchaField *pReqUserLoginWithCaptcha)
{
	if (pApi == NULL) return -1;

	return pApi->ReqUserLoginWithCaptcha(pReqUserLoginWithCaptcha, requestID);
}
//用户发出带有短信验证码的登陆请求
TRADEAPI_API int ReqUserLoginWithText(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcReqUserLoginWithTextField *pReqUserLoginWithText)
{
	if (pApi == NULL) return -1;

	return pApi->ReqUserLoginWithText(pReqUserLoginWithText, requestID);
}
//用户发出带有动态口令的登陆请求
TRADEAPI_API int ReqUserLoginWithOTP(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcReqUserLoginWithOTPField *pReqUserLoginWithOTP)
{
	if (pApi == NULL) return -1;

	return pApi->ReqUserLoginWithOTP(pReqUserLoginWithOTP, requestID);
}
//报单录入请求
TRADEAPI_API int ReqOrderInsert(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcInputOrderField *pOrder)
{
	if (pApi == NULL) return -1;

	int siez = sizeof(CThostFtdcInputOrderField);
	strcpy_s(pOrder->BusinessUnit, "HF");
	return pApi->ReqOrderInsert(pOrder, requestID);
}
//报单操作请求
TRADEAPI_API int ReqOrderAction(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcInputOrderActionField *pOrder)
{
	if (pApi == NULL) return -1;

	return pApi->ReqOrderAction(pOrder, requestID);
}
//查询最大报单数量请求
TRADEAPI_API int ReqQueryMaxOrderVolume(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQueryMaxOrderVolumeField *pMaxOrderVolume)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQueryMaxOrderVolume(pMaxOrderVolume, requestID);
}
//投资者结算结果确认
TRADEAPI_API int ReqSettlementInfoConfirm(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	
	if (pApi == NULL) return -1;

	CThostFtdcSettlementInfoConfirmField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pApi->ReqSettlementInfoConfirm(&req, requestID);
}
//执行宣告录入请求
TRADEAPI_API int ReqExecOrderInsert(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcInputExecOrderField *pInputExecOrder)
{
	if (pApi == NULL) return -1;

	return pApi->ReqExecOrderInsert(pInputExecOrder, requestID);
}
//执行宣告操作请求
TRADEAPI_API int ReqExecOrderAction(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcInputExecOrderActionField *pInputExecOrderAction)
{
	if (pApi == NULL) return -1;

	return pApi->ReqExecOrderAction(pInputExecOrderAction, requestID);
}
//询价录入请求
TRADEAPI_API int ReqForQuoteInsert(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcInputForQuoteField *pInputForQuote)
{
	if (pApi == NULL) return -1;

	return pApi->ReqForQuoteInsert(pInputForQuote, requestID);
}
//报价录入请求
TRADEAPI_API int ReqQuoteInsert(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcInputQuoteField *pInputQuote)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQuoteInsert(pInputQuote, requestID);
}
//报价操作请求
TRADEAPI_API int ReqQuoteAction(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcInputQuoteActionField *pInputQuoteAction)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQuoteAction(pInputQuoteAction, requestID);
}
//批量报单操作请求
TRADEAPI_API int ReqBatchOrderAction(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcInputBatchOrderActionField *pInputBatchOrderAction)
{
	if (pApi == NULL) return -1;

	return pApi->ReqBatchOrderAction(pInputBatchOrderAction, requestID);
}
//期权自对冲录入请求
TRADEAPI_API int ReqOptionSelfCloseInsert(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcInputOptionSelfCloseField *pInputOptionSelfClose)
{
	if (pApi == NULL) return -1;

	return pApi->ReqOptionSelfCloseInsert(pInputOptionSelfClose, requestID);
}
//期权自对冲操作请求
TRADEAPI_API int ReqOptionSelfCloseAction(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcInputOptionSelfCloseActionField *pInputOptionSelfCloseAction)
{
	if (pApi == NULL) return -1;

	return pApi->ReqOptionSelfCloseAction(pInputOptionSelfCloseAction, requestID);
}
//申请组合录入请求
TRADEAPI_API int ReqCombActionInsert(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcInputCombActionField *pInputCombAction)
{
	if (pApi == NULL) return -1;

	return pApi->ReqCombActionInsert(pInputCombAction, requestID);
}
//请求查询报单
TRADEAPI_API int ReqQryOrder(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryOrderField *pQryOrder)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQryOrder(pQryOrder, requestID);
}
//请求查询成交
TRADEAPI_API int ReqQryTrade(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryTradeField *pQryTrade)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQryTrade(pQryTrade, requestID);
}
//请求查询投资者持仓
TRADEAPI_API int ReqQryInvestorPosition(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryInvestorPositionField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pApi->ReqQryInvestorPosition(&req, requestID);
}
//请求查询资金账户
TRADEAPI_API int ReqQryTradingAccount(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryTradingAccountField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pApi->ReqQryTradingAccount(&req, requestID);
}
//请求查询投资者
TRADEAPI_API int ReqQryInvestor(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryInvestorField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pApi->ReqQryInvestor(&req, requestID);
}
//请求查询交易编码
TRADEAPI_API int ReqQryTradingCode(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcClientIDType clientID, TThostFtdcExchangeIDType	exchangeID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryTradingCodeField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (clientID != NULL)
		strcpy_s(req.ClientID, clientID);
	if (exchangeID != NULL)
		strcpy_s(req.ExchangeID, exchangeID);
	return pApi->ReqQryTradingCode(&req, requestID);
}
//请求查询合约保证金率
TRADEAPI_API int ReqQryInstrumentMarginRate(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcHedgeFlagType	hedgeFlag)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryInstrumentMarginRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	if (hedgeFlag != NULL)
		req.HedgeFlag = hedgeFlag;						//*不*能采用null进行所有查询
	return pApi->ReqQryInstrumentMarginRate(&req, requestID);
}
//请求查询合约手续费率
TRADEAPI_API int ReqQryInstrumentCommissionRate(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryInstrumentCommissionRateField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pApi->ReqQryInstrumentCommissionRate(&req, requestID);
}
//请求查询交易所
TRADEAPI_API int ReqQryExchange(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcExchangeIDType exchangeID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryExchangeField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.ExchangeID, exchangeID);
	return pApi->ReqQryExchange(&req, requestID);
}
//请求查询合约
TRADEAPI_API int ReqQryInstrument(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryInstrumentField req;
	memset(&req, 0, sizeof(req));
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pApi->ReqQryInstrument(&req, requestID);
}
//请求查询行情
TRADEAPI_API int ReqQryDepthMarketData(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryDepthMarketDataField req;
	memset(&req, 0, sizeof(req));
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pApi->ReqQryDepthMarketData(&req, requestID);
}
//请求查询投资者结算结果
TRADEAPI_API int ReqQrySettlementInfo(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcDateType	tradingDay)
{
	if (pApi == NULL) return -1;

	CThostFtdcQrySettlementInfoField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (tradingDay != NULL)
		strcpy_s(req.TradingDay, tradingDay);
	return pApi->ReqQrySettlementInfo(&req, requestID);
}
//查询持仓明细
TRADEAPI_API int ReqQryInvestorPositionDetail(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryInvestorPositionDetailField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pApi->ReqQryInvestorPositionDetail(&req, requestID);
}
//请求查询客户通知
TRADEAPI_API int ReqQryNotice(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryNoticeField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	return pApi->ReqQryNotice(&req, requestID);
}
//请求查询结算信息确认
TRADEAPI_API int ReqQrySettlementInfoConfirm(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQrySettlementInfoConfirmField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pApi->ReqQrySettlementInfoConfirm(&req, requestID);
}
//请求查询**组合**持仓明细
TRADEAPI_API int ReqQryInvestorPositionCombineDetail(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryInvestorPositionCombineDetailField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.CombInstrumentID, instrumentID);
	return pApi->ReqQryInvestorPositionCombineDetail(&req, requestID);
}
//请求查询保证金监管系统经纪公司资金账户密钥
TRADEAPI_API int ReqQryCFMMCTradingAccountKey(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryCFMMCTradingAccountKeyField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pApi->ReqQryCFMMCTradingAccountKey(&req, requestID);
}
//请求查询仓单折抵信息
TRADEAPI_API int ReqQryEWarrantOffset(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryEWarrantOffsetField *pQryEWarrantOffset)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQryEWarrantOffset(pQryEWarrantOffset, requestID);
}
//请求查询投资者品种/跨品种保证金
TRADEAPI_API int ReqQryInvestorProductGroupMargin(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryInvestorProductGroupMarginField *pQryInvestorProductGroupMargin)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQryInvestorProductGroupMargin(pQryInvestorProductGroupMargin, requestID);
}
//请求查询交易所保证金率
TRADEAPI_API int ReqQryExchangeMarginRate(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcHedgeFlagType hedgeFlag)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryExchangeMarginRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	req.HedgeFlag = hedgeFlag;
	return pApi->ReqQryExchangeMarginRate(&req, requestID);
}
//请求查询交易所调整保证金率
TRADEAPI_API int ReqQryExchangeMarginRateAdjust(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcHedgeFlagType hedgeFlag)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryExchangeMarginRateAdjustField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	req.HedgeFlag = hedgeFlag;
	return pApi->ReqQryExchangeMarginRateAdjust(&req, requestID);
}
//请求查询汇率
TRADEAPI_API int ReqQryExchangeRate(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryExchangeRateField *pQryExchangeRate)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQryExchangeRate(pQryExchangeRate, requestID);
}
//请求查询二级代理操作员银期权限
TRADEAPI_API int ReqQrySecAgentACIDMap(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQrySecAgentACIDMapField *pQrySecAgentACIDMap)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQrySecAgentACIDMap(pQrySecAgentACIDMap, requestID);
}
//请求查询产品报价汇率
TRADEAPI_API int ReqQryProductExchRate(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcInstrumentIDType productID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryProductExchRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.ProductID, productID);
	return pApi->ReqQryProductExchRate(&req, requestID);
}
//请求查询产品组
TRADEAPI_API int ReqQryProductGroup(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcInstrumentIDType productID, TThostFtdcExchangeIDType exchangeID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryProductGroupField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.ProductID, productID);
	strcpy_s(req.ExchangeID, exchangeID);
	return pApi->ReqQryProductGroup(&req, requestID);
}
//请求查询做市商合约手续费率
TRADEAPI_API int ReqQryMMInstrumentCommissionRate(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryMMInstrumentCommissionRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pApi->ReqQryMMInstrumentCommissionRate(&req, requestID);
}
//请求查询做市商期权合约手续费
TRADEAPI_API int ReqQryMMOptionInstrCommRate(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryMMOptionInstrCommRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pApi->ReqQryMMOptionInstrCommRate(&req, requestID);
}
//请求查询报单手续费
TRADEAPI_API int ReqQryInstrumentOrderCommRate(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryInstrumentOrderCommRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pApi->ReqQryInstrumentOrderCommRate(&req, requestID);
}
//请求查询资金账户
TRADEAPI_API int ReqQrySecAgentTradingAccount(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryTradingAccountField *pQryTradingAccount)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQrySecAgentTradingAccount(pQryTradingAccount, requestID);
}
//请求查询二级代理商资金校验模式
TRADEAPI_API int ReqQrySecAgentCheckMode(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQrySecAgentCheckModeField *pQrySecAgentCheckMode)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQrySecAgentCheckMode(pQrySecAgentCheckMode, requestID);
}
//请求查询期权交易成本
TRADEAPI_API int ReqQryOptionInstrTradeCost(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryOptionInstrTradeCostField *pQryOptionInstrTradeCost)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQryOptionInstrTradeCost(pQryOptionInstrTradeCost, requestID);
}
//请求查询期权合约手续费
TRADEAPI_API int ReqQryOptionInstrCommRate(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryOptionInstrCommRateField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	return pApi->ReqQryOptionInstrCommRate(&req, requestID);
}
//请求查询执行宣告
TRADEAPI_API int ReqQryExecOrder(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryExecOrderField *pQryExecOrder)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQryExecOrder(pQryExecOrder, requestID);
}
//请求查询询价
TRADEAPI_API int ReqQryForQuote(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryForQuoteField *pQryForQuote)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQryForQuote(pQryForQuote, requestID);
}
//请求查询报价
TRADEAPI_API int ReqQryQuote(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryQuoteField *pQryQuote)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQryQuote(pQryQuote, requestID);
}
//请求查询期权自对冲
TRADEAPI_API int ReqQryOptionSelfClose(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryOptionSelfCloseField *pQryOptionSelfClose)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQryOptionSelfClose(pQryOptionSelfClose, requestID);
}
//请求查询投资单元
TRADEAPI_API int ReqQryInvestUnit(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryInvestUnitField *pQryInvestUnit)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQryInvestUnit(pQryInvestUnit, requestID);
}
//请求查询组合合约安全系数
TRADEAPI_API int ReqQryCombInstrumentGuard(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryCombInstrumentGuardField *pQryCombInstrumentGuard)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQryCombInstrumentGuard(pQryCombInstrumentGuard, requestID);
}
//请求查询申请组合
TRADEAPI_API int ReqQryCombAction(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcQryCombActionField *pQryCombAction)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQryCombAction(pQryCombAction, requestID);
}
//请求查询交易通知
TRADEAPI_API int ReqQryTradingNotice(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryTradingNoticeField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pApi->ReqQryTradingNotice(&req, requestID);
}
//请求查询经纪公司交易参数
TRADEAPI_API int ReqQryBrokerTradingParams(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryBrokerTradingParamsField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pApi->ReqQryBrokerTradingParams(&req, requestID);
}
//请求查询经纪公司交易算法
TRADEAPI_API int ReqQryBrokerTradingAlgos(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcExchangeIDType exchangeID, TThostFtdcInstrumentIDType instrumentID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryBrokerTradingAlgosField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	if (exchangeID != NULL)
		strcpy_s(req.ExchangeID, exchangeID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);

	return pApi->ReqQryBrokerTradingAlgos(&req, requestID);
}
//预埋单录入请求
TRADEAPI_API int ReqParkedOrderInsert(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcParkedOrderField *ParkedOrder)
{
	if (pApi == NULL) return -1;

	return pApi->ReqParkedOrderInsert(ParkedOrder, requestID);
}
//预埋撤单录入请求
TRADEAPI_API int ReqParkedOrderAction(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcParkedOrderActionField *ParkedOrderAction)
{
	if (pApi == NULL) return -1;

	return pApi->ReqParkedOrderAction(ParkedOrderAction, requestID);
}
//请求删除预埋单
TRADEAPI_API int ReqRemoveParkedOrder(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcParkedOrderIDType parkedOrderID)
{
	if (pApi == NULL) return -1;

	CThostFtdcRemoveParkedOrderField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	strcpy_s(req.ParkedOrderID, parkedOrderID);
	return pApi->ReqRemoveParkedOrder(&req, requestID);
}
//请求删除预埋撤单
TRADEAPI_API int ReqRemoveParkedOrderAction(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcParkedOrderActionIDType parkedOrderActionID)
{
	if (pApi == NULL) return -1;

	CThostFtdcRemoveParkedOrderActionField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	strcpy_s(req.ParkedOrderActionID, parkedOrderActionID);
	return pApi->ReqRemoveParkedOrderAction(&req, requestID);
}

//请求查询转帐银行
TRADEAPI_API int ReqQryTransferBank(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBankIDType bankID, TThostFtdcBankBrchIDType bankBrchID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryTransferBankField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BankID, bankID);
	strcpy_s(req.BankBrchID, bankBrchID);
	return pApi->ReqQryTransferBank(&req, requestID);
}
//请求查询转帐流水
TRADEAPI_API int ReqQryTransferSerial(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcAccountIDType accountID, TThostFtdcBankIDType bankID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryTransferSerialField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.AccountID, accountID);
	strcpy_s(req.BankID, bankID);
	return pApi->ReqQryTransferSerial(&req, requestID);
}
//请求查询银期签约关系
TRADEAPI_API int ReqQryAccountregister(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcAccountIDType accountID, TThostFtdcBankIDType bankID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryAccountregisterField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.AccountID, accountID);
	strcpy_s(req.BankID, bankID);
	return pApi->ReqQryAccountregister(&req, requestID);
}
//请求查询签约银行
TRADEAPI_API int ReqQryContractBank(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcBankIDType bankID, TThostFtdcBankBrchIDType bankBrchID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryContractBankField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	if (bankID != NULL)
		strcpy_s(req.BankID, bankID);
	if (bankBrchID != NULL)
		strcpy_s(req.BankBrchID, bankBrchID);
	return pApi->ReqQryContractBank(&req, requestID);
}
//请求查询预埋单
TRADEAPI_API int ReqQryParkedOrder(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcExchangeIDType exchangeID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryParkedOrderField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	if (exchangeID != NULL)
		strcpy_s(req.ExchangeID, exchangeID);
	return pApi->ReqQryParkedOrder(&req, requestID);
}
//请求查询预埋撤单
TRADEAPI_API int ReqQryParkedOrderAction(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcInstrumentIDType instrumentID, TThostFtdcExchangeIDType exchangeID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQryParkedOrderActionField  req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	if (instrumentID != NULL)
		strcpy_s(req.InstrumentID, instrumentID);
	if (exchangeID != NULL)
		strcpy_s(req.ExchangeID, exchangeID);
	return pApi->ReqQryParkedOrderAction(&req, requestID);
}
//请求查询监控中心用户令牌
TRADEAPI_API int ReqQueryCFMMCTradingAccountToken(CThostFtdcTraderApi* pApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pApi == NULL) return -1;

	CThostFtdcQueryCFMMCTradingAccountTokenField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.InvestorID, investorID);
	return pApi->ReqQueryCFMMCTradingAccountToken(&req, requestID);
}
//期货发起银行资金转期货请求
TRADEAPI_API int ReqFromBankToFutureByFuture(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcReqTransferField *reqTransfer)
{
	if (pApi == NULL) return -1;

	return pApi->ReqFromBankToFutureByFuture(reqTransfer, requestID);
}
//期货发起期货资金转银行请求
TRADEAPI_API int ReqFromFutureToBankByFuture(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcReqTransferField *reqTransfer)
{
	if (pApi == NULL) return -1;

	return pApi->ReqFromFutureToBankByFuture(reqTransfer, requestID);
}
//期货发起查询银行余额请求
TRADEAPI_API int ReqQueryBankAccountMoneyByFuture(CThostFtdcTraderApi* pApi, int requestID, CThostFtdcReqQueryAccountField *reqQueryAccount)
{
	if (pApi == NULL) return -1;

	return pApi->ReqQueryBankAccountMoneyByFuture(reqQueryAccount, requestID);
}
#pragma endregion

#pragma region 回调函数
//========================================
//==================================== 回调函数 =======================================

TRADEAPI_API void WINAPI RegOnFrontConnected(CTraderSpi* pSpi, CBOnFrontConnected cb)		//当客户端与交易后台建立起通信连接时（还未登录前），该方法被调用。
{
	pSpi->cbOnFrontConnected = cb;
}

TRADEAPI_API void WINAPI RegOnFrontDisconnected(CTraderSpi* pSpi, CBOnFrontDisconnected cb)		//当客户端与交易后台通信连接断开时，该方法被调用。当发生这个情况后，API会自动重新连接，客户端可不做处理。
{
	pSpi->cbOnFrontDisconnected = cb;
}

TRADEAPI_API void WINAPI RegOnHeartBeatWarning(CTraderSpi* pSpi, CBOnHeartBeatWarning cb)		//心跳超时警告。当长时间未收到报文时，该方法被调用。
{
	pSpi->cbOnHeartBeatWarning = cb;
}

TRADEAPI_API void WINAPI RegRspAuthenticate(CTraderSpi* pSpi, CBRspAuthenticate cb)
{
	pSpi->cbRspAuthenticate = cb;
}

TRADEAPI_API void WINAPI RegRspUserLogin(CTraderSpi* pSpi, CBRspUserLogin cb)	//登录请求响应
{
	pSpi->cbRspUserLogin = cb;
}
TRADEAPI_API void WINAPI RegRspUserLogout(CTraderSpi* pSpi, CBRspUserLogout cb)	//登出请求响应
{
	pSpi->cbRspUserLogout = cb;
}
TRADEAPI_API void WINAPI RegRspUserPasswordUpdate(CTraderSpi* pSpi, CBRspUserPasswordUpdate cb)	//用户口令更新请求响应
{
	pSpi->cbRspUserPasswordUpdate = cb;
}
TRADEAPI_API void WINAPI RegRspTradingAccountPasswordUpdate(CTraderSpi* pSpi, CBRspTradingAccountPasswordUpdate cb)	//资金账户口令更新请求响应
{
	pSpi->cbRspTradingAccountPasswordUpdate = cb;
}
TRADEAPI_API void WINAPI RegRspUserAuthMethod(CTraderSpi* pSpi, CBRspUserAuthMethod cb)	//查询用户当前支持的认证模式的回复
{
	pSpi->cbRspUserAuthMethod = cb;
}
TRADEAPI_API void WINAPI RegRspGenUserCaptcha(CTraderSpi* pSpi, CBRspGenUserCaptcha cb)	//获取图形验证码请求的回复
{
	pSpi->cbRspGenUserCaptcha = cb;
}
TRADEAPI_API void WINAPI RegRspGenUserText(CTraderSpi* pSpi, CBRspGenUserText cb)	//获取短信验证码请求的回复
{
	pSpi->cbRspGenUserText = cb;
}
TRADEAPI_API void WINAPI RegRspOrderInsert(CTraderSpi* pSpi, CBRspOrderInsert cb)	//报单录入请求响应
{
	pSpi->cbRspOrderInsert = cb;
}
TRADEAPI_API void WINAPI RegRspParkedOrderInsert(CTraderSpi* pSpi, CBRspParkedOrderInsert cb)	//预埋单录入请求响应
{
	pSpi->cbRspParkedOrderInsert = cb;
}
TRADEAPI_API void WINAPI RegRspParkedOrderAction(CTraderSpi* pSpi, CBRspParkedOrderAction cb)	//预埋撤单录入请求响应
{
	pSpi->cbRspParkedOrderAction = cb;
}
TRADEAPI_API void WINAPI RegRspOrderAction(CTraderSpi* pSpi, CBRspOrderAction cb)	//报单操作请求响应
{
	pSpi->cbRspOrderAction = cb;
}
TRADEAPI_API void WINAPI RegRspQueryMaxOrderVolume(CTraderSpi* pSpi, CBRspQueryMaxOrderVolume cb)	//查询最大报单数量响应
{
	pSpi->cbRspQueryMaxOrderVolume = cb;
}
TRADEAPI_API void WINAPI RegRspSettlementInfoConfirm(CTraderSpi* pSpi, CBRspSettlementInfoConfirm cb)	//投资者结算结果确认响应
{
	pSpi->cbRspSettlementInfoConfirm = cb;
}
TRADEAPI_API void WINAPI RegRspRemoveParkedOrder(CTraderSpi* pSpi, CBRspRemoveParkedOrder cb)	//删除预埋单响应
{
	pSpi->cbRspRemoveParkedOrder = cb;
}
TRADEAPI_API void WINAPI RegRspRemoveParkedOrderAction(CTraderSpi* pSpi, CBRspRemoveParkedOrderAction cb)	//删除预埋撤单响应
{
	pSpi->cbRspRemoveParkedOrderAction = cb;
}
TRADEAPI_API void WINAPI RegBatchOrderAction(CTraderSpi* pSpi, CBRspBatchOrderAction cb)	//批量报单操作请求响应
{
	pSpi->cbRspBatchOrderAction = cb;
}
TRADEAPI_API void WINAPI RegCombActionInsert(CTraderSpi* pSpi, CBRspCombActionInsert cb)	//申请组合录入请求响应
{
	pSpi->cbRspCombActionInsert = cb;
}
TRADEAPI_API void WINAPI RegRspQryOrder(CTraderSpi* pSpi, CBRspQryOrder cb)	//请求查询报单响应
{
	pSpi->cbRspQryOrder = cb;
}
TRADEAPI_API void WINAPI RegRspQryTrade(CTraderSpi* pSpi, CBRspQryTrade cb)	//请求查询成交响应
{
	pSpi->cbRspQryTrade = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestorPosition(CTraderSpi* pSpi, CBRspQryInvestorPosition cb)	//请求查询投资者持仓响应
{
	pSpi->cbRspQryInvestorPosition = cb;
}
TRADEAPI_API void WINAPI RegRspQryTradingAccount(CTraderSpi* pSpi, CBRspQryTradingAccount cb)	//请求查询资金账户响应
{
	pSpi->cbRspQryTradingAccount = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestor(CTraderSpi* pSpi, CBRspQryInvestor cb)	//请求查询投资者响应
{
	pSpi->cbRspQryInvestor = cb;
}
TRADEAPI_API void WINAPI RegRspQryTradingCode(CTraderSpi* pSpi, CBRspQryTradingCode cb)	//请求查询交易编码响应
{
	pSpi->cbRspQryTradingCode = cb;
}
TRADEAPI_API void WINAPI RegRspQryInstrumentMarginRate(CTraderSpi* pSpi, CBRspQryInstrumentMarginRate cb)	//请求查询合约保证金率响应
{
	pSpi->cbRspQryInstrumentMarginRate = cb;
}
TRADEAPI_API void WINAPI RegRspQryInstrumentCommissionRate(CTraderSpi* pSpi, CBRspQryInstrumentCommissionRate cb)	//请求查询合约手续费率响应
{
	pSpi->cbRspQryInstrumentCommissionRate = cb;
}
TRADEAPI_API void WINAPI RegRspQryExchange(CTraderSpi* pSpi, CBRspQryExchange cb)	//请求查询交易所响应
{
	pSpi->cbRspQryExchange = cb;
}
TRADEAPI_API void WINAPI RegRspQryInstrument(CTraderSpi* pSpi, CBRspQryInstrument cb)	//请求查询合约响应
{
	pSpi->cbRspQryInstrument = cb;
}
TRADEAPI_API void WINAPI RegRspQryDepthMarketData(CTraderSpi* pSpi, CBRspQryDepthMarketData cb)	//请求查询行情响应
{
	pSpi->cbRspQryDepthMarketData = cb;
}
TRADEAPI_API void WINAPI RegRspQrySettlementInfo(CTraderSpi* pSpi, CBRspQrySettlementInfo cb)	//请求查询投资者结算结果响应
{
	pSpi->cbRspQrySettlementInfo = cb;
}
TRADEAPI_API void WINAPI RegRspQryTransferBank(CTraderSpi* pSpi, CBRspQryTransferBank cb)	//请求查询转帐银行响应
{
	pSpi->cbRspQryTransferBank = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestorPositionDetail(CTraderSpi* pSpi, CBRspQryInvestorPositionDetail cb)	//请求查询投资者持仓明细响应
{
	pSpi->cbRspQryInvestorPositionDetail = cb;
}
TRADEAPI_API void WINAPI RegRspQryNotice(CTraderSpi* pSpi, CBRspQryNotice cb)	//请求查询客户通知响应
{
	pSpi->cbRspQryNotice = cb;
}
TRADEAPI_API void WINAPI RegRspQrySettlementInfoConfirm(CTraderSpi* pSpi, CBRspQrySettlementInfoConfirm cb)	//请求查询结算信息确认响应
{
	pSpi->cbRspQrySettlementInfoConfirm = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestorPositionCombineDetail(CTraderSpi* pSpi, CBRspQryInvestorPositionCombineDetail cb)	//请求查询投资者持仓明细响应
{
	pSpi->cbRspQryInvestorPositionCombineDetail = cb;
}
TRADEAPI_API void WINAPI RegRspQryCFMMCTradingAccountKey(CTraderSpi* pSpi, CBRspQryCFMMCTradingAccountKey cb)	//查询保证金监管系统经纪公司资金账户密钥响应
{
	pSpi->cbRspQryCFMMCTradingAccountKey = cb;
}
TRADEAPI_API void WINAPI RegRspQryEWarrantOffset(CTraderSpi* pSpi, CBRspQryEWarrantOffset cb)	//请求查询仓单折抵信息
{
	pSpi->cbRspQryEWarrantOffset = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestorProductGroupMargin(CTraderSpi* pSpi, CBRspQryInvestorProductGroupMargin cb)	//请求查询投资者品种/跨品种保证金
{
	pSpi->cbRspQryInvestorProductGroupMargin = cb;
}
TRADEAPI_API void WINAPI RegRspQryExchangeMarginRate(CTraderSpi* pSpi, CBRspQryExchangeMarginRate cb)	//请求查询交易所保证金率
{
	pSpi->cbRspQryExchangeMarginRate = cb;
}
TRADEAPI_API void WINAPI RegRspQryExchangeMarginRateAdjust(CTraderSpi* pSpi, CBRspQryExchangeMarginRateAdjust cb)	//请求查询交易所调整保证金率
{
	pSpi->cbRspQryExchangeMarginRateAdjust = cb;
}
TRADEAPI_API void WINAPI RegRspQryExchangeRate(CTraderSpi* pSpi, CBRspQryExchangeRate cb)	//请求查询汇率响应
{
	pSpi->cbRspQryExchangeRate = cb;
}
TRADEAPI_API void WINAPI RegRspQrySecAgentACIDMap(CTraderSpi* pSpi, CBRspQrySecAgentACIDMap cb)	//请求查询二级代理操作员银期权限响应
{
	pSpi->cbRspQrySecAgentACIDMap = cb;
}
TRADEAPI_API void WINAPI RegRspQryProductExchRate(CTraderSpi* pSpi, CBRspQryProductExchRate cb)	//请求查询产品报价汇率
{
	pSpi->cbRspQryProductExchRate = cb;
}
TRADEAPI_API void WINAPI RegRspQryProductGroup(CTraderSpi* pSpi, CBRspQryProductGroup cb)	//请求查询产品组
{
	pSpi->cbRspQryProductGroup = cb;
}
TRADEAPI_API void WINAPI RegRspQryMMInstrumentCommissionRate(CTraderSpi* pSpi, CBRspQryMMInstrumentCommissionRate cb)	//请求查询做市商合约手续费率响应
{
	pSpi->cbRspQryMMInstrumentCommissionRate = cb;
}
TRADEAPI_API void WINAPI RegRspQryMMOptionInstrCommRate(CTraderSpi* pSpi, CBRspQryMMOptionInstrCommRate cb)	//请求查询做市商期权合约手续费响应
{
	pSpi->cbRspQryMMOptionInstrCommRate = cb;
}
TRADEAPI_API void WINAPI RegRspQryInstrumentOrderCommRate(CTraderSpi* pSpi, CBRspQryInstrumentOrderCommRate cb)	//请求查询报单手续费响应
{
	pSpi->cbRspQryInstrumentOrderCommRate = cb;
}
TRADEAPI_API void WINAPI RegRspQrySecAgentTradingAccount(CTraderSpi* pSpi, CBRspQrySecAgentTradingAccount cb)	//请求查询资金账户响应
{
	pSpi->cbRspQrySecAgentTradingAccount = cb;
}
TRADEAPI_API void WINAPI RegRspQrySecAgentCheckMode(CTraderSpi* pSpi, CBRspQrySecAgentCheckMode cb)	//请求查询二级代理商资金校验模式响应
{
	pSpi->cbRspQrySecAgentCheckMode = cb;
}
TRADEAPI_API void WINAPI RegRspQrySecAgentTradeInfo(CTraderSpi* pSpi, CBRspQrySecAgentTradeInfo cb)	//请求查询二级代理商信息响应
{
	pSpi->cbRspQrySecAgentTradeInfo = cb;
}
TRADEAPI_API void WINAPI RegRspQryOptionInstrTradeCost(CTraderSpi* pSpi, CBRspQryOptionInstrTradeCost cb)	//请求查询期权交易成本响应
{
	pSpi->cbRspQryOptionInstrTradeCost = cb;
}
TRADEAPI_API void WINAPI RegRspQryOptionInstrCommRate(CTraderSpi* pSpi, CBRspQryOptionInstrCommRate cb)	//请求查询期权合约手续费响应
{
	pSpi->cbRspQryOptionInstrCommRate = cb;
}
TRADEAPI_API void WINAPI RegRspQryExecOrder(CTraderSpi* pSpi, CBRspQryExecOrder cb)	//请求查询执行宣告响应
{
	pSpi->cbRspQryExecOrder = cb;
}
TRADEAPI_API void WINAPI RegRspQryForQuote(CTraderSpi* pSpi, CBRspQryForQuote cb)	//请求查询询价响应
{
	pSpi->cbRspQryForQuote = cb;
}
TRADEAPI_API void WINAPI RegRspQryQuote(CTraderSpi* pSpi, CBRspQryQuote cb)	//请求查询报价响应
{
	pSpi->cbRspQryQuote = cb;
}
TRADEAPI_API void WINAPI RegRspQryOptionSelfClose(CTraderSpi* pSpi, CBRspQryOptionSelfClose cb)	//请求查询期权自对冲响应
{
	pSpi->cbRspQryOptionSelfClose = cb;
}
TRADEAPI_API void WINAPI RegRspQryInvestUnit(CTraderSpi* pSpi, CBRspQryInvestUnit cb)	//请求查询投资单元响应
{
	pSpi->cbRspQryInvestUnit = cb;
}
TRADEAPI_API void WINAPI RegRspQryCombInstrumentGuard(CTraderSpi* pSpi, CBRspQryCombInstrumentGuard cb)	//请求查询组合合约安全系数响应
{
	pSpi->cbRspQryCombInstrumentGuard = cb;
}
TRADEAPI_API void WINAPI RegRspQryCombAction(CTraderSpi* pSpi, CBRspQryCombAction cb)	//请求查询申请组合响应
{
	pSpi->cbRspQryCombAction = cb;
}
TRADEAPI_API void WINAPI RegRspQryTransferSerial(CTraderSpi* pSpi, CBRspQryTransferSerial cb)	//请求查询转帐流水响应
{
	pSpi->cbRspQryTransferSerial = cb;
}
TRADEAPI_API void WINAPI RegRspQryAccountregister(CTraderSpi* pSpi, CBRspQryAccountregister cb)	//请求查询银期签约关系响应
{
	pSpi->cbRspQryAccountregister = cb;
}
TRADEAPI_API void WINAPI RegRspError(CTraderSpi* pSpi, CBRspError cb)	//错误应答
{
	pSpi->cbRspError = cb;
}
TRADEAPI_API void WINAPI RegRtnOrder(CTraderSpi* pSpi, CBRtnOrder cb)	//报单通知
{
	pSpi->cbRtnOrder = cb;
}
TRADEAPI_API void WINAPI RegRtnTrade(CTraderSpi* pSpi, CBRtnTrade cb)	//成交通知
{
	pSpi->cbRtnTrade = cb;
}
TRADEAPI_API void WINAPI RegErrRtnOrderInsert(CTraderSpi* pSpi, CBErrRtnOrderInsert cb)	//报单录入错误回报
{
	pSpi->cbErrRtnOrderInsert = cb;
}
TRADEAPI_API void WINAPI RegErrRtnOrderAction(CTraderSpi* pSpi, CBErrRtnOrderAction cb)	//报单操作错误回报
{
	pSpi->cbErrRtnOrderAction = cb;
}
TRADEAPI_API void WINAPI RegRtnInstrumentStatus(CTraderSpi* pSpi, CBRtnInstrumentStatus cb)	//合约交易状态通知
{
	pSpi->cbRtnInstrumentStatus = cb;
}
TRADEAPI_API void WINAPI RegRtnTradingNotice(CTraderSpi* pSpi, CBRtnTradingNotice cb)	//交易通知
{
	pSpi->cbRtnTradingNotice = cb;
}
TRADEAPI_API void WINAPI RegRtnErrorConditionalOrder(CTraderSpi* pSpi, CBRtnErrorConditionalOrder cb)	//提示条件单校验错误
{
	pSpi->cbRtnErrorConditionalOrder = cb;
}
TRADEAPI_API void WINAPI RegRspQryContractBank(CTraderSpi* pSpi, CBRspQryContractBank cb)	//请求查询签约银行响应
{
	pSpi->cbRspQryContractBank = cb;
}
TRADEAPI_API void WINAPI RegRspQryParkedOrder(CTraderSpi* pSpi, CBRspQryParkedOrder cb)	//请求查询预埋单响应
{
	pSpi->cbRspQryParkedOrder = cb;
}
TRADEAPI_API void WINAPI RegRspQryParkedOrderAction(CTraderSpi* pSpi, CBRspQryParkedOrderAction cb)	//请求查询预埋撤单响应
{
	pSpi->cbRspQryParkedOrderAction = cb;
}
TRADEAPI_API void WINAPI RegRspQryTradingNotice(CTraderSpi* pSpi, CBRspQryTradingNotice cb)	//请求查询交易通知响应
{
	pSpi->cbRspQryTradingNotice = cb;
}
TRADEAPI_API void WINAPI RegRspQryBrokerTradingParams(CTraderSpi* pSpi, CBRspQryBrokerTradingParams cb)	//请求查询经纪公司交易参数响应
{
	pSpi->cbRspQryBrokerTradingParams = cb;
}
TRADEAPI_API void WINAPI RegRspQryBrokerTradingAlgos(CTraderSpi* pSpi, CBRspQryBrokerTradingAlgos cb)	//请求查询经纪公司交易算法响应
{
	pSpi->cbRspQryBrokerTradingAlgos = cb;
}
TRADEAPI_API void WINAPI RegRtnFromBankToFutureByBank(CTraderSpi* pSpi, CBRtnFromBankToFutureByBank cb)	//银行发起银行资金转期货通知
{
	pSpi->cbRtnFromBankToFutureByBank = cb;
}
TRADEAPI_API void WINAPI RegRtnFromFutureToBankByBank(CTraderSpi* pSpi, CBRtnFromFutureToBankByBank cb)	//银行发起期货资金转银行通知
{
	pSpi->cbRtnFromFutureToBankByBank = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromBankToFutureByBank(CTraderSpi* pSpi, CBRtnRepealFromBankToFutureByBank cb)	//银行发起冲正银行转期货通知
{
	pSpi->cbRtnRepealFromBankToFutureByBank = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromFutureToBankByBank(CTraderSpi* pSpi, CBRtnRepealFromFutureToBankByBank cb)	//银行发起冲正期货转银行通知
{
	pSpi->cbRtnRepealFromFutureToBankByBank = cb;
}
TRADEAPI_API void WINAPI RegRtnFromBankToFutureByFuture(CTraderSpi* pSpi, CBRtnFromBankToFutureByFuture cb)	//期货发起银行资金转期货通知
{
	pSpi->cbRtnFromBankToFutureByFuture = cb;
}
TRADEAPI_API void WINAPI RegRtnFromFutureToBankByFuture(CTraderSpi* pSpi, CBRtnFromFutureToBankByFuture cb)	//期货发起期货资金转银行通知
{
	pSpi->cbRtnFromFutureToBankByFuture = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromBankToFutureByFutureManual(CTraderSpi* pSpi, CBRtnRepealFromBankToFutureByFutureManual cb)	//系统运行时期货端手工发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
{
	pSpi->cbRtnRepealFromBankToFutureByFutureManual = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromFutureToBankByFutureManual(CTraderSpi* pSpi, CBRtnRepealFromFutureToBankByFutureManual cb)	//系统运行时期货端手工发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
{
	pSpi->cbRtnRepealFromFutureToBankByFutureManual = cb;
}
TRADEAPI_API void WINAPI RegRtnQueryBankBalanceByFuture(CTraderSpi* pSpi, CBRtnQueryBankBalanceByFuture cb)	//期货发起查询银行余额通知
{
	pSpi->cbRtnQueryBankBalanceByFuture = cb;
}
TRADEAPI_API void WINAPI RegErrRtnBankToFutureByFuture(CTraderSpi* pSpi, CBErrRtnBankToFutureByFuture cb)	//期货发起银行资金转期货错误回报
{
	pSpi->cbErrRtnBankToFutureByFuture = cb;
}
TRADEAPI_API void WINAPI RegErrRtnFutureToBankByFuture(CTraderSpi* pSpi, CBErrRtnFutureToBankByFuture cb)	//期货发起期货资金转银行错误回报
{
	pSpi->cbErrRtnFutureToBankByFuture = cb;
}
TRADEAPI_API void WINAPI RegErrRtnRepealBankToFutureByFutureManual(CTraderSpi* pSpi, CBErrRtnRepealBankToFutureByFutureManual cb)	//系统运行时期货端手工发起冲正银行转期货错误回报
{
	pSpi->cbErrRtnRepealBankToFutureByFutureManual = cb;
}
TRADEAPI_API void WINAPI RegErrRtnRepealFutureToBankByFutureManual(CTraderSpi* pSpi, CBErrRtnRepealFutureToBankByFutureManual cb)	//系统运行时期货端手工发起冲正期货转银行错误回报
{
	pSpi->cbErrRtnRepealFutureToBankByFutureManual = cb;
}
TRADEAPI_API void WINAPI RegErrRtnQueryBankBalanceByFuture(CTraderSpi* pSpi, CBErrRtnQueryBankBalanceByFuture cb)	//期货发起查询银行余额错误回报
{
	pSpi->cbErrRtnQueryBankBalanceByFuture = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromBankToFutureByFuture(CTraderSpi* pSpi, CBRtnRepealFromBankToFutureByFuture cb)	//期货发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
{
	pSpi->cbRtnRepealFromBankToFutureByFuture = cb;
}
TRADEAPI_API void WINAPI RegRtnRepealFromFutureToBankByFuture(CTraderSpi* pSpi, CBRtnRepealFromFutureToBankByFuture cb)	//期货发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
{
	pSpi->cbRtnRepealFromFutureToBankByFuture = cb;
}
TRADEAPI_API void WINAPI RegRspFromBankToFutureByFuture(CTraderSpi* pSpi, CBRspFromBankToFutureByFuture cb)	//期货发起银行资金转期货应答
{
	pSpi->cbRspFromBankToFutureByFuture = cb;
}
TRADEAPI_API void WINAPI RegRspFromFutureToBankByFuture(CTraderSpi* pSpi, CBRspFromFutureToBankByFuture cb)	//期货发起期货资金转银行应答
{
	pSpi->cbRspFromFutureToBankByFuture = cb;
}
TRADEAPI_API void WINAPI RegRspQueryBankAccountMoneyByFuture(CTraderSpi* pSpi, CBRspQueryBankAccountMoneyByFuture cb)	//期货发起查询银行余额应答
{
	pSpi->cbRspQueryBankAccountMoneyByFuture = cb;
}
#pragma endregion