using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 预埋单状态
    /// </summary>
    [Serializable]
    public enum ParkedOrderStatusType
    {
        /// <summary>
        /// 未发送
        /// </summary>
        NotSend = '1',

        /// <summary>
        /// 已发送
        /// </summary>
        Send = '2',

        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = '3'
    }
}
