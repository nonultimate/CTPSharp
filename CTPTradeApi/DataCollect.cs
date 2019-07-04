using CTPCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CTPTradeApi
{
    /// <summary>
    /// 信息采集类
    /// </summary>
    public class DataCollect
    {
        /// <summary>
        /// DLL名称
        /// </summary>
        private const string DllName = "WinDataCollect.dll";

        /// <summary>
        /// 类库加载类
        /// </summary>
        private LibraryWrapper _wrapper;

        /// <summary>
        /// 获取系统信息委托方法
        /// </summary>
        /// <param name="systemInfo">系统信息</param>
        /// <param name="len">系统信息长度</param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int DelegateGetSystemInfo(StringBuilder systemInfo, out int len);

        DelegateGetSystemInfo getSystemInfo;

        /// <summary>
        /// 信息采集
        /// </summary>
        public DataCollect()
        {
            try
            {
                string path = Path.GetFullPath(string.Format("{0}\\{1}", LibraryWrapper.ProcessorArchitecture,
                        DllName));
                _wrapper = new LibraryWrapper(path);
                getSystemInfo = _wrapper.GetUnmanagedFunction<DelegateGetSystemInfo>("?CTP_GetSystemInfo@@YAHPADAAH@Z");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <returns></returns>
        public Tuple<string, int> GetSsystemInfo()
        {
            StringBuilder systemInfo = new StringBuilder(512);
            int len = 0;
            int ret = getSystemInfo(systemInfo, out len);
            if (ret != 0)
            {
                len = 0;
            }
            return Tuple.Create<string, int>(systemInfo.ToString(), len);
        }
    }
}
