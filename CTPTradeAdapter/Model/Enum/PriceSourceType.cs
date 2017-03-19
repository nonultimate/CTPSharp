using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 成交价来源类型
    /// </summary>
    [Serializable]
    public enum PriceSourceType
    {
        /// <summary>
        /// 前成交价
        /// </summary>
        LastPrice = '0',

        /// <summary>
        /// 买委托价
        /// </summary>
        Buy = '1',

        /// <summary>
        /// 卖委托价
        /// </summary>
        Sell = '2',
    }
}
