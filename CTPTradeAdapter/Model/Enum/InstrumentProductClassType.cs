using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 合约产品类型
    /// </summary>
    public enum InstrumentProductClassType
    {
        /// <summary>
        /// 期货
        /// </summary>
        Futures = '1',

        /// <summary>
        /// 期权
        /// </summary>
        Options = '2',

        /// <summary>
        /// 组合
        /// </summary>
        Combination = '3',

        /// <summary>
        /// 即期
        /// </summary>
        Spot = '4',

        /// <summary>
        /// 期转现
        /// </summary>
        EFP = '5'
    }
}
