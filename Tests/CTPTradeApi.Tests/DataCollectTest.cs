using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CTPTradeApi;

namespace CTPTradeApiTest
{
    /// <summary>
    /// DataCollectTest 的摘要说明
    /// </summary>
    [TestClass]
    public class DataCollectTest
    {
        private DataCollect _dc = new DataCollect();

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void TestGetSsystemInfo()
        {
            var info = _dc.GetSsystemInfo();
            Console.WriteLine("SystemInfo: {0}, len: {1}", info.Item1, info.Item2);
            Assert.IsTrue(info.Item2 > 0);
        }
    }
}
