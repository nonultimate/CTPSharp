using GalaSoft.MvvmLight;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 客户通知
    /// </summary>
    [Serializable]
    [ImplementPropertyChanged]
    public class NoticeInfo : ObservableObject
    {
        /// <summary>
        /// 经纪公司通知内容序列号
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 经纪公司代码
        /// </summary>
        public string BrokerID { get; set; }

        /// <summary>
        /// 消息正文
        /// </summary>
        public string Content { get; set; }
    }
}
