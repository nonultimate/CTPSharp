using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 有效期类型
    /// </summary>
    [Serializable]
    public enum TimeConditionType
    {
        /// <summary>
        /// 立即完成，否则撤销
        /// </summary>
        IOC = '1',

        /// <summary>
        /// 本节有效
        /// </summary>
        GFS = '2',

        /// <summary>
        /// 当日有效
        /// </summary>
        GFD = '3',

        /// <summary>
        /// 指定日期前有效
        /// </summary>
        GTD = '4',

        /// <summary>
        /// 撤销前有效
        /// </summary>
        GTC = '5',

        /// <summary>
        /// 集合竞价有效
        /// </summary>
        GFA = '6'
    }
}
