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
        private const string DllName = "MdApi.dll";

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
        private string _flowPath = "";

        /// <summary>
        /// 交易类句柄
        /// </summary>
        private IntPtr _handle = IntPtr.Zero;
        private IntPtr _spi = IntPtr.Zero;


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

        //请求委托
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr DelegateCreateSpi();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr DelegateGetString();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr DelegatetTradingDay(IntPtr ptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr DelegateConnect(string pFrontAddr, string flowPath, IntPtr ptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DelegateDisconnect(IntPtr ptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DelegateUserLogin(IntPtr ptr, int requestID, string brokerID, string investorID, string password);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DelegateUserLogout(IntPtr ptr, int requestID, string brokerID, string investorID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DelegateSubscribeMarketData(IntPtr ptr, string[] instrumentsID, int nCount);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void DelegateUnsubscribeMarketData(IntPtr ptr, string[] ppInstrumentID, int nCount);

        // 回调委托
        delegate void DelegateRegOnRspError(IntPtr ptr, RspError cb);
        delegate void DelegateRegOnHeartBeatWarning(IntPtr ptr, HeartBeatWarning cb);
        delegate void DelegateRegOnFrontConnected(IntPtr ptr, FrontConnected cb);
        delegate void DelegateRegOnFrontDisconnected(IntPtr ptr, FrontDisconnected cb);
        delegate void DelegateRegOnRspUserLogin(IntPtr ptr, RspUserLogin cb);
        delegate void DelegateRegOnRspUserLogout(IntPtr ptr, RspUserLogout cb);
        delegate void DelegateRegOnRspSubMarketData(IntPtr ptr, RspSubMarketData cb);
        delegate void DelegateRegOnRspUnSubMarketData(IntPtr ptr, RspUnSubMarketData cb);
        delegate void DelegateRegOnRtnDepthMarketData(IntPtr ptr, RtnDepthMarketData cb);

        #endregion

        #region 委托实例

        //请求实例
        DelegateCreateSpi createSpi;
        DelegateGetString getApiVersion;
        DelegatetTradingDay getTradingDay;
        DelegateConnect connect;
        DelegateDisconnect disconnect;
        DelegateUserLogin userLogin;
        DelegateUserLogout userLogout;
        DelegateSubscribeMarketData subscribeMarketData;
        DelegateUnsubscribeMarketData unsubscribeMarketData;
        
        //回调实例
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

        #region 构造方法

        /// <summary>
        /// MdApi.dll, thostmduserapi.dll 放在主程序的执行文件夹中
        /// </summary>
        public MarketApi()
        {
            LoadAssembly();
        }

        /// <summary>
        /// MdApi.dll, thostmduserapi.dll 放在主程序的执行文件夹中
        /// </summary>
        /// <param name="brokerID">经纪公司代码</param>
        /// <param name="frontAddr">前置地址，tcp://IP:Port</param>
        /// <param name="flowPath">存储订阅信息文件的目录，默认为当前目录</param>
        public MarketApi(string brokerID, string frontAddr, string flowPath = "")
        {
            this.FrontAddr = frontAddr;
            this.BrokerID = brokerID;
            this._flowPath = flowPath;

            LoadAssembly();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载程序集
        /// </summary>
        private void LoadAssembly()
        {
            try
            {
                string path = Path.GetFullPath(string.Format("{0}\\{1}", LibraryWrapper.ProcessorArchitecture,
                    DllName));
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

                createSpi = GetDelegate<DelegateCreateSpi>("CreateSpi");
                getApiVersion = GetDelegate<DelegateGetString>("GetApiVersion");
                getTradingDay = GetDelegate<DelegatetTradingDay>("GetTradingDay");
                connect = GetDelegate<DelegateConnect>("Connect");
                disconnect = GetDelegate<DelegateDisconnect>("DisConnect");
                userLogin = GetDelegate<DelegateUserLogin>("ReqUserLogin");
                userLogout = GetDelegate<DelegateUserLogout>("ReqUserLogout");
                subscribeMarketData = GetDelegate<DelegateSubscribeMarketData>("SubMarketData");
                unsubscribeMarketData = GetDelegate<DelegateUnsubscribeMarketData>("UnSubscribeMarketData");
                regOnRspError = GetDelegate<DelegateRegOnRspError>("RegOnRspError");
                regOnHeartBeatWarning = GetDelegate<DelegateRegOnHeartBeatWarning>("RegOnHeartBeatWarning");
                regOnFrontConnected = GetDelegate<DelegateRegOnFrontConnected>("RegOnFrontConnected");
                regOnFrontDisconnected = GetDelegate<DelegateRegOnFrontDisconnected>("RegOnFrontDisconnected");
                regOnRspUserLogin = GetDelegate<DelegateRegOnRspUserLogin>("RegOnRspUserLogin");
                regOnRspUserLogout = GetDelegate<DelegateRegOnRspUserLogout>("RegOnRspUserLogout");
                regOnRspSubMarketData = GetDelegate<DelegateRegOnRspSubMarketData>("RegOnRspSubMarketData");
                regOnRspUnSubMarketData = GetDelegate<DelegateRegOnRspUnSubMarketData>("RegOnRspUnSubMarketData");
                regOnRtnDepthMarketData = GetDelegate<DelegateRegOnRtnDepthMarketData>("RegOnRtnDepthMarketData");

                #endregion

                _spi = createSpi();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 从列表中查找入口并返回非托管方法委托(注意接口前缀不能相同，否则可能找到错误的入口)
        /// </summary>
        /// <param name="name">方法委托</param>
        /// <returns></returns>
        private T GetDelegate<T>(string name) where T : class
        {
            var entries = _entryList.Where(p => p.StartsWith(string.Format("?{0}@", name)));
            int count = entries.Count();
            if (count > 1)
            {
                throw new Exception(string.Format("More than one entries found with the name: \"{0}\"", name));
            }
            if (count == 0)
            {
                throw new Exception(string.Format("Failed to get entry point for \"{0}\"", name));
            }
            string entryName = entries.FirstOrDefault();
            return _wrapper.GetUnmanagedFunction<T>(entryName);
        }

        #endregion

        #region 接口方法
        /// <summary>
        /// 获取接口版本
        /// </summary>
        /// <returns></returns>
        public string GetApiVersion()
        {
            IntPtr ptr = getApiVersion();

            return Marshal.PtrToStringAnsi(ptr);
        }

        /// <summary>
		/// 获取当前交易日（登录成功后调用）
		/// </summary>
		/// <returns></returns>
		public string GetTradingDay()
        {
            IntPtr ptr = getTradingDay(_handle);

            return Marshal.PtrToStringAnsi(ptr);
        }

        /// <summary>
		/// 连接
		/// </summary>
        public void Connect()
        {
          _handle = connect(this.FrontAddr, this._flowPath, _spi);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            if (_handle != IntPtr.Zero)
            {
                disconnect(_handle);
                _handle = IntPtr.Zero;
            }
        }

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
            userLogin(_handle, requestID, this.BrokerID, this.InvestorID, this._password);
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="requestID">请求编号</param>
        public void UserLogout(int requestID)
        {
            userLogout(_handle, requestID, this.BrokerID, this.InvestorID);
        }

        /// <summary>
		/// 订阅行情
		/// </summary>
		/// <param name="instruments">合约代码:可填多个,订阅所有填null</param>
		public void SubscribeMarketData(params string[] instruments)
        {
            subscribeMarketData(_handle, instruments, instruments == null ? 0 : instruments.Length);
        }

        /// <summary>
        /// 退订行情
        /// </summary>
        /// <param name="instruments">合约代码:可填多个,退订所有填null</param>
        public void UnsubscribeMarketData(params string[] instruments)
        {
            unsubscribeMarketData(_handle, instruments, instruments == null ? 0 : instruments.Length);
        }

        #endregion

        #region 错误响应

        RspError rspError;

        /// <summary>
        /// 错误应答委托
        /// </summary>
        public delegate void RspError(ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);

        /// <summary>
        /// 错误应答
        /// </summary>
        public event RspError OnRspError
        {
            add { rspError += value; regOnRspError(_spi, rspError); }
            remove { rspError -= value; regOnRspError(_spi, rspError); }
        }

        #endregion

        #region 心跳响应

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
            add { heartBeatWarning += value; regOnHeartBeatWarning(_spi, heartBeatWarning); }
            remove { heartBeatWarning -= value; regOnHeartBeatWarning(_spi, heartBeatWarning); }
        }

        #endregion

        #region 连接响应

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
            add { frontConnected += value; regOnFrontConnected(_spi, frontConnected); }
            remove { frontConnected -= value; regOnFrontConnected(_spi, frontConnected); }
        }

        #endregion

        #region 断开应答

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
            add { frontDisconnected += value; regOnFrontDisconnected(_spi, frontDisconnected); }
            remove { frontDisconnected -= value; regOnFrontDisconnected(_spi, frontDisconnected); }
        }

        #endregion

        #region 登入请求应答

        RspUserLogin rspUserLogin;

        /// <summary>
        /// 登入请求应答委托
        /// </summary>
        public delegate void RspUserLogin(ref CThostFtdcRspUserLoginField pRspUserLogin,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        
        /// <summary>
        /// 登入请求应答
        /// </summary>
        public event RspUserLogin OnRspUserLogin
        {
            add { rspUserLogin += value; regOnRspUserLogin(_spi, rspUserLogin); }
            remove { rspUserLogin -= value; regOnRspUserLogin(_spi, rspUserLogin); }
        }

        #endregion

        #region 登出请求应答

        RspUserLogout rspUserLogout;

        /// <summary>
        /// 登出请求应答委托
        /// </summary>
        /// <param name="pUserLogout">用户登出请求</param>
        /// <param name="pRspInfo">响应信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        public delegate void RspUserLogout(ref CThostFtdcUserLogoutField pUserLogout,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        
        /// <summary>
        /// 登出请求应答
        /// </summary>
        public event RspUserLogout OnRspUserLogout
        {
            add { rspUserLogout += value; regOnRspUserLogout(_spi, rspUserLogout); }
            remove { rspUserLogout -= value; regOnRspUserLogout(_spi, rspUserLogout); }
        }

        #endregion

        #region 订阅行情应答

        RspSubMarketData rspSubMarketData;

        /// <summary>
        /// 订阅行情应答委托
        /// </summary>
        /// <param name="pSpecificInstrument">指定合约</param>
        /// <param name="pRspInfo">应答信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        public delegate void RspSubMarketData(ref CThostFtdcSpecificInstrumentField pSpecificInstrument,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        
        /// <summary>
        /// 订阅行情应答
        /// </summary>
        public event RspSubMarketData OnRspSubMarketData
        {
            add { rspSubMarketData += value; regOnRspSubMarketData(_spi, rspSubMarketData); }
            remove { rspSubMarketData -= value; regOnRspSubMarketData(_spi, rspSubMarketData); }
        }

        #endregion

        #region 退订请求应答

        RspUnSubMarketData rspUnSubMarketData;

        /// <summary>
        /// 退订请求应答委托
        /// </summary>
        /// <param name="pSpecificInstrument">指定合约</param>
        /// <param name="pRspInfo">响应信息</param>
        /// <param name="nRequestID">请求编号</param>
        /// <param name="bIsLast">是否为最后一条数据</param>
        public delegate void RspUnSubMarketData(ref CThostFtdcSpecificInstrumentField pSpecificInstrument,
            ref CThostFtdcRspInfoField pRspInfo, int nRequestID, byte bIsLast);
        
        /// <summary>
        /// 退订请求应答
        /// </summary>
        public event RspUnSubMarketData OnRspUnSubMarketData
        {
            add { rspUnSubMarketData += value; regOnRspUnSubMarketData(_spi, rspUnSubMarketData); }
            remove { rspUnSubMarketData -= value; regOnRspUnSubMarketData(_spi, rspUnSubMarketData); }
        }

        #endregion

        #region 深度行情通知

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
            add { rtnDepthMarketData += value; regOnRtnDepthMarketData(_spi, rtnDepthMarketData); }
            remove { rtnDepthMarketData -= value; regOnRtnDepthMarketData(_spi, rtnDepthMarketData); }
        }

        #endregion
    }
}
