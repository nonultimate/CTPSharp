using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 登录请求
    /// </summary>
    public class LoginRequest : ObservableObject
    {
        /// <summary>
        /// 交易日
        /// </summary>
        public string TradingDay { get; set; }

        /// <summary>
        /// 经纪公司代码
        /// </summary>
        public string BrokerID { get; set; }

        /// <summary>
        /// 用户代码
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户端产品信息
        /// </summary>
        public string UserProductInfo { get; set; }

        /// <summary>
        /// 接口端产品信息
        /// </summary>
        public string InterfaceProductInfo { get; set; }

        /// <summary>
        /// 协议信息
        /// </summary>
        public string ProtocolInfo { get; set; }

        /// <summary>
        /// Mac地址
        /// </summary>
        public string MacAddress { get; set; }

        /// <summary>
        /// 终端IP地址
        /// </summary>
        public string ClientIPAddress { get; set; }

        /// <summary>
        /// 终端IP端口
        /// </summary>
        public int ClientIPPort { get; set; }

        /// <summary>
        /// 登录备注
        /// </summary>
        public string LoginRemark { get; set; }

        /// <summary>
        /// 图形验证码的文字内容
        /// </summary>
        public string Captcha { get; set; }

        /// <summary>
        /// 登录验证类型
        /// </summary>
        public CaptchaType LoginType { get; set; } 
    }
}
