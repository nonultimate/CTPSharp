// TradeApi.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "TradeApi.h"
#include <iostream>
using namespace std;


#pragma warning(disable : 4996)

//回调函数
extern CBOnFrontConnected cbOnFrontConnected;		///当客户端与交易后台建立起通信连接时（还未登录前），该方法被调用。
extern CBOnFrontDisconnected cbOnFrontDisconnected;		///当客户端与交易后台通信连接断开时，该方法被调用。当发生这个情况后，API会自动重新连接，客户端可不做处理。
extern CBOnHeartBeatWarning cbOnHeartBeatWarning;		///心跳超时警告。当长时间未收到报文时，该方法被调用。
extern CBRspAuthenticate cbRspAuthenticate;		///客户端认证响应
extern CBRspUserLogin cbRspUserLogin;	///登录请求响应
extern CBRspUserLogout cbRspUserLogout;	///登出请求响应
extern CBRspUserPasswordUpdate cbRspUserPasswordUpdate;	///用户口令更新请求响应
extern CBRspTradingAccountPasswordUpdate cbRspTradingAccountPasswordUpdate;	///资金账户口令更新请求响应
extern CBRspUserAuthMethod cbRspUserAuthMethod; ///查询用户当前支持的认证模式的回复
extern CBRspGenUserCaptcha cbRspGenUserCaptcha; ///获取图形验证码请求的回复
extern CBRspGenUserText cbRspGenUserText; ///获取短信验证码请求的回复
extern CBRspOrderInsert cbRspOrderInsert;	///报单录入请求响应
extern CBRspParkedOrderInsert cbRspParkedOrderInsert;	///预埋单录入请求响应
extern CBRspParkedOrderAction cbRspParkedOrderAction;	///预埋撤单录入请求响应
extern CBRspOrderAction cbRspOrderAction;	///报单操作请求响应
extern CBRspQueryMaxOrderVolume cbRspQueryMaxOrderVolume;	///查询最大报单数量响应
extern CBRspSettlementInfoConfirm cbRspSettlementInfoConfirm;	///投资者结算结果确认响应
extern CBRspRemoveParkedOrder cbRspRemoveParkedOrder;	///删除预埋单响应
extern CBRspRemoveParkedOrderAction cbRspRemoveParkedOrderAction;	///删除预埋撤单响应
extern CBRspExecOrderInsert cbRspExecOrderInsert;	///执行宣告录入请求响应
extern CBRspExecOrderAction cbRspExecOrderAction;	///执行宣告操作请求响应
extern CBRspForQuoteInsert cbRspForQuoteInsert;	///询价录入请求响应
extern CBRspQuoteInsert cbRspQuoteInsert;	///报价录入请求响应
extern CBRspQuoteAction cbRspQuoteAction;	///报价操作请求响应
extern CBRspBatchOrderAction cbRspBatchOrderAction;	///批量报单操作请求响应
extern CBRspOptionSelfCloseInsert cbRspOptionSelfCloseInsert;	///期权自对冲录入请求响应
extern CBRspOptionSelfCloseAction cbRspOptionSelfCloseAction; ///期权自对冲操作请求响应
extern CBRspCombActionInsert cbRspCombActionInsert;	///申请组合录入请求响应
extern CBRspQryOrder cbRspQryOrder;	///请求查询报单响应
extern CBRspQryTrade cbRspQryTrade;	///请求查询成交响应
extern CBRspQryInvestorPosition cbRspQryInvestorPosition;	///请求查询投资者持仓响应
extern CBRspQryTradingAccount cbRspQryTradingAccount;	///请求查询资金账户响应
extern CBRspQryInvestor cbRspQryInvestor;	///请求查询投资者响应
extern CBRspQryTradingCode cbRspQryTradingCode;	///请求查询交易编码响应
extern CBRspQryInstrumentMarginRate cbRspQryInstrumentMarginRate;	///请求查询合约保证金率响应
extern CBRspQryInstrumentCommissionRate cbRspQryInstrumentCommissionRate;	///请求查询合约手续费率响应
extern CBRspQryExchange cbRspQryExchange;	///请求查询交易所响应
extern CBRspQryInstrument cbRspQryInstrument;	///请求查询合约响应
extern CBRspQryDepthMarketData cbRspQryDepthMarketData;	///请求查询行情响应
extern CBRspQrySettlementInfo cbRspQrySettlementInfo;	///请求查询投资者结算结果响应
extern CBRspQryTransferBank cbRspQryTransferBank;	///请求查询转帐银行响应
extern CBRspQryInvestorPositionDetail cbRspQryInvestorPositionDetail;	///请求查询投资者持仓明细响应
extern CBRspQryNotice cbRspQryNotice;	///请求查询客户通知响应
extern CBRspQrySettlementInfoConfirm cbRspQrySettlementInfoConfirm;	///请求查询结算信息确认响应
extern CBRspQryInvestorPositionCombineDetail cbRspQryInvestorPositionCombineDetail;	///请求查询投资者持仓明细响应
extern CBRspQryCFMMCTradingAccountKey cbRspQryCFMMCTradingAccountKey;	///查询保证金监管系统经纪公司资金账户密钥响应
extern CBRspQryEWarrantOffset cbRspQryEWarrantOffset;	///请求查询仓单折抵信息响应
extern CBRspQryInvestorProductGroupMargin cbRspQryInvestorProductGroupMargin;	///请求查询投资者品种/跨品种保证金响应
extern CBRspQryExchangeMarginRate cbRspQryExchangeMarginRate;	///请求查询交易所保证金率响应
extern CBRspQryExchangeMarginRateAdjust cbRspQryExchangeMarginRateAdjust;	///请求查询交易所调整保证金率响应
extern CBRspQryExchangeRate cbRspQryExchangeRate;	///请求查询汇率响应
extern CBRspQrySecAgentACIDMap cbRspQrySecAgentACIDMap;	///请求查询二级代理操作员银期权限响应
extern CBRspQryProductExchRate cbRspQryProductExchRate;	///请求查询产品报价汇率
extern CBRspQryProductGroup cbRspQryProductGroup;	///请求查询产品组
extern CBRspQryMMInstrumentCommissionRate cbRspQryMMInstrumentCommissionRate;	///请求查询做市商合约手续费率响应
extern CBRspQryMMOptionInstrCommRate cbRspQryMMOptionInstrCommRate;	///请求查询做市商期权合约手续费响应
extern CBRspQryInstrumentOrderCommRate cbRspQryInstrumentOrderCommRate;	///请求查询报单手续费响应
extern CBRspQrySecAgentTradingAccount cbRspQrySecAgentTradingAccount;	///请求查询资金账户响应
extern CBRspQrySecAgentCheckMode cbRspQrySecAgentCheckMode;	///请求查询二级代理商资金校验模式响应
extern CBRspQrySecAgentTradeInfo cbRspQrySecAgentTradeInfo; ///请求查询二级代理商信息响应
extern CBRspQryOptionInstrTradeCost cbRspQryOptionInstrTradeCost;	///请求查询期权交易成本响应
extern CBRspQryOptionInstrCommRate cbRspQryOptionInstrCommRate;	///请求查询期权合约手续费响应
extern CBRspQryExecOrder cbRspQryExecOrder;	///请求查询执行宣告响应
extern CBRspQryForQuote cbRspQryForQuote;	///请求查询询价响应
extern CBRspQryQuote cbRspQryQuote;	///请求查询报价响应
extern CBRspQryOptionSelfClose cbRspQryOptionSelfClose;	///请求查询期权自对冲响应
extern CBRspQryInvestUnit cbRspQryInvestUnit;	///请求查询投资单元响应
extern CBRspQryCombInstrumentGuard cbRspQryCombInstrumentGuard;	///请求查询组合合约安全系数响应
extern CBRspQryCombAction cbRspQryCombAction;	///请求查询申请组合响应
extern CBRspQryTransferSerial cbRspQryTransferSerial;	///请求查询转帐流水响应
extern CBRspQryAccountregister cbRspQryAccountregister;///请求查询银期签约关系响应
extern CBRspError cbRspError;	///错误应答
extern CBRtnOrder cbRtnOrder;	///报单通知
extern CBRtnTrade cbRtnTrade;	///成交通知
extern CBErrRtnOrderInsert cbErrRtnOrderInsert;	///报单录入错误回报
extern CBErrRtnOrderAction cbErrRtnOrderAction;	///报单操作错误回报
extern CBRtnInstrumentStatus cbRtnInstrumentStatus;	///合约交易状态通知
extern CBRtnTradingNotice cbRtnTradingNotice;	///交易通知
extern CBRtnErrorConditionalOrder cbRtnErrorConditionalOrder;	///提示条件单校验错误
extern CBRtnExecOrder cbRtnExecOrder;	///执行宣告通知
extern CBErrRtnExecOrderInsert cbErrRtnExecOrderInsert;	///执行宣告录入错误回报
extern CBErrRtnExecOrderAction cbErrRtnExecOrderAction;	///执行宣告操作错误回报
extern CBErrRtnForQuoteInsert cbErrRtnForQuoteInsert;	///询价录入错误回报
extern CBRtnQuote cbRtnQuote;	///报价通知
extern CBErrRtnQuoteInsert cbErrRtnQuoteInsert;	///报价录入错误回报
extern CBErrRtnQuoteAction cbErrRtnQuoteAction;	///报价操作错误回报
extern CBRtnForQuoteRsp cbRtnForQuoteRsp;	///询价通知
extern CBRtnCFMMCTradingAccountToken cbRtnCFMMCTradingAccountToken;	///保证金监控中心用户令牌
extern CBErrRtnBatchOrderAction cbErrRtnBatchOrderAction;	///批量报单操作错误回报
extern CBRtnOptionSelfClose cbRtnOptionSelfClose;	///期权自对冲通知
extern CBErrRtnOptionSelfCloseInsert cbErrRtnOptionSelfCloseInsert;	///期权自对冲录入错误回报
extern CBErrRtnOptionSelfCloseAction cbErrRtnOptionSelfCloseAction;	///期权自对冲操作错误回报
extern CBRtnCombAction cbRtnCombAction;	///申请组合通知
extern CBErrRtnCombActionInsert cbErrRtnCombActionInsert;	///申请组合录入错误回报
extern CBRspQryContractBank cbRspQryContractBank;	///请求查询签约银行响应
extern CBRspQryParkedOrder cbRspQryParkedOrder;	///请求查询预埋单响应
extern CBRspQryParkedOrderAction cbRspQryParkedOrderAction;	///请求查询预埋撤单响应
extern CBRspQryTradingNotice cbRspQryTradingNotice;	///请求查询交易通知响应
extern CBRspQryBrokerTradingParams cbRspQryBrokerTradingParams;	///请求查询经纪公司交易参数响应
extern CBRspQryBrokerTradingAlgos cbRspQryBrokerTradingAlgos;	///请求查询经纪公司交易算法响应
extern CBRspQueryCFMMCTradingAccountToken cbRspQueryCFMMCTradingAccountToken;	///请求查询监控中心用户令牌
extern CBRtnFromBankToFutureByBank cbRtnFromBankToFutureByBank;	///银行发起银行资金转期货通知
extern CBRtnFromFutureToBankByBank cbRtnFromFutureToBankByBank;	///银行发起期货资金转银行通知
extern CBRtnRepealFromBankToFutureByBank cbRtnRepealFromBankToFutureByBank;	///银行发起冲正银行转期货通知
extern CBRtnRepealFromFutureToBankByBank cbRtnRepealFromFutureToBankByBank;	///银行发起冲正期货转银行通知
extern CBRtnFromBankToFutureByFuture cbRtnFromBankToFutureByFuture;	///期货发起银行资金转期货通知
extern CBRtnFromFutureToBankByFuture cbRtnFromFutureToBankByFuture;	///期货发起期货资金转银行通知
extern CBRtnRepealFromBankToFutureByFutureManual cbRtnRepealFromBankToFutureByFutureManual;	///系统运行时期货端手工发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
extern CBRtnRepealFromFutureToBankByFutureManual cbRtnRepealFromFutureToBankByFutureManual;	///系统运行时期货端手工发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
extern CBRtnQueryBankBalanceByFuture cbRtnQueryBankBalanceByFuture;	///期货发起查询银行余额通知
extern CBErrRtnBankToFutureByFuture cbErrRtnBankToFutureByFuture;	///期货发起银行资金转期货错误回报
extern CBErrRtnFutureToBankByFuture cbErrRtnFutureToBankByFuture;	///期货发起期货资金转银行错误回报
extern CBErrRtnRepealBankToFutureByFutureManual cbErrRtnRepealBankToFutureByFutureManual;	///系统运行时期货端手工发起冲正银行转期货错误回报
extern CBErrRtnRepealFutureToBankByFutureManual cbErrRtnRepealFutureToBankByFutureManual;	///系统运行时期货端手工发起冲正期货转银行错误回报
extern CBErrRtnQueryBankBalanceByFuture cbErrRtnQueryBankBalanceByFuture;	///期货发起查询银行余额错误回报
extern CBRtnRepealFromBankToFutureByFuture cbRtnRepealFromBankToFutureByFuture;	///期货发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
extern CBRtnRepealFromFutureToBankByFuture cbRtnRepealFromFutureToBankByFuture;	///期货发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
extern CBRspFromBankToFutureByFuture cbRspFromBankToFutureByFuture;	///期货发起银行资金转期货应答
extern CBRspFromFutureToBankByFuture cbRspFromFutureToBankByFuture;	///期货发起期货资金转银行应答
extern CBRspQueryBankAccountMoneyByFuture cbRspQueryBankAccountMoneyByFuture;	///期货发起查询银行余额应答
extern CBRtnOpenAccountByBank cbRtnOpenAccountByBank;	///银行发起银期开户通知
extern CBRtnCancelAccountByBank cbRtnCancelAccountByBank;	///银行发起银期销户通知
extern CBRtnChangeAccountByBank cbRtnChangeAccountByBank;	///银行发起变更银行账号通知

//====================
// 配置参数
//extern char FRONT_ADDR[];		// 前置地址
//extern char BROKER_ID[];		// 经纪公司代码
//extern char INVESTOR_ID[];		// 投资者代码
//extern char PASSWORD[];			// 用户密码
//extern char INSTRUMENT_ID[];	// 合约代码
//extern TThostFtdcPriceType	LIMIT_PRICE;	// 价格
//extern TThostFtdcDirectionType	DIRECTION;	// 买卖方向

// 请求编号
//extern int iRequestID;
///当客户端与交易后台建立起通信连接时（还未登录前），该方法被调用。
void CTraderSpi::OnFrontConnected() { if (cbOnFrontConnected != NULL) 		cbOnFrontConnected(); }

///当客户端与交易后台通信连接断开时，该方法被调用。当发生这个情况后，API会自动重新连接，客户端可不做处理。
void CTraderSpi::OnFrontDisconnected(int nReason) { if (cbOnFrontDisconnected != NULL) cbOnFrontDisconnected(nReason); }

///心跳超时警告。当长时间未收到报文时，该方法被调用。  @param nTimeLapse 距离上次接收报文的时间
void CTraderSpi::OnHeartBeatWarning(int nTimeLapse) { if (cbOnHeartBeatWarning != NULL) cbOnHeartBeatWarning(nTimeLapse); }

///客户端认证响应
void CTraderSpi::OnRspAuthenticate(CThostFtdcRspAuthenticateField *pRspAuthenticateField, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspAuthenticate != NULL)
	{
		if (pRspAuthenticateField == NULL)
		{
			CThostFtdcRspAuthenticateField req;
			memset(&req, 0, sizeof(req));
			cbRspAuthenticate(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspAuthenticate(pRspAuthenticateField, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///登录请求响应
void CTraderSpi::OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspUserLogin != NULL)
	{
		if (pRspUserLogin == NULL)
		{
			CThostFtdcRspUserLoginField req;
			memset(&req, 0, sizeof(req));
			cbRspUserLogin(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspUserLogin(pRspUserLogin, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///登出请求响应
void CTraderSpi::OnRspUserLogout(CThostFtdcUserLogoutField *pUserLogout, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspUserLogout != NULL)
	{
		if (pUserLogout == NULL)
		{
			CThostFtdcUserLogoutField req;
			memset(&req, 0, sizeof(req));
			cbRspUserLogout(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspUserLogout(pUserLogout, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///用户口令更新请求响应
void CTraderSpi::OnRspUserPasswordUpdate(CThostFtdcUserPasswordUpdateField *pUserPasswordUpdate, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspUserPasswordUpdate != NULL)
	{
		if (pUserPasswordUpdate == NULL)
		{
			CThostFtdcUserPasswordUpdateField req;
			memset(&req, 0, sizeof(req));
			cbRspUserPasswordUpdate(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspUserPasswordUpdate(pUserPasswordUpdate, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///资金账户口令更新请求响应
void CTraderSpi::OnRspTradingAccountPasswordUpdate(CThostFtdcTradingAccountPasswordUpdateField *pTradingAccountPasswordUpdate, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspTradingAccountPasswordUpdate != NULL)
	{
		if (pTradingAccountPasswordUpdate == NULL)
		{
			CThostFtdcTradingAccountPasswordUpdateField req;
			memset(&req, 0, sizeof(req));
			cbRspTradingAccountPasswordUpdate(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspTradingAccountPasswordUpdate(pTradingAccountPasswordUpdate, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///查询用户当前支持的认证模式的回复
void CTraderSpi::OnRspUserAuthMethod(CThostFtdcRspUserAuthMethodField *pRspUserAuthMethod, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (pRspUserAuthMethod != NULL)
		cbRspUserAuthMethod(pRspUserAuthMethod, repareInfo(pRspInfo), nRequestID, bIsLast);
}

///获取图形验证码请求的回复
void CTraderSpi::OnRspGenUserCaptcha(CThostFtdcRspGenUserCaptchaField *pRspGenUserCaptcha, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (pRspGenUserCaptcha != NULL)
		cbRspGenUserCaptcha(pRspGenUserCaptcha, repareInfo(pRspInfo), nRequestID, bIsLast);
}

///获取短信验证码请求的回复
void CTraderSpi::OnRspGenUserText(CThostFtdcRspGenUserTextField *pRspGenUserText, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (pRspGenUserText != NULL)
		cbRspGenUserText(pRspGenUserText, repareInfo(pRspInfo), nRequestID, bIsLast);
}

///报单录入请求响应
void CTraderSpi::OnRspOrderInsert(CThostFtdcInputOrderField *pInputOrder, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspOrderInsert != NULL)
	{
		if (pInputOrder == NULL)
		{
			CThostFtdcInputOrderField req;
			memset(&req, 0, sizeof(req));
			cbRspOrderInsert(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspOrderInsert(pInputOrder, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///预埋单录入请求响应
void CTraderSpi::OnRspParkedOrderInsert(CThostFtdcParkedOrderField *pParkedOrder, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspParkedOrderInsert != NULL)
	{
		if (pParkedOrder == NULL)
		{
			CThostFtdcParkedOrderField req;
			memset(&req, 0, sizeof(req));
			cbRspParkedOrderInsert(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspParkedOrderInsert(pParkedOrder, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///预埋撤单录入请求响应
void CTraderSpi::OnRspParkedOrderAction(CThostFtdcParkedOrderActionField *pParkedOrderAction, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspParkedOrderAction != NULL)
	{
		if (pParkedOrderAction == NULL)
		{
			CThostFtdcParkedOrderActionField req;
			memset(&req, 0, sizeof(req));
			cbRspParkedOrderAction(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspParkedOrderAction(pParkedOrderAction, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///报单操作请求响应
void CTraderSpi::OnRspOrderAction(CThostFtdcInputOrderActionField *pInputOrderAction, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspOrderAction != NULL)
	{
		if (pInputOrderAction == NULL)
		{
			CThostFtdcInputOrderActionField req;
			memset(&req, 0, sizeof(req));
			cbRspOrderAction(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspOrderAction(pInputOrderAction, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///查询最大报单数量响应
void CTraderSpi::OnRspQueryMaxOrderVolume(CThostFtdcQueryMaxOrderVolumeField *pQueryMaxOrderVolume, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQueryMaxOrderVolume != NULL)
	{
		if (pQueryMaxOrderVolume == NULL)
		{
			CThostFtdcQueryMaxOrderVolumeField req;
			memset(&req, 0, sizeof(req));
			cbRspQueryMaxOrderVolume(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQueryMaxOrderVolume(pQueryMaxOrderVolume, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///投资者结算结果确认响应
void CTraderSpi::OnRspSettlementInfoConfirm(CThostFtdcSettlementInfoConfirmField *pSettlementInfoConfirm, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspSettlementInfoConfirm != NULL)
	{
		if (pSettlementInfoConfirm == NULL)
		{
			CThostFtdcSettlementInfoConfirmField req;
			memset(&req, 0, sizeof(req));
			cbRspSettlementInfoConfirm(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspSettlementInfoConfirm(pSettlementInfoConfirm, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///删除预埋单响应
void CTraderSpi::OnRspRemoveParkedOrder(CThostFtdcRemoveParkedOrderField *pRemoveParkedOrder, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspRemoveParkedOrder != NULL)
	{
		if (pRemoveParkedOrder == NULL)
		{
			CThostFtdcRemoveParkedOrderField req;
			memset(&req, 0, sizeof(req));
			cbRspRemoveParkedOrder(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspRemoveParkedOrder(pRemoveParkedOrder, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///删除预埋撤单响应
void CTraderSpi::OnRspRemoveParkedOrderAction(CThostFtdcRemoveParkedOrderActionField *pRemoveParkedOrderAction, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspRemoveParkedOrderAction != NULL)
	{
		if (pRemoveParkedOrderAction == NULL)
		{
			CThostFtdcRemoveParkedOrderActionField req;
			memset(&req, 0, sizeof(req));
			cbRspRemoveParkedOrderAction(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspRemoveParkedOrderAction(pRemoveParkedOrderAction, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///执行宣告录入请求响应
void CTraderSpi::OnRspExecOrderInsert(CThostFtdcInputExecOrderField * pInputExecOrder, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspExecOrderInsert != NULL)
	{
		if (pInputExecOrder == NULL)
		{
			CThostFtdcInputExecOrderField req;
			memset(&req, 0, sizeof(req));
			cbRspExecOrderInsert(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspExecOrderInsert(pInputExecOrder, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///执行宣告操作请求响应
void CTraderSpi::OnRspExecOrderAction(CThostFtdcInputExecOrderActionField * pInputExecOrderAction, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspExecOrderAction != NULL)
	{
		if (pInputExecOrderAction == NULL)
		{
			CThostFtdcInputExecOrderActionField req;
			memset(&req, 0, sizeof(req));
			cbRspExecOrderAction(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspExecOrderAction(pInputExecOrderAction, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///询价录入请求响应
void CTraderSpi::OnRspForQuoteInsert(CThostFtdcInputForQuoteField * pInputForQuote, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspForQuoteInsert != NULL)
	{
		if (pInputForQuote == NULL)
		{
			CThostFtdcInputForQuoteField req;
			memset(&req, 0, sizeof(req));
			cbRspForQuoteInsert(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspForQuoteInsert(pInputForQuote, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///报价录入请求响应
void CTraderSpi::OnRspQuoteInsert(CThostFtdcInputQuoteField * pInputQuote, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQuoteInsert != NULL)
	{
		if (pInputQuote == NULL)
		{
			CThostFtdcInputQuoteField req;
			memset(&req, 0, sizeof(req));
			cbRspQuoteInsert(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQuoteInsert(pInputQuote, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///报价操作请求响应
void CTraderSpi::OnRspQuoteAction(CThostFtdcInputQuoteActionField * pInputQuoteAction, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQuoteAction != NULL)
	{
		if (pInputQuoteAction == NULL)
		{
			CThostFtdcInputQuoteActionField req;
			memset(&req, 0, sizeof(req));
			cbRspQuoteAction(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQuoteAction(pInputQuoteAction, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///批量报单操作请求响应
void CTraderSpi::OnRspBatchOrderAction(CThostFtdcInputBatchOrderActionField * pInputBatchOrderAction, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspBatchOrderAction != NULL)
	{
		if (pInputBatchOrderAction == NULL)
		{
			CThostFtdcInputBatchOrderActionField req;
			memset(&req, 0, sizeof(req));
			cbRspBatchOrderAction(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspBatchOrderAction(pInputBatchOrderAction, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///期权自对冲录入请求响应
void CTraderSpi::OnRspOptionSelfCloseInsert(CThostFtdcInputOptionSelfCloseField *pInputOptionSelfClose, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspOptionSelfCloseInsert != NULL)
	{
		if (pInputOptionSelfClose == NULL)
		{
			CThostFtdcInputOptionSelfCloseField req;
			memset(&req, 0, sizeof(req));
			cbRspOptionSelfCloseInsert(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspOptionSelfCloseInsert(pInputOptionSelfClose, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///期权自对冲操作请求响应
void CTraderSpi::OnRspOptionSelfCloseAction(CThostFtdcInputOptionSelfCloseActionField *pInputOptionSelfCloseAction, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspOptionSelfCloseAction != NULL)
	{
		if (pInputOptionSelfCloseAction == NULL)
		{
			CThostFtdcInputOptionSelfCloseActionField req;
			memset(&req, 0, sizeof(req));
			cbRspOptionSelfCloseAction(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspOptionSelfCloseAction(pInputOptionSelfCloseAction, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///申请组合录入请求响应
void CTraderSpi::OnRspCombActionInsert(CThostFtdcInputCombActionField * pInputCombAction, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspCombActionInsert != NULL)
	{
		if (pInputCombAction == NULL)
		{
			CThostFtdcInputCombActionField req;
			memset(&req, 0, sizeof(req));
			cbRspCombActionInsert(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspCombActionInsert(pInputCombAction, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询报单响应
void CTraderSpi::OnRspQryOrder(CThostFtdcOrderField *pOrder, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryOrder != NULL)
	{
		if (pOrder == NULL)
		{
			CThostFtdcOrderField req;
			memset(&req, 0, sizeof(req));
			cbRspQryOrder(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryOrder(pOrder, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询成交响应
void CTraderSpi::OnRspQryTrade(CThostFtdcTradeField *pTrade, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryTrade != NULL)
	{
		if (pTrade == NULL)
		{
			CThostFtdcTradeField req;
			memset(&req, 0, sizeof(req));
			cbRspQryTrade(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryTrade(pTrade, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询投资者持仓响应
void CTraderSpi::OnRspQryInvestorPosition(CThostFtdcInvestorPositionField *pInvestorPosition, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryInvestorPosition != NULL)
	{
		if (pInvestorPosition == NULL)
		{
			CThostFtdcInvestorPositionField req;
			memset(&req, 0, sizeof(req));
			cbRspQryInvestorPosition(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryInvestorPosition(pInvestorPosition, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询资金账户响应
void CTraderSpi::OnRspQryTradingAccount(CThostFtdcTradingAccountField *pTradingAccount, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryTradingAccount != NULL)
	{
		if (pTradingAccount == NULL)
		{
			CThostFtdcTradingAccountField req;
			memset(&req, 0, sizeof(req));
			cbRspQryTradingAccount(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryTradingAccount(pTradingAccount, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询投资者响应
void CTraderSpi::OnRspQryInvestor(CThostFtdcInvestorField *pInvestor, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryInvestor != NULL)
	{
		if (pInvestor == NULL)
		{
			CThostFtdcInvestorField req;
			memset(&req, 0, sizeof(req));
			cbRspQryInvestor(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryInvestor(pInvestor, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询交易编码响应
void CTraderSpi::OnRspQryTradingCode(CThostFtdcTradingCodeField *pTradingCode, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryTradingCode != NULL)
	{
		if (pTradingCode == NULL)
		{
			CThostFtdcTradingCodeField req;
			memset(&req, 0, sizeof(req));
			cbRspQryTradingCode(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryTradingCode(pTradingCode, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询合约保证金率响应
void CTraderSpi::OnRspQryInstrumentMarginRate(CThostFtdcInstrumentMarginRateField *pInstrumentMarginRate, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryInstrumentMarginRate != NULL)
	{
		if (pInstrumentMarginRate == NULL)
		{
			CThostFtdcInstrumentMarginRateField req;
			memset(&req, 0, sizeof(req));
			cbRspQryInstrumentMarginRate(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryInstrumentMarginRate(pInstrumentMarginRate, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询合约手续费率响应
void CTraderSpi::OnRspQryInstrumentCommissionRate(CThostFtdcInstrumentCommissionRateField *pInstrumentCommissionRate, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryInstrumentCommissionRate != NULL)
	{
		if (pInstrumentCommissionRate == NULL)
		{
			CThostFtdcInstrumentCommissionRateField req;
			memset(&req, 0, sizeof(req));
			cbRspQryInstrumentCommissionRate(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryInstrumentCommissionRate(pInstrumentCommissionRate, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询交易所响应
void CTraderSpi::OnRspQryExchange(CThostFtdcExchangeField *pExchange, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryExchange != NULL)
	{
		if (pExchange == NULL)
		{
			CThostFtdcExchangeField req;
			memset(&req, 0, sizeof(req));
			cbRspQryExchange(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryExchange(pExchange, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询合约响应
void CTraderSpi::OnRspQryInstrument(CThostFtdcInstrumentField *pInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryInstrument != NULL)
	{
		if (pInstrument == NULL)
		{
			CThostFtdcInstrumentField req;
			memset(&req, 0, sizeof(req));
			cbRspQryInstrument(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryInstrument(pInstrument, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询行情响应
void CTraderSpi::OnRspQryDepthMarketData(CThostFtdcDepthMarketDataField *pDepthMarketData, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryDepthMarketData != NULL)
	{
		if (pDepthMarketData == NULL)
		{
			CThostFtdcDepthMarketDataField req;
			memset(&req, 0, sizeof(req));
			cbRspQryDepthMarketData(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryDepthMarketData(pDepthMarketData, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询投资者结算结果响应
void CTraderSpi::OnRspQrySettlementInfo(CThostFtdcSettlementInfoField *pSettlementInfo, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQrySettlementInfo != NULL)
	{
		if (pSettlementInfo == NULL)
		{
			CThostFtdcSettlementInfoField req;
			memset(&req, 0, sizeof(req));
			cbRspQrySettlementInfo(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQrySettlementInfo(pSettlementInfo, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询转帐银行响应
void CTraderSpi::OnRspQryTransferBank(CThostFtdcTransferBankField *pTransferBank, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryTransferBank != NULL)
	{
		if (pTransferBank == NULL)
		{
			CThostFtdcTransferBankField req;
			memset(&req, 0, sizeof(req));
			cbRspQryTransferBank(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryTransferBank(pTransferBank, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询投资者持仓明细响应
void CTraderSpi::OnRspQryInvestorPositionDetail(CThostFtdcInvestorPositionDetailField *pInvestorPositionDetail, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryInvestorPositionDetail != NULL)
	{
		if (pInvestorPositionDetail == NULL)
		{
			CThostFtdcInvestorPositionDetailField req;
			memset(&req, 0, sizeof(req));
			cbRspQryInvestorPositionDetail(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryInvestorPositionDetail(pInvestorPositionDetail, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询客户通知响应
void CTraderSpi::OnRspQryNotice(CThostFtdcNoticeField *pNotice, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryNotice != NULL)
	{
		if (pNotice == NULL)
		{
			CThostFtdcNoticeField req;
			memset(&req, 0, sizeof(req));
			cbRspQryNotice(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryNotice(pNotice, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询结算信息确认响应
void CTraderSpi::OnRspQrySettlementInfoConfirm(CThostFtdcSettlementInfoConfirmField *pSettlementInfoConfirm, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQrySettlementInfoConfirm != NULL)
	{
		if (pSettlementInfoConfirm == NULL)
		{
			CThostFtdcSettlementInfoConfirmField req;
			memset(&req, 0, sizeof(req));
			cbRspQrySettlementInfoConfirm(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQrySettlementInfoConfirm(pSettlementInfoConfirm, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询投资者持仓明细响应
void CTraderSpi::OnRspQryInvestorPositionCombineDetail(CThostFtdcInvestorPositionCombineDetailField *pInvestorPositionCombineDetail, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryInvestorPositionCombineDetail != NULL)
	{
		if (pInvestorPositionCombineDetail == NULL)
		{
			CThostFtdcInvestorPositionCombineDetailField req;
			memset(&req, 0, sizeof(req));
			cbRspQryInvestorPositionCombineDetail(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryInvestorPositionCombineDetail(pInvestorPositionCombineDetail, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///查询保证金监管系统经纪公司资金账户密钥响应
void CTraderSpi::OnRspQryCFMMCTradingAccountKey(CThostFtdcCFMMCTradingAccountKeyField *pCFMMCTradingAccountKey, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryCFMMCTradingAccountKey != NULL)
	{
		if (pCFMMCTradingAccountKey == NULL)
		{
			CThostFtdcCFMMCTradingAccountKeyField req;
			memset(&req, 0, sizeof(req));
			cbRspQryCFMMCTradingAccountKey(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryCFMMCTradingAccountKey(pCFMMCTradingAccountKey, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询仓单折抵信息响应
void CTraderSpi::OnRspQryEWarrantOffset(CThostFtdcEWarrantOffsetField * pEWarrantOffset, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryEWarrantOffset != NULL)
	{
		if (pEWarrantOffset == NULL)
		{
			CThostFtdcEWarrantOffsetField req;
			memset(&req, 0, sizeof(req));
			cbRspQryEWarrantOffset(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryEWarrantOffset(pEWarrantOffset, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询投资者品种/跨品种保证金响应
void CTraderSpi::OnRspQryInvestorProductGroupMargin(CThostFtdcInvestorProductGroupMarginField * pInvestorProductGroupMargin, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryInvestorProductGroupMargin != NULL)
	{
		if (pInvestorProductGroupMargin == NULL)
		{
			CThostFtdcInvestorProductGroupMarginField req;
			memset(&req, 0, sizeof(req));
			cbRspQryInvestorProductGroupMargin(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryInvestorProductGroupMargin(pInvestorProductGroupMargin, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询交易所保证金率响应
void CTraderSpi::OnRspQryExchangeMarginRate(CThostFtdcExchangeMarginRateField * pExchangeMarginRate, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryExchangeMarginRate != NULL)
	{
		if (pExchangeMarginRate == NULL)
		{
			CThostFtdcExchangeMarginRateField req;
			memset(&req, 0, sizeof(req));
			cbRspQryExchangeMarginRate(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryExchangeMarginRate(pExchangeMarginRate, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询交易所调整保证金率响应
void CTraderSpi::OnRspQryExchangeMarginRateAdjust(CThostFtdcExchangeMarginRateAdjustField * pExchangeMarginRateAdjust, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryExchangeMarginRateAdjust != NULL)
	{
		if (pExchangeMarginRateAdjust == NULL)
		{
			CThostFtdcExchangeMarginRateAdjustField req;
			memset(&req, 0, sizeof(req));
			cbRspQryExchangeMarginRateAdjust(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryExchangeMarginRateAdjust(pExchangeMarginRateAdjust, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询汇率响应
void CTraderSpi::OnRspQryExchangeRate(CThostFtdcExchangeRateField * pExchangeRate, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryExchangeRate != NULL)
	{
		if (pExchangeRate == NULL)
		{
			CThostFtdcExchangeRateField req;
			memset(&req, 0, sizeof(req));
			cbRspQryExchangeRate(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryExchangeRate(pExchangeRate, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询二级代理操作员银期权限响应
void CTraderSpi::OnRspQrySecAgentACIDMap(CThostFtdcSecAgentACIDMapField * pSecAgentACIDMap, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQrySecAgentACIDMap != NULL)
	{
		if (pSecAgentACIDMap == NULL)
		{
			CThostFtdcSecAgentACIDMapField req;
			memset(&req, 0, sizeof(req));
			cbRspQrySecAgentACIDMap(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQrySecAgentACIDMap(pSecAgentACIDMap, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询产品报价汇率
void CTraderSpi::OnRspQryProductExchRate(CThostFtdcProductExchRateField * pProductExchRate, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryProductExchRate != NULL)
	{
		if (pProductExchRate == NULL)
		{
			CThostFtdcProductExchRateField req;
			memset(&req, 0, sizeof(req));
			cbRspQryProductExchRate(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryProductExchRate(pProductExchRate, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询产品组
void CTraderSpi::OnRspQryProductGroup(CThostFtdcProductGroupField * pProductGroup, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryProductGroup != NULL)
	{
		if (pProductGroup == NULL)
		{
			CThostFtdcProductGroupField req;
			memset(&req, 0, sizeof(req));
			cbRspQryProductGroup(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryProductGroup(pProductGroup, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询做市商合约手续费率响应
void CTraderSpi::OnRspQryMMInstrumentCommissionRate(CThostFtdcMMInstrumentCommissionRateField * pMMInstrumentCommissionRate, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryMMInstrumentCommissionRate != NULL)
	{
		if (pMMInstrumentCommissionRate == NULL)
		{
			CThostFtdcMMInstrumentCommissionRateField req;
			memset(&req, 0, sizeof(req));
			cbRspQryMMInstrumentCommissionRate(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryMMInstrumentCommissionRate(pMMInstrumentCommissionRate, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询做市商期权合约手续费响应
void CTraderSpi::OnRspQryMMOptionInstrCommRate(CThostFtdcMMOptionInstrCommRateField * pMMOptionInstrCommRate, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryMMOptionInstrCommRate != NULL)
	{
		if (pMMOptionInstrCommRate == NULL)
		{
			CThostFtdcMMOptionInstrCommRateField req;
			memset(&req, 0, sizeof(req));
			cbRspQryMMOptionInstrCommRate(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryMMOptionInstrCommRate(pMMOptionInstrCommRate, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询报单手续费响应
void CTraderSpi::OnRspQryInstrumentOrderCommRate(CThostFtdcInstrumentOrderCommRateField * pInstrumentOrderCommRate, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryInstrumentOrderCommRate != NULL)
	{
		if (pInstrumentOrderCommRate == NULL)
		{
			CThostFtdcInstrumentOrderCommRateField req;
			memset(&req, 0, sizeof(req));
			cbRspQryInstrumentOrderCommRate(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryInstrumentOrderCommRate(pInstrumentOrderCommRate, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询资金账户响应
void CTraderSpi::OnRspQrySecAgentTradingAccount(CThostFtdcTradingAccountField *pTradingAccount, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQrySecAgentTradingAccount != NULL)
	{
		if (pTradingAccount == NULL)
		{
			CThostFtdcTradingAccountField req;
			memset(&req, 0, sizeof(req));
			cbRspQrySecAgentTradingAccount(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQrySecAgentTradingAccount(pTradingAccount, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询二级代理商资金校验模式响应
void CTraderSpi::OnRspQrySecAgentCheckMode(CThostFtdcSecAgentCheckModeField *pSecAgentCheckMode, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQrySecAgentCheckMode != NULL)
	{
		if (pSecAgentCheckMode == NULL)
		{
			CThostFtdcSecAgentCheckModeField req;
			memset(&req, 0, sizeof(req));
			cbRspQrySecAgentCheckMode(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQrySecAgentCheckMode(pSecAgentCheckMode, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询二级代理商信息响应
void CTraderSpi::OnRspQrySecAgentTradeInfo(CThostFtdcSecAgentTradeInfoField *pSecAgentTradeInfo, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (pSecAgentTradeInfo == NULL)
	{
		CThostFtdcSecAgentTradeInfoField req;
		memset(&req, 0, sizeof(req));
		cbRspQrySecAgentTradeInfo(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
	else
		cbRspQrySecAgentTradeInfo(pSecAgentTradeInfo, repareInfo(pRspInfo), nRequestID, bIsLast);
}

///请求查询期权交易成本响应
void CTraderSpi::OnRspQryOptionInstrTradeCost(CThostFtdcOptionInstrTradeCostField * pOptionInstrTradeCost, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryOptionInstrTradeCost != NULL)
	{
		if (pOptionInstrTradeCost == NULL)
		{
			CThostFtdcOptionInstrTradeCostField req;
			memset(&req, 0, sizeof(req));
			cbRspQryOptionInstrTradeCost(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryOptionInstrTradeCost(pOptionInstrTradeCost, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询期权合约手续费响应
void CTraderSpi::OnRspQryOptionInstrCommRate(CThostFtdcOptionInstrCommRateField * pOptionInstrCommRate, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryOptionInstrCommRate != NULL)
	{
		if (pOptionInstrCommRate == NULL)
		{
			CThostFtdcOptionInstrCommRateField req;
			memset(&req, 0, sizeof(req));
			cbRspQryOptionInstrCommRate(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryOptionInstrCommRate(pOptionInstrCommRate, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询执行宣告响应
void CTraderSpi::OnRspQryExecOrder(CThostFtdcExecOrderField * pExecOrder, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryExecOrder != NULL)
	{
		if (pExecOrder == NULL)
		{
			CThostFtdcExecOrderField req;
			memset(&req, 0, sizeof(req));
			cbRspQryExecOrder(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryExecOrder(pExecOrder, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询询价响应
void CTraderSpi::OnRspQryForQuote(CThostFtdcForQuoteField * pForQuote, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryForQuote != NULL)
	{
		if (pForQuote == NULL)
		{
			CThostFtdcForQuoteField req;
			memset(&req, 0, sizeof(req));
			cbRspQryForQuote(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryForQuote(pForQuote, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询报价响应
void CTraderSpi::OnRspQryQuote(CThostFtdcQuoteField * pQuote, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryQuote != NULL)
	{
		if (pQuote == NULL)
		{
			CThostFtdcQuoteField req;
			memset(&req, 0, sizeof(req));
			cbRspQryQuote(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryQuote(pQuote, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询期权自对冲响应
void CTraderSpi::OnRspQryOptionSelfClose(CThostFtdcOptionSelfCloseField *pOptionSelfClose, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryOptionSelfClose != NULL)
	{
		if (pOptionSelfClose == NULL)
		{
			CThostFtdcOptionSelfCloseField req;
			memset(&req, 0, sizeof(req));
			cbRspQryOptionSelfClose(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryOptionSelfClose(pOptionSelfClose, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询投资单元响应
void CTraderSpi::OnRspQryInvestUnit(CThostFtdcInvestUnitField *pInvestUnit, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryInvestUnit != NULL)
	{
		if (pInvestUnit == NULL)
		{
			CThostFtdcInvestUnitField req;
			memset(&req, 0, sizeof(req));
			cbRspQryInvestUnit(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryInvestUnit(pInvestUnit, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询组合合约安全系数响应
void CTraderSpi::OnRspQryCombInstrumentGuard(CThostFtdcCombInstrumentGuardField * pCombInstrumentGuard, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryCombInstrumentGuard != NULL)
	{
		if (pCombInstrumentGuard == NULL)
		{
			CThostFtdcCombInstrumentGuardField req;
			memset(&req, 0, sizeof(req));
			cbRspQryCombInstrumentGuard(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryCombInstrumentGuard(pCombInstrumentGuard, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询申请组合响应
void CTraderSpi::OnRspQryCombAction(CThostFtdcCombActionField * pCombAction, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryCombAction != NULL)
	{
		if (pCombAction == NULL)
		{
			CThostFtdcCombActionField req;
			memset(&req, 0, sizeof(req));
			cbRspQryCombAction(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryCombAction(pCombAction, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询转帐流水响应
void CTraderSpi::OnRspQryTransferSerial(CThostFtdcTransferSerialField *pTransferSerial, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryTransferSerial != NULL)
	{
		if (pTransferSerial == NULL)
		{
			CThostFtdcTransferSerialField req;
			memset(&req, 0, sizeof(req));
			cbRspQryTransferSerial(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryTransferSerial(pTransferSerial, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询银期签约关系响应
void CTraderSpi::OnRspQryAccountregister(CThostFtdcAccountregisterField *pAccountregister, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryAccountregister != NULL)
	{
		if (pAccountregister == NULL)
		{
			CThostFtdcAccountregisterField req;
			memset(&req, 0, sizeof(req));
			cbRspQryAccountregister(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryAccountregister(pAccountregister, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
};

///错误应答
void CTraderSpi::OnRspError(CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspError != NULL)
	{
		if (pRspInfo == NULL)
		{
			CThostFtdcRspInfoField req;
			memset(&req, 0, sizeof(req));
			cbRspError(&req, nRequestID, bIsLast);
		}
		else
			cbRspError(pRspInfo, nRequestID, bIsLast);
	}
}

///报单通知
void CTraderSpi::OnRtnOrder(CThostFtdcOrderField *pOrder)
{
	if (cbRtnOrder != NULL)
	{
		if (pOrder == NULL)
		{
			CThostFtdcOrderField req;
			memset(&req, 0, sizeof(req));
			cbRtnOrder(&req);
		}
		else
			cbRtnOrder(pOrder);
	}
}

///成交通知
void CTraderSpi::OnRtnTrade(CThostFtdcTradeField *pTrade)
{
	if (cbRtnTrade != NULL)
	{
		if (pTrade == NULL)
		{
			CThostFtdcTradeField req;
			memset(&req, 0, sizeof(req));
			cbRtnTrade(&req);
		}
		else
			cbRtnTrade(pTrade);
	}
}

///报单录入错误回报
void CTraderSpi::OnErrRtnOrderInsert(CThostFtdcInputOrderField *pInputOrder, CThostFtdcRspInfoField *pRspInfo)
{
	if (cbErrRtnOrderInsert != NULL)
	{
		if (pInputOrder == NULL)
		{
			CThostFtdcInputOrderField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnOrderInsert(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnOrderInsert(pInputOrder, repareInfo(pRspInfo));
	}
}

///报单操作错误回报
void CTraderSpi::OnErrRtnOrderAction(CThostFtdcOrderActionField *pOrderAction, CThostFtdcRspInfoField *pRspInfo)
{
	if (cbErrRtnOrderAction != NULL)
	{
		if (pOrderAction == NULL)
		{
			CThostFtdcOrderActionField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnOrderAction(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnOrderAction(pOrderAction, repareInfo(pRspInfo));
	}
}

///合约交易状态通知
void CTraderSpi::OnRtnInstrumentStatus(CThostFtdcInstrumentStatusField *pInstrumentStatus)
{
	if (cbRtnInstrumentStatus != NULL)
	{
		if (pInstrumentStatus == NULL)
		{
			CThostFtdcInstrumentStatusField req;
			memset(&req, 0, sizeof(req));
			cbRtnInstrumentStatus(&req);
		}
		else
			cbRtnInstrumentStatus(pInstrumentStatus);
	}
}

///交易通知
void CTraderSpi::OnRtnTradingNotice(CThostFtdcTradingNoticeInfoField *pTradingNoticeInfo)
{
	if (cbRtnTradingNotice != NULL)
	{
		if (pTradingNoticeInfo == NULL)
		{
			CThostFtdcTradingNoticeInfoField req;
			memset(&req, 0, sizeof(req));
			cbRtnTradingNotice(&req);
		}
		else
			cbRtnTradingNotice(pTradingNoticeInfo);
	}
}

///提示条件单校验错误
void CTraderSpi::OnRtnErrorConditionalOrder(CThostFtdcErrorConditionalOrderField *pErrorConditionalOrder)
{
	if (cbRtnErrorConditionalOrder != NULL)
	{
		if (pErrorConditionalOrder == NULL)
		{
			CThostFtdcErrorConditionalOrderField req;
			memset(&req, 0, sizeof(req));
			cbRtnErrorConditionalOrder(&req);
		}
		else
			cbRtnErrorConditionalOrder(pErrorConditionalOrder);
	}
}

///执行宣告通知
void CTraderSpi::OnRtnExecOrder(CThostFtdcExecOrderField * pExecOrder)
{
	if (cbRtnExecOrder != NULL)
	{
		if (pExecOrder == NULL)
		{
			CThostFtdcExecOrderField req;
			memset(&req, 0, sizeof(req));
			cbRtnExecOrder(&req);
		}
		else
			cbRtnExecOrder(pExecOrder);
	}
}

///执行宣告录入错误回报
void CTraderSpi::OnErrRtnExecOrderInsert(CThostFtdcInputExecOrderField * pInputExecOrder, CThostFtdcRspInfoField * pRspInfo)
{
	if (cbErrRtnExecOrderInsert != NULL)
	{
		if (pInputExecOrder == NULL)
		{
			CThostFtdcInputExecOrderField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnExecOrderInsert(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnExecOrderInsert(pInputExecOrder, repareInfo(pRspInfo));
	}
}

///执行宣告操作错误回报
void CTraderSpi::OnErrRtnExecOrderAction(CThostFtdcExecOrderActionField * pExecOrderAction, CThostFtdcRspInfoField * pRspInfo)
{
	if (cbErrRtnExecOrderAction != NULL)
	{
		if (pExecOrderAction == NULL)
		{
			CThostFtdcExecOrderActionField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnExecOrderAction(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnExecOrderAction(pExecOrderAction, repareInfo(pRspInfo));
	}
}

///询价录入错误回报
void CTraderSpi::OnErrRtnForQuoteInsert(CThostFtdcInputForQuoteField * pInputForQuote, CThostFtdcRspInfoField * pRspInfo)
{
	if (cbErrRtnForQuoteInsert != NULL)
	{
		if (pInputForQuote == NULL)
		{
			CThostFtdcInputForQuoteField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnForQuoteInsert(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnForQuoteInsert(pInputForQuote, repareInfo(pRspInfo));
	}
}

///报价通知
void CTraderSpi::OnRtnQuote(CThostFtdcQuoteField * pQuote)
{
	if (cbRtnQuote != NULL)
	{
		if (pQuote == NULL)
		{
			CThostFtdcQuoteField req;
			memset(&req, 0, sizeof(req));
			cbRtnQuote(&req);
		}
		else
			cbRtnQuote(pQuote);
	}
}

///报价录入错误回报
void CTraderSpi::OnErrRtnQuoteInsert(CThostFtdcInputQuoteField * pInputQuote, CThostFtdcRspInfoField * pRspInfo)
{
	if (cbErrRtnQuoteInsert != NULL)
	{
		if (pInputQuote == NULL)
		{
			CThostFtdcInputQuoteField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnQuoteInsert(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnQuoteInsert(pInputQuote, repareInfo(pRspInfo));
	}
}

///报价操作错误回报
void CTraderSpi::OnErrRtnQuoteAction(CThostFtdcQuoteActionField * pQuoteAction, CThostFtdcRspInfoField * pRspInfo)
{
	if (cbErrRtnQuoteAction != NULL)
	{
		if (pQuoteAction == NULL)
		{
			CThostFtdcQuoteActionField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnQuoteAction(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnQuoteAction(pQuoteAction, repareInfo(pRspInfo));
	}
}

///询价通知
void CTraderSpi::OnRtnForQuoteRsp(CThostFtdcForQuoteRspField * pForQuoteRsp)
{
	if (cbRtnForQuoteRsp != NULL)
	{
		if (pForQuoteRsp == NULL)
		{
			CThostFtdcForQuoteRspField req;
			memset(&req, 0, sizeof(req));
			cbRtnForQuoteRsp(&req);
		}
	}
}

///保证金监控中心用户令牌
void CTraderSpi::OnRtnCFMMCTradingAccountToken(CThostFtdcCFMMCTradingAccountTokenField * pCFMMCTradingAccountToken)
{
	if (cbRtnCFMMCTradingAccountToken != NULL)
	{
		if (pCFMMCTradingAccountToken == NULL)
		{
			CThostFtdcCFMMCTradingAccountTokenField req;
			memset(&req, 0, sizeof(req));
			cbRtnCFMMCTradingAccountToken(&req);
		}
		else
			cbRtnCFMMCTradingAccountToken(pCFMMCTradingAccountToken);
	}
}

///批量报单操作错误回报
void CTraderSpi::OnErrRtnBatchOrderAction(CThostFtdcBatchOrderActionField * pBatchOrderAction, CThostFtdcRspInfoField * pRspInfo)
{
	if (cbErrRtnBatchOrderAction != NULL)
	{
		if (pBatchOrderAction == NULL)
		{
			CThostFtdcBatchOrderActionField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnBatchOrderAction(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnBatchOrderAction(pBatchOrderAction, repareInfo(pRspInfo));
	}
}

///期权自对冲通知
void CTraderSpi::OnRtnOptionSelfClose(CThostFtdcOptionSelfCloseField *pOptionSelfClose)
{
	if (cbRtnOptionSelfClose != NULL)
	{
		if (pOptionSelfClose == NULL)
		{
			CThostFtdcOptionSelfCloseField req;
			memset(&req, 0, sizeof(req));
			cbRtnOptionSelfClose(&req);
		}
		else
			cbRtnOptionSelfClose(pOptionSelfClose);
	}
}

///期权自对冲录入错误回报
void CTraderSpi::OnErrRtnOptionSelfCloseInsert(CThostFtdcInputOptionSelfCloseField *pInputOptionSelfClose, CThostFtdcRspInfoField *pRspInfo)
{
	if (cbErrRtnOptionSelfCloseInsert != NULL)
	{
		if (pInputOptionSelfClose == NULL)
		{
			CThostFtdcInputOptionSelfCloseField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnOptionSelfCloseInsert(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnOptionSelfCloseInsert(pInputOptionSelfClose, repareInfo(pRspInfo));
	}
}

///期权自对冲操作错误回报
void CTraderSpi::OnErrRtnOptionSelfCloseAction(CThostFtdcOptionSelfCloseActionField *pOptionSelfCloseAction, CThostFtdcRspInfoField *pRspInfo)
{
	if (cbErrRtnOptionSelfCloseAction != NULL)
	{
		if (pOptionSelfCloseAction == NULL)
		{
			CThostFtdcOptionSelfCloseActionField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnOptionSelfCloseAction(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnOptionSelfCloseAction(pOptionSelfCloseAction, repareInfo(pRspInfo));
	}
}

///申请组合通知
void CTraderSpi::OnRtnCombAction(CThostFtdcCombActionField * pCombAction)
{
	if (cbRtnCombAction != NULL)
	{
		if (pCombAction == NULL)
		{
			CThostFtdcCombActionField req;
			memset(&req, 0, sizeof(req));
			cbRtnCombAction(&req);
		}
		else
			cbRtnCombAction(pCombAction);
	}
}

///申请组合录入错误回报
void CTraderSpi::OnErrRtnCombActionInsert(CThostFtdcInputCombActionField * pInputCombAction, CThostFtdcRspInfoField * pRspInfo)
{
	if (cbErrRtnCombActionInsert != NULL)
	{
		if (pInputCombAction == NULL)
		{
			CThostFtdcInputCombActionField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnCombActionInsert(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnCombActionInsert(pInputCombAction, repareInfo(pRspInfo));
	}
}

///请求查询签约银行响应
void CTraderSpi::OnRspQryContractBank(CThostFtdcContractBankField *pContractBank, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryContractBank != NULL)
	{
		if (pContractBank == NULL)
		{
			CThostFtdcContractBankField req;
			memset(&req, 0, sizeof(req));
			cbRspQryContractBank(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryContractBank(pContractBank, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询预埋单响应
void CTraderSpi::OnRspQryParkedOrder(CThostFtdcParkedOrderField *pParkedOrder, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryParkedOrder != NULL)
	{
		if (pParkedOrder == NULL)
		{
			CThostFtdcParkedOrderField req;
			memset(&req, 0, sizeof(req));
			cbRspQryParkedOrder(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryParkedOrder(pParkedOrder, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询预埋撤单响应
void CTraderSpi::OnRspQryParkedOrderAction(CThostFtdcParkedOrderActionField *pParkedOrderAction, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryParkedOrderAction != NULL)
	{
		if (pParkedOrderAction == NULL)
		{
			CThostFtdcParkedOrderActionField req;
			memset(&req, 0, sizeof(req));
			cbRspQryParkedOrderAction(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryParkedOrderAction(pParkedOrderAction, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询交易通知响应
void CTraderSpi::OnRspQryTradingNotice(CThostFtdcTradingNoticeField *pTradingNotice, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryTradingNotice != NULL)
	{
		if (pTradingNotice == NULL)
		{
			CThostFtdcTradingNoticeField req;
			memset(&req, 0, sizeof(req));
			cbRspQryTradingNotice(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryTradingNotice(pTradingNotice, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询经纪公司交易参数响应
void CTraderSpi::OnRspQryBrokerTradingParams(CThostFtdcBrokerTradingParamsField *pBrokerTradingParams, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryBrokerTradingParams != NULL)
	{
		if (pBrokerTradingParams == NULL)
		{
			CThostFtdcBrokerTradingParamsField req;
			memset(&req, 0, sizeof(req));
			cbRspQryBrokerTradingParams(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryBrokerTradingParams(pBrokerTradingParams, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询经纪公司交易算法响应
void CTraderSpi::OnRspQryBrokerTradingAlgos(CThostFtdcBrokerTradingAlgosField *pBrokerTradingAlgos, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQryBrokerTradingAlgos != NULL)
	{
		if (pBrokerTradingAlgos == NULL)
		{
			CThostFtdcBrokerTradingAlgosField req;
			memset(&req, 0, sizeof(req));
			cbRspQryBrokerTradingAlgos(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQryBrokerTradingAlgos(pBrokerTradingAlgos, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///请求查询监控中心用户令牌
void CTraderSpi::OnRspQueryCFMMCTradingAccountToken(CThostFtdcQueryCFMMCTradingAccountTokenField * pQueryCFMMCTradingAccountToken, CThostFtdcRspInfoField * pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQueryCFMMCTradingAccountToken != NULL)
	{
		if (pQueryCFMMCTradingAccountToken == NULL)
		{
			CThostFtdcQueryCFMMCTradingAccountTokenField req;
			memset(&req, 0, sizeof(req));
			cbRspQueryCFMMCTradingAccountToken(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQueryCFMMCTradingAccountToken(pQueryCFMMCTradingAccountToken, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///银行发起银行资金转期货通知
void CTraderSpi::OnRtnFromBankToFutureByBank(CThostFtdcRspTransferField *pRspTransfer)
{
	if (cbRtnFromBankToFutureByBank != NULL)
	{
		if (pRspTransfer == NULL)
		{
			CThostFtdcRspTransferField req;
			memset(&req, 0, sizeof(req));
			cbRtnFromBankToFutureByBank(&req);
		}
		else
			cbRtnFromBankToFutureByBank(pRspTransfer);
	}
}

///银行发起期货资金转银行通知
void CTraderSpi::OnRtnFromFutureToBankByBank(CThostFtdcRspTransferField *pRspTransfer)
{
	if (cbRtnFromFutureToBankByBank != NULL)
	{
		if (pRspTransfer == NULL)
		{
			CThostFtdcRspTransferField req;
			memset(&req, 0, sizeof(req));
			cbRtnFromFutureToBankByBank(&req);
		}
		else
			cbRtnFromFutureToBankByBank(pRspTransfer);
	}
}

///银行发起冲正银行转期货通知
void CTraderSpi::OnRtnRepealFromBankToFutureByBank(CThostFtdcRspRepealField *pRspRepeal)
{
	if (cbRtnRepealFromBankToFutureByBank != NULL)
	{
		if (pRspRepeal == NULL)
		{
			CThostFtdcRspRepealField req;
			memset(&req, 0, sizeof(req));
			cbRtnRepealFromBankToFutureByBank(&req);
		}
		else
			cbRtnRepealFromBankToFutureByBank(pRspRepeal);
	}
}

///银行发起冲正期货转银行通知
void CTraderSpi::OnRtnRepealFromFutureToBankByBank(CThostFtdcRspRepealField *pRspRepeal)
{
	if (cbRtnRepealFromFutureToBankByBank != NULL)
	{
		if (pRspRepeal == NULL)
		{
			CThostFtdcRspRepealField req;
			memset(&req, 0, sizeof(req));
			cbRtnRepealFromFutureToBankByBank(&req);
		}
		else
			cbRtnRepealFromFutureToBankByBank(pRspRepeal);
	}
}

///期货发起银行资金转期货通知
void CTraderSpi::OnRtnFromBankToFutureByFuture(CThostFtdcRspTransferField *pRspTransfer)
{
	if (cbRtnFromBankToFutureByFuture != NULL)
	{
		if (pRspTransfer == NULL)
		{
			CThostFtdcRspTransferField req;
			memset(&req, 0, sizeof(req));
			cbRtnFromBankToFutureByFuture(&req);
		}
		else
			cbRtnFromBankToFutureByFuture(pRspTransfer);
	}
}

///期货发起期货资金转银行通知
void CTraderSpi::OnRtnFromFutureToBankByFuture(CThostFtdcRspTransferField *pRspTransfer)
{
	if (cbRtnFromFutureToBankByFuture != NULL)
	{
		if (pRspTransfer == NULL)
		{
			CThostFtdcRspTransferField req;
			memset(&req, 0, sizeof(req));
			cbRtnFromFutureToBankByFuture(&req);
		}
		else
			cbRtnFromFutureToBankByFuture(pRspTransfer);
	}
}

///系统运行时期货端手工发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
void CTraderSpi::OnRtnRepealFromBankToFutureByFutureManual(CThostFtdcRspRepealField *pRspRepeal)
{
	if (cbRtnRepealFromBankToFutureByFutureManual != NULL)
	{
		if (pRspRepeal == NULL)
		{
			CThostFtdcRspRepealField req;
			memset(&req, 0, sizeof(req));
			cbRtnRepealFromBankToFutureByFutureManual(&req);
		}
		else
			cbRtnRepealFromBankToFutureByFutureManual(pRspRepeal);
	}
}

///系统运行时期货端手工发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
void CTraderSpi::OnRtnRepealFromFutureToBankByFutureManual(CThostFtdcRspRepealField *pRspRepeal)
{
	if (cbRtnRepealFromFutureToBankByFutureManual != NULL)
	{
		if (pRspRepeal == NULL)
		{
			CThostFtdcRspRepealField req;
			memset(&req, 0, sizeof(req));
			cbRtnRepealFromFutureToBankByFutureManual(&req);
		}
		else
			cbRtnRepealFromFutureToBankByFutureManual(pRspRepeal);
	}
}

///期货发起查询银行余额通知
void CTraderSpi::OnRtnQueryBankBalanceByFuture(CThostFtdcNotifyQueryAccountField *pNotifyQueryAccount)
{
	if (cbRtnQueryBankBalanceByFuture != NULL)
	{
		if (pNotifyQueryAccount == NULL)
		{
			CThostFtdcNotifyQueryAccountField req;
			memset(&req, 0, sizeof(req));
			cbRtnQueryBankBalanceByFuture(&req);
		}
		else
			cbRtnQueryBankBalanceByFuture(pNotifyQueryAccount);
	}
}

///期货发起银行资金转期货错误回报
void CTraderSpi::OnErrRtnBankToFutureByFuture(CThostFtdcReqTransferField *pReqTransfer, CThostFtdcRspInfoField *pRspInfo)
{
	if (cbErrRtnBankToFutureByFuture != NULL)
	{
		if (pReqTransfer == NULL)
		{
			CThostFtdcReqTransferField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnBankToFutureByFuture(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnBankToFutureByFuture(pReqTransfer, repareInfo(pRspInfo));
	}
}

///期货发起期货资金转银行错误回报
void CTraderSpi::OnErrRtnFutureToBankByFuture(CThostFtdcReqTransferField *pReqTransfer, CThostFtdcRspInfoField *pRspInfo)
{
	if (cbErrRtnFutureToBankByFuture != NULL)
	{
		if (pReqTransfer == NULL)
		{
			CThostFtdcReqTransferField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnFutureToBankByFuture(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnFutureToBankByFuture(pReqTransfer, repareInfo(pRspInfo));
	}
}

///系统运行时期货端手工发起冲正银行转期货错误回报
void CTraderSpi::OnErrRtnRepealBankToFutureByFutureManual(CThostFtdcReqRepealField *pReqRepeal, CThostFtdcRspInfoField *pRspInfo)
{
	if (cbErrRtnRepealBankToFutureByFutureManual != NULL)
	{
		if (pReqRepeal == NULL)
		{
			CThostFtdcReqRepealField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnRepealBankToFutureByFutureManual(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnRepealBankToFutureByFutureManual(pReqRepeal, repareInfo(pRspInfo));
	}
}

///系统运行时期货端手工发起冲正期货转银行错误回报
void CTraderSpi::OnErrRtnRepealFutureToBankByFutureManual(CThostFtdcReqRepealField *pReqRepeal, CThostFtdcRspInfoField *pRspInfo)
{
	if (cbErrRtnRepealFutureToBankByFutureManual != NULL)
	{
		if (pReqRepeal == NULL)
		{
			CThostFtdcReqRepealField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnRepealFutureToBankByFutureManual(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnRepealFutureToBankByFutureManual(pReqRepeal, repareInfo(pRspInfo));
	}
}

///期货发起查询银行余额错误回报
void CTraderSpi::OnErrRtnQueryBankBalanceByFuture(CThostFtdcReqQueryAccountField *pReqQueryAccount, CThostFtdcRspInfoField *pRspInfo)
{
	if (cbErrRtnQueryBankBalanceByFuture != NULL)
	{
		if (pReqQueryAccount == NULL)
		{
			CThostFtdcReqQueryAccountField req;
			memset(&req, 0, sizeof(req));
			cbErrRtnQueryBankBalanceByFuture(&req, repareInfo(pRspInfo));
		}
		else
			cbErrRtnQueryBankBalanceByFuture(pReqQueryAccount, repareInfo(pRspInfo));
	}
}

///期货发起冲正银行转期货请求，银行处理完毕后报盘发回的通知
void CTraderSpi::OnRtnRepealFromBankToFutureByFuture(CThostFtdcRspRepealField *pRspRepeal)
{
	if (cbRtnRepealFromBankToFutureByFuture != NULL)
	{
		if (pRspRepeal == NULL)
		{
			CThostFtdcRspRepealField req;
			memset(&req, 0, sizeof(req));
			cbRtnRepealFromBankToFutureByFuture(&req);
		}
		else
			cbRtnRepealFromBankToFutureByFuture(pRspRepeal);
	}
}

///期货发起冲正期货转银行请求，银行处理完毕后报盘发回的通知
void CTraderSpi::OnRtnRepealFromFutureToBankByFuture(CThostFtdcRspRepealField *pRspRepeal)
{
	if (cbRtnRepealFromFutureToBankByFuture != NULL)
	{
		if (pRspRepeal == NULL)
		{
			CThostFtdcRspRepealField req;
			memset(&req, 0, sizeof(req));
			cbRtnRepealFromFutureToBankByFuture(&req);
		}
		else
			cbRtnRepealFromFutureToBankByFuture(pRspRepeal);
	}
}

///期货发起银行资金转期货应答
void CTraderSpi::OnRspFromBankToFutureByFuture(CThostFtdcReqTransferField *pReqTransfer, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspFromBankToFutureByFuture != NULL)
	{
		if (pReqTransfer == NULL)
		{
			CThostFtdcReqTransferField req;
			memset(&req, 0, sizeof(req));
			cbRspFromBankToFutureByFuture(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspFromBankToFutureByFuture(pReqTransfer, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///期货发起期货资金转银行应答
void CTraderSpi::OnRspFromFutureToBankByFuture(CThostFtdcReqTransferField *pReqTransfer, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspFromFutureToBankByFuture != NULL)
	{
		if (pReqTransfer == NULL)
		{
			CThostFtdcReqTransferField req;
			memset(&req, 0, sizeof(req));
			cbRspFromFutureToBankByFuture(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspFromFutureToBankByFuture(pReqTransfer, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///期货发起查询银行余额应答
void CTraderSpi::OnRspQueryBankAccountMoneyByFuture(CThostFtdcReqQueryAccountField *pReqQueryAccount, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if (cbRspQueryBankAccountMoneyByFuture != NULL)
	{
		if (pReqQueryAccount == NULL)
		{
			CThostFtdcReqQueryAccountField req;
			memset(&req, 0, sizeof(req));
			cbRspQueryBankAccountMoneyByFuture(&req, repareInfo(pRspInfo), nRequestID, bIsLast);
		}
		else
			cbRspQueryBankAccountMoneyByFuture(pReqQueryAccount, repareInfo(pRspInfo), nRequestID, bIsLast);
	}
}

///银行发起银期开户通知
void CTraderSpi::OnRtnOpenAccountByBank(CThostFtdcOpenAccountField * pOpenAccount)
{
	if (cbRtnOpenAccountByBank != NULL)
	{
		if (pOpenAccount == NULL)
		{
			CThostFtdcOpenAccountField req;
			memset(&req, 0, sizeof(req));
			cbRtnOpenAccountByBank(&req);
		}
		else
			cbRtnOpenAccountByBank(pOpenAccount);
	}
}

///银行发起银期销户通知
void CTraderSpi::OnRtnCancelAccountByBank(CThostFtdcCancelAccountField * pCancelAccount)
{
	if (cbRtnCancelAccountByBank != NULL)
	{
		if (pCancelAccount == NULL)
		{
			CThostFtdcCancelAccountField req;
			memset(&req, 0, sizeof(req));
			cbRtnCancelAccountByBank(&req);
		}
		else
			cbRtnCancelAccountByBank(pCancelAccount);
	}
}

///银行发起变更银行账号通知
void CTraderSpi::OnRtnChangeAccountByBank(CThostFtdcChangeAccountField * pChangeAccount)
{
	if (cbRtnChangeAccountByBank != NULL)
	{
		if (pChangeAccount == NULL)
		{
			CThostFtdcChangeAccountField req;
			memset(&req, 0, sizeof(req));
			cbRtnChangeAccountByBank(&req);
		}
		else
			cbRtnChangeAccountByBank(pChangeAccount);
	}
}



//针对收到空反馈的处理
CThostFtdcRspInfoField rif;
CThostFtdcRspInfoField* CTraderSpi::repareInfo(CThostFtdcRspInfoField *pRspInfo)
{
	if (pRspInfo == NULL)
	{
		memset(&rif, 0, sizeof(rif));
		return &rif;
	}
	else
		return pRspInfo;
}


//bool CTraderSpi::IsMyOrder(CThostFtdcOrderField *pOrder)
//{
//	return ((pOrder->FrontID == FRONT_ID) &&
//			(pOrder->SessionID == SESSION_ID) &&
//			(strcmp(pOrder->OrderRef, ORDER_REF) == 0));
//}

//bool CTraderSpi::IsTradingOrder(CThostFtdcOrderField *pOrder)
//{
//	return ((pOrder->OrderStatus != THOST_FTDC_OST_PartTradedNotQueueing) &&
//			(pOrder->OrderStatus != THOST_FTDC_OST_Canceled) &&
//			(pOrder->OrderStatus != THOST_FTDC_OST_AllTraded));
//}
