using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace CTPCore
{
    /// <summary>
    /// DLL动态加载类
    /// </summary>
    public class LibraryWrapper : IDisposable
    {
        #region 属性

        private SafeLibraryHandle _handle;

        /// <summary>
        /// 是否是64位系统
        /// </summary>
        /// <returns></returns>
        public static bool IsAmd64
        {
            get
            {
                return IntPtr.Size == 8;
            }
        }

        /// <summary>
        /// 获取CPU架构
        /// </summary>
        /// <returns></returns>
        public static string ProcessorArchitecture
        {
            get
            {
                return IsAmd64 ? "amd64" : "x86";
            }
        }

        #endregion

        /// <summary>
        /// 加载动态链接库
        /// </summary>
        /// <param name="filename">DLL文件路径</param>
        /// <returns></returns>
        [SecurityCritical, SuppressUnmanagedCodeSecurity,
            DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern SafeLibraryHandle LoadLibrary(string filename);

        /// <summary>
        /// 获取方法地址
        /// </summary>
        /// <param name="handle">句柄</param>
        /// <param name="procname">函数名称</param>
        /// <returns></returns>
        [SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(SafeLibraryHandle handle, string procname);

        /// <summary>
        /// 释放动态链接库
        /// </summary>
        /// <param name="handle">句柄</param>
        /// <returns></returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success),
            SuppressUnmanagedCodeSecurity, DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr handle);

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="filename">DLL文件路径</param>
        /// <param name="dependencies">依赖DLL</param>
        public LibraryWrapper(string filename, params string[] dependencies)
        {
            if (dependencies != null && dependencies.Length > 0)
            {
                string dirPath = Path.GetDirectoryName(filename);
                foreach (string item in dependencies)
                {
                    string path = item.Contains(Path.DirectorySeparatorChar) ? item : Path.Combine(dirPath, item);
                    var handle = LoadLibrary(path);
                    if (handle.IsInvalid)
                    {
                        throw new Exception(string.Format("Failed to load library \"{0}\" failed", path));
                    }
                }
            }
            _handle = LoadLibrary(filename);
            if (_handle.IsInvalid)
            {
                throw new Exception(string.Format("Failed to load library \"{0}\"", filename));
            }
        }

        /// <summary>
        /// 将非托管方法转换为委托
        /// </summary>
        /// <typeparam name="T">委托类型</typeparam>
        /// <param name="name">方法名称</param>
        /// <returns></returns>
        [SecurityCritical]
        public T GetUnmanagedFunction<T>(string name) where T : class
        {
            T result = null;
            if (_handle != null)
            {
                IntPtr ptr = GetProcAddress(_handle, name);
                if (ptr != IntPtr.Zero)
                {
                    result = Marshal.GetDelegateForFunctionPointer(ptr, typeof(T)) as T;
                }
            }
            if (result == null)
            {
                throw new Exception(string.Format("Failed to get unmanaged function for name: {0}", name));
            }
            return result;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        [SecurityCritical]
        public void Dispose()
        {
            if (_handle != null && !_handle.IsClosed)
            {
                _handle.Close();
            }
        }
    }
}
