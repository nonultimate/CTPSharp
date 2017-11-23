using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 合约信息
    /// </summary>
    public class InstrumentInfo : ObservableObject
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        public string InstrumentID { get; set; }

        /// <summary>
        /// 交易所代码
        /// </summary>
        public string ExchangeID { get; set; }

        /// <summary>
        /// 合约名称
        /// </summary>
        public string InstrumentName { get; set; }

        /// <summary>
        /// 产品代码
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public InstrumentProductClassType ProductType { get; set; }

        /// <summary>
        /// 市价单最大下单量
        /// </summary>
        public int MaxMarketOrderVolume { get; set; }

        /// <summary>
        /// 市价单最小下单量
        /// </summary>
        public int MinMarketOrderVolume { get; set; }

        /// <summary>
        /// 限价单最大下单量
        /// </summary>
        public int MaxLimitOrderVolume { get; set; }

        /// <summary>
        /// 限价单最小下单量
        /// </summary>
        public int MinLimitOrderVolume { get; set; }

        /// <summary>
        /// 合约数量乘数
        /// </summary>
        public int VolumeMultiple { get; set; }

        /// <summary>
        /// 最小变动价位
        /// </summary>
        public decimal PriceTick { get; set; }

        /// <summary>
        /// 创建日
        /// </summary>
        public string CreateDate { get; set; }

        /// <summary>
        /// 上市日
        /// </summary>
        public string OpenDate { get; set; }

        /// <summary>
        /// 到期日
        /// </summary>
        public string ExpireDate { get; set; }

        /// <summary>
        /// 开始交割日
        /// </summary>
        public string StartDelivDate { get; set; }

        /// <summary>
        /// 结束交割日
        /// </summary>
        public string EndDelivDate { get; set; }

        /// <summary>
        /// 合约生命周期状态
        /// </summary>
        public InstrumentLifePhaseType LifePhaseType { get; set; }

        /// <summary>
        /// 多头保证金率
        /// </summary>
        public decimal LongMarginRatio { get; set; }

        /// <summary>
        /// 空头保证金率
        /// </summary>
        public decimal ShortMarginRatio { get; set; }
    }
}
