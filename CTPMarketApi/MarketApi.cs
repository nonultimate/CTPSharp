using CTPCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CTPMarketApi
{
    /// <summary>
    /// CTP行情接口类
    /// </summary>
    public class MarketApi
    {
        #region 属性

        /// <summary>
        /// DLL名称
        /// </summary>
        private const string dllName = "MdApi.dll";

        /// <summary>
        /// 前置地址
        /// </summary>
        public string FrontAddr { get; set; }

        /// <summary>
        /// 经纪公司代码
        /// </summary>
        public string BrokerID { get; set; }

        /// <summary>
        /// 用户代码
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
        /// 方法入库地址列表
        /// </summary>
        private List<string> _entryList = new List<string>();

        #endregion

        #region 委托定义

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate string DelegateGetTradingDay();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DelegateConnect(string pFrontAddr, string flowPath);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DelegateDisconnect();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DelegateUserLogin(int requestID, string brokerID, string investorID, string password);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DelegateUserLogout(int requestID, string brokerID, string investorID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DelegateSubscribeMarketData(string[] instrumentsID, int nCount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DelegateUnsubscribeMarketData(string[] ppInstrumentID, int nCount);

        // 回调委托
        delegate void DelegateRegOnRspError(RspError cb);
        delegate void DelegateRegOnHeartBeatWarning(HeartBeatWarning cb);
        delegate void DelegateRegOnFrontConnected(FrontConnected cb);
        delegate void DelegateRegOnFrontDisconnected(FrontDisconnected cb);
        delegate void DelegateRegOnRspUserLogin(RspUserLogin cb);
        delegate void DelegateRegOnRspUserLogout(RspUserLogout cb);
        delegate void DelegateRegOnRspSubMarketData(RspSubMarketData cb);
        delegate void DelegateRegOnRspUnSubMarketData(RspUnSubMarketData cb);
        delegate void DelegateRegOnRtnDepthMarketData(RtnDepthMarketData cb);

        DelegateGetTradingDay getTradingDay;
        DelegateConnect connect;
        DelegateDisconnect disconnect;
        DelegateUserLogin userLogin;
        DelegateUserLogout userLogout;
        DelegateSubscribeMarketData subscribeMarketData;
        DelegateUnsubscribeMarketData unsubscribeMarketData;
        
        DelegateRegOnRspError regOnRspError;
        DelegateRegOnHeartBeatWarning regOnHeartBeatWarning;
        DelegateRegOnFrontConnected regOnFrontConnected;
        DelegateRegOnFrontDisconnected regOnFrontDisconnected;
        DelegateRegOnRspUserLogin regOnRspUserLogin;
        DelegateRegOnRspUserLogout regOnRspUserLogout;
        DelegateRegOnRspSubMarketData regOnRspSubMarketData;
        DelegateRegOnRspUnSubMarketData regOnRspUnSubMarketData;
        DelegateRegOnRtnDepthMarketData regOnRtnDepthMarketData;

        #endregion

        /// <summary>
        /// MdApi.dll, thostmduserapi.dll 放在主程序的执行文件夹中
        /// </summary>
        /// <param name="brokerID">经纪公司代码:2030-CTP模拟</param>
        /// <param name="frontAddr">前置地址，tcp://IP:Port</param>
        /// <param name="flowPath">存储订阅信息文件的目录，默认为当前目录</param>
        public MarketApi(string brokerID = "", string frontAddr = "", string flowPath = "")
        {
            this.FrontAddr = frontAddr;
            this.BrokerID = brokerID;
            this._flowPath = flowPath;

            try
            {
                string path = Path.GetFullPath(string.Format("{0}\\{1}", LibraryWrapper.ProcessorArchitecture,
                    dllName));
                _wrapper = new LibraryWrapper(path, "thostmduserapi.dll");

                #region 读取方法入口列表

                string resourceName = string.Format("CTPMarketApi.Entry{0}.txt", LibraryWrapper.IsAmd64 ? "64" : "32");
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

                getTradingDay = GetDelegate<DelegateGetTradingDay>("?GetTradingDay");
                connect = GetDelegate<DelegateConnect>("?Connect");
                disconnect = GetDelegate<DelegateDisconnect>("?DisConnect");
                userLogin = GetDelegate<DelegateUserLogin>("?ReqUserLogin");
                userLogout = GetDelegate<DelegateUserLogout>("?ReqUserLogout");
                subscribeMarketData = GetDelegate<DelegateSubscribeMarketData>("?SubMarketData");
                unsubscribeMarketData = GetDelegate<DelegateUnsubscribeMarketData>("?UnSubscribeMarketData");
                regOnRspError = GetDelegate<DelegateRegOnRspError>("?RegOnRspError");
                regOnHeartBeatWarning = GetDelegate<DelegateRegOnHeartBeatWarning>("?RegOnHeartBeatWarning");
                regOnFrontConnected = GetDelegate<DelegateRegOnFrontConnected>("?RegOnFrontConnected");
                regOnFrontDisconnected = GetDelegate<DelegateRegOnFrontDisconnected>("?RegOnFrontDisconnected");
                regOnRspUserLogin = GetDelegate<DelegateRegOnRspUserLogin>("?RegOnRspUserLogin");
                regOnRspUserLogout = GetDelegate<DelegateRegOnRspUserLogout>("?RegOnRspUserLogout");
                regOnRspSubMarketData = GetDelegate<DelegateRegOnRspSubMarketData>("?RegOnRspSubMarketData");
                regOnRspUnSubMarketData = GetDelegate<DelegateRegOnRspUnSubMarketData>("?RegOnRspUnSubMarketData");
                regOnRtnDepthMarketData = GetDelegate<DelegateRegOnRtnDepthMarketData>("?RegOnRtnDepthMarketData");

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
		/// 获取当前交易日:只有登录成功后,才能得到正确的交易日
		/// </summary>
		/// <returns></returns>
		public string GetTradingDay()
        {
            return getTradingDay();
        }

        //[DllImport(dllName, EntryPoint = "?GetTradingDay@@YAPBDXZ", CallingConvention = CallingConvention.Cdecl)]
        //static extern string getTradingDay();

        /// <summary>
		/// 连接
		/// </summary>
        public void Connect()
        {
            connect(this.FrontAddr, this._flowPath);
        }

        //[DllImport(dllName, EntryPoint = "?Connect@@YAXPAD0@Z", CallingConvention = CallingConvention.Cdecl)]
        //static extern void connect(string pFrontAddr, string flowPath);

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            disconnect();
        }

        //[DllImport(dllName, EntryPoint = "?DisConnect@@YAXXZ", CallingConvention = CallingConvention.Cdecl)]
        //static extern void disconnect();

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="requestID">请求编号</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
		public void UserLogin(int requestID, string investorID, string password)
        {
            this.InvestorID = investorID;
            this._password = password;
            userLogin(requestID, this.BrokerID, this.InvestorID, this._password);
        }

        //[DllImport(dllName, EntryPoint = "?ReqUserLogin@@YAXHQAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        //static extern void userLogin(int requestID, string brokerID, string investorID, string password);

        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="requestID">请求编号</param>
        public void UserLogout(int requestID)
        {
            userLogout(requestID, this.BrokerID, this.InvestorID);
        }

        //[DllImport(dllName, EntryPoint = "?ReqUserLogout@@YAXHQAD0@Z", CallingConvention = CallingConvention.Cdecl)]
        //static extern void userLogout(int requestID, string brokerID, string investorID);

        /// <summary>
		/// 订阅行情
		/// </summary>
		/// <param name="instruments">合约代码:可填多个,订阅所有填null</param>
		public void SubscribeMarketData(params string[] instruments)
        {
            subscribeMarketData(instruments, instruments == null ? 0 : instruments.Length);
        }

        //[DllImport(dllName, EntryPoint = "?SubMarketData@@YAXQAPADH@Z", CallingConvention = CallingConvention.Cdecl)]
        //static extern void subscribeMarketData(string[] instrumentsID, int nCount);

        /// <summary>
        /// 退订行情
        /// </summary>
        /// <param name="instruments">合约代码:可填多个,退订所有填null</param>
        public void UnsubscribeMarketData(params string[] instruments)
        {
            unsubscribeMarketData(instruments, instruments == null ? 0 : instruments.Length);
        }

        //[DllImport(dllName, EntryPoint = "?UnSubscribeMarketData@@YAXQAPADH@Z", CallingConvention = CallingConvention.Cdecl)]
        //static extern void unsubscribeMarketData(string[] ppInstrumentID, int nCount);

        //回调函数

        #region 错误响应

        //[DllImport(dllName, EntryPoint = "?RegOnRspError@@YGXP6GHPAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        //static extern void regOnRspError(RspError cb);

        RspError rspError;

        /// <summary>
        /// 错误应答委托
        /// </summary>
        public delegate void RspError(ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast);

        /// <summary>
        /// 错误应答
        /// </summary>
        public event RspError OnRspError
        {
            add { rspError += value; regOnRspError(rspError); }
            remove { rspError -= value; regOnRspError(rspError); }
        }

        #endregion

        #region 心跳响应

        //[DllImport(dllName, EntryPoint = "?RegOnHeartBeatWarning@@YGXP6GHH@Z@Z", CallingConvention = CallingConvention.StdCall)]
        //static extern void regOnHeartBeatWarning(HeartBeatWarning cb);

        HeartBeatWarning heartBeatWarning;

        /// <summary>
        /// 心跳响应委托
        /// </summary>
        public delegate void HeartBeatWarning(int nTimeLapse);

        /// <summary>
        /// 心跳响应
        /// </summary>
        public event HeartBeatWarning OnHeartBeatWarning
        {
            add { heartBeatWarning += value; regOnHeartBeatWarning(heartBeatWarning); }
            remove { heartBeatWarning -= value; regOnHeartBeatWarning(heartBeatWarning); }
        }

        #endregion

        #region 连接响应

        //[DllImport(dllName, EntryPoint = "?RegOnFrontConnected@@YGXP6GHXZ@Z", CallingConvention = CallingConvention.StdCall)]
        //static extern void regOnFrontConnected(FrontConnected cb);

        FrontConnected frontConnected;

        /// <summary>
        /// 连接响应委托
        /// </summary>
        public delegate void FrontConnected();

        /// <summary>
        /// 连接响应
        /// </summary>
        public event FrontConnected OnFrontConnected
        {
            add { frontConnected += value; regOnFrontConnected(frontConnected); }
            remove { frontConnected -= value; regOnFrontConnected(frontConnected); }
        }

        #endregion

        #region 断开应答

        //[DllImport(dllName, EntryPoint = "?RegOnFrontDisconnected@@YGXP6GHH@Z@Z", CallingConvention = CallingConvention.StdCall)]
        //static extern void regOnFrontDisconnected(FrontDisconnected cb);

        FrontDisconnected frontDisconnected;

        /// <summary>
        /// 断开应答委托
        /// </summary>
        public delegate void FrontDisconnected(int nReason);

        /// <summary>
        /// 断开应答
        /// </summary>
        public event FrontDisconnected OnFrontDisconnected
        {
            add { frontDisconnected += value; regOnFrontDisconnected(frontDisconnected); }
            remove { frontDisconnected -= value; regOnFrontDisconnected(frontDisconnected); }
        }

        #endregion

        #region 登入请求应答

        //[DllImport(dllName, EntryPoint = "?RegOnRspUserLogin@@YGXP6GHPAUCThostFtdcRspUserLoginField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        //static extern void regOnRspUserLogin(RspUserLogin cb);

        RspUserLogin rspUserLogin;

        /// <summary>
        /// 登入请求应答委托
        /// </summary>
        public delegate void RspUserLogin(ref CThostFtdcRspUserLoginField pRspUserLogin,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast);
        
        /// <summary>
        /// 登入请求应答
        /// </summary>
        public event RspUserLogin OnRspUserLogin
        {
            add { rspUserLogin += value; regOnRspUserLogin(rspUserLogin); }
            remove { rspUserLogin -= value; regOnRspUserLogin(rspUserLogin); }
        }

        #endregion

        #region 登出请求应答

        //[DllImport(dllName, EntryPoint = "?RegOnRspUserLogout@@YGXP6GHPAUCThostFtdcUserLogoutField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        //static extern void regOnRspUserLogout(RspUserLogout cb);

        RspUserLogout rspUserLogout;

        /// <summary>
        /// 登出请求应答委托
        /// </summary>
        /// <param name="pUserLogout">用户登出请求</param>
        /// <param name="pRspInfo">响应信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        public delegate void RspUserLogout(ref CThostFtdcUserLogoutField pUserLogout,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast);
        
        /// <summary>
        /// 登出请求应答
        /// </summary>
        public event RspUserLogout OnRspUserLogout
        {
            add { rspUserLogout += value; regOnRspUserLogout(rspUserLogout); }
            remove { rspUserLogout -= value; regOnRspUserLogout(rspUserLogout); }
        }

        #endregion

        #region 订阅行情应答

        //[DllImport(dllName, EntryPoint = "?RegOnRspSubMarketData@@YGXP6GHPAUCThostFtdcSpecificInstrumentField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        //static extern void regOnRspSubMarketData(RspSubMarketData cb);

        RspSubMarketData rspSubMarketData;

        /// <summary>
        /// 订阅行情应答委托
        /// </summary>
        /// <param name="pSpecificInstrument">指定合约</param>
        /// <param name="pRspInfo">应答信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        public delegate void RspSubMarketData(ref CThostFtdcSpecificInstrumentField pSpecificInstrument,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast);
        
        /// <summary>
        /// 订阅行情应答
        /// </summary>
        public event RspSubMarketData OnRspSubMarketData
        {
            add { rspSubMarketData += value; regOnRspSubMarketData(rspSubMarketData); }
            remove { rspSubMarketData -= value; regOnRspSubMarketData(rspSubMarketData); }
        }

        #endregion

        #region 退订请求应答

        //[DllImport(dllName, EntryPoint = "?RegOnRspUnSubMarketData@@YGXP6GHPAUCThostFtdcSpecificInstrumentField@@PAUCThostFtdcRspInfoField@@H_N@Z@Z", CallingConvention = CallingConvention.StdCall)]
        //static extern void regOnRspUnSubMarketData(RspUnSubMarketData cb);

        RspUnSubMarketData rspUnSubMarketData;

        /// <summary>
        /// 退订请求应答委托
        /// </summary>
        /// <param name="pSpecificInstrument">指定合约</param>
        /// <param name="pRspInfo">响应信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        public delegate void RspUnSubMarketData(ref CThostFtdcSpecificInstrumentField pSpecificInstrument,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast);
        
        /// <summary>
        /// 退订请求应答
        /// </summary>
        public event RspUnSubMarketData OnRspUnSubMarketData
        {
            add { rspUnSubMarketData += value; regOnRspUnSubMarketData(rspUnSubMarketData); }
            remove { rspUnSubMarketData -= value; regOnRspUnSubMarketData(rspUnSubMarketData); }
        }

        #endregion

        #region 深度行情通知

        //[DllImport(dllName, EntryPoint = "?RegOnRtnDepthMarketData@@YGXP6GHPAUCThostFtdcDepthMarketDataField@@@Z@Z", CallingConvention = CallingConvention.StdCall)]
        //static extern void regOnRtnDepthMarketData(RtnDepthMarketData cb);

        RtnDepthMarketData rtnDepthMarketData;

        /// <summary>
        /// 深度行情通知委托
        /// </summary>
        /// <param name="pDepthMarketData">深度行情</param>
        public delegate void RtnDepthMarketData(ref CThostFtdcDepthMarketDataField pDepthMarketData);

        /// <summary>
        /// 深度行情通知
        /// </summary>
        public event RtnDepthMarketData OnRtnDepthMarketData
        {
            add { rtnDepthMarketData += value; regOnRtnDepthMarketData(rtnDepthMarketData); }
            remove { rtnDepthMarketData -= value; regOnRtnDepthMarketData(rtnDepthMarketData); }
        }

        #endregion
    }
}
