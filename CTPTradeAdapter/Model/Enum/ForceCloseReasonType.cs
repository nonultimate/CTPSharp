using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 强平原因类型
    /// </summary>
    [Serializable]
    public enum ForceCloseReasonType
    {
        /// <summary>
        /// 非强平
        /// </summary>
        NotForceClose = '0',

        /// <summary>
        /// 资金不足
        /// </summary>
        LackDeposit = '1',

        /// <summary>
        /// 客户超仓
        /// </summary>
        ClientOverPositionLimit = '2',

        /// <summary>
        /// 会员超仓
        /// </summary>
        MemberOverPositionLimit = '3',

        /// <summary>
        /// 持仓非整数倍
        /// </summary>
        NotMultiple = '4',

        /// <summary>
        /// 违规
        /// </summary>
        Violation = '5',

        /// <summary>
        /// 其它
        /// </summary>
        Other = '6',

        /// <summary>
        /// 自然人临近交割
        /// </summary>
        PersonDeliv = '7'
    }
}
