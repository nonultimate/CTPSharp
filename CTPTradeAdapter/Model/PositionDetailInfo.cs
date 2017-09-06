using GalaSoft.MvvmLight;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 持仓信息
    /// </summary>
    [Serializable]
    [ImplementPropertyChanged]
     public class PositionDetailInfo : ObservableObject
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        public string InstrumentID { get; set; }

        /// <summary>
        /// 经纪公司代码
        /// </summary>
        public string BrokerID { get; set; }

        /// <summary>
        /// 投资者代码
        /// </summary>
        public string InvestorID { get; set; }

        /// <summary>
        /// 投机套保标志
        /// </summary>
        public HedgeFlag HedgeFlag { get; set; }

        /// <summary>
        /// 买卖
        /// </summary>
        public DirectionType Direction { get; set; }

        /// <summary>
        /// 开仓日期
        /// </summary>
        public string OpenDate { get; set; }

        /// <summary>
        /// 成交编号
        /// </summary>
        public string TradeID { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// 开仓价
        /// </summary>
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// 交易日
        /// </summary>
        public string TradingDay { get; set; }
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID { get; set; }

        /// <summary>
        /// 成交类型
        /// </summary>
        public TradeType TradeType { get; set; }

        /// <summary>
        /// 组合合约代码
        /// </summary>
        public string CombInstrumentID { get; set; }

        /// <summary>
        /// 交易所代码
        /// </summary>
        public string ExchangeID { get; set; }

        /// <summary>
        /// 逐日盯市平仓盈亏
        /// </summary>
        public decimal CloseProfitByDate { get; set; }

        /// <summary>
        /// 逐笔对冲平仓盈亏
        /// </summary>
        public decimal CloseProfitByTrade { get; set; }

        /// <summary>
        /// 逐日盯市持仓盈亏
        /// </summary>
        public decimal PositionProfitByDate { get; set; }

        /// <summary>
        /// 逐笔对冲持仓盈亏
        /// </summary>
        public decimal PositionProfitByTrade { get; set; }

        /// <summary>
        /// 投资者保证金
        /// </summary>
        public decimal Margin { get; set; }

        /// <summary>
        /// 交易所保证金
        /// </summary>
        public decimal ExchangeMargin { get; set; }

        /// <summary>
        /// 保证金率
        /// </summary>
        public decimal MarginRateByMoney { get; set; }

        /// <summary>
        /// 保证金率(按手数)
        /// </summary>
        public decimal MarginRateByVolume { get; set; }

        /// <summary>
        /// 昨结算价
        /// </summary>
        public decimal LastSettlementPrice { get; set; }

        /// <summary>
        /// 结算价
        /// </summary>
        public decimal SettlementPrice { get; set; }

        /// <summary>
        /// 平仓量
        /// </summary>
        public decimal CloseVolume { get; set; }

        /// <summary>
        /// 平仓金额
        /// </summary>
        public decimal CloseAmount { get; set; }
    }
}
