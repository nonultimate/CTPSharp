using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 报单参数类
    /// </summary>
    public class OrderParameter
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
        /// 报单引用
        /// </summary>
        public string OrderRef;

        /// <summary>
        /// 买卖方向
        /// </summary>
        public DirectionType Direction;

        /// <summary>
        /// 报单数量
        /// </summary>
        public decimal Quantity;

        /// <summary>
        /// 报单价格
        /// </summary>
        public decimal Price;

        /// <summary>
        /// 止损价
        /// </summary>
        public decimal StopPrice;

        /// <summary>
        /// 报单价格类型
        /// </summary>
        public OrderPriceType PriceType;

        /// <summary>
        /// 开平仓标志
        /// </summary>
        public OpenCloseFlag OpenCloseFlag;

        /// <summary>
        /// 投机套保标志
        /// </summary>
        public HedgeFlag HedgeFlag;

        /// <summary>
        /// 有效期类型
        /// </summary>
        public TimeConditionType TimeCondition;

        /// <summary>
        /// GTD日期
        /// </summary>
        public string GTDDate;

        /// <summary>
        /// 最小成交量
        /// </summary>
        public decimal MinVolume;

        /// <summary>
        /// 成交量类型
        /// </summary>
        public VolumeConditionType VolumeCondition;

        /// <summary>
        /// 触发条件
        /// </summary>
        public ContingentConditionType ContingentCondition;

        /// <summary>
        /// 强平原因
        /// </summary>
        public ForceCloseReasonType ForceCloseReason;

        /// <summary>
        /// 用户强评标志
        /// </summary>
        public int UserForceClose;

        /// <summary>
        /// 自动挂起标志
        /// </summary>
        public int IsAutoSuspend;

        /// <summary>
        /// 预埋报单编号
        /// </summary>
        public string ParkedOrderID;
    }
}
