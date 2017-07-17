using GalaSoft.MvvmLight;

namespace CTPMarketAdapter.Model
{
    /// <summary>
    /// CTP行情数据
    /// </summary>
    public class CTPMarketData : ObservableObject
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        public string InstrmentID { get; set; }

        /// <summary>
        /// 交易所代码
        /// </summary>
        public string ExchangeID { get; set; }

        /// <summary>
        /// 最新价
        /// </summary>
        public decimal LastPrice { get; set; }

        /// <summary>
        /// 上次结算价
        /// </summary>
        public decimal PreSettlementPrice { get; set; }

        /// <summary>
        /// 昨收价
        /// </summary>
        public decimal PreClosePrice { get; set; }

        /// <summary>
        /// 昨持仓量
        /// </summary>
        public decimal PreOpenInterest { get; set; }

        /// <summary>
        /// 今开盘价
        /// </summary>
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// 最高价
        /// </summary>
        public decimal HighestPrice { get; set; }

        /// <summary>
        /// 最低价
        /// </summary>
        public decimal LowestPrice { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// 成交金额
        /// </summary>
        public decimal Turnover { get; set; }

        /// <summary>
        /// 持仓量
        /// </summary>
        public decimal OpenInterest { get; set; }

        /// <summary>
        /// 今收盘价
        /// </summary>
        public decimal ClosePrice { get; set; }

        /// <summary>
        /// 本次结算价
        /// </summary>
        public decimal SettlementPrice { get; set; }

        /// <summary>
        /// 涨停价
        /// </summary>
        public decimal UpperLimitPrice { get; set; }

        /// <summary>
        /// 跌停价
        /// </summary>
        public decimal LowerLimitPrice { get; set; }

        /// <summary>
        /// 买一价
        /// </summary>
        public decimal BidPrice1 { get; set; }

        /// <summary>
        /// 买一量
        /// </summary>
        public decimal BidVolume1 { get; set; }

        /// <summary>
        /// 买二价
        /// </summary>
        public decimal BidPrice2 { get; set; }

        /// <summary>
        /// 买二量
        /// </summary>
        public decimal BidVolume2 { get; set; }

        /// <summary>
        /// 买三价
        /// </summary>
        public decimal BidPrice3 { get; set; }

        /// <summary>
        /// 买三量
        /// </summary>
        public decimal BidVolume3 { get; set; }

        /// <summary>
        /// 买四价
        /// </summary>
        public decimal BidPrice4 { get; set; }

        /// <summary>
        /// 买四量
        /// </summary>
        public decimal BidVolume4 { get; set; }

        /// <summary>
        /// 买五价
        /// </summary>
        public decimal BidPrice5 { get; set; }

        /// <summary>
        /// 买五量
        /// </summary>
        public decimal BidVolume5 { get; set; }

        /// <summary>
        /// 卖一价
        /// </summary>
        public decimal AskPrice1 { get; set; }

        /// <summary>
        /// 卖一量
        /// </summary>
        public decimal AskVolume1 { get; set; }

        /// <summary>
        /// 卖二价
        /// </summary>
        public decimal AskPrice2 { get; set; }

        /// <summary>
        /// 卖二量
        /// </summary>
        public decimal AskVolume2 { get; set; }

        /// <summary>
        /// 卖三价
        /// </summary>
        public decimal AskPrice3 { get; set; }

        /// <summary>
        /// 卖三量
        /// </summary>
        public decimal AskVolume3 { get; set; }

        /// <summary>
        /// 卖四价
        /// </summary>
        public decimal AskPrice4 { get; set; }

        /// <summary>
        /// 卖四量
        /// </summary>
        public decimal AskVolume4 { get; set; }

        /// <summary>
        /// 卖五价
        /// </summary>
        public decimal AskPrice5 { get; set; }

        /// <summary>
        /// 卖五量
        /// </summary>
        public decimal AskVolume5 { get; set; }

        /// <summary>
        /// 当日均价
        /// </summary>
        public decimal AveragePrice { get; set; }

        /// <summary>
        /// 涨跌额
        /// </summary>
        public decimal AdvanceDeclineAmount { get; set; }

        /// <summary>
        /// 涨跌幅
        /// </summary>
        public decimal AdvanceDecline { get; set; }
    }
}
