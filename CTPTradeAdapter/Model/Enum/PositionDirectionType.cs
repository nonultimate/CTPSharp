using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 持仓多空方向类型
    /// </summary>
    [Serializable]
    public enum PositionDirectionType
    {
        /// <summary>
        /// 净
        /// </summary>
        Net = '1',

        /// <summary>
        /// 多头
        /// </summary>
        Long = '2',

        /// <summary>
        /// 空头
        /// </summary>
        Short = '3'
    }
}
