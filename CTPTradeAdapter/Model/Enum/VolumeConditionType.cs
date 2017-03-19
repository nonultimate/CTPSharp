using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 成交量类型
    /// </summary>
    [Serializable]
    public enum VolumeConditionType
    {
        /// <summary>
        /// 任何数量
        /// </summary>
        AV = '1',

        /// <summary>
        /// 最小数量
        /// </summary>
        MV = '2',

        /// <summary>
        /// 全部数量
        /// </summary>
        CV = '3'
    }
}
