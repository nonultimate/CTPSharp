using GalaSoft.MvvmLight;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 委托信息
    /// </summary>
    [Serializable]
    [ImplementPropertyChanged]
    public class OrderInfo : ObservableObject
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
        /// 报单引用
        /// </summary>
        public string OrderRef { get; set; }

        /// <summary>
        /// 报单编号
        /// </summary>
        public string OrderSysID { get; set; }

        /// <summary>
        /// 本地报单编号
        /// </summary>
        public string OrderLocalID { get; set; }

        /// <summary>
        /// 买卖类型
        /// </summary>
        public DirectionType Direction { get; set; }

        /// <summary>
        /// 报单价格
        /// </summary>
        public decimal OrderPrice { get; set; }

        /// <summary>
        /// 报单数量
        /// </summary>
        public decimal OrderQuantity { get; set; }

        /// <summary>
        /// 报单状态
        /// </summary>
        public OrderStatusType OrderStatus { get; set; }

        /// <summary>
        /// 状态信息
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// 报单日期
        /// </summary>
        public string OrderDate { get; set; }

        /// <summary>
        /// 报单时间
        /// </summary>
        public string OrderTime { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int SequenceNo { get; set; }
    }
}
