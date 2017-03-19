using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 成交类型
    /// </summary>
    [Serializable]
    public enum TradeType
    {
        /// <summary>
        /// 普通成交
        /// </summary>
        Common = '0',

        /// <summary>
        /// 期权执行
        /// </summary>
        OptionsExecution = '1',

        /// <summary>
        /// OTC成交
        /// </summary>
        OTC = '2',

        /// <summary>
        /// 期转现衍生成交
        /// </summary>
        EFPDerived = '3',

        /// <summary>
        /// 组合衍生成交
        /// </summary>
        CombinationDerived = '4',
    }
}
