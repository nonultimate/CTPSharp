// MdApi.cpp : 定义 DLL 应用程序的导出函数。
//
#include "stdafx.h"
#include "MdApi.h"
#include <iostream>
//#include <vector>		//动态数组,支持赋值
//using namespace std;

#include "..\api\amd64\ThostFtdcMdApi.h"

// UserApi对象
CThostFtdcMdApi* pUserApi;

// 请求编号
int iRequestID = 0;

//回调函数
CBOnRspError cbOnRspError = 0;
CBOnHeartBeatWarning cbOnHeartBeatWarning = 0;

CBOnFrontConnected cbOnFrontConnected = 0;
CBOnFrontDisconnected cbOnFrontDisconnected = 0;
CBOnRspUserLogin cbOnRspUserLogin = 0;
CBOnRspUserLogout cbOnRspUserLogout = 0;
CBOnRspSubMarketData cbOnRspSubMarketData = 0;
CBOnRspUnSubMarketData cbOnRspUnSubMarketData = 0;
CBOnRtnDepthMarketData cbOnRtnDepthMarketData = 0;

//获取接口版本
MDAPI_API const char* GetApiVersion()
{
	return CThostFtdcMdApi::GetApiVersion();
}

//连接
MDAPI_API void Connect(char* frontAddr, char *pszFlowPath)
{

	CThostFtdcMdSpi* pUserSpi = new CMdSpi();
	// 初始化UserApi
	pUserApi = CThostFtdcMdApi::CreateFtdcMdApi(pszFlowPath);			// 创建UserApi

	pUserApi->RegisterSpi(pUserSpi);						// 注册事件类
	pUserApi->RegisterFront(frontAddr);					// connect
	pUserApi->Init();
	//	pUserApi->Join();
}

//断开连接
MDAPI_API void DisConnect()
{
	pUserApi->Release();
}

//获取当前交易日:只有登录成功后,才能得到正确的交易日
MDAPI_API const char *GetTradingDay()
{
	return pUserApi->GetTradingDay();
}

//登录
MDAPI_API void ReqUserLogin(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID, TThostFtdcPasswordType password)
{
	CThostFtdcReqUserLoginField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, investorID);
	strcpy_s(req.Password, password);
	pUserApi->ReqUserLogin(&req, requestID);
}

//登出请求
MDAPI_API void ReqUserLogout(int requestID, TThostFtdcBrokerIDType brokerID, TThostFtdcInvestorIDType investorID)
{
	CThostFtdcUserLogoutField req;
	memset(&req, 0, sizeof(req));
	strcpy_s(req.BrokerID, brokerID);
	strcpy_s(req.UserID, investorID);
	pUserApi->ReqUserLogout(&req, requestID);
}
//订阅行情
MDAPI_API void SubMarketData(char* instrumentsID[], int nCount)
{
	pUserApi->SubscribeMarketData(instrumentsID, nCount);
}

//退订行情
MDAPI_API void UnSubscribeMarketData(char *ppInstrumentID[], int nCount)
{
	pUserApi->UnSubscribeMarketData(ppInstrumentID, nCount);
}

//============================================ 回调 函数注册 ===========================================
MDAPI_API void WINAPI RegOnRspError(CBOnRspError cb)
{
	cbOnRspError = cb;
}
//心跳
MDAPI_API void WINAPI RegOnHeartBeatWarning(CBOnHeartBeatWarning cb)
{
	cbOnHeartBeatWarning = cb;
}

//连接应答
MDAPI_API void WINAPI RegOnFrontConnected(CBOnFrontConnected cb)
{
	cbOnFrontConnected = cb;
}
//连接断开
MDAPI_API void WINAPI RegOnFrontDisconnected(CBOnFrontDisconnected cb)
{
	cbOnFrontDisconnected = cb;
}
//登录请求应答
MDAPI_API void WINAPI RegOnRspUserLogin(CBOnRspUserLogin cb)
{
	cbOnRspUserLogin = cb;
}
//登出请求应答
MDAPI_API void WINAPI RegOnRspUserLogout(CBOnRspUserLogout cb)
{
	cbOnRspUserLogout = cb;
}
//订阅行情应答
MDAPI_API void WINAPI RegOnRspSubMarketData(CBOnRspSubMarketData cb)
{
	cbOnRspSubMarketData = cb;
}

//退订行情应答
MDAPI_API void WINAPI RegOnRspUnSubMarketData(CBOnRspUnSubMarketData cb)
{
	cbOnRspUnSubMarketData = cb;
}
//深度行情通知
MDAPI_API void WINAPI RegOnRtnDepthMarketData(CBOnRtnDepthMarketData cb)
{
	cbOnRtnDepthMarketData = cb;
}