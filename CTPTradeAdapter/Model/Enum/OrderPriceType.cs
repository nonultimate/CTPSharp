using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 报单价格类型
    /// </summary>
    [Serializable]
    public enum OrderPriceType
    {
        /// <summary>
        /// 任意价
        /// </summary>
        AnyPrice = '1',

        /// <summary>
        /// 限价
        /// </summary>
        LimitPrice = '2',

        /// <summary>
        /// 最优价
        /// </summary>
        BestPrice = '3',

        /// <summary>
        /// 最新价
        /// </summary>
        LastPrice = '4',
    }
}
