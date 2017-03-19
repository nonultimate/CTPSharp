using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 开平仓标志
    /// </summary>
    [Serializable]
    public enum OpenCloseFlag
    {
        /// <summary>
        /// 开仓
        /// </summary>
        Open = '0',

        /// <summary>
        /// 平仓
        /// </summary>
        Close = '1',

        /// <summary>
        /// 强平
        /// </summary>
        ForceClose = '2',

        /// <summary>
        /// 平今
        /// </summary>
        CloseToday = '3',

        /// <summary>
        /// 平昨
        /// </summary>
        CloseYesterday = '4',

        /// <summary>
        /// 强减
        /// </summary>
        ForceOff = '5',

        /// <summary>
        /// 本地强平
        /// </summary>
        LocalForceClose = '6'
    }
}
