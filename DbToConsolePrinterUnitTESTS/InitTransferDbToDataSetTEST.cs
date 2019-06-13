using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using DbToConsolePrinter.ClassFiles;
using System.Data;
using DbToConsolePrinter.DriverClassFiles;
using DbToConsolePrinter;
using System.Data.Common;

namespace DbToConsolePrinterUnitTESTS
{
    [TestClass]
    public class InitTransferDbToDataSetTEST
    {
        [TestMethod]
        public void TestTransferToDataSet()
        {
            // must use hard coded params name b/c can't access config file
            string provider = "System.Data.SqlClient";
            string connString = @"Server=(localdb)\mssqllocaldb;Integrated Security=True;
                 AttachDbFilename=|DataDirectory|\DefaultDb.mdf;Trusted_Connection=Yes;";

            try
            {
                DataSet dataSet = null;

                // Returning generic Database Provider
                var factory = new DbProvider(provider).Provider;

                // Making connection to appropriate DB based on provider
                var connection = factory.CreateConnection();
                connection.ConnectionString = connString;

                // Data access 
                var dbToDataSet = new DbToDataSet(factory, connection);
                dataSet = new InitTransferDbToDataSet(dbToDataSet)
                    .TransferDataToDataSet();

                // This is merely testing that a transfer occurred (not null)
                Assert.IsNotNull(dataSet);
            }
            catch (DbException ex)
            {
                Program.ErrorMessage(ex);
                Console.WriteLine("ErrorCode: {0}", ex.ErrorCode);
            }

        }
    }
}
