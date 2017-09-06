using GalaSoft.MvvmLight;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 投资者信息
    /// </summary>
    [Serializable]
    [ImplementPropertyChanged]
    public class InvestorInfo : ObservableObject
    {
        /// <summary>
        /// 投资者代码
        /// </summary>
        public string InvestorID { get; set; }

        /// <summary>
        /// 经纪公司代码
        /// </summary>
        public string BrokerID { get; set; }

        /// <summary>
        /// 投资者分组代码
        /// </summary>
        public string InvestorGroupID { get; set; }

        /// <summary>
        /// 投资者名称
        /// </summary>
        public string InvestorName { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public IdCardType IdentifiedCardType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdentifiedCardNo { get; set; }

        /// <summary>
        /// 是否活跃
        /// </summary>
        public int IsActive { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 通讯地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 开户日期
        /// </summary>
        public string OpenDate { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 手续费率模板代码
        /// </summary>
        public string CommModelID { get; set; }

        /// <summary>
        /// 保证金率模板代码
        /// </summary>
        public string MarginModelID { get; set; }
    }
}
