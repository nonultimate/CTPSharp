using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 撤单参数类
    /// </summary>
    public class CancelOrderParameter
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        public string InstrumentID;

        /// <summary>
        /// 交易所代码
        /// </summary>
        public string ExchangeID;

        /// <summary>
        /// 报单操作引用
        /// </summary>
        public int OrderActionRef;

        /// <summary>
        /// 报单引用
        /// </summary>
        public string OrderRef;

        /// <summary>
        /// 报单编号
        /// </summary>
        public string OrderSysID;

        /// <summary>
        /// 操作标志
        /// </summary>
        public ActionFlag ActionFlag;

        /// <summary>
        /// 报单数量
        /// </summary>
        public decimal Quantity;

        /// <summary>
        /// 报单价格
        /// </summary>
        public decimal Price;

        /// <summary>
        /// 预埋撤单编号
        /// </summary>
        public string ParkedOrderActionID;

        /// <summary>
        /// 预埋单状态
        /// </summary>
        public ParkedOrderStatusType Status;

        /// <summary>
        /// 撤单参数构造函数
        /// </summary>
        public CancelOrderParameter()
        {
            ActionFlag = ActionFlag.Delete;
            Status = ParkedOrderStatusType.Deleted;
        }
    }
}
