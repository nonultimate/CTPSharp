using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 持仓日期类型
    /// </summary>
    [Serializable]
    public enum PositionDateType
    {
        /// <summary>
        /// 今日持仓
        /// </summary>
        Today = '1',

        /// <summary>
        /// 历史持仓
        /// </summary>
        History = '2'
    }
}
