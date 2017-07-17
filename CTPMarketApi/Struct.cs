using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CTPMarketApi
{
    /// <summary>
    /// 信息分发
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcDisseminationField
    {
        /// <summary>
        /// 序列系列号
        /// </summary>
        public short SequenceSeries;
        /// <summary>
        /// 序列号
        /// </summary>
        public int SequenceNo;
    }

    /// <summary>
    /// 用户登录请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcReqUserLoginField
    {
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 用户端产品信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string UserProductInfo;
        /// <summary>
        /// 接口端产品信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string InterfaceProductInfo;
        /// <summary>
        /// 协议信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ProtocolInfo;
        /// <summary>
        /// Mac地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string MacAddress;
        /// <summary>
        /// 动态密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string OneTimePassword;
        /// <summary>
        /// 终端IP地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string ClientIPAddress;
    }

    /// <summary>
    /// 用户登录应答
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcRspUserLoginField
    {
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 登录成功时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string LoginTime;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 交易系统名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string SystemName;
        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID;
        /// <summary>
        /// 会话编号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 最大报单引用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string MaxOrderRef;
        /// <summary>
        /// 上期所时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string SHFETime;
        /// <summary>
        /// 大商所时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string DCETime;
        /// <summary>
        /// 郑商所时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string CZCETime;
        /// <summary>
        /// 中金所时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string FFEXTime;
    }

    /// <summary>
    /// 用户登出请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcUserLogoutField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
    }

    /// <summary>
    /// 强制交易员退出
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcForceUserLogoutField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
    }

    /// <summary>
    /// 银期转帐报文头
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTransferHeaderField
    {
        /// <summary>
        /// 版本号，常量，1.0
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Version;
        /// <summary>
        /// 交易代码，必填
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 交易日期，必填，格式：yyyymmdd
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间，必填，格式：hhmmss
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 发起方流水号，N/A
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeSerial;
        /// <summary>
        /// 期货公司代码，必填
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string FutureID;
        /// <summary>
        /// 银行代码，根据查询银行得到，必填
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分中心代码，根据查询银行得到，必填
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBrchID;
        /// <summary>
        /// 操作员，N/A
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 交易设备类型，N/A
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 记录数，N/A
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string RecordNum;
        /// <summary>
        /// 会话编号，N/A
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 请求编号，N/A
        /// </summary>
        public int RequestID;
    }

    /// <summary>
    /// 银行资金转期货请求，TradeCode=202001
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTransferBankToFutureReqField
    {
        /// <summary>
        /// 期货资金账户
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string FutureAccount;
        /// <summary>
        /// 密码标志
        /// </summary>
        public EnumFuturePwdFlagType FuturePwdFlag;
        /// <summary>
        /// 密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string FutureAccPwd;
        /// <summary>
        /// 转账金额
        /// </summary>
        public double TradeAmt;
        /// <summary>
        /// 客户手续费
        /// </summary>
        public double CustFee;
        /// <summary>
        /// 币种：RMB-人民币 USD-美圆 HKD-港元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyCode;
    }

    /// <summary>
    /// 银行资金转期货请求响应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTransferBankToFutureRspField
    {
        /// <summary>
        /// 响应代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string RetCode;
        /// <summary>
        /// 响应信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string RetInfo;
        /// <summary>
        /// 资金账户
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string FutureAccount;
        /// <summary>
        /// 转帐金额
        /// </summary>
        public double TradeAmt;
        /// <summary>
        /// 应收客户手续费
        /// </summary>
        public double CustFee;
        /// <summary>
        /// 币种
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyCode;
    }

    /// <summary>
    /// 期货资金转银行请求，TradeCode=202002
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTransferFutureToBankReqField
    {
        /// <summary>
        /// 期货资金账户
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string FutureAccount;
        /// <summary>
        /// 密码标志
        /// </summary>
        public EnumFuturePwdFlagType FuturePwdFlag;
        /// <summary>
        /// 密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string FutureAccPwd;
        /// <summary>
        /// 转账金额
        /// </summary>
        public double TradeAmt;
        /// <summary>
        /// 客户手续费
        /// </summary>
        public double CustFee;
        /// <summary>
        /// 币种：RMB-人民币 USD-美圆 HKD-港元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyCode;
    }

    /// <summary>
    /// 期货资金转银行请求响应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTransferFutureToBankRspField
    {
        /// <summary>
        /// 响应代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string RetCode;
        /// <summary>
        /// 响应信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string RetInfo;
        /// <summary>
        /// 资金账户
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string FutureAccount;
        /// <summary>
        /// 转帐金额
        /// </summary>
        public double TradeAmt;
        /// <summary>
        /// 应收客户手续费
        /// </summary>
        public double CustFee;
        /// <summary>
        /// 币种
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyCode;
    }

    /// <summary>
    /// 查询银行资金请求，TradeCode=204002
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTransferQryBankReqField
    {
        /// <summary>
        /// 期货资金账户
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string FutureAccount;
        /// <summary>
        /// 密码标志
        /// </summary>
        public EnumFuturePwdFlagType FuturePwdFlag;
        /// <summary>
        /// 密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string FutureAccPwd;
        /// <summary>
        /// 币种：RMB-人民币 USD-美圆 HKD-港元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyCode;
    }

    /// <summary>
    /// 查询银行资金请求响应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTransferQryBankRspField
    {
        /// <summary>
        /// 响应代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string RetCode;
        /// <summary>
        /// 响应信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string RetInfo;
        /// <summary>
        /// 资金账户
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string FutureAccount;
        /// <summary>
        /// 银行余额
        /// </summary>
        public double TradeAmt;
        /// <summary>
        /// 银行可用余额
        /// </summary>
        public double UseAmt;
        /// <summary>
        /// 银行可取余额
        /// </summary>
        public double FetchAmt;
        /// <summary>
        /// 币种
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyCode;
    }

    /// <summary>
    /// 查询银行交易明细请求，TradeCode=204999
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTransferQryDetailReqField
    {
        /// <summary>
        /// 期货资金账户
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string FutureAccount;
    }

    /// <summary>
    /// 查询银行交易明细请求响应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTransferQryDetailRspField
    {
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 交易代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 期货流水号
        /// </summary>
        public int FutureSerial;
        /// <summary>
        /// 期货公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string FutureID;
        /// <summary>
        /// 资金帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 22)]
        public string FutureAccount;
        /// <summary>
        /// 银行流水号
        /// </summary>
        public int BankSerial;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分中心代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBrchID;
        /// <summary>
        /// 银行账号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string CertCode;
        /// <summary>
        /// 货币代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyCode;
        /// <summary>
        /// 发生金额
        /// </summary>
        public double TxAmount;
        /// <summary>
        /// 有效标志
        /// </summary>
        public EnumTransferValidFlagType Flag;
    }

    /// <summary>
    /// 响应信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcRspInfoField
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 交易所
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcExchangeField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 交易所名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ExchangeName;
        /// <summary>
        /// 交易所属性
        /// </summary>
        public EnumExchangePropertyType ExchangeProperty;
    }

    /// <summary>
    /// 产品
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcProductField
    {
        /// <summary>
        /// 产品代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ProductID;
        /// <summary>
        /// 产品名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string ProductName;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 产品类型
        /// </summary>
        public EnumProductClassType ProductClass;
        /// <summary>
        /// 合约数量乘数
        /// </summary>
        public int VolumeMultiple;
        /// <summary>
        /// 最小变动价位
        /// </summary>
        public double PriceTick;
        /// <summary>
        /// 市价单最大下单量
        /// </summary>
        public int MaxMarketOrderVolume;
        /// <summary>
        /// 市价单最小下单量
        /// </summary>
        public int MinMarketOrderVolume;
        /// <summary>
        /// 限价单最大下单量
        /// </summary>
        public int MaxLimitOrderVolume;
        /// <summary>
        /// 限价单最小下单量
        /// </summary>
        public int MinLimitOrderVolume;
        /// <summary>
        /// 持仓类型
        /// </summary>
        public EnumPositionTypeType PositionType;
        /// <summary>
        /// 持仓日期类型
        /// </summary>
        public EnumPositionDateTypeType PositionDateType;
    }

    /// <summary>
    /// 合约
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInstrumentField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 合约名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string InstrumentName;
        /// <summary>
        /// 合约在交易所的代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ExchangeInstID;
        /// <summary>
        /// 产品代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ProductID;
        /// <summary>
        /// 产品类型
        /// </summary>
        public EnumProductClassType ProductClass;
        /// <summary>
        /// 交割年份
        /// </summary>
        public int DeliveryYear;
        /// <summary>
        /// 交割月
        /// </summary>
        public int DeliveryMonth;
        /// <summary>
        /// 市价单最大下单量
        /// </summary>
        public int MaxMarketOrderVolume;
        /// <summary>
        /// 市价单最小下单量
        /// </summary>
        public int MinMarketOrderVolume;
        /// <summary>
        /// 限价单最大下单量
        /// </summary>
        public int MaxLimitOrderVolume;
        /// <summary>
        /// 限价单最小下单量
        /// </summary>
        public int MinLimitOrderVolume;
        /// <summary>
        /// 合约数量乘数
        /// </summary>
        public int VolumeMultiple;
        /// <summary>
        /// 最小变动价位
        /// </summary>
        public double PriceTick;
        /// <summary>
        /// 创建日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string CreateDate;
        /// <summary>
        /// 上市日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string OpenDate;
        /// <summary>
        /// 到期日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExpireDate;
        /// <summary>
        /// 开始交割日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string StartDelivDate;
        /// <summary>
        /// 结束交割日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string EndDelivDate;
        /// <summary>
        /// 合约生命周期状态
        /// </summary>
        public EnumInstLifePhaseType InstLifePhase;
        /// <summary>
        /// 当前是否交易
        /// </summary>
        public int IsTrading;
        /// <summary>
        /// 持仓类型
        /// </summary>
        public EnumPositionTypeType PositionType;
        /// <summary>
        /// 持仓日期类型
        /// </summary>
        public EnumPositionDateTypeType PositionDateType;
        /// <summary>
        /// 多头保证金率
        /// </summary>
        public double LongMarginRatio;
        /// <summary>
        /// 空头保证金率
        /// </summary>
        public double ShortMarginRatio;
    }

    /// <summary>
    /// 经纪公司
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcBrokerField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 经纪公司简称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string BrokerAbbr;
        /// <summary>
        /// 经纪公司名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string BrokerName;
        /// <summary>
        /// 是否活跃
        /// </summary>
        public int IsActive;
    }

    /// <summary>
    /// 交易所交易员
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTraderField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 安装数量
        /// </summary>
        public int InstallCount;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
    }

    /// <summary>
    /// 投资者
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInvestorField
    {
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者分组代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorGroupID;
        /// <summary>
        /// 投资者名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string InvestorName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdentifiedCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 是否活跃
        /// </summary>
        public int IsActive;
        /// <summary>
        /// 联系电话
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Telephone;
        /// <summary>
        /// 通讯地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 101)]
        public string Address;
        /// <summary>
        /// 开户日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string OpenDate;
        /// <summary>
        /// 手机
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Mobile;
    }

    /// <summary>
    /// 交易编码
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTradingCodeField
    {
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 客户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientID;
        /// <summary>
        /// 是否活跃
        /// </summary>
        public int IsActive;
        /// <summary>
        /// 交易编码类型
        /// </summary>
        public EnumClientIDTypeType ClientIDType;
    }

    /// <summary>
    /// 会员编码和经纪公司编码对照表
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcPartBrokerField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 是否活跃
        /// </summary>
        public int IsActive;
    }

    /// <summary>
    /// 管理用户
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSuperUserField
    {
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 用户名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string UserName;
        /// <summary>
        /// 密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 是否活跃
        /// </summary>
        public int IsActive;
    }

    /// <summary>
    /// 管理用户功能权限
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSuperUserFunctionField
    {
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 功能代码
        /// </summary>
        public EnumFunctionCodeType FunctionCode;
    }

    /// <summary>
    /// 投资者组
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInvestorGroupField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者分组代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorGroupID;
        /// <summary>
        /// 投资者分组名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string InvestorGroupName;
    }

    /// <summary>
    /// 资金账户
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTradingAccountField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 上次质押金额
        /// </summary>
        public double PreMortgage;
        /// <summary>
        /// 上次信用额度
        /// </summary>
        public double PreCredit;
        /// <summary>
        /// 上次存款额
        /// </summary>
        public double PreDeposit;
        /// <summary>
        /// 上次结算准备金
        /// </summary>
        public double PreBalance;
        /// <summary>
        /// 上次占用的保证金
        /// </summary>
        public double PreMargin;
        /// <summary>
        /// 利息基数
        /// </summary>
        public double InterestBase;
        /// <summary>
        /// 利息收入
        /// </summary>
        public double Interest;
        /// <summary>
        /// 入金金额
        /// </summary>
        public double Deposit;
        /// <summary>
        /// 出金金额
        /// </summary>
        public double Withdraw;
        /// <summary>
        /// 冻结的保证金
        /// </summary>
        public double FrozenMargin;
        /// <summary>
        /// 冻结的资金
        /// </summary>
        public double FrozenCash;
        /// <summary>
        /// 冻结的手续费
        /// </summary>
        public double FrozenCommission;
        /// <summary>
        /// 当前保证金总额
        /// </summary>
        public double CurrMargin;
        /// <summary>
        /// 资金差额
        /// </summary>
        public double CashIn;
        /// <summary>
        /// 手续费
        /// </summary>
        public double Commission;
        /// <summary>
        /// 平仓盈亏
        /// </summary>
        public double CloseProfit;
        /// <summary>
        /// 持仓盈亏
        /// </summary>
        public double PositionProfit;
        /// <summary>
        /// 期货结算准备金
        /// </summary>
        public double Balance;
        /// <summary>
        /// 可用资金
        /// </summary>
        public double Available;
        /// <summary>
        /// 可取资金
        /// </summary>
        public double WithdrawQuota;
        /// <summary>
        /// 基本准备金
        /// </summary>
        public double Reserve;
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID;
        /// <summary>
        /// 信用额度
        /// </summary>
        public double Credit;
        /// <summary>
        /// 质押金额
        /// </summary>
        public double Mortgage;
        /// <summary>
        /// 交易所保证金
        /// </summary>
        public double ExchangeMargin;
        /// <summary>
        /// 投资者交割保证金
        /// </summary>
        public double DeliveryMargin;
        /// <summary>
        /// 交易所交割保证金
        /// </summary>
        public double ExchangeDeliveryMargin;
    }

    /// <summary>
    /// 投资者持仓
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInvestorPositionField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 持仓多空方向
        /// </summary>
        public EnumPosiDirectionType PosiDirection;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
        /// <summary>
        /// 持仓日期
        /// </summary>
        public EnumPositionDateType PositionDate;
        /// <summary>
        /// 上日持仓
        /// </summary>
        public int YdPosition;
        /// <summary>
        /// 今日持仓
        /// </summary>
        public int Position;
        /// <summary>
        /// 多头冻结
        /// </summary>
        public int LongFrozen;
        /// <summary>
        /// 空头冻结
        /// </summary>
        public int ShortFrozen;
        /// <summary>
        /// 开仓冻结金额
        /// </summary>
        public double LongFrozenAmount;
        /// <summary>
        /// 开仓冻结金额
        /// </summary>
        public double ShortFrozenAmount;
        /// <summary>
        /// 开仓量
        /// </summary>
        public int OpenVolume;
        /// <summary>
        /// 平仓量
        /// </summary>
        public int CloseVolume;
        /// <summary>
        /// 开仓金额
        /// </summary>
        public double OpenAmount;
        /// <summary>
        /// 平仓金额
        /// </summary>
        public double CloseAmount;
        /// <summary>
        /// 持仓成本
        /// </summary>
        public double PositionCost;
        /// <summary>
        /// 上次占用的保证金
        /// </summary>
        public double PreMargin;
        /// <summary>
        /// 占用的保证金
        /// </summary>
        public double UseMargin;
        /// <summary>
        /// 冻结的保证金
        /// </summary>
        public double FrozenMargin;
        /// <summary>
        /// 冻结的资金
        /// </summary>
        public double FrozenCash;
        /// <summary>
        /// 冻结的手续费
        /// </summary>
        public double FrozenCommission;
        /// <summary>
        /// 资金差额
        /// </summary>
        public double CashIn;
        /// <summary>
        /// 手续费
        /// </summary>
        public double Commission;
        /// <summary>
        /// 平仓盈亏
        /// </summary>
        public double CloseProfit;
        /// <summary>
        /// 持仓盈亏
        /// </summary>
        public double PositionProfit;
        /// <summary>
        /// 上次结算价
        /// </summary>
        public double PreSettlementPrice;
        /// <summary>
        /// 本次结算价
        /// </summary>
        public double SettlementPrice;
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID;
        /// <summary>
        /// 开仓成本
        /// </summary>
        public double OpenCost;
        /// <summary>
        /// 交易所保证金
        /// </summary>
        public double ExchangeMargin;
        /// <summary>
        /// 组合成交形成的持仓
        /// </summary>
        public int CombPosition;
        /// <summary>
        /// 组合多头冻结
        /// </summary>
        public int CombLongFrozen;
        /// <summary>
        /// 组合空头冻结
        /// </summary>
        public int CombShortFrozen;
        /// <summary>
        /// 逐日盯市平仓盈亏
        /// </summary>
        public double CloseProfitByDate;
        /// <summary>
        /// 逐笔对冲平仓盈亏
        /// </summary>
        public double CloseProfitByTrade;
        /// <summary>
        /// 今日持仓
        /// </summary>
        public int TodayPosition;
        /// <summary>
        /// 保证金率
        /// </summary>
        public double MarginRateByMoney;
        /// <summary>
        /// 保证金率(按手数)
        /// </summary>
        public double MarginRateByVolume;
    }

    /// <summary>
    /// 合约保证金率
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInstrumentMarginRateField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 投资者范围
        /// </summary>
        public EnumInvestorRangeType InvestorRange;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
        /// <summary>
        /// 多头保证金率
        /// </summary>
        public double LongMarginRatioByMoney;
        /// <summary>
        /// 多头保证金费
        /// </summary>
        public double LongMarginRatioByVolume;
        /// <summary>
        /// 空头保证金率
        /// </summary>
        public double ShortMarginRatioByMoney;
        /// <summary>
        /// 空头保证金费
        /// </summary>
        public double ShortMarginRatioByVolume;
        /// <summary>
        /// 是否相对交易所收取
        /// </summary>
        public int IsRelative;
    }

    /// <summary>
    /// 合约手续费率
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInstrumentCommissionRateField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 投资者范围
        /// </summary>
        public EnumInvestorRangeType InvestorRange;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 开仓手续费率
        /// </summary>
        public double OpenRatioByMoney;
        /// <summary>
        /// 开仓手续费
        /// </summary>
        public double OpenRatioByVolume;
        /// <summary>
        /// 平仓手续费率
        /// </summary>
        public double CloseRatioByMoney;
        /// <summary>
        /// 平仓手续费
        /// </summary>
        public double CloseRatioByVolume;
        /// <summary>
        /// 平今手续费率
        /// </summary>
        public double CloseTodayRatioByMoney;
        /// <summary>
        /// 平今手续费
        /// </summary>
        public double CloseTodayRatioByVolume;
    }

    /// <summary>
    /// 深度行情
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcDepthMarketDataField
    {
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 合约在交易所的代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ExchangeInstID;
        /// <summary>
        /// 最新价
        /// </summary>
        public double LastPrice;
        /// <summary>
        /// 上次结算价
        /// </summary>
        public double PreSettlementPrice;
        /// <summary>
        /// 昨收盘
        /// </summary>
        public double PreClosePrice;
        /// <summary>
        /// 昨持仓量
        /// </summary>
        public double PreOpenInterest;
        /// <summary>
        /// 今开盘
        /// </summary>
        public double OpenPrice;
        /// <summary>
        /// 最高价
        /// </summary>
        public double HighestPrice;
        /// <summary>
        /// 最低价
        /// </summary>
        public double LowestPrice;
        /// <summary>
        /// 数量
        /// </summary>
        public int Volume;
        /// <summary>
        /// 成交金额
        /// </summary>
        public double Turnover;
        /// <summary>
        /// 持仓量
        /// </summary>
        public double OpenInterest;
        /// <summary>
        /// 今收盘
        /// </summary>
        public double ClosePrice;
        /// <summary>
        /// 本次结算价
        /// </summary>
        public double SettlementPrice;
        /// <summary>
        /// 涨停板价
        /// </summary>
        public double UpperLimitPrice;
        /// <summary>
        /// 跌停板价
        /// </summary>
        public double LowerLimitPrice;
        /// <summary>
        /// 昨虚实度
        /// </summary>
        public double PreDelta;
        /// <summary>
        /// 今虚实度
        /// </summary>
        public double CurrDelta;
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string UpdateTime;
        /// <summary>
        /// 最后修改毫秒
        /// </summary>
        public int UpdateMillisec;
        /// <summary>
        /// 申买价一
        /// </summary>
        public double BidPrice1;
        /// <summary>
        /// 申买量一
        /// </summary>
        public int BidVolume1;
        /// <summary>
        /// 申卖价一
        /// </summary>
        public double AskPrice1;
        /// <summary>
        /// 申卖量一
        /// </summary>
        public int AskVolume1;
        /// <summary>
        /// 申买价二
        /// </summary>
        public double BidPrice2;
        /// <summary>
        /// 申买量二
        /// </summary>
        public int BidVolume2;
        /// <summary>
        /// 申卖价二
        /// </summary>
        public double AskPrice2;
        /// <summary>
        /// 申卖量二
        /// </summary>
        public int AskVolume2;
        /// <summary>
        /// 申买价三
        /// </summary>
        public double BidPrice3;
        /// <summary>
        /// 申买量三
        /// </summary>
        public int BidVolume3;
        /// <summary>
        /// 申卖价三
        /// </summary>
        public double AskPrice3;
        /// <summary>
        /// 申卖量三
        /// </summary>
        public int AskVolume3;
        /// <summary>
        /// 申买价四
        /// </summary>
        public double BidPrice4;
        /// <summary>
        /// 申买量四
        /// </summary>
        public int BidVolume4;
        /// <summary>
        /// 申卖价四
        /// </summary>
        public double AskPrice4;
        /// <summary>
        /// 申卖量四
        /// </summary>
        public int AskVolume4;
        /// <summary>
        /// 申买价五
        /// </summary>
        public double BidPrice5;
        /// <summary>
        /// 申买量五
        /// </summary>
        public int BidVolume5;
        /// <summary>
        /// 申卖价五
        /// </summary>
        public double AskPrice5;
        /// <summary>
        /// 申卖量五
        /// </summary>
        public int AskVolume5;
        /// <summary>
        /// 当日均价
        /// </summary>
        public double AveragePrice;
    }

    /// <summary>
    /// 投资者合约交易权限
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInstrumentTradingRightField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 投资者范围
        /// </summary>
        public EnumInvestorRangeType InvestorRange;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 交易权限
        /// </summary>
        public EnumTradingRightType TradingRight;
    }

    /// <summary>
    /// 经纪公司用户
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcBrokerUserField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 用户名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string UserName;
        /// <summary>
        /// 用户类型
        /// </summary>
        public EnumUserTypeType UserType;
        /// <summary>
        /// 是否活跃
        /// </summary>
        public int IsActive;
        /// <summary>
        /// 是否使用令牌
        /// </summary>
        public int IsUsingOTP;
    }

    /// <summary>
    /// 经纪公司用户口令
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcBrokerUserPasswordField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
    }

    /// <summary>
    /// 经纪公司用户功能权限
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcBrokerUserFunctionField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 经纪公司功能代码
        /// </summary>
        public EnumBrokerFunctionCodeType BrokerFunctionCode;
    }

    /// <summary>
    /// 交易所交易员报盘机
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTraderOfferField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 本地报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderLocalID;
        /// <summary>
        /// 交易所交易员连接状态
        /// </summary>
        public EnumTraderConnectStatusType TraderConnectStatus;
        /// <summary>
        /// 发出连接请求的日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ConnectRequestDate;
        /// <summary>
        /// 发出连接请求的时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ConnectRequestTime;
        /// <summary>
        /// 上次报告日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string LastReportDate;
        /// <summary>
        /// 上次报告时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string LastReportTime;
        /// <summary>
        /// 完成连接日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ConnectDate;
        /// <summary>
        /// 完成连接时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ConnectTime;
        /// <summary>
        /// 启动日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string StartDate;
        /// <summary>
        /// 启动时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string StartTime;
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
    }

    /// <summary>
    /// 投资者结算结果
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSettlementInfoField
    {
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 序号
        /// </summary>
        public int SequenceNo;
        /// <summary>
        /// 消息正文
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 501)]
        public string Content;
    }

    /// <summary>
    /// 合约保证金率调整
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInstrumentMarginRateAdjustField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 投资者范围
        /// </summary>
        public EnumInvestorRangeType InvestorRange;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
        /// <summary>
        /// 多头保证金率
        /// </summary>
        public double LongMarginRatioByMoney;
        /// <summary>
        /// 多头保证金费
        /// </summary>
        public double LongMarginRatioByVolume;
        /// <summary>
        /// 空头保证金率
        /// </summary>
        public double ShortMarginRatioByMoney;
        /// <summary>
        /// 空头保证金费
        /// </summary>
        public double ShortMarginRatioByVolume;
        /// <summary>
        /// 是否相对交易所收取
        /// </summary>
        public int IsRelative;
    }

    /// <summary>
    /// 交易所保证金率
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcExchangeMarginRateField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
        /// <summary>
        /// 多头保证金率
        /// </summary>
        public double LongMarginRatioByMoney;
        /// <summary>
        /// 多头保证金费
        /// </summary>
        public double LongMarginRatioByVolume;
        /// <summary>
        /// 空头保证金率
        /// </summary>
        public double ShortMarginRatioByMoney;
        /// <summary>
        /// 空头保证金费
        /// </summary>
        public double ShortMarginRatioByVolume;
    }

    /// <summary>
    /// 交易所保证金率调整
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcExchangeMarginRateAdjustField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
        /// <summary>
        /// 跟随交易所投资者多头保证金率
        /// </summary>
        public double LongMarginRatioByMoney;
        /// <summary>
        /// 跟随交易所投资者多头保证金费
        /// </summary>
        public double LongMarginRatioByVolume;
        /// <summary>
        /// 跟随交易所投资者空头保证金率
        /// </summary>
        public double ShortMarginRatioByMoney;
        /// <summary>
        /// 跟随交易所投资者空头保证金费
        /// </summary>
        public double ShortMarginRatioByVolume;
        /// <summary>
        /// 交易所多头保证金率
        /// </summary>
        public double ExchLongMarginRatioByMoney;
        /// <summary>
        /// 交易所多头保证金费
        /// </summary>
        public double ExchLongMarginRatioByVolume;
        /// <summary>
        /// 交易所空头保证金率
        /// </summary>
        public double ExchShortMarginRatioByMoney;
        /// <summary>
        /// 交易所空头保证金费
        /// </summary>
        public double ExchShortMarginRatioByVolume;
        /// <summary>
        /// 不跟随交易所投资者多头保证金率
        /// </summary>
        public double NoLongMarginRatioByMoney;
        /// <summary>
        /// 不跟随交易所投资者多头保证金费
        /// </summary>
        public double NoLongMarginRatioByVolume;
        /// <summary>
        /// 不跟随交易所投资者空头保证金率
        /// </summary>
        public double NoShortMarginRatioByMoney;
        /// <summary>
        /// 不跟随交易所投资者空头保证金费
        /// </summary>
        public double NoShortMarginRatioByVolume;
    }

    /// <summary>
    /// 结算引用
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSettlementRefField
    {
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID;
    }

    /// <summary>
    /// 当前时间
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcCurrentTimeField
    {
        /// <summary>
        /// 当前日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string CurrDate;
        /// <summary>
        /// 当前时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string CurrTime;
        /// <summary>
        /// 当前时间（毫秒）
        /// </summary>
        public int CurrMillisec;
    }

    /// <summary>
    /// 通讯阶段
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcCommPhaseField
    {
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 通讯时段编号
        /// </summary>
        public short CommPhaseNo;
    }

    /// <summary>
    /// 登录信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcLoginInfoField
    {
        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID;
        /// <summary>
        /// 会话编号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 登录日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string LoginDate;
        /// <summary>
        /// 登录时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string LoginTime;
        /// <summary>
        /// IP地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string IPAddress;
        /// <summary>
        /// 用户端产品信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string UserProductInfo;
        /// <summary>
        /// 接口端产品信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string InterfaceProductInfo;
        /// <summary>
        /// 协议信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ProtocolInfo;
        /// <summary>
        /// 系统名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string SystemName;
        /// <summary>
        /// 密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 最大报单引用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string MaxOrderRef;
        /// <summary>
        /// 上期所时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string SHFETime;
        /// <summary>
        /// 大商所时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string DCETime;
        /// <summary>
        /// 郑商所时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string CZCETime;
        /// <summary>
        /// 中金所时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string FFEXTime;
        /// <summary>
        /// Mac地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string MacAddress;
        /// <summary>
        /// 动态密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string OneTimePassword;
    }

    /// <summary>
    /// 登录信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcLogoutAllField
    {
        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID;
        /// <summary>
        /// 会话编号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 系统名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string SystemName;
    }

    /// <summary>
    /// 前置状态
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcFrontStatusField
    {
        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID;
        /// <summary>
        /// 上次报告日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string LastReportDate;
        /// <summary>
        /// 上次报告时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string LastReportTime;
        /// <summary>
        /// 是否活跃
        /// </summary>
        public int IsActive;
    }

    /// <summary>
    /// 用户口令变更
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcUserPasswordUpdateField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 原来的口令
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string OldPassword;
        /// <summary>
        /// 新的口令
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string NewPassword;
    }

    /// <summary>
    /// 输入报单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInputOrderField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 报单引用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderRef;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 报单价格条件
        /// </summary>
        public EnumOrderPriceTypeType OrderPriceType;
        /// <summary>
        /// 买卖方向
        /// </summary>
        public EnumDirectionType Direction;
        /// <summary>
        /// 组合开平标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public char[] CombOffsetFlag;
        /// <summary>
        /// 组合投机套保标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public char[] CombHedgeFlag;
        /// <summary>
        /// 价格
        /// </summary>
        public double LimitPrice;
        /// <summary>
        /// 数量
        /// </summary>
        public int VolumeTotalOriginal;
        /// <summary>
        /// 有效期类型
        /// </summary>
        public EnumTimeConditionType TimeCondition;
        /// <summary>
        /// GTD日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string GTDDate;
        /// <summary>
        /// 成交量类型
        /// </summary>
        public EnumVolumeConditionType VolumeCondition;
        /// <summary>
        /// 最小成交量
        /// </summary>
        public int MinVolume;
        /// <summary>
        /// 触发条件
        /// </summary>
        public EnumContingentConditionType ContingentCondition;
        /// <summary>
        /// 止损价
        /// </summary>
        public double StopPrice;
        /// <summary>
        /// 强平原因
        /// </summary>
        public EnumForceCloseReasonType ForceCloseReason;
        /// <summary>
        /// 自动挂起标志
        /// </summary>
        public int IsAutoSuspend;
        /// <summary>
        /// 业务单元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string BusinessUnit;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 用户强评标志
        /// </summary>
        public int UserForceClose;
    }

    /// <summary>
    /// 报单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcOrderField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 报单引用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderRef;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 报单价格条件
        /// </summary>
        public EnumOrderPriceTypeType OrderPriceType;
        /// <summary>
        /// 买卖方向
        /// </summary>
        public EnumDirectionType Direction;
        /// <summary>
        /// 组合开平标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string CombOffsetFlag;
        /// <summary>
        /// 组合投机套保标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string CombHedgeFlag;
        /// <summary>
        /// 价格
        /// </summary>
        public double LimitPrice;
        /// <summary>
        /// 数量
        /// </summary>
        public int VolumeTotalOriginal;
        /// <summary>
        /// 有效期类型
        /// </summary>
        public EnumTimeConditionType TimeCondition;
        /// <summary>
        /// GTD日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string GTDDate;
        /// <summary>
        /// 成交量类型
        /// </summary>
        public EnumVolumeConditionType VolumeCondition;
        /// <summary>
        /// 最小成交量
        /// </summary>
        public int MinVolume;
        /// <summary>
        /// 触发条件
        /// </summary>
        public EnumContingentConditionType ContingentCondition;
        /// <summary>
        /// 止损价
        /// </summary>
        public double StopPrice;
        /// <summary>
        /// 强平原因
        /// </summary>
        public EnumForceCloseReasonType ForceCloseReason;
        /// <summary>
        /// 自动挂起标志
        /// </summary>
        public int IsAutoSuspend;
        /// <summary>
        /// 业务单元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string BusinessUnit;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 本地报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderLocalID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 客户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientID;
        /// <summary>
        /// 合约在交易所的代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ExchangeInstID;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 报单提交状态
        /// </summary>
        public EnumOrderSubmitStatusType OrderSubmitStatus;
        /// <summary>
        /// 报单提示序号
        /// </summary>
        public int NotifySequence;
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID;
        /// <summary>
        /// 报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string OrderSysID;
        /// <summary>
        /// 报单来源
        /// </summary>
        public EnumOrderSourceType OrderSource;
        /// <summary>
        /// 报单状态
        /// </summary>
        public EnumOrderStatusType OrderStatus;
        /// <summary>
        /// 报单类型
        /// </summary>
        public EnumOrderTypeType OrderType;
        /// <summary>
        /// 今成交数量
        /// </summary>
        public int VolumeTraded;
        /// <summary>
        /// 剩余数量
        /// </summary>
        public int VolumeTotal;
        /// <summary>
        /// 报单日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string InsertDate;
        /// <summary>
        /// 委托时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string InsertTime;
        /// <summary>
        /// 激活时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ActiveTime;
        /// <summary>
        /// 挂起时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string SuspendTime;
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string UpdateTime;
        /// <summary>
        /// 撤销时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string CancelTime;
        /// <summary>
        /// 最后修改交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string ActiveTraderID;
        /// <summary>
        /// 结算会员编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClearingPartID;
        /// <summary>
        /// 序号
        /// </summary>
        public int SequenceNo;
        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID;
        /// <summary>
        /// 会话编号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 用户端产品信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string UserProductInfo;
        /// <summary>
        /// 状态信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string StatusMsg;
        /// <summary>
        /// 用户强评标志
        /// </summary>
        public int UserForceClose;
        /// <summary>
        /// 操作用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string ActiveUserID;
        /// <summary>
        /// 经纪公司报单编号
        /// </summary>
        public int BrokerOrderSeq;
        /// <summary>
        /// 相关报单
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string RelativeOrderSysID;
    }

    /// <summary>
    /// 交易所报单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcExchangeOrderField
    {
        /// <summary>
        /// 报单价格条件
        /// </summary>
        public EnumOrderPriceTypeType OrderPriceType;
        /// <summary>
        /// 买卖方向
        /// </summary>
        public EnumDirectionType Direction;
        /// <summary>
        /// 组合开平标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string CombOffsetFlag;
        /// <summary>
        /// 组合投机套保标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string CombHedgeFlag;
        /// <summary>
        /// 价格
        /// </summary>
        public double LimitPrice;
        /// <summary>
        /// 数量
        /// </summary>
        public int VolumeTotalOriginal;
        /// <summary>
        /// 有效期类型
        /// </summary>
        public EnumTimeConditionType TimeCondition;
        /// <summary>
        /// GTD日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string GTDDate;
        /// <summary>
        /// 成交量类型
        /// </summary>
        public EnumVolumeConditionType VolumeCondition;
        /// <summary>
        /// 最小成交量
        /// </summary>
        public int MinVolume;
        /// <summary>
        /// 触发条件
        /// </summary>
        public EnumContingentConditionType ContingentCondition;
        /// <summary>
        /// 止损价
        /// </summary>
        public double StopPrice;
        /// <summary>
        /// 强平原因
        /// </summary>
        public EnumForceCloseReasonType ForceCloseReason;
        /// <summary>
        /// 自动挂起标志
        /// </summary>
        public int IsAutoSuspend;
        /// <summary>
        /// 业务单元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string BusinessUnit;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 本地报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderLocalID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 客户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientID;
        /// <summary>
        /// 合约在交易所的代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ExchangeInstID;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 报单提交状态
        /// </summary>
        public EnumOrderSubmitStatusType OrderSubmitStatus;
        /// <summary>
        /// 报单提示序号
        /// </summary>
        public int NotifySequence;
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID;
        /// <summary>
        /// 报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string OrderSysID;
        /// <summary>
        /// 报单来源
        /// </summary>
        public EnumOrderSourceType OrderSource;
        /// <summary>
        /// 报单状态
        /// </summary>
        public EnumOrderStatusType OrderStatus;
        /// <summary>
        /// 报单类型
        /// </summary>
        public EnumOrderTypeType OrderType;
        /// <summary>
        /// 今成交数量
        /// </summary>
        public int VolumeTraded;
        /// <summary>
        /// 剩余数量
        /// </summary>
        public int VolumeTotal;
        /// <summary>
        /// 报单日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string InsertDate;
        /// <summary>
        /// 委托时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string InsertTime;
        /// <summary>
        /// 激活时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ActiveTime;
        /// <summary>
        /// 挂起时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string SuspendTime;
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string UpdateTime;
        /// <summary>
        /// 撤销时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string CancelTime;
        /// <summary>
        /// 最后修改交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string ActiveTraderID;
        /// <summary>
        /// 结算会员编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClearingPartID;
        /// <summary>
        /// 序号
        /// </summary>
        public int SequenceNo;
    }

    /// <summary>
    /// 交易所报单插入失败
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcExchangeOrderInsertErrorField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 本地报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderLocalID;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 输入报单操作
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInputOrderActionField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 报单操作引用
        /// </summary>
        public int OrderActionRef;
        /// <summary>
        /// 报单引用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderRef;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID;
        /// <summary>
        /// 会话编号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string OrderSysID;
        /// <summary>
        /// 操作标志
        /// </summary>
        public EnumActionFlagType ActionFlag;
        /// <summary>
        /// 价格
        /// </summary>
        public double LimitPrice;
        /// <summary>
        /// 数量变化
        /// </summary>
        public int VolumeChange;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
    }

    /// <summary>
    /// 报单操作
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcOrderActionField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 报单操作引用
        /// </summary>
        public int OrderActionRef;
        /// <summary>
        /// 报单引用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderRef;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID;
        /// <summary>
        /// 会话编号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string OrderSysID;
        /// <summary>
        /// 操作标志
        /// </summary>
        public EnumActionFlagType ActionFlag;
        /// <summary>
        /// 价格
        /// </summary>
        public double LimitPrice;
        /// <summary>
        /// 数量变化
        /// </summary>
        public int VolumeChange;
        /// <summary>
        /// 操作日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ActionDate;
        /// <summary>
        /// 操作时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ActionTime;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 本地报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderLocalID;
        /// <summary>
        /// 操作本地编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string ActionLocalID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 客户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientID;
        /// <summary>
        /// 业务单元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string BusinessUnit;
        /// <summary>
        /// 报单操作状态
        /// </summary>
        public EnumOrderActionStatusType OrderActionStatus;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 状态信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string StatusMsg;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
    }

    /// <summary>
    /// 交易所报单操作
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcExchangeOrderActionField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string OrderSysID;
        /// <summary>
        /// 操作标志
        /// </summary>
        public EnumActionFlagType ActionFlag;
        /// <summary>
        /// 价格
        /// </summary>
        public double LimitPrice;
        /// <summary>
        /// 数量变化
        /// </summary>
        public int VolumeChange;
        /// <summary>
        /// 操作日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ActionDate;
        /// <summary>
        /// 操作时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ActionTime;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 本地报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderLocalID;
        /// <summary>
        /// 操作本地编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string ActionLocalID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 客户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientID;
        /// <summary>
        /// 业务单元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string BusinessUnit;
        /// <summary>
        /// 报单操作状态
        /// </summary>
        public EnumOrderActionStatusType OrderActionStatus;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
    }

    /// <summary>
    /// 交易所报单操作失败
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcExchangeOrderActionErrorField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string OrderSysID;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 本地报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderLocalID;
        /// <summary>
        /// 操作本地编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string ActionLocalID;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 交易所成交
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcExchangeTradeField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 成交编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TradeID;
        /// <summary>
        /// 买卖方向
        /// </summary>
        public EnumDirectionType Direction;
        /// <summary>
        /// 报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string OrderSysID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 客户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientID;
        /// <summary>
        /// 交易角色
        /// </summary>
        public EnumTradingRoleType TradingRole;
        /// <summary>
        /// 合约在交易所的代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ExchangeInstID;
        /// <summary>
        /// 开平标志
        /// </summary>
        public EnumOffsetFlagType OffsetFlag;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
        /// <summary>
        /// 价格
        /// </summary>
        public double Price;
        /// <summary>
        /// 数量
        /// </summary>
        public int Volume;
        /// <summary>
        /// 成交时期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 成交时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 成交类型
        /// </summary>
        public EnumTradeTypeType TradeType;
        /// <summary>
        /// 成交价来源
        /// </summary>
        public EnumPriceSourceType PriceSource;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
        /// <summary>
        /// 本地报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderLocalID;
        /// <summary>
        /// 结算会员编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClearingPartID;
        /// <summary>
        /// 业务单元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string BusinessUnit;
        /// <summary>
        /// 序号
        /// </summary>
        public int SequenceNo;
    }

    /// <summary>
    /// 成交
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTradeField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 报单引用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderRef;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 成交编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TradeID;
        /// <summary>
        /// 买卖方向
        /// </summary>
        public EnumDirectionType Direction;
        /// <summary>
        /// 报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string OrderSysID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 客户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientID;
        /// <summary>
        /// 交易角色
        /// </summary>
        public EnumTradingRoleType TradingRole;
        /// <summary>
        /// 合约在交易所的代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ExchangeInstID;
        /// <summary>
        /// 开平标志
        /// </summary>
        public EnumOffsetFlagType OffsetFlag;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
        /// <summary>
        /// 价格
        /// </summary>
        public double Price;
        /// <summary>
        /// 数量
        /// </summary>
        public int Volume;
        /// <summary>
        /// 成交时期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 成交时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 成交类型
        /// </summary>
        public EnumTradeTypeType TradeType;
        /// <summary>
        /// 成交价来源
        /// </summary>
        public EnumPriceSourceType PriceSource;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
        /// <summary>
        /// 本地报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderLocalID;
        /// <summary>
        /// 结算会员编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClearingPartID;
        /// <summary>
        /// 业务单元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string BusinessUnit;
        /// <summary>
        /// 序号
        /// </summary>
        public int SequenceNo;
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID;
        /// <summary>
        /// 经纪公司报单编号
        /// </summary>
        public int BrokerOrderSeq;
    }

    /// <summary>
    /// 用户会话
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcUserSessionField
    {
        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID;
        /// <summary>
        /// 会话编号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 登录日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string LoginDate;
        /// <summary>
        /// 登录时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string LoginTime;
        /// <summary>
        /// IP地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string IPAddress;
        /// <summary>
        /// 用户端产品信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string UserProductInfo;
        /// <summary>
        /// 接口端产品信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string InterfaceProductInfo;
        /// <summary>
        /// 协议信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ProtocolInfo;
        /// <summary>
        /// Mac地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string MacAddress;
    }

    /// <summary>
    /// 查询最大报单数量
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQueryMaxOrderVolumeField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 买卖方向
        /// </summary>
        public EnumDirectionType Direction;
        /// <summary>
        /// 开平标志
        /// </summary>
        public EnumOffsetFlagType OffsetFlag;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
        /// <summary>
        /// 最大允许报单数量
        /// </summary>
        public int MaxVolume;
    }

    /// <summary>
    /// 投资者结算结果确认信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSettlementInfoConfirmField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 确认日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ConfirmDate;
        /// <summary>
        /// 确认时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ConfirmTime;
    }

    /// <summary>
    /// 出入金同步
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSyncDepositField
    {
        /// <summary>
        /// 出入金流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string DepositSeqNo;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 入金金额
        /// </summary>
        public double Deposit;
        /// <summary>
        /// 是否强制进行
        /// </summary>
        public int IsForce;
    }

    /// <summary>
    /// 经纪公司同步
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcBrokerSyncField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
    }

    /// <summary>
    /// 正在同步中的投资者
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSyncingInvestorField
    {
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者分组代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorGroupID;
        /// <summary>
        /// 投资者名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string InvestorName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdentifiedCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 是否活跃
        /// </summary>
        public int IsActive;
        /// <summary>
        /// 联系电话
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Telephone;
        /// <summary>
        /// 通讯地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 101)]
        public string Address;
        /// <summary>
        /// 开户日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string OpenDate;
        /// <summary>
        /// 手机
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Mobile;
    }

    /// <summary>
    /// 正在同步中的交易代码
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSyncingTradingCodeField
    {
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 客户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientID;
        /// <summary>
        /// 是否活跃
        /// </summary>
        public int IsActive;
        /// <summary>
        /// 交易编码类型
        /// </summary>
        public EnumClientIDTypeType ClientIDType;
    }

    /// <summary>
    /// 正在同步中的投资者分组
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSyncingInvestorGroupField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者分组代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorGroupID;
        /// <summary>
        /// 投资者分组名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string InvestorGroupName;
    }

    /// <summary>
    /// 正在同步中的交易账号
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSyncingTradingAccountField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 上次质押金额
        /// </summary>
        public double PreMortgage;
        /// <summary>
        /// 上次信用额度
        /// </summary>
        public double PreCredit;
        /// <summary>
        /// 上次存款额
        /// </summary>
        public double PreDeposit;
        /// <summary>
        /// 上次结算准备金
        /// </summary>
        public double PreBalance;
        /// <summary>
        /// 上次占用的保证金
        /// </summary>
        public double PreMargin;
        /// <summary>
        /// 利息基数
        /// </summary>
        public double InterestBase;
        /// <summary>
        /// 利息收入
        /// </summary>
        public double Interest;
        /// <summary>
        /// 入金金额
        /// </summary>
        public double Deposit;
        /// <summary>
        /// 出金金额
        /// </summary>
        public double Withdraw;
        /// <summary>
        /// 冻结的保证金
        /// </summary>
        public double FrozenMargin;
        /// <summary>
        /// 冻结的资金
        /// </summary>
        public double FrozenCash;
        /// <summary>
        /// 冻结的手续费
        /// </summary>
        public double FrozenCommission;
        /// <summary>
        /// 当前保证金总额
        /// </summary>
        public double CurrMargin;
        /// <summary>
        /// 资金差额
        /// </summary>
        public double CashIn;
        /// <summary>
        /// 手续费
        /// </summary>
        public double Commission;
        /// <summary>
        /// 平仓盈亏
        /// </summary>
        public double CloseProfit;
        /// <summary>
        /// 持仓盈亏
        /// </summary>
        public double PositionProfit;
        /// <summary>
        /// 期货结算准备金
        /// </summary>
        public double Balance;
        /// <summary>
        /// 可用资金
        /// </summary>
        public double Available;
        /// <summary>
        /// 可取资金
        /// </summary>
        public double WithdrawQuota;
        /// <summary>
        /// 基本准备金
        /// </summary>
        public double Reserve;
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID;
        /// <summary>
        /// 信用额度
        /// </summary>
        public double Credit;
        /// <summary>
        /// 质押金额
        /// </summary>
        public double Mortgage;
        /// <summary>
        /// 交易所保证金
        /// </summary>
        public double ExchangeMargin;
        /// <summary>
        /// 投资者交割保证金
        /// </summary>
        public double DeliveryMargin;
        /// <summary>
        /// 交易所交割保证金
        /// </summary>
        public double ExchangeDeliveryMargin;
    }

    /// <summary>
    /// 正在同步中的投资者持仓
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSyncingInvestorPositionField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 持仓多空方向
        /// </summary>
        public EnumPosiDirectionType PosiDirection;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
        /// <summary>
        /// 持仓日期
        /// </summary>
        public EnumPositionDateType PositionDate;
        /// <summary>
        /// 上日持仓
        /// </summary>
        public int YdPosition;
        /// <summary>
        /// 今日持仓
        /// </summary>
        public int Position;
        /// <summary>
        /// 多头冻结
        /// </summary>
        public int LongFrozen;
        /// <summary>
        /// 空头冻结
        /// </summary>
        public int ShortFrozen;
        /// <summary>
        /// 开仓冻结金额
        /// </summary>
        public double LongFrozenAmount;
        /// <summary>
        /// 开仓冻结金额
        /// </summary>
        public double ShortFrozenAmount;
        /// <summary>
        /// 开仓量
        /// </summary>
        public int OpenVolume;
        /// <summary>
        /// 平仓量
        /// </summary>
        public int CloseVolume;
        /// <summary>
        /// 开仓金额
        /// </summary>
        public double OpenAmount;
        /// <summary>
        /// 平仓金额
        /// </summary>
        public double CloseAmount;
        /// <summary>
        /// 持仓成本
        /// </summary>
        public double PositionCost;
        /// <summary>
        /// 上次占用的保证金
        /// </summary>
        public double PreMargin;
        /// <summary>
        /// 占用的保证金
        /// </summary>
        public double UseMargin;
        /// <summary>
        /// 冻结的保证金
        /// </summary>
        public double FrozenMargin;
        /// <summary>
        /// 冻结的资金
        /// </summary>
        public double FrozenCash;
        /// <summary>
        /// 冻结的手续费
        /// </summary>
        public double FrozenCommission;
        /// <summary>
        /// 资金差额
        /// </summary>
        public double CashIn;
        /// <summary>
        /// 手续费
        /// </summary>
        public double Commission;
        /// <summary>
        /// 平仓盈亏
        /// </summary>
        public double CloseProfit;
        /// <summary>
        /// 持仓盈亏
        /// </summary>
        public double PositionProfit;
        /// <summary>
        /// 上次结算价
        /// </summary>
        public double PreSettlementPrice;
        /// <summary>
        /// 本次结算价
        /// </summary>
        public double SettlementPrice;
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID;
        /// <summary>
        /// 开仓成本
        /// </summary>
        public double OpenCost;
        /// <summary>
        /// 交易所保证金
        /// </summary>
        public double ExchangeMargin;
        /// <summary>
        /// 组合成交形成的持仓
        /// </summary>
        public int CombPosition;
        /// <summary>
        /// 组合多头冻结
        /// </summary>
        public int CombLongFrozen;
        /// <summary>
        /// 组合空头冻结
        /// </summary>
        public int CombShortFrozen;
        /// <summary>
        /// 逐日盯市平仓盈亏
        /// </summary>
        public double CloseProfitByDate;
        /// <summary>
        /// 逐笔对冲平仓盈亏
        /// </summary>
        public double CloseProfitByTrade;
        /// <summary>
        /// 今日持仓
        /// </summary>
        public int TodayPosition;
        /// <summary>
        /// 保证金率
        /// </summary>
        public double MarginRateByMoney;
        /// <summary>
        /// 保证金率(按手数)
        /// </summary>
        public double MarginRateByVolume;
    }

    /// <summary>
    /// 正在同步中的合约保证金率
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSyncingInstrumentMarginRateField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 投资者范围
        /// </summary>
        public EnumInvestorRangeType InvestorRange;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
        /// <summary>
        /// 多头保证金率
        /// </summary>
        public double LongMarginRatioByMoney;
        /// <summary>
        /// 多头保证金费
        /// </summary>
        public double LongMarginRatioByVolume;
        /// <summary>
        /// 空头保证金率
        /// </summary>
        public double ShortMarginRatioByMoney;
        /// <summary>
        /// 空头保证金费
        /// </summary>
        public double ShortMarginRatioByVolume;
        /// <summary>
        /// 是否相对交易所收取
        /// </summary>
        public int IsRelative;
    }

    /// <summary>
    /// 正在同步中的合约手续费率
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSyncingInstrumentCommissionRateField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 投资者范围
        /// </summary>
        public EnumInvestorRangeType InvestorRange;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 开仓手续费率
        /// </summary>
        public double OpenRatioByMoney;
        /// <summary>
        /// 开仓手续费
        /// </summary>
        public double OpenRatioByVolume;
        /// <summary>
        /// 平仓手续费率
        /// </summary>
        public double CloseRatioByMoney;
        /// <summary>
        /// 平仓手续费
        /// </summary>
        public double CloseRatioByVolume;
        /// <summary>
        /// 平今手续费率
        /// </summary>
        public double CloseTodayRatioByMoney;
        /// <summary>
        /// 平今手续费
        /// </summary>
        public double CloseTodayRatioByVolume;
    }

    /// <summary>
    /// 正在同步中的合约交易权限
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSyncingInstrumentTradingRightField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 投资者范围
        /// </summary>
        public EnumInvestorRangeType InvestorRange;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 交易权限
        /// </summary>
        public EnumTradingRightType TradingRight;
    }

    /// <summary>
    /// 查询报单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryOrderField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string OrderSysID;
        /// <summary>
        /// 开始时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string InsertTimeStart;
        /// <summary>
        /// 结束时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string InsertTimeEnd;
    }

    /// <summary>
    /// 查询成交
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryTradeField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 成交编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TradeID;
        /// <summary>
        /// 开始时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTimeStart;
        /// <summary>
        /// 结束时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTimeEnd;
    }

    /// <summary>
    /// 查询投资者持仓
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryInvestorPositionField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
    }

    /// <summary>
    /// 查询资金账户
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryTradingAccountField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
    }

    /// <summary>
    /// 查询投资者
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryInvestorField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
    }

    /// <summary>
    /// 查询交易编码
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryTradingCodeField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 客户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientID;
        /// <summary>
        /// 交易编码类型
        /// </summary>
        public EnumClientIDTypeType ClientIDType;
    }

    /// <summary>
    /// 查询交易编码
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryInvestorGroupField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
    }

    /// <summary>
    /// 查询交易编码
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryInstrumentMarginRateField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
    }

    /// <summary>
    /// 查询交易编码
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryInstrumentCommissionRateField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
    }

    /// <summary>
    /// 查询交易编码
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryInstrumentTradingRightField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
    }

    /// <summary>
    /// 查询经纪公司
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryBrokerField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
    }

    /// <summary>
    /// 查询交易员
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryTraderField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
    }

    /// <summary>
    /// 查询经纪公司会员代码
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryPartBrokerField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
    }

    /// <summary>
    /// 查询管理用户功能权限
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQrySuperUserFunctionField
    {
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
    }

    /// <summary>
    /// 查询用户会话
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryUserSessionField
    {
        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID;
        /// <summary>
        /// 会话编号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
    }

    /// <summary>
    /// 查询前置状态
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryFrontStatusField
    {
        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID;
    }

    /// <summary>
    /// 查询交易所报单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryExchangeOrderField
    {
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 客户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientID;
        /// <summary>
        /// 合约在交易所的代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ExchangeInstID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
    }

    /// <summary>
    /// 查询报单操作
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryOrderActionField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
    }

    /// <summary>
    /// 查询交易所报单操作
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryExchangeOrderActionField
    {
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 客户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
    }

    /// <summary>
    /// 查询管理用户
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQrySuperUserField
    {
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
    }

    /// <summary>
    /// 查询交易所
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryExchangeField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
    }

    /// <summary>
    /// 查询产品
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryProductField
    {
        /// <summary>
        /// 产品代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ProductID;
    }

    /// <summary>
    /// 查询合约
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryInstrumentField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 合约在交易所的代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ExchangeInstID;
        /// <summary>
        /// 产品代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ProductID;
    }

    /// <summary>
    /// 查询行情
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryDepthMarketDataField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
    }

    /// <summary>
    /// 查询经纪公司用户
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryBrokerUserField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
    }

    /// <summary>
    /// 查询经纪公司用户权限
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryBrokerUserFunctionField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
    }

    /// <summary>
    /// 查询交易员报盘机
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryTraderOfferField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
    }

    /// <summary>
    /// 查询出入金流水
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQrySyncDepositField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 出入金流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string DepositSeqNo;
    }

    /// <summary>
    /// 查询投资者结算结果
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQrySettlementInfoField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
    }

    /// <summary>
    /// 查询报单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryHisOrderField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string OrderSysID;
        /// <summary>
        /// 开始时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string InsertTimeStart;
        /// <summary>
        /// 结束时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string InsertTimeEnd;
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID;
    }

    /// <summary>
    /// 市场行情
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcMarketDataField
    {
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 合约在交易所的代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ExchangeInstID;
        /// <summary>
        /// 最新价
        /// </summary>
        public double LastPrice;
        /// <summary>
        /// 上次结算价
        /// </summary>
        public double PreSettlementPrice;
        /// <summary>
        /// 昨收盘
        /// </summary>
        public double PreClosePrice;
        /// <summary>
        /// 昨持仓量
        /// </summary>
        public double PreOpenInterest;
        /// <summary>
        /// 今开盘
        /// </summary>
        public double OpenPrice;
        /// <summary>
        /// 最高价
        /// </summary>
        public double HighestPrice;
        /// <summary>
        /// 最低价
        /// </summary>
        public double LowestPrice;
        /// <summary>
        /// 数量
        /// </summary>
        public int Volume;
        /// <summary>
        /// 成交金额
        /// </summary>
        public double Turnover;
        /// <summary>
        /// 持仓量
        /// </summary>
        public double OpenInterest;
        /// <summary>
        /// 今收盘
        /// </summary>
        public double ClosePrice;
        /// <summary>
        /// 本次结算价
        /// </summary>
        public double SettlementPrice;
        /// <summary>
        /// 涨停板价
        /// </summary>
        public double UpperLimitPrice;
        /// <summary>
        /// 跌停板价
        /// </summary>
        public double LowerLimitPrice;
        /// <summary>
        /// 昨虚实度
        /// </summary>
        public double PreDelta;
        /// <summary>
        /// 今虚实度
        /// </summary>
        public double CurrDelta;
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string UpdateTime;
        /// <summary>
        /// 最后修改毫秒
        /// </summary>
        public int UpdateMillisec;
    }

    /// <summary>
    /// 行情基础属性
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcMarketDataBaseField
    {
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 上次结算价
        /// </summary>
        public double PreSettlementPrice;
        /// <summary>
        /// 昨收盘
        /// </summary>
        public double PreClosePrice;
        /// <summary>
        /// 昨持仓量
        /// </summary>
        public double PreOpenInterest;
        /// <summary>
        /// 昨虚实度
        /// </summary>
        public double PreDelta;
    }

    /// <summary>
    /// 行情静态属性
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcMarketDataStaticField
    {
        /// <summary>
        /// 今开盘
        /// </summary>
        public double OpenPrice;
        /// <summary>
        /// 最高价
        /// </summary>
        public double HighestPrice;
        /// <summary>
        /// 最低价
        /// </summary>
        public double LowestPrice;
        /// <summary>
        /// 今收盘
        /// </summary>
        public double ClosePrice;
        /// <summary>
        /// 涨停板价
        /// </summary>
        public double UpperLimitPrice;
        /// <summary>
        /// 跌停板价
        /// </summary>
        public double LowerLimitPrice;
        /// <summary>
        /// 本次结算价
        /// </summary>
        public double SettlementPrice;
        /// <summary>
        /// 今虚实度
        /// </summary>
        public double CurrDelta;
    }

    /// <summary>
    /// 行情最新成交属性
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcMarketDataLastMatchField
    {
        /// <summary>
        /// 最新价
        /// </summary>
        public double LastPrice;
        /// <summary>
        /// 数量
        /// </summary>
        public int Volume;
        /// <summary>
        /// 成交金额
        /// </summary>
        public double Turnover;
        /// <summary>
        /// 持仓量
        /// </summary>
        public double OpenInterest;
    }

    /// <summary>
    /// 行情最优价属性
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcMarketDataBestPriceField
    {
        /// <summary>
        /// 申买价一
        /// </summary>
        public double BidPrice1;
        /// <summary>
        /// 申买量一
        /// </summary>
        public int BidVolume1;
        /// <summary>
        /// 申卖价一
        /// </summary>
        public double AskPrice1;
        /// <summary>
        /// 申卖量一
        /// </summary>
        public int AskVolume1;
    }

    /// <summary>
    /// 行情申买二、三属性
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcMarketDataBid23Field
    {
        /// <summary>
        /// 申买价二
        /// </summary>
        public double BidPrice2;
        /// <summary>
        /// 申买量二
        /// </summary>
        public int BidVolume2;
        /// <summary>
        /// 申买价三
        /// </summary>
        public double BidPrice3;
        /// <summary>
        /// 申买量三
        /// </summary>
        public int BidVolume3;
    }

    /// <summary>
    /// 行情申卖二、三属性
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcMarketDataAsk23Field
    {
        /// <summary>
        /// 申卖价二
        /// </summary>
        public double AskPrice2;
        /// <summary>
        /// 申卖量二
        /// </summary>
        public int AskVolume2;
        /// <summary>
        /// 申卖价三
        /// </summary>
        public double AskPrice3;
        /// <summary>
        /// 申卖量三
        /// </summary>
        public int AskVolume3;
    }

    /// <summary>
    /// 行情申买四、五属性
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcMarketDataBid45Field
    {
        /// <summary>
        /// 申买价四
        /// </summary>
        public double BidPrice4;
        /// <summary>
        /// 申买量四
        /// </summary>
        public int BidVolume4;
        /// <summary>
        /// 申买价五
        /// </summary>
        public double BidPrice5;
        /// <summary>
        /// 申买量五
        /// </summary>
        public int BidVolume5;
    }

    /// <summary>
    /// 行情申卖四、五属性
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcMarketDataAsk45Field
    {
        /// <summary>
        /// 申卖价四
        /// </summary>
        public double AskPrice4;
        /// <summary>
        /// 申卖量四
        /// </summary>
        public int AskVolume4;
        /// <summary>
        /// 申卖价五
        /// </summary>
        public double AskPrice5;
        /// <summary>
        /// 申卖量五
        /// </summary>
        public int AskVolume5;
    }

    /// <summary>
    /// 行情更新时间属性
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcMarketDataUpdateTimeField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string UpdateTime;
        /// <summary>
        /// 最后修改毫秒
        /// </summary>
        public int UpdateMillisec;
    }

    /// <summary>
    /// 指定的合约
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSpecificInstrumentField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
    }

    /// <summary>
    /// 合约状态
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInstrumentStatusField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 合约在交易所的代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ExchangeInstID;
        /// <summary>
        /// 结算组代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string SettlementGroupID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 合约交易状态
        /// </summary>
        public EnumInstrumentStatusType InstrumentStatus;
        /// <summary>
        /// 交易阶段编号
        /// </summary>
        public int TradingSegmentSN;
        /// <summary>
        /// 进入本状态时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string EnterTime;
        /// <summary>
        /// 进入本状态原因
        /// </summary>
        public EnumInstStatusEnterReasonType EnterReason;
    }

    /// <summary>
    /// 查询合约状态
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryInstrumentStatusField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 合约在交易所的代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ExchangeInstID;
    }

    /// <summary>
    /// 投资者账户
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInvestorAccountField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
    }

    /// <summary>
    /// 浮动盈亏算法
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcPositionProfitAlgorithmField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 盈亏算法
        /// </summary>
        public EnumAlgorithmType Algorithm;
        /// <summary>
        /// 备注
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 161)]
        public string Memo;
    }

    /// <summary>
    /// 会员资金折扣
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcDiscountField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者范围
        /// </summary>
        public EnumInvestorRangeType InvestorRange;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 资金折扣比例
        /// </summary>
        public double Discount;
    }

    /// <summary>
    /// 查询转帐银行
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryTransferBankField
    {
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分中心代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBrchID;
    }

    /// <summary>
    /// 转帐银行
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTransferBankField
    {
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分中心代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBrchID;
        /// <summary>
        /// 银行名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 101)]
        public string BankName;
        /// <summary>
        /// 是否活跃
        /// </summary>
        public int IsActive;
    }

    /// <summary>
    /// 查询投资者持仓明细
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryInvestorPositionDetailField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
    }

    /// <summary>
    /// 投资者持仓明细
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInvestorPositionDetailField
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
        /// <summary>
        /// 买卖
        /// </summary>
        public EnumDirectionType Direction;
        /// <summary>
        /// 开仓日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string OpenDate;
        /// <summary>
        /// 成交编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TradeID;
        /// <summary>
        /// 数量
        /// </summary>
        public int Volume;
        /// <summary>
        /// 开仓价
        /// </summary>
        public double OpenPrice;
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID;
        /// <summary>
        /// 成交类型
        /// </summary>
        public EnumTradeTypeType TradeType;
        /// <summary>
        /// 组合合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string CombInstrumentID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 逐日盯市平仓盈亏
        /// </summary>
        public double CloseProfitByDate;
        /// <summary>
        /// 逐笔对冲平仓盈亏
        /// </summary>
        public double CloseProfitByTrade;
        /// <summary>
        /// 逐日盯市持仓盈亏
        /// </summary>
        public double PositionProfitByDate;
        /// <summary>
        /// 逐笔对冲持仓盈亏
        /// </summary>
        public double PositionProfitByTrade;
        /// <summary>
        /// 投资者保证金
        /// </summary>
        public double Margin;
        /// <summary>
        /// 交易所保证金
        /// </summary>
        public double ExchMargin;
        /// <summary>
        /// 保证金率
        /// </summary>
        public double MarginRateByMoney;
        /// <summary>
        /// 保证金率(按手数)
        /// </summary>
        public double MarginRateByVolume;
        /// <summary>
        /// 昨结算价
        /// </summary>
        public double LastSettlementPrice;
        /// <summary>
        /// 结算价
        /// </summary>
        public double SettlementPrice;
        /// <summary>
        /// 平仓量
        /// </summary>
        public int CloseVolume;
        /// <summary>
        /// 平仓金额
        /// </summary>
        public double CloseAmount;
    }

    /// <summary>
    /// 资金账户口令域
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTradingAccountPasswordField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
    }

    /// <summary>
    /// 交易所行情报盘机
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcMDTraderOfferField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 本地报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderLocalID;
        /// <summary>
        /// 交易所交易员连接状态
        /// </summary>
        public EnumTraderConnectStatusType TraderConnectStatus;
        /// <summary>
        /// 发出连接请求的日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ConnectRequestDate;
        /// <summary>
        /// 发出连接请求的时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ConnectRequestTime;
        /// <summary>
        /// 上次报告日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string LastReportDate;
        /// <summary>
        /// 上次报告时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string LastReportTime;
        /// <summary>
        /// 完成连接日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ConnectDate;
        /// <summary>
        /// 完成连接时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ConnectTime;
        /// <summary>
        /// 启动日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string StartDate;
        /// <summary>
        /// 启动时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string StartTime;
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
    }

    /// <summary>
    /// 查询行情报盘机
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryMDTraderOfferField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
    }

    /// <summary>
    /// 查询客户通知
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryNoticeField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
    }

    /// <summary>
    /// 客户通知
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcNoticeField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 消息正文
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 501)]
        public string Content;
        /// <summary>
        /// 经纪公司通知内容序列号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
        public string SequenceLabel;
    }

    /// <summary>
    /// 用户权限
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcUserRightField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 客户权限类型
        /// </summary>
        public EnumUserRightTypeType UserRightType;
        /// <summary>
        /// 是否禁止
        /// </summary>
        public int IsForbidden;
    }

    /// <summary>
    /// 查询结算信息确认域
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQrySettlementInfoConfirmField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
    }

    /// <summary>
    /// 装载结算信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcLoadSettlementInfoField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
    }

    /// <summary>
    /// 经纪公司可提资金算法表
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcBrokerWithdrawAlgorithmField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 可提资金算法
        /// </summary>
        public EnumAlgorithmType WithdrawAlgorithm;
        /// <summary>
        /// 资金使用率
        /// </summary>
        public double UsingRatio;
        /// <summary>
        /// 可提是否包含平仓盈利
        /// </summary>
        public EnumIncludeCloseProfitType IncludeCloseProfit;
        /// <summary>
        /// 本日无仓且无成交客户是否受可提比例限制
        /// </summary>
        public EnumAllWithoutTradeType AllWithoutTrade;
        /// <summary>
        /// 可用是否包含平仓盈利
        /// </summary>
        public EnumIncludeCloseProfitType AvailIncludeCloseProfit;
        /// <summary>
        /// 是否启用用户事件
        /// </summary>
        public int IsBrokerUserEvent;
    }

    /// <summary>
    /// 资金账户口令变更域
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTradingAccountPasswordUpdateV1Field
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 原来的口令
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string OldPassword;
        /// <summary>
        /// 新的口令
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string NewPassword;
    }

    /// <summary>
    /// 资金账户口令变更域
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTradingAccountPasswordUpdateField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 原来的口令
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string OldPassword;
        /// <summary>
        /// 新的口令
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string NewPassword;
    }

    /// <summary>
    /// 查询组合合约分腿
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryCombinationLegField
    {
        /// <summary>
        /// 组合合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string CombInstrumentID;
        /// <summary>
        /// 单腿编号
        /// </summary>
        public int LegID;
        /// <summary>
        /// 单腿合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string LegInstrumentID;
    }

    /// <summary>
    /// 查询组合合约分腿
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQrySyncStatusField
    {
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
    }

    /// <summary>
    /// 组合交易合约的单腿
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcCombinationLegField
    {
        /// <summary>
        /// 组合合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string CombInstrumentID;
        /// <summary>
        /// 单腿编号
        /// </summary>
        public int LegID;
        /// <summary>
        /// 单腿合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string LegInstrumentID;
        /// <summary>
        /// 买卖方向
        /// </summary>
        public EnumDirectionType Direction;
        /// <summary>
        /// 单腿乘数
        /// </summary>
        public int LegMultiple;
        /// <summary>
        /// 派生层数
        /// </summary>
        public int ImplyLevel;
    }

    /// <summary>
    /// 数据同步状态
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcSyncStatusField
    {
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 数据同步状态
        /// </summary>
        public EnumDataSyncStatusType DataSyncStatus;
    }

    /// <summary>
    /// 查询联系人
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryLinkManField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
    }

    /// <summary>
    /// 联系人
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcLinkManField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 联系人类型
        /// </summary>
        public EnumPersonTypeType PersonType;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdentifiedCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string PersonName;
        /// <summary>
        /// 联系电话
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Telephone;
        /// <summary>
        /// 通讯地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 101)]
        public string Address;
        /// <summary>
        /// 邮政编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string ZipCode;
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority;
    }

    /// <summary>
    /// 查询经纪公司用户事件
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryBrokerUserEventField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 用户事件类型
        /// </summary>
        public EnumUserEventTypeType UserEventType;
    }

    /// <summary>
    /// 查询经纪公司用户事件
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcBrokerUserEventField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 用户事件类型
        /// </summary>
        public EnumUserEventTypeType UserEventType;
        /// <summary>
        /// 用户事件序号
        /// </summary>
        public int EventSequenceNo;
        /// <summary>
        /// 事件发生日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string EventDate;
        /// <summary>
        /// 事件发生时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string EventTime;
        /// <summary>
        /// 用户事件信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
        public string UserEventInfo;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
    }

    /// <summary>
    /// 查询签约银行请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryContractBankField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分中心代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBrchID;
    }

    /// <summary>
    /// 查询签约银行响应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcContractBankField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分中心代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBrchID;
        /// <summary>
        /// 银行名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 101)]
        public string BankName;
    }

    /// <summary>
    /// 投资者组合持仓明细
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInvestorPositionCombineDetailField
    {
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 开仓日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string OpenDate;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 组合编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string ComTradeID;
        /// <summary>
        /// 撮合编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TradeID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
        /// <summary>
        /// 买卖
        /// </summary>
        public EnumDirectionType Direction;
        /// <summary>
        /// 持仓量
        /// </summary>
        public int TotalAmt;
        /// <summary>
        /// 投资者保证金
        /// </summary>
        public double Margin;
        /// <summary>
        /// 交易所保证金
        /// </summary>
        public double ExchMargin;
        /// <summary>
        /// 保证金率
        /// </summary>
        public double MarginRateByMoney;
        /// <summary>
        /// 保证金率(按手数)
        /// </summary>
        public double MarginRateByVolume;
        /// <summary>
        /// 单腿编号
        /// </summary>
        public int LegID;
        /// <summary>
        /// 单腿乘数
        /// </summary>
        public int LegMultiple;
        /// <summary>
        /// 组合持仓合约编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string CombInstrumentID;
    }

    /// <summary>
    /// 预埋单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcParkedOrderField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 报单引用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderRef;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 报单价格条件
        /// </summary>
        public EnumOrderPriceTypeType OrderPriceType;
        /// <summary>
        /// 买卖方向
        /// </summary>
        public EnumDirectionType Direction;
        /// <summary>
        /// 组合开平标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string CombOffsetFlag;
        /// <summary>
        /// 组合投机套保标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string CombHedgeFlag;
        /// <summary>
        /// 价格
        /// </summary>
        public double LimitPrice;
        /// <summary>
        /// 数量
        /// </summary>
        public int VolumeTotalOriginal;
        /// <summary>
        /// 有效期类型
        /// </summary>
        public EnumTimeConditionType TimeCondition;
        /// <summary>
        /// GTD日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string GTDDate;
        /// <summary>
        /// 成交量类型
        /// </summary>
        public EnumVolumeConditionType VolumeCondition;
        /// <summary>
        /// 最小成交量
        /// </summary>
        public int MinVolume;
        /// <summary>
        /// 触发条件
        /// </summary>
        public EnumContingentConditionType ContingentCondition;
        /// <summary>
        /// 止损价
        /// </summary>
        public double StopPrice;
        /// <summary>
        /// 强平原因
        /// </summary>
        public EnumForceCloseReasonType ForceCloseReason;
        /// <summary>
        /// 自动挂起标志
        /// </summary>
        public int IsAutoSuspend;
        /// <summary>
        /// 业务单元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string BusinessUnit;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 用户强评标志
        /// </summary>
        public int UserForceClose;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 预埋报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string ParkedOrderID;
        /// <summary>
        /// 用户类型
        /// </summary>
        public EnumUserTypeType UserType;
        /// <summary>
        /// 预埋单状态
        /// </summary>
        public EnumParkedOrderStatusType Status;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 输入预埋单操作
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcParkedOrderActionField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 报单操作引用
        /// </summary>
        public int OrderActionRef;
        /// <summary>
        /// 报单引用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderRef;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID;
        /// <summary>
        /// 会话编号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string OrderSysID;
        /// <summary>
        /// 操作标志
        /// </summary>
        public EnumActionFlagType ActionFlag;
        /// <summary>
        /// 价格
        /// </summary>
        public double LimitPrice;
        /// <summary>
        /// 数量变化
        /// </summary>
        public int VolumeChange;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 预埋撤单单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string ParkedOrderActionID;
        /// <summary>
        /// 用户类型
        /// </summary>
        public EnumUserTypeType UserType;
        /// <summary>
        /// 预埋撤单状态
        /// </summary>
        public EnumParkedOrderStatusType Status;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 查询预埋单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryParkedOrderField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
    }

    /// <summary>
    /// 查询预埋撤单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryParkedOrderActionField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
    }

    /// <summary>
    /// 删除预埋单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcRemoveParkedOrderField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 预埋报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string ParkedOrderID;
    }

    /// <summary>
    /// 删除预埋撤单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcRemoveParkedOrderActionField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 预埋撤单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string ParkedOrderActionID;
    }

    /// <summary>
    /// 经纪公司可提资金算法表
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcInvestorWithdrawAlgorithmField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者范围
        /// </summary>
        public EnumInvestorRangeType InvestorRange;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 可提资金比例
        /// </summary>
        public double UsingRatio;
    }

    /// <summary>
    /// 查询组合持仓明细
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryInvestorPositionCombineDetailField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 组合持仓合约编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string CombInstrumentID;
    }

    /// <summary>
    /// 成交均价
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcMarketDataAveragePriceField
    {
        /// <summary>
        /// 当日均价
        /// </summary>
        public double AveragePrice;
    }

    /// <summary>
    /// 校验投资者密码
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcVerifyInvestorPasswordField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
    }

    /// <summary>
    /// 用户IP
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcUserIPField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// IP地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string IPAddress;
        /// <summary>
        /// IP地址掩码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string IPMask;
        /// <summary>
        /// Mac地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string MacAddress;
    }

    /// <summary>
    /// 用户事件通知信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTradingNoticeInfoField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 发送时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string SendTime;
        /// <summary>
        /// 消息正文
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 501)]
        public string FieldContent;
        /// <summary>
        /// 序列系列号
        /// </summary>
        public short SequenceSeries;
        /// <summary>
        /// 序列号
        /// </summary>
        public int SequenceNo;
    }

    /// <summary>
    /// 用户事件通知
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTradingNoticeField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者范围
        /// </summary>
        public EnumInvestorRangeType InvestorRange;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 序列系列号
        /// </summary>
        public short SequenceSeries;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 发送时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string SendTime;
        /// <summary>
        /// 序列号
        /// </summary>
        public int SequenceNo;
        /// <summary>
        /// 消息正文
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 501)]
        public string FieldContent;
    }

    /// <summary>
    /// 查询交易事件通知
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryTradingNoticeField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
    }

    /// <summary>
    /// 查询错误报单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryErrOrderField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
    }

    /// <summary>
    /// 错误报单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcErrOrderField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 报单引用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderRef;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 报单价格条件
        /// </summary>
        public EnumOrderPriceTypeType OrderPriceType;
        /// <summary>
        /// 买卖方向
        /// </summary>
        public EnumDirectionType Direction;
        /// <summary>
        /// 组合开平标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string CombOffsetFlag;
        /// <summary>
        /// 组合投机套保标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string CombHedgeFlag;
        /// <summary>
        /// 价格
        /// </summary>
        public double LimitPrice;
        /// <summary>
        /// 数量
        /// </summary>
        public int VolumeTotalOriginal;
        /// <summary>
        /// 有效期类型
        /// </summary>
        public EnumTimeConditionType TimeCondition;
        /// <summary>
        /// GTD日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string GTDDate;
        /// <summary>
        /// 成交量类型
        /// </summary>
        public EnumVolumeConditionType VolumeCondition;
        /// <summary>
        /// 最小成交量
        /// </summary>
        public int MinVolume;
        /// <summary>
        /// 触发条件
        /// </summary>
        public EnumContingentConditionType ContingentCondition;
        /// <summary>
        /// 止损价
        /// </summary>
        public double StopPrice;
        /// <summary>
        /// 强平原因
        /// </summary>
        public EnumForceCloseReasonType ForceCloseReason;
        /// <summary>
        /// 自动挂起标志
        /// </summary>
        public int IsAutoSuspend;
        /// <summary>
        /// 业务单元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string BusinessUnit;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 用户强评标志
        /// </summary>
        public int UserForceClose;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 查询错误报单操作
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcErrorConditionalOrderField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 报单引用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderRef;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 报单价格条件
        /// </summary>
        public EnumOrderPriceTypeType OrderPriceType;
        /// <summary>
        /// 买卖方向
        /// </summary>
        public EnumDirectionType Direction;
        /// <summary>
        /// 组合开平标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string CombOffsetFlag;
        /// <summary>
        /// 组合投机套保标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string CombHedgeFlag;
        /// <summary>
        /// 价格
        /// </summary>
        public double LimitPrice;
        /// <summary>
        /// 数量
        /// </summary>
        public int VolumeTotalOriginal;
        /// <summary>
        /// 有效期类型
        /// </summary>
        public EnumTimeConditionType TimeCondition;
        /// <summary>
        /// GTD日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string GTDDate;
        /// <summary>
        /// 成交量类型
        /// </summary>
        public EnumVolumeConditionType VolumeCondition;
        /// <summary>
        /// 最小成交量
        /// </summary>
        public int MinVolume;
        /// <summary>
        /// 触发条件
        /// </summary>
        public EnumContingentConditionType ContingentCondition;
        /// <summary>
        /// 止损价
        /// </summary>
        public double StopPrice;
        /// <summary>
        /// 强平原因
        /// </summary>
        public EnumForceCloseReasonType ForceCloseReason;
        /// <summary>
        /// 自动挂起标志
        /// </summary>
        public int IsAutoSuspend;
        /// <summary>
        /// 业务单元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string BusinessUnit;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 本地报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderLocalID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 客户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientID;
        /// <summary>
        /// 合约在交易所的代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string ExchangeInstID;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 报单提交状态
        /// </summary>
        public EnumOrderSubmitStatusType OrderSubmitStatus;
        /// <summary>
        /// 报单提示序号
        /// </summary>
        public int NotifySequence;
        /// <summary>
        /// 交易日
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 结算编号
        /// </summary>
        public int SettlementID;
        /// <summary>
        /// 报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string OrderSysID;
        /// <summary>
        /// 报单来源
        /// </summary>
        public EnumOrderSourceType OrderSource;
        /// <summary>
        /// 报单状态
        /// </summary>
        public EnumOrderStatusType OrderStatus;
        /// <summary>
        /// 报单类型
        /// </summary>
        public EnumOrderTypeType OrderType;
        /// <summary>
        /// 今成交数量
        /// </summary>
        public int VolumeTraded;
        /// <summary>
        /// 剩余数量
        /// </summary>
        public int VolumeTotal;
        /// <summary>
        /// 报单日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string InsertDate;
        /// <summary>
        /// 委托时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string InsertTime;
        /// <summary>
        /// 激活时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ActiveTime;
        /// <summary>
        /// 挂起时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string SuspendTime;
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string UpdateTime;
        /// <summary>
        /// 撤销时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string CancelTime;
        /// <summary>
        /// 最后修改交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string ActiveTraderID;
        /// <summary>
        /// 结算会员编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClearingPartID;
        /// <summary>
        /// 序号
        /// </summary>
        public int SequenceNo;
        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID;
        /// <summary>
        /// 会话编号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 用户端产品信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string UserProductInfo;
        /// <summary>
        /// 状态信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string StatusMsg;
        /// <summary>
        /// 用户强评标志
        /// </summary>
        public int UserForceClose;
        /// <summary>
        /// 操作用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string ActiveUserID;
        /// <summary>
        /// 经纪公司报单编号
        /// </summary>
        public int BrokerOrderSeq;
        /// <summary>
        /// 相关报单
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string RelativeOrderSysID;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 查询错误报单操作
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryErrOrderActionField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
    }

    /// <summary>
    /// 错误报单操作
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcErrOrderActionField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 报单操作引用
        /// </summary>
        public int OrderActionRef;
        /// <summary>
        /// 报单引用
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderRef;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 前置编号
        /// </summary>
        public int FrontID;
        /// <summary>
        /// 会话编号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string OrderSysID;
        /// <summary>
        /// 操作标志
        /// </summary>
        public EnumActionFlagType ActionFlag;
        /// <summary>
        /// 价格
        /// </summary>
        public double LimitPrice;
        /// <summary>
        /// 数量变化
        /// </summary>
        public int VolumeChange;
        /// <summary>
        /// 操作日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ActionDate;
        /// <summary>
        /// 操作时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ActionTime;
        /// <summary>
        /// 交易所交易员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string TraderID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 本地报单编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string OrderLocalID;
        /// <summary>
        /// 操作本地编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string ActionLocalID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 客户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ClientID;
        /// <summary>
        /// 业务单元
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string BusinessUnit;
        /// <summary>
        /// 报单操作状态
        /// </summary>
        public EnumOrderActionStatusType OrderActionStatus;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 状态信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string StatusMsg;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 查询交易所状态
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryExchangeSequenceField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
    }

    /// <summary>
    /// 交易所状态
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcExchangeSequenceField
    {
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 序号
        /// </summary>
        public int SequenceNo;
        /// <summary>
        /// 合约交易状态
        /// </summary>
        public EnumInstrumentStatusType MarketStatus;
    }

    /// <summary>
    /// 根据价格查询最大报单数量
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQueryMaxOrderVolumeWithPriceField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 买卖方向
        /// </summary>
        public EnumDirectionType Direction;
        /// <summary>
        /// 开平标志
        /// </summary>
        public EnumOffsetFlagType OffsetFlag;
        /// <summary>
        /// 投机套保标志
        /// </summary>
        public EnumHedgeFlagType HedgeFlag;
        /// <summary>
        /// 最大允许报单数量
        /// </summary>
        public int MaxVolume;
        /// <summary>
        /// 报单价格
        /// </summary>
        public double Price;
    }

    /// <summary>
    /// 查询经纪公司交易参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryBrokerTradingParamsField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
    }

    /// <summary>
    /// 经纪公司交易参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcBrokerTradingParamsField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 保证金价格类型
        /// </summary>
        public EnumMarginPriceTypeType MarginPriceType;
        /// <summary>
        /// 盈亏算法
        /// </summary>
        public EnumAlgorithmType Algorithm;
        /// <summary>
        /// 可用是否包含平仓盈利
        /// </summary>
        public EnumIncludeCloseProfitType AvailIncludeCloseProfit;
    }

    /// <summary>
    /// 查询经纪公司交易算法
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryBrokerTradingAlgosField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
    }

    /// <summary>
    /// 经纪公司交易算法
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcBrokerTradingAlgosField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;
        /// <summary>
        /// 持仓处理算法编号
        /// </summary>
        public EnumHandlePositionAlgoIDType HandlePositionAlgoID;
        /// <summary>
        /// 寻找保证金率算法编号
        /// </summary>
        public EnumFindMarginRateAlgoIDType FindMarginRateAlgoID;
        /// <summary>
        /// 资金处理算法编号
        /// </summary>
        public EnumHandleTradingAccountAlgoIDType HandleTradingAccountAlgoID;
    }

    /// <summary>
    /// 查询经纪公司资金
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQueryBrokerDepositField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
    }

    /// <summary>
    /// 经纪公司资金
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcBrokerDepositField
    {
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 会员代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;
        /// <summary>
        /// 上次结算准备金
        /// </summary>
        public double PreBalance;
        /// <summary>
        /// 当前保证金总额
        /// </summary>
        public double CurrMargin;
        /// <summary>
        /// 平仓盈亏
        /// </summary>
        public double CloseProfit;
        /// <summary>
        /// 期货结算准备金
        /// </summary>
        public double Balance;
        /// <summary>
        /// 入金金额
        /// </summary>
        public double Deposit;
        /// <summary>
        /// 出金金额
        /// </summary>
        public double Withdraw;
        /// <summary>
        /// 可提资金
        /// </summary>
        public double Available;
        /// <summary>
        /// 基本准备金
        /// </summary>
        public double Reserve;
        /// <summary>
        /// 冻结的保证金
        /// </summary>
        public double FrozenMargin;
    }

    /// <summary>
    /// 查询保证金监管系统经纪公司密钥
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryCFMMCBrokerKeyField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
    }

    /// <summary>
    /// 保证金监管系统经纪公司密钥
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcCFMMCBrokerKeyField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 经纪公司统一编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 密钥生成日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string CreateDate;
        /// <summary>
        /// 密钥生成时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string CreateTime;
        /// <summary>
        /// 密钥编号
        /// </summary>
        public int KeyID;
        /// <summary>
        /// 动态密钥
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string CurrentKey;
        /// <summary>
        /// 动态密钥类型
        /// </summary>
        public EnumCFMMCKeyKindType KeyKind;
    }

    /// <summary>
    /// 保证金监管系统经纪公司资金账户密钥
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcCFMMCTradingAccountKeyField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 经纪公司统一编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string ParticipantID;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 密钥编号
        /// </summary>
        public int KeyID;
        /// <summary>
        /// 动态密钥
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string CurrentKey;
    }

    /// <summary>
    /// 请求查询保证金监管系统经纪公司资金账户密钥
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryCFMMCTradingAccountKeyField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
    }

    /// <summary>
    /// 用户动态令牌参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcBrokerUserOTPParamField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 动态令牌提供商
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
        public string OTPVendorsID;
        /// <summary>
        /// 动态令牌序列号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string SerialNumber;
        /// <summary>
        /// 令牌密钥
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string AuthKey;
        /// <summary>
        /// 漂移值
        /// </summary>
        public int LastDrift;
        /// <summary>
        /// 成功值
        /// </summary>
        public int LastSuccess;
        /// <summary>
        /// 动态令牌类型
        /// </summary>
        public EnumOTPTypeType OTPType;
    }

    /// <summary>
    /// 手工同步用户动态令牌
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcManualSyncBrokerUserOTPField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 用户代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 动态令牌类型
        /// </summary>
        public EnumOTPTypeType OTPType;
        /// <summary>
        /// 第一个动态密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string FirstOTP;
        /// <summary>
        /// 第二个动态密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string SecondOTP;
    }

    /// <summary>
    /// 转帐开户请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcReqOpenAccountField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 客户姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string CustomerName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 性别
        /// </summary>
        public EnumGenderType Gender;
        /// <summary>
        /// 国家代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string CountryCode;
        /// <summary>
        /// 客户类型
        /// </summary>
        public EnumCustTypeType CustType;
        /// <summary>
        /// 地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 101)]
        public string Address;
        /// <summary>
        /// 邮编
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string ZipCode;
        /// <summary>
        /// 电话号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Telephone;
        /// <summary>
        /// 手机
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string MobilePhone;
        /// <summary>
        /// 传真
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Fax;
        /// <summary>
        /// 电子邮件
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string EMail;
        /// <summary>
        /// 资金账户状态
        /// </summary>
        public EnumMoneyAccountStatusType MoneyAccountStatus;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankPassWord;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 验证客户证件号码标志
        /// </summary>
        public EnumYesNoIndicatorType VerifyCertNoFlag;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 汇钞标志
        /// </summary>
        public EnumCashExchangeCodeType CashExchangeCode;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 银行帐号类型
        /// </summary>
        public EnumBankAccTypeType BankAccType;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货单位帐号类型
        /// </summary>
        public EnumBankAccTypeType BankSecuAccType;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 期货单位帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankSecuAcc;
        /// <summary>
        /// 银行密码标志
        /// </summary>
        public EnumPwdFlagType BankPwdFlag;
        /// <summary>
        /// 期货资金密码核对标志
        /// </summary>
        public EnumPwdFlagType SecuPwdFlag;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
    }

    /// <summary>
    /// 转帐销户请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcReqCancelAccountField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 客户姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string CustomerName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 性别
        /// </summary>
        public EnumGenderType Gender;
        /// <summary>
        /// 国家代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string CountryCode;
        /// <summary>
        /// 客户类型
        /// </summary>
        public EnumCustTypeType CustType;
        /// <summary>
        /// 地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 101)]
        public string Address;
        /// <summary>
        /// 邮编
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string ZipCode;
        /// <summary>
        /// 电话号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Telephone;
        /// <summary>
        /// 手机
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string MobilePhone;
        /// <summary>
        /// 传真
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Fax;
        /// <summary>
        /// 电子邮件
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string EMail;
        /// <summary>
        /// 资金账户状态
        /// </summary>
        public EnumMoneyAccountStatusType MoneyAccountStatus;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankPassWord;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 验证客户证件号码标志
        /// </summary>
        public EnumYesNoIndicatorType VerifyCertNoFlag;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 汇钞标志
        /// </summary>
        public EnumCashExchangeCodeType CashExchangeCode;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 银行帐号类型
        /// </summary>
        public EnumBankAccTypeType BankAccType;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货单位帐号类型
        /// </summary>
        public EnumBankAccTypeType BankSecuAccType;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 期货单位帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankSecuAcc;
        /// <summary>
        /// 银行密码标志
        /// </summary>
        public EnumPwdFlagType BankPwdFlag;
        /// <summary>
        /// 期货资金密码核对标志
        /// </summary>
        public EnumPwdFlagType SecuPwdFlag;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
    }

    /// <summary>
    /// 变更银行账户请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcReqChangeAccountField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 客户姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string CustomerName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 性别
        /// </summary>
        public EnumGenderType Gender;
        /// <summary>
        /// 国家代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string CountryCode;
        /// <summary>
        /// 客户类型
        /// </summary>
        public EnumCustTypeType CustType;
        /// <summary>
        /// 地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 101)]
        public string Address;
        /// <summary>
        /// 邮编
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string ZipCode;
        /// <summary>
        /// 电话号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Telephone;
        /// <summary>
        /// 手机
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string MobilePhone;
        /// <summary>
        /// 传真
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Fax;
        /// <summary>
        /// 电子邮件
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string EMail;
        /// <summary>
        /// 资金账户状态
        /// </summary>
        public EnumMoneyAccountStatusType MoneyAccountStatus;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankPassWord;
        /// <summary>
        /// 新银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string NewBankAccount;
        /// <summary>
        /// 新银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string NewBankPassWord;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 银行帐号类型
        /// </summary>
        public EnumBankAccTypeType BankAccType;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 验证客户证件号码标志
        /// </summary>
        public EnumYesNoIndicatorType VerifyCertNoFlag;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 银行密码标志
        /// </summary>
        public EnumPwdFlagType BankPwdFlag;
        /// <summary>
        /// 期货资金密码核对标志
        /// </summary>
        public EnumPwdFlagType SecuPwdFlag;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
    }

    /// <summary>
    /// 转账请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcReqTransferField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 客户姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string CustomerName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 客户类型
        /// </summary>
        public EnumCustTypeType CustType;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankPassWord;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 期货公司流水号
        /// </summary>
        public int FutureSerial;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 验证客户证件号码标志
        /// </summary>
        public EnumYesNoIndicatorType VerifyCertNoFlag;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 转帐金额
        /// </summary>
        public double TradeAmount;
        /// <summary>
        /// 期货可取金额
        /// </summary>
        public double FutureFetchAmount;
        /// <summary>
        /// 费用支付标志
        /// </summary>
        public EnumFeePayFlagType FeePayFlag;
        /// <summary>
        /// 应收客户费用
        /// </summary>
        public double CustFee;
        /// <summary>
        /// 应收期货公司费用
        /// </summary>
        public double BrokerFee;
        /// <summary>
        /// 发送方给接收方的消息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Message;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 银行帐号类型
        /// </summary>
        public EnumBankAccTypeType BankAccType;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货单位帐号类型
        /// </summary>
        public EnumBankAccTypeType BankSecuAccType;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 期货单位帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankSecuAcc;
        /// <summary>
        /// 银行密码标志
        /// </summary>
        public EnumPwdFlagType BankPwdFlag;
        /// <summary>
        /// 期货资金密码核对标志
        /// </summary>
        public EnumPwdFlagType SecuPwdFlag;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
        /// <summary>
        /// 转账交易状态
        /// </summary>
        public EnumTransferStatusType TransferStatus;
    }

    /// <summary>
    /// 银行发起银行资金转期货响应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcRspTransferField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 客户姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string CustomerName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 客户类型
        /// </summary>
        public EnumCustTypeType CustType;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankPassWord;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 期货公司流水号
        /// </summary>
        public int FutureSerial;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 验证客户证件号码标志
        /// </summary>
        public EnumYesNoIndicatorType VerifyCertNoFlag;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 转帐金额
        /// </summary>
        public double TradeAmount;
        /// <summary>
        /// 期货可取金额
        /// </summary>
        public double FutureFetchAmount;
        /// <summary>
        /// 费用支付标志
        /// </summary>
        public EnumFeePayFlagType FeePayFlag;
        /// <summary>
        /// 应收客户费用
        /// </summary>
        public double CustFee;
        /// <summary>
        /// 应收期货公司费用
        /// </summary>
        public double BrokerFee;
        /// <summary>
        /// 发送方给接收方的消息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Message;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 银行帐号类型
        /// </summary>
        public EnumBankAccTypeType BankAccType;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货单位帐号类型
        /// </summary>
        public EnumBankAccTypeType BankSecuAccType;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 期货单位帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankSecuAcc;
        /// <summary>
        /// 银行密码标志
        /// </summary>
        public EnumPwdFlagType BankPwdFlag;
        /// <summary>
        /// 期货资金密码核对标志
        /// </summary>
        public EnumPwdFlagType SecuPwdFlag;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
        /// <summary>
        /// 转账交易状态
        /// </summary>
        public EnumTransferStatusType TransferStatus;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 冲正请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcReqRepealField
    {
        /// <summary>
        /// 冲正时间间隔
        /// </summary>
        public int RepealTimeInterval;
        /// <summary>
        /// 已经冲正次数
        /// </summary>
        public int RepealedTimes;
        /// <summary>
        /// 银行冲正标志
        /// </summary>
        public EnumBankRepealFlagType BankRepealFlag;
        /// <summary>
        /// 期商冲正标志
        /// </summary>
        public EnumBrokerRepealFlagType BrokerRepealFlag;
        /// <summary>
        /// 被冲正平台流水号
        /// </summary>
        public int PlateRepealSerial;
        /// <summary>
        /// 被冲正银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankRepealSerial;
        /// <summary>
        /// 被冲正期货流水号
        /// </summary>
        public int FutureRepealSerial;
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 客户姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string CustomerName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 客户类型
        /// </summary>
        public EnumCustTypeType CustType;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankPassWord;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 期货公司流水号
        /// </summary>
        public int FutureSerial;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 验证客户证件号码标志
        /// </summary>
        public EnumYesNoIndicatorType VerifyCertNoFlag;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 转帐金额
        /// </summary>
        public double TradeAmount;
        /// <summary>
        /// 期货可取金额
        /// </summary>
        public double FutureFetchAmount;
        /// <summary>
        /// 费用支付标志
        /// </summary>
        public EnumFeePayFlagType FeePayFlag;
        /// <summary>
        /// 应收客户费用
        /// </summary>
        public double CustFee;
        /// <summary>
        /// 应收期货公司费用
        /// </summary>
        public double BrokerFee;
        /// <summary>
        /// 发送方给接收方的消息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Message;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 银行帐号类型
        /// </summary>
        public EnumBankAccTypeType BankAccType;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货单位帐号类型
        /// </summary>
        public EnumBankAccTypeType BankSecuAccType;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 期货单位帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankSecuAcc;
        /// <summary>
        /// 银行密码标志
        /// </summary>
        public EnumPwdFlagType BankPwdFlag;
        /// <summary>
        /// 期货资金密码核对标志
        /// </summary>
        public EnumPwdFlagType SecuPwdFlag;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
        /// <summary>
        /// 转账交易状态
        /// </summary>
        public EnumTransferStatusType TransferStatus;
    }

    /// <summary>
    /// 冲正响应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcRspRepealField
    {
        /// <summary>
        /// 冲正时间间隔
        /// </summary>
        public int RepealTimeInterval;
        /// <summary>
        /// 已经冲正次数
        /// </summary>
        public int RepealedTimes;
        /// <summary>
        /// 银行冲正标志
        /// </summary>
        public EnumBankRepealFlagType BankRepealFlag;
        /// <summary>
        /// 期商冲正标志
        /// </summary>
        public EnumBrokerRepealFlagType BrokerRepealFlag;
        /// <summary>
        /// 被冲正平台流水号
        /// </summary>
        public int PlateRepealSerial;
        /// <summary>
        /// 被冲正银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankRepealSerial;
        /// <summary>
        /// 被冲正期货流水号
        /// </summary>
        public int FutureRepealSerial;
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 客户姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string CustomerName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 客户类型
        /// </summary>
        public EnumCustTypeType CustType;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankPassWord;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 期货公司流水号
        /// </summary>
        public int FutureSerial;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 验证客户证件号码标志
        /// </summary>
        public EnumYesNoIndicatorType VerifyCertNoFlag;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 转帐金额
        /// </summary>
        public double TradeAmount;
        /// <summary>
        /// 期货可取金额
        /// </summary>
        public double FutureFetchAmount;
        /// <summary>
        /// 费用支付标志
        /// </summary>
        public EnumFeePayFlagType FeePayFlag;
        /// <summary>
        /// 应收客户费用
        /// </summary>
        public double CustFee;
        /// <summary>
        /// 应收期货公司费用
        /// </summary>
        public double BrokerFee;
        /// <summary>
        /// 发送方给接收方的消息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Message;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 银行帐号类型
        /// </summary>
        public EnumBankAccTypeType BankAccType;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货单位帐号类型
        /// </summary>
        public EnumBankAccTypeType BankSecuAccType;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 期货单位帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankSecuAcc;
        /// <summary>
        /// 银行密码标志
        /// </summary>
        public EnumPwdFlagType BankPwdFlag;
        /// <summary>
        /// 期货资金密码核对标志
        /// </summary>
        public EnumPwdFlagType SecuPwdFlag;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
        /// <summary>
        /// 转账交易状态
        /// </summary>
        public EnumTransferStatusType TransferStatus;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 查询账户信息请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcReqQueryAccountField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 客户姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string CustomerName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 客户类型
        /// </summary>
        public EnumCustTypeType CustType;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankPassWord;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 期货公司流水号
        /// </summary>
        public int FutureSerial;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 验证客户证件号码标志
        /// </summary>
        public EnumYesNoIndicatorType VerifyCertNoFlag;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 银行帐号类型
        /// </summary>
        public EnumBankAccTypeType BankAccType;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货单位帐号类型
        /// </summary>
        public EnumBankAccTypeType BankSecuAccType;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 期货单位帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankSecuAcc;
        /// <summary>
        /// 银行密码标志
        /// </summary>
        public EnumPwdFlagType BankPwdFlag;
        /// <summary>
        /// 期货资金密码核对标志
        /// </summary>
        public EnumPwdFlagType SecuPwdFlag;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
    }

    /// <summary>
    /// 查询账户信息响应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcRspQueryAccountField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 客户姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string CustomerName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 客户类型
        /// </summary>
        public EnumCustTypeType CustType;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankPassWord;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 期货公司流水号
        /// </summary>
        public int FutureSerial;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 验证客户证件号码标志
        /// </summary>
        public EnumYesNoIndicatorType VerifyCertNoFlag;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 银行帐号类型
        /// </summary>
        public EnumBankAccTypeType BankAccType;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货单位帐号类型
        /// </summary>
        public EnumBankAccTypeType BankSecuAccType;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 期货单位帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankSecuAcc;
        /// <summary>
        /// 银行密码标志
        /// </summary>
        public EnumPwdFlagType BankPwdFlag;
        /// <summary>
        /// 期货资金密码核对标志
        /// </summary>
        public EnumPwdFlagType SecuPwdFlag;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
        /// <summary>
        /// 银行可用金额
        /// </summary>
        public double BankUseAmount;
        /// <summary>
        /// 银行可取金额
        /// </summary>
        public double BankFetchAmount;
    }

    /// <summary>
    /// 期商签到签退
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcFutureSignIOField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
    }

    /// <summary>
    /// 期商签到响应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcRspFutureSignInField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
        /// <summary>
        /// PIN密钥
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string PinKey;
        /// <summary>
        /// MAC密钥
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string MacKey;
    }

    /// <summary>
    /// 期商签退请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcReqFutureSignOutField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
    }

    /// <summary>
    /// 期商签退响应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcRspFutureSignOutField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 查询指定流水号的交易结果请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcReqQueryTradeResultBySerialField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 流水号
        /// </summary>
        public int Reference;
        /// <summary>
        /// 本流水号发布者的机构类型
        /// </summary>
        public EnumInstitutionTypeType RefrenceIssureType;
        /// <summary>
        /// 本流水号发布者机构编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string RefrenceIssure;
        /// <summary>
        /// 客户姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string CustomerName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 客户类型
        /// </summary>
        public EnumCustTypeType CustType;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankPassWord;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 转帐金额
        /// </summary>
        public double TradeAmount;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
    }

    /// <summary>
    /// 查询指定流水号的交易结果响应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcRspQueryTradeResultBySerialField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
        /// <summary>
        /// 流水号
        /// </summary>
        public int Reference;
        /// <summary>
        /// 本流水号发布者的机构类型
        /// </summary>
        public EnumInstitutionTypeType RefrenceIssureType;
        /// <summary>
        /// 本流水号发布者机构编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string RefrenceIssure;
        /// <summary>
        /// 原始返回代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string OriginReturnCode;
        /// <summary>
        /// 原始返回码描述
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string OriginDescrInfoForReturnCode;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankPassWord;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 转帐金额
        /// </summary>
        public double TradeAmount;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
    }

    /// <summary>
    /// 日终文件就绪请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcReqDayEndFileReadyField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 文件业务功能
        /// </summary>
        public EnumFileBusinessCodeType FileBusinessCode;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcReturnResultField
    {
        /// <summary>
        /// 返回代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string ReturnCode;
        /// <summary>
        /// 返回码描述
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string DescrInfoForReturnCode;
    }

    /// <summary>
    /// 验证期货资金密码
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcVerifyFuturePasswordField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankPassWord;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
    }

    /// <summary>
    /// 验证客户信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcVerifyCustInfoField
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string CustomerName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 客户类型
        /// </summary>
        public EnumCustTypeType CustType;
    }

    /// <summary>
    /// 验证期货资金密码和客户信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcVerifyFuturePasswordAndCustInfoField
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string CustomerName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 客户类型
        /// </summary>
        public EnumCustTypeType CustType;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
    }

    /// <summary>
    /// 验证期货资金密码和客户信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcDepositResultInformField
    {
        /// <summary>
        /// 出入金流水号，该流水号为银期报盘返回的流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string DepositSeqNo;
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 入金金额
        /// </summary>
        public double Deposit;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 返回代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string ReturnCode;
        /// <summary>
        /// 返回码描述
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string DescrInfoForReturnCode;
    }

    /// <summary>
    /// 交易核心向银期报盘发出密钥同步请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcReqSyncKeyField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 交易核心给银期报盘的消息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Message;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
    }

    /// <summary>
    /// 交易核心向银期报盘发出密钥同步响应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcRspSyncKeyField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 交易核心给银期报盘的消息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Message;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 查询账户信息通知
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcNotifyQueryAccountField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 客户姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string CustomerName;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 客户类型
        /// </summary>
        public EnumCustTypeType CustType;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankPassWord;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 期货密码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string Password;
        /// <summary>
        /// 期货公司流水号
        /// </summary>
        public int FutureSerial;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 验证客户证件号码标志
        /// </summary>
        public EnumYesNoIndicatorType VerifyCertNoFlag;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 银行帐号类型
        /// </summary>
        public EnumBankAccTypeType BankAccType;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货单位帐号类型
        /// </summary>
        public EnumBankAccTypeType BankSecuAccType;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 期货单位帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankSecuAcc;
        /// <summary>
        /// 银行密码标志
        /// </summary>
        public EnumPwdFlagType BankPwdFlag;
        /// <summary>
        /// 期货资金密码核对标志
        /// </summary>
        public EnumPwdFlagType SecuPwdFlag;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
        /// <summary>
        /// 银行可用金额
        /// </summary>
        public double BankUseAmount;
        /// <summary>
        /// 银行可取金额
        /// </summary>
        public double BankFetchAmount;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 银期转账交易流水表
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcTransferSerialField
    {
        /// <summary>
        /// 平台流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 交易发起方日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 交易代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 会话编号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 银行帐号类型
        /// </summary>
        public EnumBankAccTypeType BankAccType;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankAccount;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 期货公司编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 期货公司帐号类型
        /// </summary>
        public EnumFutureAccTypeType FutureAccType;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 投资者代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string InvestorID;
        /// <summary>
        /// 期货公司流水号
        /// </summary>
        public int FutureSerial;
        /// <summary>
        /// 证件类型
        /// </summary>
        public EnumIdCardTypeType IdCardType;
        /// <summary>
        /// 证件号码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
        public string IdentifiedCardNo;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 交易金额
        /// </summary>
        public double TradeAmount;
        /// <summary>
        /// 应收客户费用
        /// </summary>
        public double CustFee;
        /// <summary>
        /// 应收期货公司费用
        /// </summary>
        public double BrokerFee;
        /// <summary>
        /// 有效标志
        /// </summary>
        public EnumAvailabilityFlagType AvailabilityFlag;
        /// <summary>
        /// 操作员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperatorCode;
        /// <summary>
        /// 新银行帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
        public string BankNewAccount;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 请求查询转帐流水
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcQryTransferSerialField
    {
        /// <summary>
        /// 经纪公司代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 投资者帐号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string AccountID;
        /// <summary>
        /// 银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
    }

    /// <summary>
    /// 期商签到通知
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcNotifyFutureSignInField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
        /// <summary>
        /// PIN密钥
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string PinKey;
        /// <summary>
        /// MAC密钥
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string MacKey;
    }

    /// <summary>
    /// 期商签退通知
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcNotifyFutureSignOutField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 摘要
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Digest;
        /// <summary>
        /// 币种代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string CurrencyID;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }

    /// <summary>
    /// 交易核心向银期报盘发出密钥同步处理结果的通知
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CThostFtdcNotifySyncKeyField
    {
        /// <summary>
        /// 业务功能码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string TradeCode;
        /// <summary>
        /// 银行代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string BankID;
        /// <summary>
        /// 银行分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string BankBranchID;
        /// <summary>
        /// 期商代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string BrokerID;
        /// <summary>
        /// 期商分支机构代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string BrokerBranchID;
        /// <summary>
        /// 交易日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeDate;
        /// <summary>
        /// 交易时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradeTime;
        /// <summary>
        /// 银行流水号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string BankSerial;
        /// <summary>
        /// 交易系统日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string TradingDay;
        /// <summary>
        /// 银期平台消息流水号
        /// </summary>
        public int PlateSerial;
        /// <summary>
        /// 最后分片标志
        /// </summary>
        public EnumLastFragmentType LastFragment;
        /// <summary>
        /// 会话号
        /// </summary>
        public int SessionID;
        /// <summary>
        /// 安装编号
        /// </summary>
        public int InstallID;
        /// <summary>
        /// 用户标识
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string UserID;
        /// <summary>
        /// 交易核心给银期报盘的消息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Message;
        /// <summary>
        /// 渠道标志
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string DeviceID;
        /// <summary>
        /// 期货公司银行编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string BrokerIDByBank;
        /// <summary>
        /// 交易柜员
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string OperNo;
        /// <summary>
        /// 请求编号
        /// </summary>
        public int RequestID;
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TID;
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorID;
        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string ErrorMsg;
    }
    /// <summary>
    /// TFtdcExchangePropertyType是一个交易所属性类型
    /// </summary>
    public enum EnumExchangePropertyType : byte
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = (byte)'0',

        /// <summary>
        /// 根据成交生成报单
        /// </summary>
        GenOrderByTrade = (byte)'1'
    }

    /// <summary>
    /// TFtdcIdCardTypeType是一个证件类型类型
    /// </summary>
    public enum EnumIdCardTypeType : byte
    {
        /// <summary>
        /// 组织机构代码
        /// </summary>
        EID = (byte)'0',

        /// <summary>
        /// 身份证
        /// </summary>
        IDCard = (byte)'1',

        /// <summary>
        /// 军官证
        /// </summary>
        OfficerIDCard = (byte)'2',

        /// <summary>
        /// 警官证
        /// </summary>
        PoliceIDCard = (byte)'3',

        /// <summary>
        /// 士兵证
        /// </summary>
        SoldierIDCard = (byte)'4',

        /// <summary>
        /// 户口簿
        /// </summary>
        HouseholdRegister = (byte)'5',

        /// <summary>
        /// 护照
        /// </summary>
        Passport = (byte)'6',

        /// <summary>
        /// 台胞证
        /// </summary>
        TaiwanCompatriotIDCard = (byte)'7',

        /// <summary>
        /// 回乡证
        /// </summary>
        HomeComingCard = (byte)'8',

        /// <summary>
        /// 营业执照号
        /// </summary>
        LicenseNo = (byte)'9',

        /// <summary>
        /// 其他证件
        /// </summary>
        OtherCard = (byte)'x'
    }

    /// <summary>
    /// TFtdcInvestorRangeType是一个投资者范围类型
    /// </summary>
    public enum EnumInvestorRangeType : byte
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = (byte)'1',

        /// <summary>
        /// 投资者组
        /// </summary>
        Group = (byte)'2',

        /// <summary>
        /// 单一投资者
        /// </summary>
        Single = (byte)'3'
    }

    /// <summary>
    /// TFtdcDepartmentRangeType是一个投资者范围类型
    /// </summary>
    public enum EnumDepartmentRangeType : byte
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = (byte)'1',

        /// <summary>
        /// 组织架构
        /// </summary>
        Group = (byte)'2',

        /// <summary>
        /// 单一投资者
        /// </summary>
        Single = (byte)'3'
    }

    /// <summary>
    /// TFtdcDataSyncStatusType是一个数据同步状态类型
    /// </summary>
    public enum EnumDataSyncStatusType : byte
    {
        /// <summary>
        /// 未同步
        /// </summary>
        Asynchronous = (byte)'1',

        /// <summary>
        /// 同步中
        /// </summary>
        Synchronizing = (byte)'2',

        /// <summary>
        /// 已同步
        /// </summary>
        Synchronized = (byte)'3'
    }

    /// <summary>
    /// TFtdcBrokerDataSyncStatusType是一个经纪公司数据同步状态类型
    /// </summary>
    public enum EnumBrokerDataSyncStatusType : byte
    {
        /// <summary>
        /// 已同步
        /// </summary>
        Synchronized = (byte)'1',

        /// <summary>
        /// 同步中
        /// </summary>
        Synchronizing = (byte)'2'
    }

    /// <summary>
    /// TFtdcExchangeConnectStatusType是一个交易所连接状态类型
    /// </summary>
    public enum EnumExchangeConnectStatusType : byte
    {
        /// <summary>
        /// 没有任何连接
        /// </summary>
        NoConnection = (byte)'1',

        /// <summary>
        /// 已经发出合约查询请求
        /// </summary>
        QryInstrumentSent = (byte)'2',

        /// <summary>
        /// 已经获取信息
        /// </summary>
        GotInformation = (byte)'9'
    }

    /// <summary>
    /// TFtdcTraderConnectStatusType是一个交易所交易员连接状态类型
    /// </summary>
    public enum EnumTraderConnectStatusType : byte
    {
        /// <summary>
        /// 没有任何连接
        /// </summary>
        NotConnected = (byte)'1',

        /// <summary>
        /// 已经连接
        /// </summary>
        Connected = (byte)'2',

        /// <summary>
        /// 已经发出合约查询请求
        /// </summary>
        QryInstrumentSent = (byte)'3',

        /// <summary>
        /// 订阅私有流
        /// </summary>
        SubPrivateFlow = (byte)'4'
    }

    /// <summary>
    /// TFtdcFunctionCodeType是一个功能代码类型
    /// </summary>
    public enum EnumFunctionCodeType : byte
    {
        /// <summary>
        /// 数据异步化
        /// </summary>
        DataAsync = (byte)'1',

        /// <summary>
        /// 强制用户登出
        /// </summary>
        ForceUserLogout = (byte)'2',

        /// <summary>
        /// 变更管理用户口令
        /// </summary>
        UserPasswordUpdate = (byte)'3',

        /// <summary>
        /// 变更经纪公司口令
        /// </summary>
        BrokerPasswordUpdate = (byte)'4',

        /// <summary>
        /// 变更投资者口令
        /// </summary>
        InvestorPasswordUpdate = (byte)'5',

        /// <summary>
        /// 报单插入
        /// </summary>
        OrderInsert = (byte)'6',

        /// <summary>
        /// 报单操作
        /// </summary>
        OrderAction = (byte)'7',

        /// <summary>
        /// 同步系统数据
        /// </summary>
        SyncSystemData = (byte)'8',

        /// <summary>
        /// 同步经纪公司数据
        /// </summary>
        SyncBrokerData = (byte)'9',

        /// <summary>
        /// 批量同步经纪公司数据
        /// </summary>
        BachSyncBrokerData = (byte)'A',

        /// <summary>
        /// 超级查询
        /// </summary>
        SuperQuery = (byte)'B',

        /// <summary>
        /// 报单插入
        /// </summary>
        ParkedOrderInsert = (byte)'C',

        /// <summary>
        /// 报单操作
        /// </summary>
        ParkedOrderAction = (byte)'D',

        /// <summary>
        /// 同步动态令牌
        /// </summary>
        SyncOTP = (byte)'E'
    }

    /// <summary>
    /// TFtdcBrokerFunctionCodeType是一个经纪公司功能代码类型
    /// </summary>
    public enum EnumBrokerFunctionCodeType : byte
    {
        /// <summary>
        /// 强制用户登出
        /// </summary>
        ForceUserLogout = (byte)'1',

        /// <summary>
        /// 变更用户口令
        /// </summary>
        UserPasswordUpdate = (byte)'2',

        /// <summary>
        /// 同步经纪公司数据
        /// </summary>
        SyncBrokerData = (byte)'3',

        /// <summary>
        /// 批量同步经纪公司数据
        /// </summary>
        BachSyncBrokerData = (byte)'4',

        /// <summary>
        /// 报单插入
        /// </summary>
        OrderInsert = (byte)'5',

        /// <summary>
        /// 报单操作
        /// </summary>
        OrderAction = (byte)'6',

        /// <summary>
        /// 全部查询
        /// </summary>
        AllQuery = (byte)'7',

        /// <summary>
        /// 系统功能：登入/登出/修改密码等
        /// </summary>
        log = (byte)'a',

        /// <summary>
        /// 基本查询：查询基础数据，如合约，交易所等常量
        /// </summary>
        BaseQry = (byte)'b',

        /// <summary>
        /// 交易查询：如查成交，委托
        /// </summary>
        TradeQry = (byte)'c',

        /// <summary>
        /// 交易功能：报单，撤单
        /// </summary>
        Trade = (byte)'d',

        /// <summary>
        /// 银期转账
        /// </summary>
        Virement = (byte)'e',

        /// <summary>
        /// 风险监控
        /// </summary>
        Risk = (byte)'f',

        /// <summary>
        /// 查询/管理：查询会话，踢人等
        /// </summary>
        Session = (byte)'g',

        /// <summary>
        /// 风控通知控制
        /// </summary>
        RiskNoticeCtl = (byte)'h',

        /// <summary>
        /// 风控通知发送
        /// </summary>
        RiskNotice = (byte)'i',

        /// <summary>
        /// 察看经纪公司资金权限
        /// </summary>
        BrokerDeposit = (byte)'j',

        /// <summary>
        /// 资金查询
        /// </summary>
        QueryFund = (byte)'k',

        /// <summary>
        /// 报单查询
        /// </summary>
        QueryOrder = (byte)'l',

        /// <summary>
        /// 成交查询
        /// </summary>
        QueryTrade = (byte)'m',

        /// <summary>
        /// 持仓查询
        /// </summary>
        QueryPosition = (byte)'n',

        /// <summary>
        /// 行情查询
        /// </summary>
        QueryMarketData = (byte)'o',

        /// <summary>
        /// 用户事件查询
        /// </summary>
        QueryUserEvent = (byte)'p',

        /// <summary>
        /// 风险通知查询
        /// </summary>
        QueryRiskNotify = (byte)'q',

        /// <summary>
        /// 出入金查询
        /// </summary>
        QueryFundChange = (byte)'r',

        /// <summary>
        /// 投资者信息查询
        /// </summary>
        QueryInvestor = (byte)'s',

        /// <summary>
        /// 交易编码查询
        /// </summary>
        QueryTradingCode = (byte)'t',

        /// <summary>
        /// 强平
        /// </summary>
        ForceClose = (byte)'u',

        /// <summary>
        /// 压力测试
        /// </summary>
        PressTest = (byte)'v',

        /// <summary>
        /// 权益反算
        /// </summary>
        RemainCalc = (byte)'w',

        /// <summary>
        /// 净持仓保证金指标
        /// </summary>
        NetPositionInd = (byte)'x',

        /// <summary>
        /// 风险预算
        /// </summary>
        RiskPredict = (byte)'y',

        /// <summary>
        /// 数据导出
        /// </summary>
        DataExport = (byte)'z',

        /// <summary>
        /// 风控指标设置
        /// </summary>
        RiskTargetSetup = (byte)'A',

        /// <summary>
        /// 行情预警
        /// </summary>
        MarketDataWarn = (byte)'B',

        /// <summary>
        /// 同步动态令牌
        /// </summary>
        SyncOTP = (byte)'E'
    }

    /// <summary>
    /// TFtdcOrderActionStatusType是一个报单操作状态类型
    /// </summary>
    public enum EnumOrderActionStatusType : byte
    {
        /// <summary>
        /// 已经提交
        /// </summary>
        Submitted = (byte)'a',

        /// <summary>
        /// 已经接受
        /// </summary>
        Accepted = (byte)'b',

        /// <summary>
        /// 已经被拒绝
        /// </summary>
        Rejected = (byte)'c'
    }

    /// <summary>
    /// TFtdcOrderStatusType是一个报单状态类型
    /// </summary>
    public enum EnumOrderStatusType : byte
    {
        /// <summary>
        /// 全部成交
        /// </summary>
        AllTraded = (byte)'0',

        /// <summary>
        /// 部分成交还在队列中
        /// </summary>
        PartTradedQueueing = (byte)'1',

        /// <summary>
        /// 部分成交不在队列中
        /// </summary>
        PartTradedNotQueueing = (byte)'2',

        /// <summary>
        /// 未成交还在队列中
        /// </summary>
        NoTradeQueueing = (byte)'3',

        /// <summary>
        /// 未成交不在队列中
        /// </summary>
        NoTradeNotQueueing = (byte)'4',

        /// <summary>
        /// 撤单
        /// </summary>
        Canceled = (byte)'5',

        /// <summary>
        /// 未知
        /// </summary>
        Unknown = (byte)'a',

        /// <summary>
        /// 尚未触发
        /// </summary>
        NotTouched = (byte)'b',

        /// <summary>
        /// 已触发
        /// </summary>
        Touched = (byte)'c'
    }

    /// <summary>
    /// TFtdcOrderSubmitStatusType是一个报单提交状态类型
    /// </summary>
    public enum EnumOrderSubmitStatusType : byte
    {
        /// <summary>
        /// 已经提交
        /// </summary>
        InsertSubmitted = (byte)'0',

        /// <summary>
        /// 撤单已经提交
        /// </summary>
        CancelSubmitted = (byte)'1',

        /// <summary>
        /// 修改已经提交
        /// </summary>
        ModifySubmitted = (byte)'2',

        /// <summary>
        /// 已经接受
        /// </summary>
        Accepted = (byte)'3',

        /// <summary>
        /// 报单已经被拒绝
        /// </summary>
        InsertRejected = (byte)'4',

        /// <summary>
        /// 撤单已经被拒绝
        /// </summary>
        CancelRejected = (byte)'5',

        /// <summary>
        /// 改单已经被拒绝
        /// </summary>
        ModifyRejected = (byte)'6'
    }

    /// <summary>
    /// TFtdcPositionDateType是一个持仓日期类型
    /// </summary>
    public enum EnumPositionDateType : byte
    {
        /// <summary>
        /// 今日持仓
        /// </summary>
        Today = (byte)'1',

        /// <summary>
        /// 历史持仓
        /// </summary>
        History = (byte)'2'
    }

    /// <summary>
    /// TFtdcPositionDateTypeType是一个持仓日期类型类型
    /// </summary>
    public enum EnumPositionDateTypeType : byte
    {
        /// <summary>
        /// 使用历史持仓
        /// </summary>
        UseHistory = (byte)'1',

        /// <summary>
        /// 不使用历史持仓
        /// </summary>
        NoUseHistory = (byte)'2'
    }

    /// <summary>
    /// TFtdcTradingRoleType是一个交易角色类型
    /// </summary>
    public enum EnumTradingRoleType : byte
    {
        /// <summary>
        /// 代理
        /// </summary>
        Broker = (byte)'1',

        /// <summary>
        /// 自营
        /// </summary>
        Host = (byte)'2',

        /// <summary>
        /// 做市商
        /// </summary>
        Maker = (byte)'3'
    }

    /// <summary>
    /// TFtdcProductClassType是一个产品类型类型
    /// </summary>
    public enum EnumProductClassType : byte
    {
        /// <summary>
        /// 期货
        /// </summary>
        Futures = (byte)'1',

        /// <summary>
        /// 期权
        /// </summary>
        Options = (byte)'2',

        /// <summary>
        /// 组合
        /// </summary>
        Combination = (byte)'3',

        /// <summary>
        /// 即期
        /// </summary>
        Spot = (byte)'4',

        /// <summary>
        /// 期转现
        /// </summary>
        EFP = (byte)'5'
    }

    /// <summary>
    /// TFtdcInstLifePhaseType是一个合约生命周期状态类型
    /// </summary>
    public enum EnumInstLifePhaseType : byte
    {
        /// <summary>
        /// 未上市
        /// </summary>
        NotStart = (byte)'0',

        /// <summary>
        /// 上市
        /// </summary>
        Started = (byte)'1',

        /// <summary>
        /// 停牌
        /// </summary>
        Pause = (byte)'2',

        /// <summary>
        /// 到期
        /// </summary>
        Expired = (byte)'3'
    }

    /// <summary>
    /// TFtdcDirectionType是一个买卖方向类型
    /// </summary>
    public enum EnumDirectionType : byte
    {
        /// <summary>
        /// 买
        /// </summary>
        Buy = (byte)'0',

        /// <summary>
        /// 卖
        /// </summary>
        Sell = (byte)'1'
    }

    /// <summary>
    /// TFtdcPositionTypeType是一个持仓类型类型
    /// </summary>
    public enum EnumPositionTypeType : byte
    {
        /// <summary>
        /// 净持仓
        /// </summary>
        Net = (byte)'1',

        /// <summary>
        /// 综合持仓
        /// </summary>
        Gross = (byte)'2'
    }

    /// <summary>
    /// TFtdcPosiDirectionType是一个持仓多空方向类型
    /// </summary>
    public enum EnumPosiDirectionType : byte
    {
        /// <summary>
        /// 净
        /// </summary>
        Net = (byte)'1',

        /// <summary>
        /// 多头
        /// </summary>
        Long = (byte)'2',

        /// <summary>
        /// 空头
        /// </summary>
        Short = (byte)'3'
    }

    /// <summary>
    /// TFtdcSysSettlementStatusType是一个系统结算状态类型
    /// </summary>
    public enum EnumSysSettlementStatusType : byte
    {
        /// <summary>
        /// 不活跃
        /// </summary>
        NonActive = (byte)'1',

        /// <summary>
        /// 启动
        /// </summary>
        Startup = (byte)'2',

        /// <summary>
        /// 操作
        /// </summary>
        Operating = (byte)'3',

        /// <summary>
        /// 结算
        /// </summary>
        Settlement = (byte)'4',

        /// <summary>
        /// 结算完成
        /// </summary>
        SettlementFinished = (byte)'5'
    }

    /// <summary>
    /// TFtdcRatioAttrType是一个费率属性类型
    /// </summary>
    public enum EnumRatioAttrType : byte
    {
        /// <summary>
        /// 交易费率
        /// </summary>
        Trade = (byte)'0',

        /// <summary>
        /// 结算费率
        /// </summary>
        Settlement = (byte)'1'
    }

    /// <summary>
    /// TFtdcHedgeFlagType是一个投机套保标志类型
    /// </summary>
    public enum EnumHedgeFlagType : byte
    {
        /// <summary>
        /// 投机
        /// </summary>
        Speculation = (byte)'1',

        /// <summary>
        /// 套利
        /// </summary>
        Arbitrage = (byte)'2',

        /// <summary>
        /// 套保
        /// </summary>
        Hedge = (byte)'3'
    }

    /// <summary>
    /// TFtdcClientIDTypeType是一个交易编码类型类型
    /// </summary>
    public enum EnumClientIDTypeType : byte
    {
        /// <summary>
        /// 投机
        /// </summary>
        Speculation = (byte)'1',

        /// <summary>
        /// 套利
        /// </summary>
        Arbitrage = (byte)'2',

        /// <summary>
        /// 套保
        /// </summary>
        Hedge = (byte)'3'
    }

    /// <summary>
    /// TFtdcOrderPriceTypeType是一个报单价格条件类型
    /// </summary>
    public enum EnumOrderPriceTypeType : byte
    {
        /// <summary>
        /// 任意价
        /// </summary>
        AnyPrice = (byte)'1',

        /// <summary>
        /// 限价
        /// </summary>
        LimitPrice = (byte)'2',

        /// <summary>
        /// 最优价
        /// </summary>
        BestPrice = (byte)'3',

        /// <summary>
        /// 最新价
        /// </summary>
        LastPrice = (byte)'4',

        /// <summary>
        /// 最新价浮动上浮1个ticks
        /// </summary>
        LastPricePlusOneTicks = (byte)'5',

        /// <summary>
        /// 最新价浮动上浮2个ticks
        /// </summary>
        LastPricePlusTwoTicks = (byte)'6',

        /// <summary>
        /// 最新价浮动上浮3个ticks
        /// </summary>
        LastPricePlusThreeTicks = (byte)'7',

        /// <summary>
        /// 卖一价
        /// </summary>
        AskPrice1 = (byte)'8',

        /// <summary>
        /// 卖一价浮动上浮1个ticks
        /// </summary>
        AskPrice1PlusOneTicks = (byte)'9',

        /// <summary>
        /// 卖一价浮动上浮2个ticks
        /// </summary>
        AskPrice1PlusTwoTicks = (byte)'A',

        /// <summary>
        /// 卖一价浮动上浮3个ticks
        /// </summary>
        AskPrice1PlusThreeTicks = (byte)'B',

        /// <summary>
        /// 买一价
        /// </summary>
        BidPrice1 = (byte)'C',

        /// <summary>
        /// 买一价浮动上浮1个ticks
        /// </summary>
        BidPrice1PlusOneTicks = (byte)'D',

        /// <summary>
        /// 买一价浮动上浮2个ticks
        /// </summary>
        BidPrice1PlusTwoTicks = (byte)'E',

        /// <summary>
        /// 买一价浮动上浮3个ticks
        /// </summary>
        BidPrice1PlusThreeTicks = (byte)'F'
    }

    /// <summary>
    /// TFtdcOffsetFlagType是一个开平标志类型
    /// </summary>
    public enum EnumOffsetFlagType : byte
    {
        /// <summary>
        /// 开仓
        /// </summary>
        Open = (byte)'0',

        /// <summary>
        /// 平仓
        /// </summary>
        Close = (byte)'1',

        /// <summary>
        /// 强平
        /// </summary>
        ForceClose = (byte)'2',

        /// <summary>
        /// 平今
        /// </summary>
        CloseToday = (byte)'3',

        /// <summary>
        /// 平昨
        /// </summary>
        CloseYesterday = (byte)'4',

        /// <summary>
        /// 强减
        /// </summary>
        ForceOff = (byte)'5',

        /// <summary>
        /// 本地强平
        /// </summary>
        LocalForceClose = (byte)'6'
    }

    /// <summary>
    /// TFtdcForceCloseReasonType是一个强平原因类型
    /// </summary>
    public enum EnumForceCloseReasonType : byte
    {
        /// <summary>
        /// 非强平
        /// </summary>
        NotForceClose = (byte)'0',

        /// <summary>
        /// 资金不足
        /// </summary>
        LackDeposit = (byte)'1',

        /// <summary>
        /// 客户超仓
        /// </summary>
        ClientOverPositionLimit = (byte)'2',

        /// <summary>
        /// 会员超仓
        /// </summary>
        MemberOverPositionLimit = (byte)'3',

        /// <summary>
        /// 持仓非整数倍
        /// </summary>
        NotMultiple = (byte)'4',

        /// <summary>
        /// 违规
        /// </summary>
        Violation = (byte)'5',

        /// <summary>
        /// 其它
        /// </summary>
        Other = (byte)'6',

        /// <summary>
        /// 自然人临近交割
        /// </summary>
        PersonDeliv = (byte)'7'
    }

    /// <summary>
    /// TFtdcOrderTypeType是一个报单类型类型
    /// </summary>
    public enum EnumOrderTypeType : byte
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = (byte)'0',

        /// <summary>
        /// 报价衍生
        /// </summary>
        DeriveFromQuote = (byte)'1',

        /// <summary>
        /// 组合衍生
        /// </summary>
        DeriveFromCombination = (byte)'2',

        /// <summary>
        /// 组合报单
        /// </summary>
        Combination = (byte)'3',

        /// <summary>
        /// 条件单
        /// </summary>
        ConditionalOrder = (byte)'4',

        /// <summary>
        /// 互换单
        /// </summary>
        Swap = (byte)'5'
    }

    /// <summary>
    /// TFtdcTimeConditionType是一个有效期类型类型
    /// </summary>
    public enum EnumTimeConditionType : byte
    {
        /// <summary>
        /// 立即完成，否则撤销
        /// </summary>
        IOC = (byte)'1',

        /// <summary>
        /// 本节有效
        /// </summary>
        GFS = (byte)'2',

        /// <summary>
        /// 当日有效
        /// </summary>
        GFD = (byte)'3',

        /// <summary>
        /// 指定日期前有效
        /// </summary>
        GTD = (byte)'4',

        /// <summary>
        /// 撤销前有效
        /// </summary>
        GTC = (byte)'5',

        /// <summary>
        /// 集合竞价有效
        /// </summary>
        GFA = (byte)'6'
    }

    /// <summary>
    /// TFtdcVolumeConditionType是一个成交量类型类型
    /// </summary>
    public enum EnumVolumeConditionType : byte
    {
        /// <summary>
        /// 任何数量
        /// </summary>
        AV = (byte)'1',

        /// <summary>
        /// 最小数量
        /// </summary>
        MV = (byte)'2',

        /// <summary>
        /// 全部数量
        /// </summary>
        CV = (byte)'3'
    }

    /// <summary>
    /// TFtdcContingentConditionType是一个触发条件类型
    /// </summary>
    public enum EnumContingentConditionType : byte
    {
        /// <summary>
        /// 立即
        /// </summary>
        Immediately = (byte)'1',

        /// <summary>
        /// 止损
        /// </summary>
        Touch = (byte)'2',

        /// <summary>
        /// 止赢
        /// </summary>
        TouchProfit = (byte)'3',

        /// <summary>
        /// 预埋单
        /// </summary>
        ParkedOrder = (byte)'4',

        /// <summary>
        /// 最新价大于条件价
        /// </summary>
        LastPriceGreaterThanStopPrice = (byte)'5',

        /// <summary>
        /// 最新价大于等于条件价
        /// </summary>
        LastPriceGreaterEqualStopPrice = (byte)'6',

        /// <summary>
        /// 最新价小于条件价
        /// </summary>
        LastPriceLesserThanStopPrice = (byte)'7',

        /// <summary>
        /// 最新价小于等于条件价
        /// </summary>
        LastPriceLesserEqualStopPrice = (byte)'8',

        /// <summary>
        /// 卖一价大于条件价
        /// </summary>
        AskPriceGreaterThanStopPrice = (byte)'9',

        /// <summary>
        /// 卖一价大于等于条件价
        /// </summary>
        AskPriceGreaterEqualStopPrice = (byte)'A',

        /// <summary>
        /// 卖一价小于条件价
        /// </summary>
        AskPriceLesserThanStopPrice = (byte)'B',

        /// <summary>
        /// 卖一价小于等于条件价
        /// </summary>
        AskPriceLesserEqualStopPrice = (byte)'C',

        /// <summary>
        /// 买一价大于条件价
        /// </summary>
        BidPriceGreaterThanStopPrice = (byte)'D',

        /// <summary>
        /// 买一价大于等于条件价
        /// </summary>
        BidPriceGreaterEqualStopPrice = (byte)'E',

        /// <summary>
        /// 买一价小于条件价
        /// </summary>
        BidPriceLesserThanStopPrice = (byte)'F',

        /// <summary>
        /// 买一价小于等于条件价
        /// </summary>
        BidPriceLesserEqualStopPrice = (byte)'H'
    }

    /// <summary>
    /// TFtdcActionFlagType是一个操作标志类型
    /// </summary>
    public enum EnumActionFlagType : byte
    {
        /// <summary>
        /// 删除
        /// </summary>
        Delete = (byte)'0',

        /// <summary>
        /// 修改
        /// </summary>
        Modify = (byte)'3'
    }

    /// <summary>
    /// TFtdcTradingRightType是一个交易权限类型
    /// </summary>
    public enum EnumTradingRightType : byte
    {
        /// <summary>
        /// 可以交易
        /// </summary>
        Allow = (byte)'0',

        /// <summary>
        /// 只能平仓
        /// </summary>
        CloseOnly = (byte)'1',

        /// <summary>
        /// 不能交易
        /// </summary>
        Forbidden = (byte)'2'
    }

    /// <summary>
    /// TFtdcOrderSourceType是一个报单来源类型
    /// </summary>
    public enum EnumOrderSourceType : byte
    {
        /// <summary>
        /// 来自参与者
        /// </summary>
        Participant = (byte)'0',

        /// <summary>
        /// 来自管理员
        /// </summary>
        Administrator = (byte)'1'
    }

    /// <summary>
    /// TFtdcTradeTypeType是一个成交类型类型
    /// </summary>
    public enum EnumTradeTypeType : byte
    {
        /// <summary>
        /// 普通成交
        /// </summary>
        Common = (byte)'0',

        /// <summary>
        /// 期权执行
        /// </summary>
        OptionsExecution = (byte)'1',

        /// <summary>
        /// OTC成交
        /// </summary>
        OTC = (byte)'2',

        /// <summary>
        /// 期转现衍生成交
        /// </summary>
        EFPDerived = (byte)'3',

        /// <summary>
        /// 组合衍生成交
        /// </summary>
        CombinationDerived = (byte)'4'
    }

    /// <summary>
    /// TFtdcPriceSourceType是一个成交价来源类型
    /// </summary>
    public enum EnumPriceSourceType : byte
    {
        /// <summary>
        /// 前成交价
        /// </summary>
        LastPrice = (byte)'0',

        /// <summary>
        /// 买委托价
        /// </summary>
        Buy = (byte)'1',

        /// <summary>
        /// 卖委托价
        /// </summary>
        Sell = (byte)'2'
    }

    /// <summary>
    /// TFtdcInstrumentStatusType是一个合约交易状态类型
    /// </summary>
    public enum EnumInstrumentStatusType : byte
    {
        /// <summary>
        /// 开盘前
        /// </summary>
        BeforeTrading = (byte)'0',

        /// <summary>
        /// 非交易
        /// </summary>
        NoTrading = (byte)'1',

        /// <summary>
        /// 连续交易
        /// </summary>
        Continous = (byte)'2',

        /// <summary>
        /// 集合竞价报单
        /// </summary>
        AuctionOrdering = (byte)'3',

        /// <summary>
        /// 集合竞价价格平衡
        /// </summary>
        AuctionBalance = (byte)'4',

        /// <summary>
        /// 集合竞价撮合
        /// </summary>
        AuctionMatch = (byte)'5',

        /// <summary>
        /// 收盘
        /// </summary>
        Closed = (byte)'6'
    }

    /// <summary>
    /// TFtdcInstStatusEnterReasonType是一个品种进入交易状态原因类型
    /// </summary>
    public enum EnumInstStatusEnterReasonType : byte
    {
        /// <summary>
        /// 自动切换
        /// </summary>
        Automatic = (byte)'1',

        /// <summary>
        /// 手动切换
        /// </summary>
        Manual = (byte)'2',

        /// <summary>
        /// 熔断
        /// </summary>
        Fuse = (byte)'3'
    }

    /// <summary>
    /// TFtdcBatchStatusType是一个处理状态类型
    /// </summary>
    public enum EnumBatchStatusType : byte
    {
        /// <summary>
        /// 未上传
        /// </summary>
        NoUpload = (byte)'1',

        /// <summary>
        /// 已上传
        /// </summary>
        Uploaded = (byte)'2',

        /// <summary>
        /// 审核失败
        /// </summary>
        Failed = (byte)'3'
    }

    /// <summary>
    /// TFtdcReturnStyleType是一个按品种返还方式类型
    /// </summary>
    public enum EnumReturnStyleType : byte
    {
        /// <summary>
        /// 按所有品种
        /// </summary>
        All = (byte)'1',

        /// <summary>
        /// 按品种
        /// </summary>
        ByProduct = (byte)'2'
    }

    /// <summary>
    /// TFtdcReturnPatternType是一个返还模式类型
    /// </summary>
    public enum EnumReturnPatternType : byte
    {
        /// <summary>
        /// 按成交手数
        /// </summary>
        ByVolume = (byte)'1',

        /// <summary>
        /// 按留存手续费
        /// </summary>
        ByFeeOnHand = (byte)'2'
    }

    /// <summary>
    /// TFtdcReturnLevelType是一个返还级别类型
    /// </summary>
    public enum EnumReturnLevelType : byte
    {
        /// <summary>
        /// 级别1
        /// </summary>
        Level1 = (byte)'1',

        /// <summary>
        /// 级别2
        /// </summary>
        Level2 = (byte)'2',

        /// <summary>
        /// 级别3
        /// </summary>
        Level3 = (byte)'3',

        /// <summary>
        /// 级别4
        /// </summary>
        Level4 = (byte)'4',

        /// <summary>
        /// 级别5
        /// </summary>
        Level5 = (byte)'5',

        /// <summary>
        /// 级别6
        /// </summary>
        Level6 = (byte)'6',

        /// <summary>
        /// 级别7
        /// </summary>
        Level7 = (byte)'7',

        /// <summary>
        /// 级别8
        /// </summary>
        Level8 = (byte)'8',

        /// <summary>
        /// 级别9
        /// </summary>
        Level9 = (byte)'9'
    }

    /// <summary>
    /// TFtdcReturnStandardType是一个返还标准类型
    /// </summary>
    public enum EnumReturnStandardType : byte
    {
        /// <summary>
        /// 分阶段返还
        /// </summary>
        ByPeriod = (byte)'1',

        /// <summary>
        /// 按某一标准
        /// </summary>
        ByStandard = (byte)'2'
    }

    /// <summary>
    /// TFtdcMortgageTypeType是一个质押类型类型
    /// </summary>
    public enum EnumMortgageTypeType : byte
    {
        /// <summary>
        /// 质出
        /// </summary>
        Out = (byte)'0',

        /// <summary>
        /// 质入
        /// </summary>
        In = (byte)'1'
    }

    /// <summary>
    /// TFtdcInvestorSettlementParamIDType是一个投资者结算参数代码类型
    /// </summary>
    public enum EnumInvestorSettlementParamIDType : byte
    {
        /// <summary>
        /// 基础保证金
        /// </summary>
        BaseMargin = (byte)'1',

        /// <summary>
        /// 最低权益标准
        /// </summary>
        LowestInterest = (byte)'2',

        /// <summary>
        /// 质押比例
        /// </summary>
        MortgageRatio = (byte)'4',

        /// <summary>
        /// 保证金算法
        /// </summary>
        MarginWay = (byte)'5',

        /// <summary>
        /// 结算单(盯市)权益等于结存
        /// </summary>
        BillDeposit = (byte)'9'
    }

    /// <summary>
    /// TFtdcExchangeSettlementParamIDType是一个交易所结算参数代码类型
    /// </summary>
    public enum EnumExchangeSettlementParamIDType : byte
    {
        /// <summary>
        /// 质押比例
        /// </summary>
        MortgageRatio = (byte)'1',

        /// <summary>
        /// 分项资金导入项
        /// </summary>
        OtherFundItem = (byte)'2',

        /// <summary>
        /// 分项资金入交易所出入金
        /// </summary>
        OtherFundImport = (byte)'3',

        /// <summary>
        /// 上期所交割手续费收取方式
        /// </summary>
        SHFEDelivFee = (byte)'4',

        /// <summary>
        /// 大商所交割手续费收取方式
        /// </summary>
        DCEDelivFee = (byte)'5',

        /// <summary>
        /// 中金所开户最低可用金额
        /// </summary>
        CFFEXMinPrepa = (byte)'6'
    }

    /// <summary>
    /// TFtdcSystemParamIDType是一个系统参数代码类型
    /// </summary>
    public enum EnumSystemParamIDType : byte
    {
        /// <summary>
        /// 投资者代码最小长度
        /// </summary>
        InvestorIDMinLength = (byte)'1',

        /// <summary>
        /// 投资者帐号代码最小长度
        /// </summary>
        AccountIDMinLength = (byte)'2',

        /// <summary>
        /// 投资者开户默认登录权限
        /// </summary>
        UserRightLogon = (byte)'3',

        /// <summary>
        /// 投资者交易结算单成交汇总方式
        /// </summary>
        SettlementBillTrade = (byte)'4',

        /// <summary>
        /// 统一开户更新交易编码方式
        /// </summary>
        TradingCode = (byte)'5',

        /// <summary>
        /// 结算是否判断存在未复核的出入金和分项资金
        /// </summary>
        CheckFund = (byte)'6',

        /// <summary>
        /// 上传的结算文件标识
        /// </summary>
        UploadSettlementFile = (byte)'U',

        /// <summary>
        /// 下载的保证金存管文件
        /// </summary>
        DownloadCSRCFile = (byte)'D',

        /// <summary>
        /// 结算单文件标识
        /// </summary>
        SettlementBillFile = (byte)'S',

        /// <summary>
        /// 证监会文件标识
        /// </summary>
        CSRCOthersFile = (byte)'C',

        /// <summary>
        /// 投资者照片路径
        /// </summary>
        InvestorPhoto = (byte)'P',

        /// <summary>
        /// 上报保证金监控中心数据
        /// </summary>
        CSRCData = (byte)'R',

        /// <summary>
        /// 开户密码录入方式
        /// </summary>
        InvestorPwdModel = (byte)'I'
    }

    /// <summary>
    /// TFtdcTradeParamIDType是一个交易系统参数代码类型
    /// </summary>
    public enum EnumTradeParamIDType : byte
    {
        /// <summary>
        /// 系统加密算法
        /// </summary>
        EncryptionStandard = (byte)'E',

        /// <summary>
        /// 系统风险算法
        /// </summary>
        RiskMode = (byte)'R',

        /// <summary>
        /// 系统风险算法是否全局 0-否 1-是
        /// </summary>
        RiskModeGlobal = (byte)'G'
    }

    /// <summary>
    /// TFtdcFileIDType是一个文件标识类型
    /// </summary>
    public enum EnumFileIDType : byte
    {
        /// <summary>
        /// 资金数据
        /// </summary>
        SettlementFund = (byte)'F',

        /// <summary>
        /// 成交数据
        /// </summary>
        Trade = (byte)'T',

        /// <summary>
        /// 投资者持仓数据
        /// </summary>
        InvestorPosition = (byte)'P',

        /// <summary>
        /// 投资者分项资金数据
        /// </summary>
        SubEntryFund = (byte)'O',

        /// <summary>
        /// 郑商所组合持仓数据
        /// </summary>
        CZCECombinationPos = (byte)'C',

        /// <summary>
        /// 上报保证金监控中心数据
        /// </summary>
        CSRCData = (byte)'R'
    }

    /// <summary>
    /// TFtdcFileTypeType是一个文件上传类型类型
    /// </summary>
    public enum EnumFileTypeType : byte
    {
        /// <summary>
        /// 结算
        /// </summary>
        Settlement = (byte)'0',

        /// <summary>
        /// 核对
        /// </summary>
        Check = (byte)'1'
    }

    /// <summary>
    /// TFtdcFileFormatType是一个文件格式类型
    /// </summary>
    public enum EnumFileFormatType : byte
    {
        /// <summary>
        /// 文本文件(.txt)
        /// </summary>
        Txt = (byte)'0',

        /// <summary>
        /// 压缩文件(.zip)
        /// </summary>
        Zip = (byte)'1',

        /// <summary>
        /// DBF文件(.dbf)
        /// </summary>
        DBF = (byte)'2'
    }

    /// <summary>
    /// TFtdcFileUploadStatusType是一个文件状态类型
    /// </summary>
    public enum EnumFileUploadStatusType : byte
    {
        /// <summary>
        /// 上传成功
        /// </summary>
        SucceedUpload = (byte)'1',

        /// <summary>
        /// 上传失败
        /// </summary>
        FailedUpload = (byte)'2',

        /// <summary>
        /// 导入成功
        /// </summary>
        SucceedLoad = (byte)'3',

        /// <summary>
        /// 导入部分成功
        /// </summary>
        PartSucceedLoad = (byte)'4',

        /// <summary>
        /// 导入失败
        /// </summary>
        FailedLoad = (byte)'5'
    }

    /// <summary>
    /// TFtdcTransferDirectionType是一个移仓方向类型
    /// </summary>
    public enum EnumTransferDirectionType : byte
    {
        /// <summary>
        /// 移出
        /// </summary>
        Out = (byte)'0',

        /// <summary>
        /// 移入
        /// </summary>
        In = (byte)'1'
    }

    /// <summary>
    /// TFtdcBankFlagType是一个银行统一标识类型类型
    /// </summary>
    public enum EnumBankFlagType : byte
    {
        /// <summary>
        /// 工商银行
        /// </summary>
        ICBC = (byte)'1',

        /// <summary>
        /// 农业银行
        /// </summary>
        ABC = (byte)'2',

        /// <summary>
        /// 中国银行
        /// </summary>
        BC = (byte)'3',

        /// <summary>
        /// 建设银行
        /// </summary>
        CBC = (byte)'4',

        /// <summary>
        /// 交通银行
        /// </summary>
        BOC = (byte)'5',

        /// <summary>
        /// 其他银行
        /// </summary>
        Other = (byte)'Z'
    }

    /// <summary>
    /// TFtdcSpecialCreateRuleType是一个特殊的创建规则类型
    /// </summary>
    public enum EnumSpecialCreateRuleType : byte
    {
        /// <summary>
        /// 没有特殊创建规则
        /// </summary>
        NoSpecialRule = (byte)'0',

        /// <summary>
        /// 不包含春节
        /// </summary>
        NoSpringFestival = (byte)'1'
    }

    /// <summary>
    /// TFtdcBasisPriceTypeType是一个挂牌基准价类型类型
    /// </summary>
    public enum EnumBasisPriceTypeType : byte
    {
        /// <summary>
        /// 上一合约结算价
        /// </summary>
        LastSettlement = (byte)'1',

        /// <summary>
        /// 上一合约收盘价
        /// </summary>
        LaseClose = (byte)'2'
    }

    /// <summary>
    /// TFtdcProductLifePhaseType是一个产品生命周期状态类型
    /// </summary>
    public enum EnumProductLifePhaseType : byte
    {
        /// <summary>
        /// 活跃
        /// </summary>
        Active = (byte)'1',

        /// <summary>
        /// 不活跃
        /// </summary>
        NonActive = (byte)'2',

        /// <summary>
        /// 注销
        /// </summary>
        Canceled = (byte)'3'
    }

    /// <summary>
    /// TFtdcDeliveryModeType是一个交割方式类型
    /// </summary>
    public enum EnumDeliveryModeType : byte
    {
        /// <summary>
        /// 现金交割
        /// </summary>
        CashDeliv = (byte)'1',

        /// <summary>
        /// 实物交割
        /// </summary>
        CommodityDeliv = (byte)'2'
    }

    /// <summary>
    /// TFtdcFundIOTypeType是一个出入金类型类型
    /// </summary>
    public enum EnumFundIOTypeType : byte
    {
        /// <summary>
        /// 出入金
        /// </summary>
        FundIO = (byte)'1',

        /// <summary>
        /// 银期转帐
        /// </summary>
        Transfer = (byte)'2'
    }

    /// <summary>
    /// TFtdcFundTypeType是一个资金类型类型
    /// </summary>
    public enum EnumFundTypeType : byte
    {
        /// <summary>
        /// 银行存款
        /// </summary>
        Deposite = (byte)'1',

        /// <summary>
        /// 分项资金
        /// </summary>
        ItemFund = (byte)'2',

        /// <summary>
        /// 公司调整
        /// </summary>
        Company = (byte)'3'
    }

    /// <summary>
    /// TFtdcFundDirectionType是一个出入金方向类型
    /// </summary>
    public enum EnumFundDirectionType : byte
    {
        /// <summary>
        /// 入金
        /// </summary>
        In = (byte)'1',

        /// <summary>
        /// 出金
        /// </summary>
        Out = (byte)'2'
    }

    /// <summary>
    /// TFtdcFundStatusType是一个资金状态类型
    /// </summary>
    public enum EnumFundStatusType : byte
    {
        /// <summary>
        /// 已录入
        /// </summary>
        Record = (byte)'1',

        /// <summary>
        /// 已复核
        /// </summary>
        Check = (byte)'2',

        /// <summary>
        /// 已冲销
        /// </summary>
        Charge = (byte)'3'
    }

    /// <summary>
    /// TFtdcPublishStatusType是一个发布状态类型
    /// </summary>
    public enum EnumPublishStatusType : byte
    {
        /// <summary>
        /// 未发布
        /// </summary>
        None = (byte)'1',

        /// <summary>
        /// 正在发布
        /// </summary>
        Publishing = (byte)'2',

        /// <summary>
        /// 已发布
        /// </summary>
        Published = (byte)'3'
    }

    /// <summary>
    /// TFtdcSystemStatusType是一个系统状态类型
    /// </summary>
    public enum EnumSystemStatusType : byte
    {
        /// <summary>
        /// 不活跃
        /// </summary>
        NonActive = (byte)'1',

        /// <summary>
        /// 启动
        /// </summary>
        Startup = (byte)'2',

        /// <summary>
        /// 交易开始初始化
        /// </summary>
        Initialize = (byte)'3',

        /// <summary>
        /// 交易完成初始化
        /// </summary>
        Initialized = (byte)'4',

        /// <summary>
        /// 收市开始
        /// </summary>
        Close = (byte)'5',

        /// <summary>
        /// 收市完成
        /// </summary>
        Closed = (byte)'6',

        /// <summary>
        /// 结算
        /// </summary>
        Settlement = (byte)'7'
    }

    /// <summary>
    /// TFtdcSettlementStatusType是一个结算状态类型
    /// </summary>
    public enum EnumSettlementStatusType : byte
    {
        /// <summary>
        /// 初始
        /// </summary>
        Initialize = (byte)'0',

        /// <summary>
        /// 结算中
        /// </summary>
        Settlementing = (byte)'1',

        /// <summary>
        /// 已结算
        /// </summary>
        Settlemented = (byte)'2',

        /// <summary>
        /// 结算完成
        /// </summary>
        Finished = (byte)'3'
    }

    /// <summary>
    /// TFtdcInvestorTypeType是一个投资者类型类型
    /// </summary>
    public enum EnumInvestorTypeType : byte
    {
        /// <summary>
        /// 自然人
        /// </summary>
        Person = (byte)'0',

        /// <summary>
        /// 法人
        /// </summary>
        Company = (byte)'1',

        /// <summary>
        /// 投资基金
        /// </summary>
        Fund = (byte)'2'
    }

    /// <summary>
    /// TFtdcBrokerTypeType是一个经纪公司类型类型
    /// </summary>
    public enum EnumBrokerTypeType : byte
    {
        /// <summary>
        /// 交易会员
        /// </summary>
        Trade = (byte)'0',

        /// <summary>
        /// 交易结算会员
        /// </summary>
        TradeSettle = (byte)'1'
    }

    /// <summary>
    /// TFtdcRiskLevelType是一个风险等级类型
    /// </summary>
    public enum EnumRiskLevelType : byte
    {
        /// <summary>
        /// 低风险客户
        /// </summary>
        Low = (byte)'1',

        /// <summary>
        /// 普通客户
        /// </summary>
        Normal = (byte)'2',

        /// <summary>
        /// 关注客户
        /// </summary>
        Focus = (byte)'3',

        /// <summary>
        /// 风险客户
        /// </summary>
        Risk = (byte)'4'
    }

    /// <summary>
    /// TFtdcFeeAcceptStyleType是一个手续费收取方式类型
    /// </summary>
    public enum EnumFeeAcceptStyleType : byte
    {
        /// <summary>
        /// 按交易收取
        /// </summary>
        ByTrade = (byte)'1',

        /// <summary>
        /// 按交割收取
        /// </summary>
        ByDeliv = (byte)'2',

        /// <summary>
        /// 不收
        /// </summary>
        None = (byte)'3',

        /// <summary>
        /// 按指定手续费收取
        /// </summary>
        FixFee = (byte)'4'
    }

    /// <summary>
    /// TFtdcPasswordTypeType是一个密码类型类型
    /// </summary>
    public enum EnumPasswordTypeType : byte
    {
        /// <summary>
        /// 交易密码
        /// </summary>
        Trade = (byte)'1',

        /// <summary>
        /// 资金密码
        /// </summary>
        Account = (byte)'2'
    }

    /// <summary>
    /// TFtdcAlgorithmType是一个盈亏算法类型
    /// </summary>
    public enum EnumAlgorithmType : byte
    {
        /// <summary>
        /// 浮盈浮亏都计算
        /// </summary>
        All = (byte)'1',

        /// <summary>
        /// 浮盈不计，浮亏计
        /// </summary>
        OnlyLost = (byte)'2',

        /// <summary>
        /// 浮盈计，浮亏不计
        /// </summary>
        OnlyGain = (byte)'3',

        /// <summary>
        /// 浮盈浮亏都不计算
        /// </summary>
        None = (byte)'4'
    }

    /// <summary>
    /// TFtdcIncludeCloseProfitType是一个是否包含平仓盈利类型
    /// </summary>
    public enum EnumIncludeCloseProfitType : byte
    {
        /// <summary>
        /// 包含平仓盈利
        /// </summary>
        Include = (byte)'0',

        /// <summary>
        /// 不包含平仓盈利
        /// </summary>
        NotInclude = (byte)'2'
    }

    /// <summary>
    /// TFtdcAllWithoutTradeType是一个是否受可提比例限制类型
    /// </summary>
    public enum EnumAllWithoutTradeType : byte
    {
        /// <summary>
        /// 不受可提比例限制
        /// </summary>
        Enable = (byte)'0',

        /// <summary>
        /// 受可提比例限制
        /// </summary>
        Disable = (byte)'2'
    }

    /// <summary>
    /// TFtdcFuturePwdFlagType是一个资金密码核对标志类型
    /// </summary>
    public enum EnumFuturePwdFlagType : byte
    {
        /// <summary>
        /// 不核对
        /// </summary>
        UnCheck = (byte)'0',

        /// <summary>
        /// 核对
        /// </summary>
        Check = (byte)'1'
    }

    /// <summary>
    /// TFtdcTransferTypeType是一个银期转账类型类型
    /// </summary>
    public enum EnumTransferTypeType : byte
    {
        /// <summary>
        /// 银行转期货
        /// </summary>
        BankToFuture = (byte)'0',

        /// <summary>
        /// 期货转银行
        /// </summary>
        FutureToBank = (byte)'1'
    }

    /// <summary>
    /// TFtdcTransferValidFlagType是一个转账有效标志类型
    /// </summary>
    public enum EnumTransferValidFlagType : byte
    {
        /// <summary>
        /// 无效或失败
        /// </summary>
        Invalid = (byte)'0',

        /// <summary>
        /// 有效
        /// </summary>
        Valid = (byte)'1',

        /// <summary>
        /// 冲正
        /// </summary>
        Reverse = (byte)'2'
    }

    /// <summary>
    /// TFtdcReasonType是一个事由类型
    /// </summary>
    public enum EnumReasonType : byte
    {
        /// <summary>
        /// 错单
        /// </summary>
        CD = (byte)'0',

        /// <summary>
        /// 资金在途
        /// </summary>
        ZT = (byte)'1',

        /// <summary>
        /// 其它
        /// </summary>
        QT = (byte)'2'
    }

    /// <summary>
    /// TFtdcSexType是一个性别类型
    /// </summary>
    public enum EnumSexType : byte
    {
        /// <summary>
        /// 未知
        /// </summary>
        None = (byte)'0',

        /// <summary>
        /// 男
        /// </summary>
        Man = (byte)'1',

        /// <summary>
        /// 女
        /// </summary>
        Woman = (byte)'2'
    }

    /// <summary>
    /// TFtdcUserTypeType是一个用户类型类型
    /// </summary>
    public enum EnumUserTypeType : byte
    {
        /// <summary>
        /// 投资者
        /// </summary>
        Investor = (byte)'0',

        /// <summary>
        /// 操作员
        /// </summary>
        Operator = (byte)'1',

        /// <summary>
        /// 管理员
        /// </summary>
        SuperUser = (byte)'2'
    }

    /// <summary>
    /// TFtdcRateTypeType是一个费率类型类型
    /// </summary>
    public enum EnumRateTypeType : byte
    {
        /// <summary>
        /// 保证金率
        /// </summary>
        MarginRate = (byte)'2',

        /// <summary>
        /// 手续费率
        /// </summary>
        CommRate = (byte)'1',

        /// <summary>
        /// 所有
        /// </summary>
        AllRate = (byte)'0'
    }

    /// <summary>
    /// TFtdcNoteTypeType是一个通知类型类型
    /// </summary>
    public enum EnumNoteTypeType : byte
    {
        /// <summary>
        /// 交易结算单
        /// </summary>
        TradeSettleBill = (byte)'1',

        /// <summary>
        /// 交易结算月报
        /// </summary>
        TradeSettleMonth = (byte)'2',

        /// <summary>
        /// 追加保证金通知书
        /// </summary>
        CallMarginNotes = (byte)'3',

        /// <summary>
        /// 强行平仓通知书
        /// </summary>
        ForceCloseNotes = (byte)'4',

        /// <summary>
        /// 成交通知书
        /// </summary>
        TradeNotes = (byte)'5',

        /// <summary>
        /// 交割通知书
        /// </summary>
        DelivNotes = (byte)'6'
    }

    /// <summary>
    /// TFtdcSettlementStyleType是一个结算单方式类型
    /// </summary>
    public enum EnumSettlementStyleType : byte
    {
        /// <summary>
        /// 逐日盯市
        /// </summary>
        Day = (byte)'1',

        /// <summary>
        /// 逐笔对冲
        /// </summary>
        Volume = (byte)'2'
    }

    /// <summary>
    /// TFtdcSettlementBillTypeType是一个结算单类型类型
    /// </summary>
    public enum EnumSettlementBillTypeType : byte
    {
        /// <summary>
        /// 日报
        /// </summary>
        Day = (byte)'0',

        /// <summary>
        /// 月报
        /// </summary>
        Month = (byte)'1'
    }

    /// <summary>
    /// TFtdcUserRightTypeType是一个客户权限类型类型
    /// </summary>
    public enum EnumUserRightTypeType : byte
    {
        /// <summary>
        /// 登录
        /// </summary>
        Logon = (byte)'1',

        /// <summary>
        /// 银期转帐
        /// </summary>
        Transfer = (byte)'2',

        /// <summary>
        /// 邮寄结算单
        /// </summary>
        EMail = (byte)'3',

        /// <summary>
        /// 传真结算单
        /// </summary>
        Fax = (byte)'4',

        /// <summary>
        /// 条件单
        /// </summary>
        ConditionOrder = (byte)'5'
    }

    /// <summary>
    /// TFtdcMarginPriceTypeType是一个保证金价格类型类型
    /// </summary>
    public enum EnumMarginPriceTypeType : byte
    {
        /// <summary>
        /// 昨结算价
        /// </summary>
        PreSettlementPrice = (byte)'1',

        /// <summary>
        /// 最新价
        /// </summary>
        SettlementPrice = (byte)'2',

        /// <summary>
        /// 成交均价
        /// </summary>
        AveragePrice = (byte)'3',

        /// <summary>
        /// 开仓价
        /// </summary>
        OpenPrice = (byte)'4'
    }

    /// <summary>
    /// TFtdcBillGenStatusType是一个结算单生成状态类型
    /// </summary>
    public enum EnumBillGenStatusType : byte
    {
        /// <summary>
        /// 不生成
        /// </summary>
        None = (byte)'0',

        /// <summary>
        /// 未生成
        /// </summary>
        NoGenerated = (byte)'1',

        /// <summary>
        /// 已生成
        /// </summary>
        Generated = (byte)'2'
    }

    /// <summary>
    /// TFtdcAlgoTypeType是一个算法类型类型
    /// </summary>
    public enum EnumAlgoTypeType : byte
    {
        /// <summary>
        /// 持仓处理算法
        /// </summary>
        HandlePositionAlgo = (byte)'1',

        /// <summary>
        /// 寻找保证金率算法
        /// </summary>
        FindMarginRateAlgo = (byte)'2'
    }

    /// <summary>
    /// TFtdcHandlePositionAlgoIDType是一个持仓处理算法编号类型
    /// </summary>
    public enum EnumHandlePositionAlgoIDType : byte
    {
        /// <summary>
        /// 基本
        /// </summary>
        Base = (byte)'1',

        /// <summary>
        /// 大连商品交易所
        /// </summary>
        DCE = (byte)'2',

        /// <summary>
        /// 郑州商品交易所
        /// </summary>
        CZCE = (byte)'3'
    }

    /// <summary>
    /// TFtdcFindMarginRateAlgoIDType是一个寻找保证金率算法编号类型
    /// </summary>
    public enum EnumFindMarginRateAlgoIDType : byte
    {
        /// <summary>
        /// 基本
        /// </summary>
        Base = (byte)'1',

        /// <summary>
        /// 大连商品交易所
        /// </summary>
        DCE = (byte)'2',

        /// <summary>
        /// 郑州商品交易所
        /// </summary>
        CZCE = (byte)'3'
    }

    /// <summary>
    /// TFtdcHandleTradingAccountAlgoIDType是一个资金处理算法编号类型
    /// </summary>
    public enum EnumHandleTradingAccountAlgoIDType : byte
    {
        /// <summary>
        /// 基本
        /// </summary>
        Base = (byte)'1',

        /// <summary>
        /// 大连商品交易所
        /// </summary>
        DCE = (byte)'2',

        /// <summary>
        /// 郑州商品交易所
        /// </summary>
        CZCE = (byte)'3'
    }

    /// <summary>
    /// TFtdcPersonTypeType是一个联系人类型类型
    /// </summary>
    public enum EnumPersonTypeType : byte
    {
        /// <summary>
        /// 指定下单人
        /// </summary>
        Order = (byte)'1',

        /// <summary>
        /// 开户授权人
        /// </summary>
        Open = (byte)'2',

        /// <summary>
        /// 资金调拨人
        /// </summary>
        Fund = (byte)'3',

        /// <summary>
        /// 结算单确认人
        /// </summary>
        Settlement = (byte)'4'
    }

    /// <summary>
    /// TFtdcQueryInvestorRangeType是一个查询范围类型
    /// </summary>
    public enum EnumQueryInvestorRangeType : byte
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = (byte)'1',

        /// <summary>
        /// 查询分类
        /// </summary>
        Group = (byte)'2',

        /// <summary>
        /// 单一投资者
        /// </summary>
        Single = (byte)'3'
    }

    /// <summary>
    /// TFtdcInvestorRiskStatusType是一个投资者风险状态类型
    /// </summary>
    public enum EnumInvestorRiskStatusType : byte
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = (byte)'1',

        /// <summary>
        /// 警告
        /// </summary>
        Warn = (byte)'2',

        /// <summary>
        /// 追保
        /// </summary>
        Call = (byte)'3',

        /// <summary>
        /// 强平
        /// </summary>
        Force = (byte)'4',

        /// <summary>
        /// 异常
        /// </summary>
        Exception = (byte)'5'
    }

    /// <summary>
    /// TFtdcUserEventTypeType是一个用户事件类型类型
    /// </summary>
    public enum EnumUserEventTypeType : byte
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = (byte)' ',

        /// <summary>
        /// 登录
        /// </summary>
        Login = (byte)'1',

        /// <summary>
        /// 登出
        /// </summary>
        Logout = (byte)'2',

        /// <summary>
        /// 交易成功
        /// </summary>
        Trading = (byte)'3',

        /// <summary>
        /// 交易失败
        /// </summary>
        TradingError = (byte)'4',

        /// <summary>
        /// 修改密码
        /// </summary>
        UpdatePassword = (byte)'5',

        /// <summary>
        /// 其他
        /// </summary>
        Other = (byte)'9'
    }

    /// <summary>
    /// TFtdcCloseStyleType是一个平仓方式类型
    /// </summary>
    public enum EnumCloseStyleType : byte
    {
        /// <summary>
        /// 先开先平
        /// </summary>
        Close = (byte)'0',

        /// <summary>
        /// 先平今再平昨
        /// </summary>
        CloseToday = (byte)'1'
    }

    /// <summary>
    /// TFtdcStatModeType是一个统计方式类型
    /// </summary>
    public enum EnumStatModeType : byte
    {
        /// <summary>
        /// ----
        /// </summary>
        Non = (byte)'0',

        /// <summary>
        /// 按合约统计
        /// </summary>
        Instrument = (byte)'1',

        /// <summary>
        /// 按产品统计
        /// </summary>
        Product = (byte)'2',

        /// <summary>
        /// 按投资者统计
        /// </summary>
        Investor = (byte)'3'
    }

    /// <summary>
    /// TFtdcParkedOrderStatusType是一个预埋单状态类型
    /// </summary>
    public enum EnumParkedOrderStatusType : byte
    {
        /// <summary>
        /// 未发送
        /// </summary>
        NotSend = (byte)'1',

        /// <summary>
        /// 已发送
        /// </summary>
        Send = (byte)'2',

        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = (byte)'3'
    }

    /// <summary>
    /// TFtdcVirDealStatusType是一个处理状态类型
    /// </summary>
    public enum EnumVirDealStatusType : byte
    {
        /// <summary>
        /// 正在处理
        /// </summary>
        Dealing = (byte)'1',

        /// <summary>
        /// 处理成功
        /// </summary>
        DeaclSucceed = (byte)'2'
    }

    /// <summary>
    /// TFtdcOrgSystemIDType是一个原有系统代码类型
    /// </summary>
    public enum EnumOrgSystemIDType : byte
    {
        /// <summary>
        /// 综合交易平台
        /// </summary>
        Standard = (byte)'0',

        /// <summary>
        /// 易盛系统
        /// </summary>
        ESunny = (byte)'1',

        /// <summary>
        /// 金仕达V6系统
        /// </summary>
        KingStarV6 = (byte)'2'
    }

    /// <summary>
    /// TFtdcVirTradeStatusType是一个交易状态类型
    /// </summary>
    public enum EnumVirTradeStatusType : byte
    {
        /// <summary>
        /// 正常处理中
        /// </summary>
        NaturalDeal = (byte)'0',

        /// <summary>
        /// 成功结束
        /// </summary>
        SucceedEnd = (byte)'1',

        /// <summary>
        /// 失败结束
        /// </summary>
        FailedEND = (byte)'2',

        /// <summary>
        /// 异常中
        /// </summary>
        Exception = (byte)'3',

        /// <summary>
        /// 已人工异常处理
        /// </summary>
        ManualDeal = (byte)'4',

        /// <summary>
        /// 通讯异常 ，请人工处理
        /// </summary>
        MesException = (byte)'5',

        /// <summary>
        /// 系统出错，请人工处理
        /// </summary>
        SysException = (byte)'6'
    }

    /// <summary>
    /// TFtdcVirBankAccTypeType是一个银行帐户类型类型
    /// </summary>
    public enum EnumVirBankAccTypeType : byte
    {
        /// <summary>
        /// 存折
        /// </summary>
        BankBook = (byte)'1',

        /// <summary>
        /// 储蓄卡
        /// </summary>
        BankCard = (byte)'2',

        /// <summary>
        /// 信用卡
        /// </summary>
        CreditCard = (byte)'3'
    }

    /// <summary>
    /// TFtdcVirementStatusType是一个银行帐户类型类型
    /// </summary>
    public enum EnumVirementStatusType : byte
    {
        /// <summary>
        /// 正常
        /// </summary>
        Natural = (byte)'0',

        /// <summary>
        /// 销户
        /// </summary>
        Canceled = (byte)'9'
    }

    /// <summary>
    /// TFtdcVirementAvailAbilityType是一个有效标志类型
    /// </summary>
    public enum EnumVirementAvailAbilityType : byte
    {
        /// <summary>
        /// 未确认
        /// </summary>
        NoAvailAbility = (byte)'0',

        /// <summary>
        /// 有效
        /// </summary>
        AvailAbility = (byte)'1',

        /// <summary>
        /// 冲正
        /// </summary>
        Repeal = (byte)'2'
    }

    /// <summary>
    /// TFtdcVirementTradeCodeType是一个交易代码类型
    /// </summary>
    public enum EnumVirementTradeCodeType : byte
    {
        /// <summary>
        /// 银行发起银行资金转期货
        /// </summary>
        BankBankToFuture = (byte)'1',//'102001',

        /// <summary>
        /// 银行发起期货资金转银行
        /// </summary>
        BankFutureToBank = (byte)'2',//'102002',

        /// <summary>
        /// 期货发起银行资金转期货
        /// </summary>
        FutureBankToFuture = (byte)'3',//'202001',

        /// <summary>
        /// 期货发起期货资金转银行
        /// </summary>
        FutureFutureToBank = (byte)'4'//'202002'
    }

    /// <summary>
    /// TFtdcCFMMCKeyKindType是一个动态密钥类别(保证金监管)类型
    /// </summary>
    public enum EnumCFMMCKeyKindType : byte
    {
        /// <summary>
        /// 主动请求更新
        /// </summary>
        REQUEST = (byte)'R',

        /// <summary>
        /// CFMMC自动更新
        /// </summary>
        AUTO = (byte)'A',

        /// <summary>
        /// CFMMC手动更新
        /// </summary>
        MANUAL = (byte)'M'
    }

    /// <summary>
    /// TFtdcCertificationTypeType是一个证件类型类型
    /// </summary>
    public enum EnumCertificationTypeType : byte
    {
        /// <summary>
        /// 身份证
        /// </summary>
        IDCard = (byte)'0',

        /// <summary>
        /// 护照
        /// </summary>
        Passport = (byte)'1',

        /// <summary>
        /// 军官证
        /// </summary>
        OfficerIDCard = (byte)'2',

        /// <summary>
        /// 士兵证
        /// </summary>
        SoldierIDCard = (byte)'3',

        /// <summary>
        /// 回乡证
        /// </summary>
        HomeComingCard = (byte)'4',

        /// <summary>
        /// 户口簿
        /// </summary>
        HouseholdRegister = (byte)'5',

        /// <summary>
        /// 营业执照号
        /// </summary>
        LicenseNo = (byte)'6',

        /// <summary>
        /// 组织机构代码证
        /// </summary>
        InstitutionCodeCard = (byte)'7',

        /// <summary>
        /// 临时营业执照号
        /// </summary>
        TempLicenseNo = (byte)'8',

        /// <summary>
        /// 民办非企业登记证书
        /// </summary>
        NoEnterpriseLicenseNo = (byte)'9',

        /// <summary>
        /// 其他证件
        /// </summary>
        OtherCard = (byte)'x',

        /// <summary>
        /// 主管部门批文
        /// </summary>
        SuperDepAgree = (byte)'a'
    }

    /// <summary>
    /// TFtdcFileBusinessCodeType是一个文件业务功能类型
    /// </summary>
    public enum EnumFileBusinessCodeType : byte
    {
        /// <summary>
        /// 其他
        /// </summary>
        Others = (byte)'0',

        /// <summary>
        /// 转账交易明细对账
        /// </summary>
        TransferDetails = (byte)'1',

        /// <summary>
        /// 客户账户状态对账
        /// </summary>
        CustAccStatus = (byte)'2',

        /// <summary>
        /// 账户类交易明细对账
        /// </summary>
        AccountTradeDetails = (byte)'3',

        /// <summary>
        /// 期货账户信息变更明细对账
        /// </summary>
        FutureAccountChangeInfoDetails = (byte)'4',

        /// <summary>
        /// 客户资金台账余额明细对账
        /// </summary>
        CustMoneyDetail = (byte)'5',

        /// <summary>
        /// 客户销户结息明细对账
        /// </summary>
        CustCancelAccountInfo = (byte)'6',

        /// <summary>
        /// 客户资金余额对账结果
        /// </summary>
        CustMoneyResult = (byte)'7',

        /// <summary>
        /// 其它对账异常结果文件
        /// </summary>
        OthersExceptionResult = (byte)'8',

        /// <summary>
        /// 客户结息净额明细
        /// </summary>
        CustInterestNetMoneyDetails = (byte)'9',

        /// <summary>
        /// 客户资金交收明细
        /// </summary>
        CustMoneySendAndReceiveDetails = (byte)'a',

        /// <summary>
        /// 法人存管银行资金交收汇总
        /// </summary>
        CorporationMoneyTotal = (byte)'b',

        /// <summary>
        /// 主体间资金交收汇总
        /// </summary>
        MainbodyMoneyTotal = (byte)'c',

        /// <summary>
        /// 总分平衡监管数据
        /// </summary>
        MainPartMonitorData = (byte)'d',

        /// <summary>
        /// 存管银行备付金余额
        /// </summary>
        PreparationMoney = (byte)'e',

        /// <summary>
        /// 协办存管银行资金监管数据
        /// </summary>
        BankMoneyMonitorData = (byte)'f'
    }

    /// <summary>
    /// TFtdcCashExchangeCodeType是一个汇钞标志类型
    /// </summary>
    public enum EnumCashExchangeCodeType : byte
    {
        /// <summary>
        /// 汇
        /// </summary>
        Exchange = (byte)'1',

        /// <summary>
        /// 钞
        /// </summary>
        Cash = (byte)'2'
    }

    /// <summary>
    /// TFtdcYesNoIndicatorType是一个是或否标识类型
    /// </summary>
    public enum EnumYesNoIndicatorType : byte
    {
        /// <summary>
        /// 是
        /// </summary>
        Yes = (byte)'0',

        /// <summary>
        /// 否
        /// </summary>
        No = (byte)'1'
    }

    /// <summary>
    /// TFtdcBanlanceTypeType是一个余额类型类型
    /// </summary>
    public enum EnumBanlanceTypeType : byte
    {
        /// <summary>
        /// 当前余额
        /// </summary>
        CurrentMoney = (byte)'0',

        /// <summary>
        /// 可用余额
        /// </summary>
        UsableMoney = (byte)'1',

        /// <summary>
        /// 可取余额
        /// </summary>
        FetchableMoney = (byte)'2',

        /// <summary>
        /// 冻结余额
        /// </summary>
        FreezeMoney = (byte)'3'
    }

    /// <summary>
    /// TFtdcGenderType是一个性别类型
    /// </summary>
    public enum EnumGenderType : byte
    {
        /// <summary>
        /// 未知状态
        /// </summary>
        Unknown = (byte)'0',

        /// <summary>
        /// 男
        /// </summary>
        Male = (byte)'1',

        /// <summary>
        /// 女
        /// </summary>
        Female = (byte)'2'
    }

    /// <summary>
    /// TFtdcFeePayFlagType是一个费用支付标志类型
    /// </summary>
    public enum EnumFeePayFlagType : byte
    {
        /// <summary>
        /// 由受益方支付费用
        /// </summary>
        BEN = (byte)'0',

        /// <summary>
        /// 由发送方支付费用
        /// </summary>
        OUR = (byte)'1',

        /// <summary>
        /// 由发送方支付发起的费用，受益方支付接受的费用
        /// </summary>
        SHA = (byte)'2'
    }

    /// <summary>
    /// TFtdcPassWordKeyTypeType是一个密钥类型类型
    /// </summary>
    public enum EnumPassWordKeyTypeType : byte
    {
        /// <summary>
        /// 交换密钥
        /// </summary>
        ExchangeKey = (byte)'0',

        /// <summary>
        /// 密码密钥
        /// </summary>
        PassWordKey = (byte)'1',

        /// <summary>
        /// MAC密钥
        /// </summary>
        MACKey = (byte)'2',

        /// <summary>
        /// 报文密钥
        /// </summary>
        MessageKey = (byte)'3'
    }

    /// <summary>
    /// TFtdcFBTPassWordTypeType是一个密码类型类型
    /// </summary>
    public enum EnumFBTPassWordTypeType : byte
    {
        /// <summary>
        /// 查询
        /// </summary>
        Query = (byte)'0',

        /// <summary>
        /// 取款
        /// </summary>
        Fetch = (byte)'1',

        /// <summary>
        /// 转帐
        /// </summary>
        Transfer = (byte)'2',

        /// <summary>
        /// 交易
        /// </summary>
        Trade = (byte)'3'
    }

    /// <summary>
    /// TFtdcFBTEncryModeType是一个加密方式类型
    /// </summary>
    public enum EnumFBTEncryModeType : byte
    {
        /// <summary>
        /// 不加密
        /// </summary>
        NoEncry = (byte)'0',

        /// <summary>
        /// DES
        /// </summary>
        DES = (byte)'1',

        /// <summary>
        /// 3DES
        /// </summary>
        DES3 = (byte)'2'
    }

    /// <summary>
    /// TFtdcBankRepealFlagType是一个银行冲正标志类型
    /// </summary>
    public enum EnumBankRepealFlagType : byte
    {
        /// <summary>
        /// 银行无需自动冲正
        /// </summary>
        BankNotNeedRepeal = (byte)'0',

        /// <summary>
        /// 银行待自动冲正
        /// </summary>
        BankWaitingRepeal = (byte)'1',

        /// <summary>
        /// 银行已自动冲正
        /// </summary>
        BankBeenRepealed = (byte)'2'
    }

    /// <summary>
    /// TFtdcBrokerRepealFlagType是一个期商冲正标志类型
    /// </summary>
    public enum EnumBrokerRepealFlagType : byte
    {
        /// <summary>
        /// 期商无需自动冲正
        /// </summary>
        BrokerNotNeedRepeal = (byte)'0',

        /// <summary>
        /// 期商待自动冲正
        /// </summary>
        BrokerWaitingRepeal = (byte)'1',

        /// <summary>
        /// 期商已自动冲正
        /// </summary>
        BrokerBeenRepealed = (byte)'2'
    }

    /// <summary>
    /// TFtdcInstitutionTypeType是一个机构类别类型
    /// </summary>
    public enum EnumInstitutionTypeType : byte
    {
        /// <summary>
        /// 银行
        /// </summary>
        Bank = (byte)'0',

        /// <summary>
        /// 期商
        /// </summary>
        Future = (byte)'1',

        /// <summary>
        /// 券商
        /// </summary>
        Store = (byte)'2'
    }

    /// <summary>
    /// TFtdcLastFragmentType是一个最后分片标志类型
    /// </summary>
    public enum EnumLastFragmentType : byte
    {
        /// <summary>
        /// 是最后分片
        /// </summary>
        Yes = (byte)'0',

        /// <summary>
        /// 不是最后分片
        /// </summary>
        No = (byte)'1'
    }

    /// <summary>
    /// TFtdcBankAccStatusType是一个银行账户状态类型
    /// </summary>
    public enum EnumBankAccStatusType : byte
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = (byte)'0',

        /// <summary>
        /// 冻结
        /// </summary>
        Freeze = (byte)'1',

        /// <summary>
        /// 挂失
        /// </summary>
        ReportLoss = (byte)'2'
    }

    /// <summary>
    /// TFtdcMoneyAccountStatusType是一个资金账户状态类型
    /// </summary>
    public enum EnumMoneyAccountStatusType : byte
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = (byte)'0',

        /// <summary>
        /// 销户
        /// </summary>
        Cancel = (byte)'1'
    }

    /// <summary>
    /// TFtdcManageStatusType是一个存管状态类型
    /// </summary>
    public enum EnumManageStatusType : byte
    {
        /// <summary>
        /// 指定存管
        /// </summary>
        Point = (byte)'0',

        /// <summary>
        /// 预指定
        /// </summary>
        PrePoint = (byte)'1',

        /// <summary>
        /// 撤销指定
        /// </summary>
        CancelPoint = (byte)'2'
    }

    /// <summary>
    /// TFtdcSystemTypeType是一个应用系统类型类型
    /// </summary>
    public enum EnumSystemTypeType : byte
    {
        /// <summary>
        /// 银期转帐
        /// </summary>
        FutureBankTransfer = (byte)'0',

        /// <summary>
        /// 银证转帐
        /// </summary>
        StockBankTransfer = (byte)'1',

        /// <summary>
        /// 第三方存管
        /// </summary>
        TheThirdPartStore = (byte)'2'
    }

    /// <summary>
    /// TFtdcTxnEndFlagType是一个银期转帐划转结果标志类型
    /// </summary>
    public enum EnumTxnEndFlagType : byte
    {
        /// <summary>
        /// 正常处理中
        /// </summary>
        NormalProcessing = (byte)'0',

        /// <summary>
        /// 成功结束
        /// </summary>
        Success = (byte)'1',

        /// <summary>
        /// 失败结束
        /// </summary>
        Failed = (byte)'2',

        /// <summary>
        /// 异常中
        /// </summary>
        Abnormal = (byte)'3',

        /// <summary>
        /// 已人工异常处理
        /// </summary>
        ManualProcessedForException = (byte)'4',

        /// <summary>
        /// 通讯异常 ，请人工处理
        /// </summary>
        CommuFailedNeedManualProcess = (byte)'5',

        /// <summary>
        /// 系统出错，请人工处理
        /// </summary>
        SysErrorNeedManualProcess = (byte)'6'
    }

    /// <summary>
    /// TFtdcProcessStatusType是一个银期转帐服务处理状态类型
    /// </summary>
    public enum EnumProcessStatusType : byte
    {
        /// <summary>
        /// 未处理
        /// </summary>
        NotProcess = (byte)'0',

        /// <summary>
        /// 开始处理
        /// </summary>
        StartProcess = (byte)'1',

        /// <summary>
        /// 处理完成
        /// </summary>
        Finished = (byte)'2'
    }

    /// <summary>
    /// TFtdcCustTypeType是一个客户类型类型
    /// </summary>
    public enum EnumCustTypeType : byte
    {
        /// <summary>
        /// 自然人
        /// </summary>
        Person = (byte)'0',

        /// <summary>
        /// 机构户
        /// </summary>
        Institution = (byte)'1'
    }

    /// <summary>
    /// TFtdcFBTTransferDirectionType是一个银期转帐方向类型
    /// </summary>
    public enum EnumFBTTransferDirectionType : byte
    {
        /// <summary>
        /// 入金，银行转期货
        /// </summary>
        FromBankToFuture = (byte)'1',

        /// <summary>
        /// 出金，期货转银行
        /// </summary>
        FromFutureToBank = (byte)'2'
    }

    /// <summary>
    /// TFtdcOpenOrDestroyType是一个开销户类别类型
    /// </summary>
    public enum EnumOpenOrDestroyType : byte
    {
        /// <summary>
        /// 开户
        /// </summary>
        Open = (byte)'1',

        /// <summary>
        /// 销户
        /// </summary>
        Destroy = (byte)'0'
    }

    /// <summary>
    /// TFtdcAvailabilityFlagType是一个有效标志类型
    /// </summary>
    public enum EnumAvailabilityFlagType : byte
    {
        /// <summary>
        /// 未确认
        /// </summary>
        Invalid = (byte)'0',

        /// <summary>
        /// 有效
        /// </summary>
        Valid = (byte)'1',

        /// <summary>
        /// 冲正
        /// </summary>
        Repeal = (byte)'2'
    }

    /// <summary>
    /// TFtdcOrganTypeType是一个机构类型类型
    /// </summary>
    public enum EnumOrganTypeType : byte
    {
        /// <summary>
        /// 银行代理
        /// </summary>
        Bank = (byte)'1',

        /// <summary>
        /// 交易前置
        /// </summary>
        Future = (byte)'2',

        /// <summary>
        /// 银期转帐平台管理
        /// </summary>
        PlateForm = (byte)'9'
    }

    /// <summary>
    /// TFtdcOrganLevelType是一个机构级别类型
    /// </summary>
    public enum EnumOrganLevelType : byte
    {
        /// <summary>
        /// 银行总行或期商总部
        /// </summary>
        HeadQuarters = (byte)'1',

        /// <summary>
        /// 银行分中心或期货公司营业部
        /// </summary>
        Branch = (byte)'2'
    }

    /// <summary>
    /// TFtdcProtocalIDType是一个协议类型类型
    /// </summary>
    public enum EnumProtocalIDType : byte
    {
        /// <summary>
        /// 期商协议
        /// </summary>
        FutureProtocal = (byte)'0',

        /// <summary>
        /// 工行协议
        /// </summary>
        ICBCProtocal = (byte)'1',

        /// <summary>
        /// 农行协议
        /// </summary>
        ABCProtocal = (byte)'2',

        /// <summary>
        /// 中国银行协议
        /// </summary>
        CBCProtocal = (byte)'3',

        /// <summary>
        /// 建行协议
        /// </summary>
        CCBProtocal = (byte)'4',

        /// <summary>
        /// 交行协议
        /// </summary>
        BOCOMProtocal = (byte)'5',

        /// <summary>
        /// 银期转帐平台协议
        /// </summary>
        FBTPlateFormProtocal = (byte)'X'
    }

    /// <summary>
    /// TFtdcConnectModeType是一个套接字连接方式类型
    /// </summary>
    public enum EnumConnectModeType : byte
    {
        /// <summary>
        /// 短连接
        /// </summary>
        ShortConnect = (byte)'0',

        /// <summary>
        /// 长连接
        /// </summary>
        LongConnect = (byte)'1'
    }

    /// <summary>
    /// TFtdcSyncModeType是一个套接字通信方式类型
    /// </summary>
    public enum EnumSyncModeType : byte
    {
        /// <summary>
        /// 异步
        /// </summary>
        ASync = (byte)'0',

        /// <summary>
        /// 同步
        /// </summary>
        Sync = (byte)'1'
    }

    /// <summary>
    /// TFtdcBankAccTypeType是一个银行帐号类型类型
    /// </summary>
    public enum EnumBankAccTypeType : byte
    {
        /// <summary>
        /// 银行存折
        /// </summary>
        BankBook = (byte)'1',

        /// <summary>
        /// 储蓄卡
        /// </summary>
        SavingCard = (byte)'2',

        /// <summary>
        /// 信用卡
        /// </summary>
        CreditCard = (byte)'3'
    }

    /// <summary>
    /// TFtdcFutureAccTypeType是一个期货公司帐号类型类型
    /// </summary>
    public enum EnumFutureAccTypeType : byte
    {
        /// <summary>
        /// 银行存折
        /// </summary>
        BankBook = (byte)'1',

        /// <summary>
        /// 储蓄卡
        /// </summary>
        SavingCard = (byte)'2',

        /// <summary>
        /// 信用卡
        /// </summary>
        CreditCard = (byte)'3'
    }

    /// <summary>
    /// TFtdcOrganStatusType是一个接入机构状态类型
    /// </summary>
    public enum EnumOrganStatusType : byte
    {
        /// <summary>
        /// 启用
        /// </summary>
        Ready = (byte)'0',

        /// <summary>
        /// 签到
        /// </summary>
        CheckIn = (byte)'1',

        /// <summary>
        /// 签退
        /// </summary>
        CheckOut = (byte)'2',

        /// <summary>
        /// 对帐文件到达
        /// </summary>
        CheckFileArrived = (byte)'3',

        /// <summary>
        /// 对帐
        /// </summary>
        CheckDetail = (byte)'4',

        /// <summary>
        /// 日终清理
        /// </summary>
        DayEndClean = (byte)'5',

        /// <summary>
        /// 注销
        /// </summary>
        Invalid = (byte)'9'
    }

    /// <summary>
    /// TFtdcCCBFeeModeType是一个建行收费模式类型
    /// </summary>
    public enum EnumCCBFeeModeType : byte
    {
        /// <summary>
        /// 按金额扣收
        /// </summary>
        ByAmount = (byte)'1',

        /// <summary>
        /// 按月扣收
        /// </summary>
        ByMonth = (byte)'2'
    }

    /// <summary>
    /// TFtdcCommApiTypeType是一个通讯API类型类型
    /// </summary>
    public enum EnumCommApiTypeType : byte
    {
        /// <summary>
        /// 客户端
        /// </summary>
        Client = (byte)'1',

        /// <summary>
        /// 服务端
        /// </summary>
        Server = (byte)'2',

        /// <summary>
        /// 交易系统的UserApi
        /// </summary>
        UserApi = (byte)'3'
    }

    /// <summary>
    /// TFtdcLinkStatusType是一个连接状态类型
    /// </summary>
    public enum EnumLinkStatusType : byte
    {
        /// <summary>
        /// 已经连接
        /// </summary>
        Connected = (byte)'1',

        /// <summary>
        /// 没有连接
        /// </summary>
        Disconnected = (byte)'2'
    }

    /// <summary>
    /// TFtdcPwdFlagType是一个密码核对标志类型
    /// </summary>
    public enum EnumPwdFlagType : byte
    {
        /// <summary>
        /// 不核对
        /// </summary>
        NoCheck = (byte)'0',

        /// <summary>
        /// 明文核对
        /// </summary>
        BlankCheck = (byte)'1',

        /// <summary>
        /// 密文核对
        /// </summary>
        EncryptCheck = (byte)'2'
    }

    /// <summary>
    /// TFtdcSecuAccTypeType是一个期货帐号类型类型
    /// </summary>
    public enum EnumSecuAccTypeType : byte
    {
        /// <summary>
        /// 资金帐号
        /// </summary>
        AccountID = (byte)'1',

        /// <summary>
        /// 资金卡号
        /// </summary>
        CardID = (byte)'2',

        /// <summary>
        /// 上海股东帐号
        /// </summary>
        SHStockholderID = (byte)'3',

        /// <summary>
        /// 深圳股东帐号
        /// </summary>
        SZStockholderID = (byte)'4'
    }

    /// <summary>
    /// TFtdcTransferStatusType是一个转账交易状态类型
    /// </summary>
    public enum EnumTransferStatusType : byte
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = (byte)'0',

        /// <summary>
        /// 被冲正
        /// </summary>
        Repealed = (byte)'1'
    }

    /// <summary>
    /// TFtdcSponsorTypeType是一个发起方类型
    /// </summary>
    public enum EnumSponsorTypeType : byte
    {
        /// <summary>
        /// 期商
        /// </summary>
        Broker = (byte)'0',

        /// <summary>
        /// 银行
        /// </summary>
        Bank = (byte)'1'
    }

    /// <summary>
    /// TFtdcReqRspTypeType是一个请求响应类别类型
    /// </summary>
    public enum EnumReqRspTypeType : byte
    {
        /// <summary>
        /// 请求
        /// </summary>
        Request = (byte)'0',

        /// <summary>
        /// 响应
        /// </summary>
        Response = (byte)'1'
    }

    /// <summary>
    /// TFtdcFBTUserEventTypeType是一个银期转帐用户事件类型类型
    /// </summary>
    public enum EnumFBTUserEventTypeType : byte
    {
        /// <summary>
        /// 签到
        /// </summary>
        SignIn = (byte)'0',

        /// <summary>
        /// 银行转期货
        /// </summary>
        FromBankToFuture = (byte)'1',

        /// <summary>
        /// 期货转银行
        /// </summary>
        FromFutureToBank = (byte)'2',

        /// <summary>
        /// 开户
        /// </summary>
        OpenAccount = (byte)'3',

        /// <summary>
        /// 销户
        /// </summary>
        CancelAccount = (byte)'4',

        /// <summary>
        /// 变更银行账户
        /// </summary>
        ChangeAccount = (byte)'5',

        /// <summary>
        /// 冲正银行转期货
        /// </summary>
        RepealFromBankToFuture = (byte)'6',

        /// <summary>
        /// 冲正期货转银行
        /// </summary>
        RepealFromFutureToBank = (byte)'7',

        /// <summary>
        /// 查询银行账户
        /// </summary>
        QueryBankAccount = (byte)'8',

        /// <summary>
        /// 查询期货账户
        /// </summary>
        QueryFutureAccount = (byte)'9',

        /// <summary>
        /// 签退
        /// </summary>
        SignOut = (byte)'A',

        /// <summary>
        /// 密钥同步
        /// </summary>
        SyncKey = (byte)'B',

        /// <summary>
        /// 其他
        /// </summary>
        Other = (byte)'Z'
    }

    /// <summary>
    /// TFtdcNotifyClassType是一个风险通知类型类型
    /// </summary>
    public enum EnumNotifyClassType : byte
    {
        /// <summary>
        /// 正常
        /// </summary>
        NOERROR = (byte)'0',

        /// <summary>
        /// 警示
        /// </summary>
        Warn = (byte)'1',

        /// <summary>
        /// 追保
        /// </summary>
        Call = (byte)'2',

        /// <summary>
        /// 强平
        /// </summary>
        Force = (byte)'3',

        /// <summary>
        /// 穿仓
        /// </summary>
        CHUANCANG = (byte)'4',

        /// <summary>
        /// 异常
        /// </summary>
        Exception = (byte)'5'
    }

    /// <summary>
    /// TFtdcForceCloseTypeType是一个强平单类型类型
    /// </summary>
    public enum EnumForceCloseTypeType : byte
    {
        /// <summary>
        /// 手工强平
        /// </summary>
        Manual = (byte)'0',

        /// <summary>
        /// 单一投资者辅助强平
        /// </summary>
        Single = (byte)'1',

        /// <summary>
        /// 批量投资者辅助强平
        /// </summary>
        Group = (byte)'2'
    }

    /// <summary>
    /// TFtdcRiskNotifyMethodType是一个风险通知途径类型
    /// </summary>
    public enum EnumRiskNotifyMethodType : byte
    {
        /// <summary>
        /// 系统通知
        /// </summary>
        System = (byte)'0',

        /// <summary>
        /// 短信通知
        /// </summary>
        SMS = (byte)'1',

        /// <summary>
        /// 邮件通知
        /// </summary>
        EMail = (byte)'2',

        /// <summary>
        /// 人工通知
        /// </summary>
        Manual = (byte)'3'
    }

    /// <summary>
    /// TFtdcRiskNotifyStatusType是一个风险通知状态类型
    /// </summary>
    public enum EnumRiskNotifyStatusType : byte
    {
        /// <summary>
        /// 未生成
        /// </summary>
        NotGen = (byte)'0',

        /// <summary>
        /// 已生成未发送
        /// </summary>
        Generated = (byte)'1',

        /// <summary>
        /// 发送失败
        /// </summary>
        SendError = (byte)'2',

        /// <summary>
        /// 已发送未接收
        /// </summary>
        SendOk = (byte)'3',

        /// <summary>
        /// 已接收未确认
        /// </summary>
        Received = (byte)'4',

        /// <summary>
        /// 已确认
        /// </summary>
        Confirmed = (byte)'5'
    }

    /// <summary>
    /// TFtdcRiskUserEventType是一个风控用户操作事件类型
    /// </summary>
    public enum EnumRiskUserEventType : byte
    {
        /// <summary>
        /// 导出数据
        /// </summary>
        ExportData = (byte)'0'
    }

    /// <summary>
    /// TFtdcConditionalOrderSortTypeType是一个条件单索引条件类型
    /// </summary>
    public enum EnumConditionalOrderSortTypeType : byte
    {
        /// <summary>
        /// 使用最新价升序
        /// </summary>
        LastPriceAsc = (byte)'0',

        /// <summary>
        /// 使用最新价降序
        /// </summary>
        LastPriceDesc = (byte)'1',

        /// <summary>
        /// 使用卖价升序
        /// </summary>
        AskPriceAsc = (byte)'2',

        /// <summary>
        /// 使用卖价降序
        /// </summary>
        AskPriceDesc = (byte)'3',

        /// <summary>
        /// 使用买价升序
        /// </summary>
        BidPriceAsc = (byte)'4',

        /// <summary>
        /// 使用买价降序
        /// </summary>
        BidPriceDesc = (byte)'5'
    }

    /// <summary>
    /// TFtdcSendTypeType是一个报送状态类型
    /// </summary>
    public enum EnumSendTypeType : byte
    {
        /// <summary>
        /// 未发送
        /// </summary>
        NoSend = (byte)'0',

        /// <summary>
        /// 已发送
        /// </summary>
        Sended = (byte)'1',

        /// <summary>
        /// 已生成
        /// </summary>
        Generated = (byte)'2',

        /// <summary>
        /// 报送失败
        /// </summary>
        SendFail = (byte)'3',

        /// <summary>
        /// 接收成功
        /// </summary>
        Success = (byte)'4',

        /// <summary>
        /// 接收失败
        /// </summary>
        Fail = (byte)'5',

        /// <summary>
        /// 取消报送
        /// </summary>
        Cancel = (byte)'6'
    }

    /// <summary>
    /// TFtdcClientIDStatusType是一个交易编码状态类型
    /// </summary>
    public enum EnumClientIDStatusType : byte
    {
        /// <summary>
        /// 未申请
        /// </summary>
        NoApply = (byte)'1',

        /// <summary>
        /// 已提交申请
        /// </summary>
        Submited = (byte)'2',

        /// <summary>
        /// 已发送申请
        /// </summary>
        Sended = (byte)'3',

        /// <summary>
        /// 完成
        /// </summary>
        Success = (byte)'4',

        /// <summary>
        /// 拒绝
        /// </summary>
        Refuse = (byte)'5',

        /// <summary>
        /// 已撤销编码
        /// </summary>
        Cancel = (byte)'6'
    }

    /// <summary>
    /// TFtdcQuestionTypeType是一个特有信息类型类型
    /// </summary>
    public enum EnumQuestionTypeType : byte
    {
        /// <summary>
        /// 单选
        /// </summary>
        Radio = (byte)'1',

        /// <summary>
        /// 多选
        /// </summary>
        Option = (byte)'2',

        /// <summary>
        /// 填空
        /// </summary>
        Blank = (byte)'3'
    }

    /// <summary>
    /// TFtdcProcessTypeType是一个流程功能类型类型
    /// </summary>
    public enum EnumProcessTypeType : byte
    {
        /// <summary>
        /// 申请交易编码
        /// </summary>
        ApplyTradingCode = (byte)'1',

        /// <summary>
        /// 撤销交易编码
        /// </summary>
        CancelTradingCode = (byte)'2',

        /// <summary>
        /// 修改身份信息
        /// </summary>
        ModifyIDCard = (byte)'3',

        /// <summary>
        /// 修改一般信息
        /// </summary>
        ModifyNoIDCard = (byte)'4',

        /// <summary>
        /// 交易所开户报备
        /// </summary>
        ExchOpenBak = (byte)'5',

        /// <summary>
        /// 交易所销户报备
        /// </summary>
        ExchCancelBak = (byte)'6'
    }

    /// <summary>
    /// TFtdcBusinessTypeType是一个业务类型类型
    /// </summary>
    public enum EnumBusinessTypeType : byte
    {
        /// <summary>
        /// 请求
        /// </summary>
        Request = (byte)'1',

        /// <summary>
        /// 应答
        /// </summary>
        Response = (byte)'2',

        /// <summary>
        /// 通知
        /// </summary>
        Notice = (byte)'3'
    }

    /// <summary>
    /// TFtdcCfmmcReturnCodeType是一个监控中心返回码类型
    /// </summary>
    public enum EnumCfmmcReturnCodeType : byte
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = (byte)'0',

        /// <summary>
        /// 该客户已经有流程在处理中
        /// </summary>
        Working = (byte)'1',

        /// <summary>
        /// 监控中客户资料检查失败
        /// </summary>
        InfoFail = (byte)'2',

        /// <summary>
        /// 监控中实名制检查失败
        /// </summary>
        IDCardFail = (byte)'3',

        /// <summary>
        /// 其他错误
        /// </summary>
        OtherFail = (byte)'4'
    }

    /// <summary>
    /// TFtdcClientTypeType是一个客户类型类型
    /// </summary>
    public enum EnumClientTypeType : byte
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = (byte)'0',

        /// <summary>
        /// 个人
        /// </summary>
        Person = (byte)'1',

        /// <summary>
        /// 单位
        /// </summary>
        Company = (byte)'2'
    }

    /// <summary>
    /// TFtdcExchangeIDTypeType是一个交易所编号类型
    /// </summary>
    public enum EnumExchangeIDTypeType : byte
    {
        /// <summary>
        /// 上海期货交易所
        /// </summary>
        SHFE = (byte)'S',

        /// <summary>
        /// 郑州商品交易所
        /// </summary>
        CZCE = (byte)'Z',

        /// <summary>
        /// 大连商品交易所
        /// </summary>
        DCE = (byte)'D',

        /// <summary>
        /// 中国金融期货交易所
        /// </summary>
        CFFEX = (byte)'J'
    }

    /// <summary>
    /// TFtdcExClientIDTypeType是一个交易编码类型类型
    /// </summary>
    public enum EnumExClientIDTypeType : byte
    {
        /// <summary>
        /// 套保
        /// </summary>
        Hedge = (byte)'1',

        /// <summary>
        /// 套利
        /// </summary>
        Arbitrage = (byte)'2',

        /// <summary>
        /// 投机
        /// </summary>
        Speculation = (byte)'3'
    }

    /// <summary>
    /// TFtdcUpdateFlagType是一个更新状态类型
    /// </summary>
    public enum EnumUpdateFlagType : byte
    {
        /// <summary>
        /// 未更新
        /// </summary>
        NoUpdate = (byte)'0',

        /// <summary>
        /// 更新全部信息成功
        /// </summary>
        Success = (byte)'1',

        /// <summary>
        /// 更新全部信息失败
        /// </summary>
        Fail = (byte)'2',

        /// <summary>
        /// 更新交易编码成功
        /// </summary>
        TCSuccess = (byte)'3',

        /// <summary>
        /// 更新交易编码失败
        /// </summary>
        TCFail = (byte)'4',

        /// <summary>
        /// 已丢弃
        /// </summary>
        Cancel = (byte)'5'
    }

    /// <summary>
    /// TFtdcApplyOperateIDType是一个申请动作类型
    /// </summary>
    public enum EnumApplyOperateIDType : byte
    {
        /// <summary>
        /// 开户
        /// </summary>
        OpenInvestor = (byte)'1',

        /// <summary>
        /// 修改身份信息
        /// </summary>
        ModifyIDCard = (byte)'2',

        /// <summary>
        /// 修改一般信息
        /// </summary>
        ModifyNoIDCard = (byte)'3',

        /// <summary>
        /// 申请交易编码
        /// </summary>
        ApplyTradingCode = (byte)'4',

        /// <summary>
        /// 撤销交易编码
        /// </summary>
        CancelTradingCode = (byte)'5',

        /// <summary>
        /// 销户
        /// </summary>
        CancelInvestor = (byte)'6'
    }

    /// <summary>
    /// TFtdcApplyStatusIDType是一个申请状态类型
    /// </summary>
    public enum EnumApplyStatusIDType : byte
    {
        /// <summary>
        /// 未补全
        /// </summary>
        NoComplete = (byte)'1',

        /// <summary>
        /// 已提交
        /// </summary>
        Submited = (byte)'2',

        /// <summary>
        /// 已审核
        /// </summary>
        Checked = (byte)'3',

        /// <summary>
        /// 已拒绝
        /// </summary>
        Refused = (byte)'4',

        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = (byte)'5'
    }

    /// <summary>
    /// TFtdcSendMethodType是一个发送方式类型
    /// </summary>
    public enum EnumSendMethodType : byte
    {
        /// <summary>
        /// 电子发送
        /// </summary>
        ByAPI = (byte)'1',

        /// <summary>
        /// 文件发送
        /// </summary>
        ByFile = (byte)'2'
    }

    /// <summary>
    /// TFtdcEventModeType是一个操作方法类型
    /// </summary>
    public enum EnumEventModeType : byte
    {
        /// <summary>
        /// 增加
        /// </summary>
        ADD = (byte)'1',

        /// <summary>
        /// 修改
        /// </summary>
        UPDATE = (byte)'2',

        /// <summary>
        /// 删除
        /// </summary>
        DELETE = (byte)'3',

        /// <summary>
        /// 复核
        /// </summary>
        CHECK = (byte)'4'
    }

    /// <summary>
    /// TFtdcUOAAutoSendType是一个统一开户申请自动发送类型
    /// </summary>
    public enum EnumUOAAutoSendType : byte
    {
        /// <summary>
        /// 自动发送并接收
        /// </summary>
        ASR = (byte)'1',

        /// <summary>
        /// 自动发送，不自动接收
        /// </summary>
        ASNR = (byte)'2',

        /// <summary>
        /// 不自动发送，自动接收
        /// </summary>
        NSAR = (byte)'3',

        /// <summary>
        /// 不自动发送，也不自动接收
        /// </summary>
        NSR = (byte)'4'
    }

    /// <summary>
    /// TFtdcFlowIDType是一个流程ID类型
    /// </summary>
    public enum EnumFlowIDType : byte
    {
        /// <summary>
        /// 投资者对应投资者组设置
        /// </summary>
        InvestorGroupFlow = (byte)'1'
    }

    /// <summary>
    /// TFtdcCheckLevelType是一个复核级别类型
    /// </summary>
    public enum EnumCheckLevelType : byte
    {
        /// <summary>
        /// 零级复核
        /// </summary>
        Zero = (byte)'0',

        /// <summary>
        /// 一级复核
        /// </summary>
        One = (byte)'1',

        /// <summary>
        /// 二级复核
        /// </summary>
        Two = (byte)'2'
    }

    /// <summary>
    /// TFtdcCheckStatusType是一个复核级别类型
    /// </summary>
    public enum EnumCheckStatusType : byte
    {
        /// <summary>
        /// 未复核
        /// </summary>
        Init = (byte)'0',

        /// <summary>
        /// 复核中
        /// </summary>
        Checking = (byte)'1',

        /// <summary>
        /// 已复核
        /// </summary>
        Checked = (byte)'2',

        /// <summary>
        /// 拒绝
        /// </summary>
        Refuse = (byte)'3',

        /// <summary>
        /// 作废
        /// </summary>
        Cancel = (byte)'4'
    }

    /// <summary>
    /// TFtdcUsedStatusType是一个生效状态类型
    /// </summary>
    public enum EnumUsedStatusType : byte
    {
        /// <summary>
        /// 未生效
        /// </summary>
        Unused = (byte)'0',

        /// <summary>
        /// 已生效
        /// </summary>
        Used = (byte)'1',

        /// <summary>
        /// 生效失败
        /// </summary>
        Fail = (byte)'2'
    }

    /// <summary>
    /// TFtdcBankAcountOriginType是一个账户来源类型
    /// </summary>
    public enum EnumBankAcountOriginType : byte
    {
        /// <summary>
        /// 手工录入
        /// </summary>
        ByAccProperty = (byte)'0',

        /// <summary>
        /// 银期转账
        /// </summary>
        ByFBTransfer = (byte)'1'
    }

    /// <summary>
    /// TFtdcMonthBillTradeSumType是一个结算单月报成交汇总方式类型
    /// </summary>
    public enum EnumMonthBillTradeSumType : byte
    {
        /// <summary>
        /// 同日同合约
        /// </summary>
        ByInstrument = (byte)'0',

        /// <summary>
        /// 同日同合约同价格
        /// </summary>
        ByDayInsPrc = (byte)'1',

        /// <summary>
        /// 同合约
        /// </summary>
        ByDayIns = (byte)'2'
    }

    /// <summary>
    /// TFtdcFBTTradeCodeEnumType是一个银期交易代码枚举类型
    /// </summary>
    public enum EnumFBTTradeCodeEnumType : byte
    {
        /// <summary>
        /// 银行发起银行转期货
        /// </summary>
        BankLaunchBankToBroker = (byte)'1',//'102001',

        /// <summary>
        /// 期货发起银行转期货
        /// </summary>
        BrokerLaunchBankToBroker = (byte)'2',//'202001',

        /// <summary>
        /// 银行发起期货转银行
        /// </summary>
        BankLaunchBrokerToBank = (byte)'3',//'102002',

        /// <summary>
        /// 期货发起期货转银行
        /// </summary>
        BrokerLaunchBrokerToBank = (byte)'4'//'202002'
    }

    /// <summary>
    /// TFtdcOTPTypeType是一个动态令牌类型类型
    /// </summary>
    public enum EnumOTPTypeType : byte
    {
        /// <summary>
        /// 无动态令牌
        /// </summary>
        NONE = (byte)'0',

        /// <summary>
        /// 时间令牌
        /// </summary>
        TOTP = (byte)'1'
    }

    /// <summary>
    /// TFtdcOTPStatusType是一个动态令牌状态类型
    /// </summary>
    public enum EnumOTPStatusType : byte
    {
        /// <summary>
        /// 未使用
        /// </summary>
        Unused = (byte)'0',

        /// <summary>
        /// 已使用
        /// </summary>
        Used = (byte)'1',

        /// <summary>
        /// 注销
        /// </summary>
        Disuse = (byte)'2'
    }

    /// <summary>
    /// TFtdcBrokerUserTypeType是一个经济公司用户类型类型
    /// </summary>
    public enum EnumBrokerUserTypeType : byte
    {
        /// <summary>
        /// 投资者
        /// </summary>
        Investor = (byte)'1'
    }

    /// <summary>
    /// TFtdcFutureTypeType是一个期货类型类型
    /// </summary>
    public enum EnumFutureTypeType : byte
    {
        /// <summary>
        /// 商品期货
        /// </summary>
        Commodity = (byte)'1',

        /// <summary>
        /// 金融期货
        /// </summary>
        Financial = (byte)'2'
    }

    /// <summary>
    /// TFtdcFundEventTypeType是一个资金管理操作类型类型
    /// </summary>
    public enum EnumFundEventTypeType : byte
    {
        /// <summary>
        /// 转账限额
        /// </summary>
        Restriction = (byte)'0',

        /// <summary>
        /// 当日转账限额
        /// </summary>
        TodayRestriction = (byte)'1',

        /// <summary>
        /// 期商流水
        /// </summary>
        Transfer = (byte)'2',

        /// <summary>
        /// 资金冻结
        /// </summary>
        Credit = (byte)'3',

        /// <summary>
        /// 投资者可提资金比例
        /// </summary>
        InvestorWithdrawAlm = (byte)'4',

        /// <summary>
        /// 单个银行帐户转账限额
        /// </summary>
        BankRestriction = (byte)'5'
    }

    /// <summary>
    /// TFtdcAccountSourceTypeType是一个资金账户来源类型
    /// </summary>
    public enum EnumAccountSourceTypeType : byte
    {
        /// <summary>
        /// 银期同步
        /// </summary>
        FBTransfer = (byte)'0',

        /// <summary>
        /// 手工录入
        /// </summary>
        ManualEntry = (byte)'1'
    }

    /// <summary>
    /// TFtdcCodeSourceTypeType是一个交易编码来源类型
    /// </summary>
    public enum EnumCodeSourceTypeType : byte
    {
        /// <summary>
        /// 统一开户
        /// </summary>
        UnifyAccount = (byte)'0',

        /// <summary>
        /// 手工录入
        /// </summary>
        ManualEntry = (byte)'1'
    }

    /// <summary>
    /// TFtdcUserRangeType是一个操作员范围类型
    /// </summary>
    public enum EnumUserRangeType : byte
    {
        /// <summary>
        /// 所有
        /// </summary>
        All = (byte)'0',

        /// <summary>
        /// 单一操作员
        /// </summary>
        Single = (byte)'1'
    }

    /// <summary>
    /// TFtdcByGroupType是一个交易统计表按客户统计方式类型
    /// </summary>
    public enum EnumByGroupType : byte
    {
        /// <summary>
        /// 按投资者统计
        /// </summary>
        Investor = (byte)'2',

        /// <summary>
        /// 按类统计
        /// </summary>
        Group = (byte)'1'
    }

    /// <summary>
    /// TFtdcTradeSumStatModeType是一个交易统计表按范围统计方式类型
    /// </summary>
    public enum EnumTradeSumStatModeType : byte
    {
        /// <summary>
        /// 按合约统计
        /// </summary>
        Instrument = (byte)'1',

        /// <summary>
        /// 按产品统计
        /// </summary>
        Product = (byte)'2',

        /// <summary>
        /// 按交易所统计
        /// </summary>
        Exchange = (byte)'3'
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EnumBoolType : int
    {
        /// <summary>
        /// 
        /// </summary>
        No = 0,
        /// <summary>
        /// 
        /// </summary>
        Yes = 1
    }
}
