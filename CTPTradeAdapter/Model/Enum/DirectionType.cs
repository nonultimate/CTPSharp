using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 买卖类型
    /// </summary>
    [Serializable]
    public enum DirectionType
    {
        /// <summary>
        /// 买
        /// </summary>
        Buy = '0',

        /// <summary>
        /// 卖
        /// </summary>
        Sell = '1'
    }
}
