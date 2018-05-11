// MdApi.cpp : 定义 DLL 应用程序的导出函数。
//
#include "stdafx.h"
#include "MdApi.h"
#include <iostream>
//#include <vector>		//动态数组,支持赋值
//using namespace std;

#include "..\api\amd64\ThostFtdcMdApi.h"

// 请求编号
//int iRequestID = 0;

#pragma region 请求方法

//创建回调类
MDAPI_API CMdSpi* CreateSpi()
{
	CMdSpi* pUserSpi = new CMdSpi();
	return pUserSpi;
}

//获取接口版本
MDAPI_API const char* GetApiVersion()
{
	return CThostFtdcMdApi::GetApiVersion();
}

//连接
MDAPI_API CThostFtdcMdApi*  Connect(char* frontAddr, char *pszFlowPath, CMdSpi* pUserSpi)
{
	// 初始化UserApi
	CThostFtdcMdApi* pUserApi = CThostFtdcMdApi::CreateFtdcMdApi(pszFlowPath);			// 创建UserApi

	pUserApi->RegisterSpi((CThostFtdcMdSpi*)pUserSpi);						// 注册事件类
	pUserApi->RegisterFront(frontAddr);					// connect
	pUserApi->Init();
	//	pUserApi->Join();
	return pUserApi;
}

//断开连接
MDAPI_API void DisConnect(CThostFtdcMdApi* pUserApi)
{
	if (pUserApi == NULL) return;

	pUserApi->Release();
}

//获取当前交易日:只有登录成功后,才能得到正确的交易日
MDAPI_API const char *GetTradingDay(CThostFtdcMdApi* pUserApi)
{
	if (pUserApi == NULL) return NULL;

	return pUserApi->GetTradingDay();
}

//登录
MDAPI_API void ReqUserLogin(CThostFtdcMdApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcPasswordType password)
{
	if (pUserApi == NULL) return;

	CThostFtdcReqUserLoginField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, investorID);
	strcpy_s(req.Password, password);
	pUserApi->ReqUserLogin(&req, requestID);
}

//登出请求
MDAPI_API void ReqUserLogout(CThostFtdcMdApi* pUserApi, int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	if (pUserApi == NULL) return;

	CThostFtdcUserLogoutField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, investorID);
	pUserApi->ReqUserLogout(&req, requestID);
}
//订阅行情
MDAPI_API void SubMarketData(CThostFtdcMdApi* pUserApi, char* instrumentsID[], int nCount)
{
	if (pUserApi == NULL) return;

	pUserApi->SubscribeMarketData(instrumentsID, nCount);
}

//退订行情
MDAPI_API void UnSubscribeMarketData(CThostFtdcMdApi* pUserApi, char *ppInstrumentID[], int nCount)
{
	if (pUserApi == NULL) return;

	pUserApi->UnSubscribeMarketData(ppInstrumentID, nCount);
}


#pragma endregion

#pragma region 回调函数

//============================================ 回调 函数注册 ===========================================
MDAPI_API void WINAPI RegOnRspError(CMdSpi* pUserSpi, CBOnRspError cb)
{
	pUserSpi->cbOnRspError = cb;
}
//心跳
MDAPI_API void WINAPI RegOnHeartBeatWarning(CMdSpi* pUserSpi, CBOnHeartBeatWarning cb)
{
	pUserSpi->cbOnHeartBeatWarning = cb;
}

//连接应答
MDAPI_API void WINAPI RegOnFrontConnected(CMdSpi* pUserSpi, CBOnFrontConnected cb)
{
	pUserSpi->cbOnFrontConnected = cb;
}
//连接断开
MDAPI_API void WINAPI RegOnFrontDisconnected(CMdSpi* pUserSpi, CBOnFrontDisconnected cb)
{
	pUserSpi->cbOnFrontDisconnected = cb;
}
//登录请求应答
MDAPI_API void WINAPI RegOnRspUserLogin(CMdSpi* pUserSpi, CBOnRspUserLogin cb)
{
	pUserSpi->cbOnRspUserLogin = cb;
}
//登出请求应答
MDAPI_API void WINAPI RegOnRspUserLogout(CMdSpi* pUserSpi, CBOnRspUserLogout cb)
{
	pUserSpi->cbOnRspUserLogout = cb;
}
//订阅行情应答
MDAPI_API void WINAPI RegOnRspSubMarketData(CMdSpi* pUserSpi, CBOnRspSubMarketData cb)
{
	pUserSpi->cbOnRspSubMarketData = cb;
}

//退订行情应答
MDAPI_API void WINAPI RegOnRspUnSubMarketData(CMdSpi* pUserSpi, CBOnRspUnSubMarketData cb)
{
	pUserSpi->cbOnRspUnSubMarketData = cb;
}
//深度行情通知
MDAPI_API void WINAPI RegOnRtnDepthMarketData(CMdSpi* pUserSpi, CBOnRtnDepthMarketData cb)
{
	pUserSpi->cbOnRtnDepthMarketData = cb;
}

#pragma endregion