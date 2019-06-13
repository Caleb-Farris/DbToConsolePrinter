using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbToConsolePrinter;
using DbToConsolePrinter.ClassFiles;

namespace DbToConsolePrinterUnitTESTS
{
    [TestClass]
    public class DbProviderUnitTEST
    {
        [TestMethod]
        public void TestProviderName()
        {
            // must use hard coded provider name b/c can't access config file
            string provider = "System.Data.SqlClient";
            var factory = new DbProvider(provider);
            var actual = factory.ProviderName;
            var expected = "System.Data.SqlClient";

            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod]
        public void TestProviderObject()
        {
            // must use hard coded provider name b/c can't access config file
            string provider = "System.Data.SqlClient";

            var factory = new DbProvider(provider);
            var actual = factory.Provider;

            Assert.IsInstanceOfType(actual, typeof(System.Data.Common.DbProviderFactory));

        }
    }
}
