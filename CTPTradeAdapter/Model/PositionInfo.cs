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
    public class PositionInfo : ObservableObject
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        public string BrokerID { get; set; }

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
        /// 持仓多空方向
        /// </summary>
        public PositionDirectionType PositionDirection { get; set; }

        /// <summary>
        /// 投机套保标志
        /// </summary>
        public HedgeFlag HedgeFlag { get; set; }

        /// <summary>
        /// 持仓日期
        /// </summary>
        public PositionDateType PositionDate { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 昨日持仓
        /// </summary>
        public decimal PrePosition { get; set; }

        /// <summary>
        /// 今日持仓
        /// </summary>
        public decimal Position { get; set; }

        /// <summary>
        /// 多头冻结
        /// </summary>
        public decimal LongFrozen { get; set; }

        /// <summary>
        /// 空头冻结
        /// </summary>
        public decimal ShortFrozen { get; set; }

        /// <summary>
        /// 开仓冻结金额
        /// </summary>
        public decimal LongFrozenAmount { get; set; }

        /// <summary>
        /// 开仓冻结金额
        /// </summary>
        public decimal ShortFrozenAmount { get; set; }

        /// <summary>
        /// 开仓量
        /// </summary>
        public decimal OpenVolume { get; set; }

        /// <summary>
        /// 平仓量
        /// </summary>
        public decimal CloseVolume { get; set; }

        /// <summary>
        /// 开仓金额
        /// </summary>
        public decimal OpenAmount { get; set; }

        /// <summary>
        /// 平仓金额
        /// </summary>
        public decimal CloseAmount { get; set; }

        /// <summary>
        /// 持仓成本
        /// </summary>
        public decimal PositionCost { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// 持仓盈亏
        /// </summary>
        public decimal PositionProfit { get; set; }
    }
}
