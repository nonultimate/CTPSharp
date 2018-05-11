// 下列 ifdef 块是创建使从 DLL 导出更简单的
// 宏的标准方法。此 DLL 中的所有文件都是用命令行上定义的 MDAPI_EXPORTS
// 符号编译的。在使用此 DLL 的
// 任何其他项目上不应定义此符号。这样，源文件中包含此文件的任何其他项目都会将
// MDAPI_API 函数视为是从 DLL 导入的，而此 DLL 则将用此宏定义的
// 符号视为是被导出的。
#ifdef MDAPI_EXPORTS
#define MDAPI_API __declspec(dllexport)
#else
#define MDAPI_API __declspec(dllimport)
#endif
#include "..\api\amd64\ThostFtdcMdApi.h"

#pragma region 回调委托申明

typedef int (WINAPI *CBOnRspError)(CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);

///心跳超时警告。当长时间未收到报文时，该方法被调用。
///@param nTimeLapse 距离上次接收报文的时间
typedef int (WINAPI *CBOnHeartBeatWarning)(int nTimeLapse);

///连接
typedef int (WINAPI *CBOnFrontConnected)(void);
///当客户端与交易后台通信连接断开时，该方法被调用。当发生这个情况后，API会自动重新连接，客户端可不做处理。
///@param nReason 错误原因
///        0x1001 网络读失败
///        0x1002 网络写失败
///        0x2001 接收心跳超时
///        0x2002 发送心跳失败
///        0x2003 收到错误报文
typedef int (WINAPI *CBOnFrontDisconnected)(int nReason);

///用户登录请求
typedef int (WINAPI *CBOnRspUserLogin)(CThostFtdcRspUserLoginField *pRspUserLogin, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);

//登录请求响应
typedef int (WINAPI *CBOnRspUserLogout)(CThostFtdcUserLogoutField *pUserLogout, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);


//订阅行情应答
typedef int (WINAPI *CBOnRspSubMarketData)(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);

//取消行情应答
typedef int (WINAPI *CBOnRspUnSubMarketData)(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);

//深度行情通知
typedef int (WINAPI *CBOnRtnDepthMarketData)(CThostFtdcDepthMarketDataField *pDepthMarketData);

#pragma endregion
// 此类是从 MdApi.dll 导出的
class /*MDAPI_API*/ CMdSpi : public CThostFtdcMdSpi
{
public:
#pragma region 回调委托实例

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

#pragma endregion

	CMdSpi(void);
	// TODO: 在此添加您的方法。
	
	///错误应答
	virtual void OnRspError(CThostFtdcRspInfoField *pRspInfo,int nRequestID, bool bIsLast);

	///当客户端与交易后台通信连接断开时，该方法被调用。当发生这个情况后，API会自动重新连接，客户端可不做处理。
	///@param nReason 错误原因
	///        0x1001 网络读失败
	///        0x1002 网络写失败
	///        0x2001 接收心跳超时
	///        0x2002 发送心跳失败
	///        0x2003 收到错误报文
	virtual void OnFrontDisconnected(int nReason);
		
	///心跳超时警告。当长时间未收到报文时，该方法被调用。
	///@param nTimeLapse 距离上次接收报文的时间
	virtual void OnHeartBeatWarning(int nTimeLapse);

	///当客户端与交易后台建立起通信连接时（还未登录前），该方法被调用。
	virtual void OnFrontConnected();
	
	///登录请求响应
	virtual void OnRspUserLogin(CThostFtdcRspUserLoginField *pRspUserLogin,	CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);

	///登出请求响应
	virtual void OnRspUserLogout(CThostFtdcUserLogoutField *pUserLogout, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);

	///订阅行情应答
	virtual void OnRspSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);

	///取消订阅行情应答
	virtual void OnRspUnSubMarketData(CThostFtdcSpecificInstrumentField *pSpecificInstrument, CThostFtdcRspInfoField *pRspInfo, int nRequestID, bool bIsLast);

	///深度行情通知
	virtual void OnRtnDepthMarketData(CThostFtdcDepthMarketDataField *pDepthMarketData);

};

