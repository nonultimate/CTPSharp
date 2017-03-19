using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 操作标志
    /// </summary>
    [Serializable]
    public enum ActionFlag
    {
        /// <summary>
        /// 删除
        /// </summary>
        Delete = '0',

        /// <summary>
        /// 修改
        /// </summary>
        Modify = '3'
    }
}
