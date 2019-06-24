using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPTradeAdapter.Model
{
    /// <summary>
    /// 验证码类型
    /// </summary>
    public enum CaptchaType
    {
        /// <summary>
        /// 图片验证码
        /// </summary>
        Captcha,

        /// <summary>
        /// 短信验证码
        /// </summary>
        Text,

        /// <summary>
        /// 动态密码
        /// </summary>
        OTP
    }
}
