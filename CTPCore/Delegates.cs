namespace CTPCore
{
    /// <summary>
    /// 数据回调委托
    /// </summary>
    /// <param name="result">结果对象</param>
    public delegate void DataCallback(DataResult result);

    /// <summary>
    /// 数据列表回调委托
    /// </summary>
    /// <typeparam name="T">列表元素类型</typeparam>
    /// <param name="result">列表结果对象</param>
    public delegate void DataListCallback<T>(DataListResult<T> result);
}
