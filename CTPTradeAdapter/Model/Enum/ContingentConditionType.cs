using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 触发条件类型
    /// </summary>
    [Serializable]
    public enum ContingentConditionType
    {
        /// <summary>
        /// 立即
        /// </summary>
        Immediately = '1',

        /// <summary>
        /// 止损
        /// </summary>
        Touch = '2',

        /// <summary>
        /// 止赢
        /// </summary>
        TouchProfit = '3',

        /// <summary>
        /// 预埋单
        /// </summary>
        ParkedOrder = '4',

        /// <summary>
        /// 最新价大于条件价
        /// </summary>
        LastPriceGreaterThanStopPrice = '5',

        /// <summary>
        /// 最新价大于等于条件价
        /// </summary>
        LastPriceGreaterEqualStopPrice = '6',

        /// <summary>
        /// 最新价小于条件价
        /// </summary>
        LastPriceLesserThanStopPrice = '7',

        /// <summary>
        /// 最新价小于等于条件价
        /// </summary>
        LastPriceLesserEqualStopPrice = '8',

        /// <summary>
        /// 卖一价大于条件价
        /// </summary>
        AskPriceGreaterThanStopPrice = '9',

        /// <summary>
        /// 卖一价大于等于条件价
        /// </summary>
        AskPriceGreaterEqualStopPrice = 'A',

        /// <summary>
        /// 卖一价小于条件价
        /// </summary>
        AskPriceLesserThanStopPrice = 'B',

        /// <summary>
        /// 卖一价小于等于条件价
        /// </summary>
        AskPriceLesserEqualStopPrice = 'C',

        /// <summary>
        /// 买一价大于条件价
        /// </summary>
        BidPriceGreaterThanStopPrice = 'D',

        /// <summary>
        /// 买一价大于等于条件价
        /// </summary>
        BidPriceGreaterEqualStopPrice = 'E',

        /// <summary>
        /// 买一价小于条件价
        /// </summary>
        BidPriceLesserThanStopPrice = 'F',

        /// <summary>
        /// 买一价小于等于条件价
        /// </summary>
        BidPriceLesserEqualStopPrice = 'H'
    }
}
