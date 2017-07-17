using CTPCore;

namespace CTPMarketAdapter.Interface
{
    /// <summary>
    /// 行情接口
    /// </summary>
    public interface IMarketApi
    {
        /// <summary>
        /// 是否已连接
        /// </summary>
        /// <returns></returns>
        bool IsConnected();

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="callback">连接服务器回调</param>
        /// <param name="brokerID">经纪商代码</param>
        /// <param name="frontAddress">前置服务器地址</param>
        void Connect(DataCallback callback, string brokerID, string frontAddress);

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="callback">断开连接回调</param>
        void Disconnect(DataCallback callback);

        /// <summary>
        /// 获取交易日
        /// </summary>
        /// <returns></returns>
        string GetTradingDay();

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="callback">登录回调</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="password">密码</param>
        void UserLogin(DataCallback callback, string investorID, string password);

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="callback">登出回调</param>
        void UserLogout(DataCallback callback);

        /// <summary>
        /// 订阅行情
        /// </summary>
        /// <param name="instruments">合约代码，传空订阅所有</param>
        void SubscribeMarket(params string[] instruments);

        /// <summary>
        /// 退订行情
        /// </summary>
        /// <param name="instruments">合约代码，传空退订所有</param>
        void UnsubscribeMarket(params string[] instruments);
    }
}
