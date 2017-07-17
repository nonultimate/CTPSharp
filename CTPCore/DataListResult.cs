using System;
using System.Collections.ObjectModel;

namespace CTPCore
{
    /// <summary>
    /// 集合对象
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    [Serializable]
    public class DataListResult<T>
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public ObservableCollection<T> Result { get; set; }

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

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataListResult()
        {
            Result = new ObservableCollection<T>();
        }
    }
}
