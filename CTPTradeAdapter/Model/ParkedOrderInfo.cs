using GalaSoft.MvvmLight;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 预埋单信息
    /// </summary>
    [Serializable]
    [ImplementPropertyChanged]
    public class ParkedOrderInfo : ObservableObject
    {
        /// <summary>
        /// 投资者ID
        /// </summary>
        public string InvestorID { get; set; }

        /// <summary>
        /// 合约代码
        /// </summary>
        public string InstrumentID { get; set; }

        /// <summary>
        /// 交易所代码
        /// </summary>
        public string ExchangeID { get; set; }

        /// <summary>
        /// 报单操作引用
        /// </summary>
        public int OrderActionRef { get; set; }

        /// <summary>
        /// 报单引用
        /// </summary>
        public string OrderRef { get; set; }

        /// <summary>
        /// 报单编号
        /// </summary>
        public string OrderSysID { get; set; }

        /// <summary>
        /// 操作标志
        /// </summary>
        public ActionFlag ActionFlag { get; set; }

        /// <summary>
        /// 买卖类型
        /// </summary>
        public DirectionType Direction { get; set; }

        /// <summary>
        /// 报单价格类型
        /// </summary>
        public OrderPriceType PriceType { get; set; }

        /// <summary>
        /// 开平仓标志
        /// </summary>
        public OpenCloseFlag OpenCloseFlag { get; set; }

        /// <summary>
        /// 投机套保标志
        /// </summary>
        public HedgeFlag HedgeFlag { get; set; }

        /// <summary>
        /// 报单数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 报单价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 预埋报单编号
        /// </summary>
        public string ParkedOrderID { get; set; }

        /// <summary>
        /// 预埋撤单编号
        /// </summary>
        public string ParkedOrderActionID { get; set; }

        /// <summary>
        /// 预埋单状态
        /// </summary>
        public ParkedOrderStatusType Status { get; set; }
    }
}
