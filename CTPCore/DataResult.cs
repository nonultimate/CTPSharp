using System;

namespace CTPCore
{
    /// <summary>
    /// 返回结果
    /// </summary>
    [Serializable]
    public class DataResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// 返回代码
        /// </summary>
        public int ReturnCode { get; set; }
    }
}
