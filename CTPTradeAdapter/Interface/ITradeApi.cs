using CTPCore;
using CTPTradeAdapter.Model;
using CTPTradeApi;

namespace CTPTradeAdapter.Interface
{
    /// <summary>
    /// 交易接口
    /// </summary>
    public interface ITradeApi
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
        /// <param name="frondAddress">前置服务器地址</param>
        void Connect(DataCallback callback, string brokerID, string frondAddress);

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="callback">断开连接回调</param>
        void Disconnect(DataCallback callback);

        /// <summary>
        /// 客户端认证
        /// </summary>
        /// <param name="callback">认证回调</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="productInfo">产品信息</param>
        /// <param name="authCode">认证代码</param>
        /// <param name="appID">应用代码</param>
        /// <returns></returns>
        int Authenticate(DataCallback callback, string investorID, string productInfo, string authCode, string appID);

        /// <summary>
        /// 需要在终端认证成功后，用户登录前调用该接口
        /// </summary>
        /// <param name="clientIP">终端IP</param>
        /// <param name="clientPort">终端端口</param>
        /// <param name="loginTime">登录时间</param>
        /// <returns></returns>
        int RegisterUserSystemInfo(string clientIP, int clientPort, string loginTime);

        /// <summary>
        /// 上报用户终端信息，用于中继服务器操作员登录模式
        /// 操作员登录后，可以多次调用该接口上报客户信息
        /// </summary>
        /// <param name="clientIP">终端IP</param>
        /// <param name="clientPort">终端端口</param>
        /// <param name="loginTime">登录时间</param>
        /// <returns></returns>
        int SubmitUserSystemInfo(string clientIP, int clientPort, string loginTime);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="callback">登录回调</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="password">密码</param>
        /// <param name="oneTimePassword">动态密码</param>
        int UserLogin(DataCallback callback, string investorID, string password, string oneTimePassword);

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="callback">登出回调</param>
        int UserLogout(DataCallback callback);

        /// <summary>
        /// 获取交易日
        /// </summary>
        /// <returns></returns>
        string GetTradingDay();

        /// <summary>
        /// 更新用户口令
        /// </summary>
        /// <param name="callback">更新回调</param>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        int UpdateUserPassword(DataCallback callback, string oldPassword, string newPassword);

        /// <summary>
        /// 更新资金账号口令
        /// </summary>
        /// <param name="callback">更新回调</param>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        int UpdateTradingAccountPassword(DataCallback callback, string oldPassword, string newPassword);

        /// <summary>
        /// 查询用户当前支持的认证模式
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="tradingDay">交易日</param>
        /// <returns></returns>
        int UserAuthMethod(DataCallback callback, string investorID, string tradingDay);

        /// <summary>
        /// 用户发出获取图形验证码请求
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="tradingDay">交易日</param>
        /// <returns></returns>
        int GenUserCaptcha(DataCallback callback, string investorID, string tradingDay);

        /// <summary>
        /// 用户发出获取短信验证码请求
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <param name="investorID">投资者账号</param>
        /// <param name="tradingDay">交易日</param>
        /// <returns></returns>
        int GenUserText(DataCallback callback, string investorID, string tradingDay);

        /// <summary>
        /// 用户发出带有验证码的登录请求
        /// </summary>
        /// <param name="callback">登录回调</param>
        /// <param name="request">登录请求</param>
        /// <returns></returns>
        int UserLoginWithCaptcha(DataCallback callback, LoginRequest request);

        /// <summary>
        /// 报单
        /// </summary>
        /// <param name="callback">报单回调</param>
        /// <param name="parameter">报单参数</param>
        int InsertOrder(DataCallback<OrderInfo> callback, OrderParameter parameter);

        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="callback">撤单回调</param>
        /// <param name="parameter">撤单参数</param>
        int CancelOrder(DataCallback<OrderInfo> callback, CancelOrderParameter parameter);

        /// <summary>
        /// 预埋单录入
        /// </summary>
        /// <param name="callback">报单回调</param>
        /// <param name="parameter">预埋单参数</param>
        int InsertParkedOrder(DataCallback<ParkedOrderInfo> callback, OrderParameter parameter);

        /// <summary>
        /// 预埋撤单
        /// </summary>
        /// <param name="callback">撤单回调</param>
        /// <param name="parameter"></param>
        int CancelParkedOrder(DataCallback<ParkedOrderInfo> callback, CancelOrderParameter parameter);

        /// <summary>
        /// 确认结算结果
        /// </summary>
        /// <param name="callback">结果回调</param>
        /// <returns></returns>
        int SettlementInfoConfirm(DataCallback callback);

        /// <summary>
        /// 查询当日委托
        /// </summary>
        /// <param name="callback">查询回调</param>
        void QueryOrder(DataListCallback<OrderInfo> callback);

        /// <summary>
        /// 查询当日成交
        /// </summary>
        /// <param name="callback">查询回调</param>
        void QueryTrade(DataListCallback<TradeInfo> callback);

        /// <summary>
        /// 查询资金账户
        /// </summary>
        /// <param name="callback">查询回调</param>
        void QueryAccount(DataCallback<AccountInfo> callback);

        /// <summary>
        /// 查询持仓
        /// </summary>
        /// <param name="callback">查询回调</param>
        void QueryPosition(DataListCallback<PositionInfo> callback);
        
        /// <summary>
        /// 查询预埋单
        /// </summary>
        /// <param name="callback">查询回调</param>
        void QueryParkedOrder(DataListCallback<ParkedOrderInfo> callback);

        /// <summary>
        /// 查询预埋撤单
        /// </summary>
        /// <param name="callback">查询回调</param>
        void QueryParkedOrderAction(DataListCallback<ParkedCanelOrderInfo> callback);

        /// <summary>
        /// 查询合约列表
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="instrumentID">指定合约，null查询所有</param>
        /// <returns></returns>
        void QueryInstrument(DataListCallback<InstrumentInfo> callback, string instrumentID);

        /// <summary>
        /// 查询投资者
        /// </summary>
        /// <param name="callback">查询回调</param>
        void QueryInvestor(DataCallback<InvestorInfo> callback);

        /// <summary>
        /// 查询投资者持仓明细
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <param name="instrumentID">合约代码:不填-查所有</param>
        /// <returns></returns>
        void QueryInvestorPositionDetail(DataListCallback<PositionDetailInfo> callback, string instrumentID);

        /// <summary>
        /// 请求查询客户通知
        /// </summary>
        /// <param name="callback">查询回调</param>
        void QueryNotice(DataListCallback<NoticeInfo> callback);

        /// <summary>
        /// 查询结算结果
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <returns></returns>
        void QuerySettlementInfo(DataCallback callback);

        /// <summary>
        /// 查询结算信息确认
        /// </summary>
        /// <param name="callback">查询回调</param>
        /// <returns></returns>
        void QuerySettlementInfoConfirm(DataCallback callback);
    }
}
