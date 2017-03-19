using GalaSoft.MvvmLight;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 成交信息
    /// </summary>
    [Serializable]
    [ImplementPropertyChanged]
    public class TradeInfo : ObservableObject
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
        /// 成交编号
        /// </summary>
        public string TradeID { get; set; }

        /// <summary>
        /// 买卖类型
        /// </summary>
        public DirectionType Direction { get; set; }

        /// <summary>
        /// 开平仓标志
        /// </summary>
        public OpenCloseFlag OpenCloseFlag;

        /// <summary>
        /// 投机套保标志
        /// </summary>
        public HedgeFlag HedgeFlag;

        /// <summary>
        /// 成交价格
        /// </summary>
        public decimal TradePrice { get; set; }

        /// <summary>
        /// 成交数量
        /// </summary>
        public decimal TradeQuantity { get; set; }

        /// <summary>
        /// 成交日期
        /// </summary>
        public string TradeDate { get; set; }

        /// <summary>
        /// 成交时间
        /// </summary>
        public string TradeTime { get; set; }

        /// <summary>
        /// 成交类型
        /// </summary>
        public TradeType TradeType { get; set; }

        /// <summary>
        /// 成交价来源
        /// </summary>
        public PriceSourceType PriceSource { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int SequenceNo { get; set; }
    }
}
