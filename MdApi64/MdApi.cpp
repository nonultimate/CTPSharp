// MdApi.cpp : 定义 DLL 应用程序的导出函数。
//
#include "stdafx.h"
#include "MdApi.h"
#include <iostream>
//#include <vector>		//动态数组,支持赋值
using namespace std;

#include "..\api\amd64\ThostFtdcMdApi.h"


// 这是导出变量的一个示例
//MDAPI_API int nMdApi=0;

extern CBOnRspError cbOnRspError;
extern CBOnHeartBeatWarning cbOnHeartBeatWarning;

extern CBOnFrontConnected cbOnFrontConnected;
extern CBOnFrontDisconnected cbOnFrontDisconnected;
extern CBOnRspUserLogin cbOnRspUserLogin;
extern CBOnRspUserLogout cbOnRspUserLogout;
extern CBOnRspSubMarketData cbOnRspSubMarketData;
extern CBOnRspUnSubMarketData cbOnRspUnSubMarketData;
extern CBOnRtnDepthMarketData cbOnRtnDepthMarketData;

// 请求编号
//extern int iRequestID;



// 这是已导出类的构造函数。
// 有关类定义的信息，请参阅 MdApi.h
CMdSpi::CMdSpi()
{
	return;
}


void CMdSpi::OnRspError(CThostFtdcRspInfoField *pRspInfo,int nRequestID, bool bIsLast)
{
	if(cbOnRspError != NULL)
		cbOnRspError(pRspInfo, nRequestID, bIsLast);
}

void CMdSpi::OnFrontDisconnected(int nReason)
{
	//cerr << "--->>> " << __FUNCTION__ << endl;
	//cerr << "--->>> Reason = " << nReason << endl;
	if(cbOnFrontDisconnected!=NULL)
		cbOnFrontDisconnected(nReason);
}
		
void CMdSpi::OnHeartBeatWarning(int nTimeLapse)
{
	if(cbOnHeartBeatWarning != NULL)
		cbOnHeartBeatWarning(nTimeLapse);
}

void CMdSpi::OnFrontConnected()
{
	//cerr << "--->>> " << __FUNCTION__ << endl;
	if(cbOnFrontConnected!=NULL)
		cbOnFrontConnected();
}

void CMdSpi::OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin,
		CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if(cbOnRspUserLogin!=NULL)
		cbOnRspUserLogin(pRspUserLogin,pRspInfo,nRequestID,bIsLast);
}

void CMdSpi::OnRspUserLogout(CThostFtdcUserLogoutField *pUserLogout, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	if(cbOnRspUserLogout!=NULL)
		cbOnRspUserLogout(pUserLogout, pRspInfo, nRequestID, bIsLast);
}

void CMdSpi::OnRspSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	//cerr << __FUNCTION__ << endl;
	if(cbOnRspSubMarketData!=NULL)
		cbOnRspSubMarketData(pSpecificInstrument,pRspInfo,nRequestID,bIsLast);
}

void CMdSpi::OnRspUnSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast)
{
	//cerr << __FUNCTION__ << endl;
	if(cbOnRspUnSubMarketData!=NULL)
		cbOnRspUnSubMarketData(pSpecificInstrument, pRspInfo,nRequestID,bIsLast);
}

void CMdSpi::OnRtnDepthMarketData(CThostFtdcDepthMarketDataField *pDepthMarketData)
{
	//cerr << "深度行情" << endl;
	if(cbOnRtnDepthMarketData!=NULL)
		cbOnRtnDepthMarketData(pDepthMarketData);
}
