using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 合约生命周期状态
    /// </summary>
    public enum InstrumentLifePhaseType
    {
        /// <summary>
        /// 未上市
        /// </summary>
        NotStart = '0',

        /// <summary>
        /// 上市
        /// </summary>
        Started = '1',

        /// <summary>
        /// 停牌
        /// </summary>
        Pause = '2',

        /// <summary>
        /// 到期
        /// </summary>
        Expired = '3'
    }
}
