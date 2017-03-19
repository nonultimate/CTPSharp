using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 投机套保标志
    /// </summary>
    [Serializable]
    public enum HedgeFlag
    {
        /// <summary>
        /// 投机
        /// </summary>
        Speculation = '1',

        /// <summary>
        /// 套利
        /// </summary>
        Arbitrage = '2',

        /// <summary>
        /// 套保
        /// </summary>
        Hedge = '3',
    }
}
