using GalaSoft.MvvmLight;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 账户信息
    /// </summary>
    [Serializable]
    [ImplementPropertyChanged]
    public class AccountInfo : ObservableObject
    {
        /// <summary>
        /// 交易日
        /// </summary>
        public string TradingDay { get; set; }

        /// <summary>
        /// 登录成功时间
        /// </summary>
        public string LoginTime { get; set; }

        /// <summary>
        /// 经纪公司代码
        /// </summary>
        public string BrokerID { get; set; }

        /// <summary>
        /// 投资者账号
        /// </summary>
        public string InvestorID { get; set; }

        /// <summary>
        /// 上次质押金额
        /// </summary>
        public decimal PreMortgage { get; set; }

        /// <summary>
        /// 上次信用额度
        /// </summary>
        public decimal PreCredit { get; set; }

        /// <summary>
        /// 上次存款额
        /// </summary>
        public decimal PreDeposit { get; set; }

        /// <summary>
        /// 上次结算准备金
        /// </summary>
        public decimal PreBalance { get; set; }

        /// <summary>
        /// 上次占用的保证金
        /// </summary>
        public decimal PreMargin { get; set; }

        /// <summary>
        /// 利息基数
        /// </summary>
        public decimal InterestBase { get; set; }

        /// <summary>
        /// 利息收入
        /// </summary>
        public decimal Interest { get; set; }

        /// <summary>
        /// 入金金额
        /// </summary>
        public decimal Deposit { get; set; }

        /// <summary>
        /// 出金金额
        /// </summary>
        public decimal Withdraw { get; set; }

        /// <summary>
        /// 冻结的保证金
        /// </summary>
        public decimal FrozenMargin { get; set; }

        /// <summary>
        /// 冻结的资金
        /// </summary>
        public decimal FrozenCash { get; set; }

        /// <summary>
        /// 冻结的手续费
        /// </summary>
        public decimal FrozenCommission { get; set; }

        /// <summary>
        /// 当前保证金总额
        /// </summary>
        public decimal TotalMargin { get; set; }

        /// <summary>
        /// 资金差额
        /// </summary>
        public decimal CashIn { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// 平仓盈亏
        /// </summary>
        public decimal CloseProfit { get; set; }

        /// <summary>
        /// 持仓盈亏
        /// </summary>
        public decimal PositionProfit { get; set; }

        /// <summary>
        /// 期货结算准备金
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 可用资金
        /// </summary>
        public decimal Available { get; set; }

        /// <summary>
        /// 可取资金
        /// </summary>
        public decimal WithdrawQuota { get; set; }

        /// <summary>
        /// 基本准备金
        /// </summary>
        public decimal Reserve { get; set; }

        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID { get; set; }

        /// <summary>
        /// 信用额度
        /// </summary>
        public decimal Credit { get; set; }

        /// <summary>
        /// 质押金额
        /// </summary>
        public decimal Mortgage { get; set; }

        /// <summary>
        /// 交易所保证金
        /// </summary>
        public decimal ExchangeMargin { get; set; }

        /// <summary>
        /// 投资者交割保证金
        /// </summary>
        public decimal DeliveryMargin { get; set; }

        /// <summary>
        /// 交易所交割保证金
        /// </summary>
        public decimal ExchangeDeliveryMargin { get; set; }

        /// <summary>
        /// 保底期货结算准备金
        /// </summary>
        public decimal ReserveBalance { get; set; }
    }
}
