using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 报单状态类型
    /// </summary>
    [Serializable]
    public enum OrderStatusType
    {
        /// <summary>
        /// 全部成交
        /// </summary>
        AllTraded = '0',

        /// <summary>
        /// 部分成交还在队列中
        /// </summary>
        PartTradedQueueing = '1',

        /// <summary>
        /// 部分成交不在队列中
        /// </summary>
        PartTradedNotQueueing = '2',

        /// <summary>
        /// 未成交还在队列中
        /// </summary>
        NoTradeQueueing = '3',

        /// <summary>
        /// 未成交不在队列中
        /// </summary>
        NoTradeNotQueueing = '4',

        /// <summary>
        /// 撤单
        /// </summary>
        Canceled = '5',

        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 'a',

        /// <summary>
        /// 尚未触发
        /// </summary>
        NotTouched = 'b',

        /// <summary>
        /// 已触发
        /// </summary>
        Touched = 'c',
    }
}
