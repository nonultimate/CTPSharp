using GalaSoft.MvvmLight;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 预埋单撤单信息
    /// </summary>
    [Serializable]
    [ImplementPropertyChanged]
    public class ParkedCanelOrderInfo : ObservableObject
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
        /// 预埋撤单单编号
        /// </summary>
        public string ParkedOrderActionID { get; set; }

        /// <summary>
        /// 预埋撤单状态
        /// </summary>
        public ParkedOrderStatusType Status;
    }
}
