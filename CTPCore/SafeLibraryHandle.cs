using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace CTPCore
{
    /// <summary>
    /// 安全链接库句柄
    /// </summary>
    public sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        [SecurityCritical]
        private SafeLibraryHandle() : base(true)
        {
        }

        /// <summary>
        /// 释放句柄
        /// </summary>
        /// <returns></returns>
        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            return LibraryWrapper.FreeLibrary(base.handle);
        }
    }
}
