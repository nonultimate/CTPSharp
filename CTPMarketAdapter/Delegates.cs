using CTPMarketAdapter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTPMarketAdapter
{
    /// <summary>
    /// 行情数据改变委托方法
    /// </summary>
    public delegate void MarketDataChangedHandler(CTPMarketData marketData);
}
