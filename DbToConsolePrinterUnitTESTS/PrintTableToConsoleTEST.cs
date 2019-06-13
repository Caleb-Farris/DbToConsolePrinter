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
    public class PrintTableToConsoleTEST
    {
        // For testing queries against the TablePrinter
        private List<string> _testQueries = null;

        // Just a setup for some test queries.  
        public PrintTableToConsoleTEST()
        {
            List<string> queries = null;

            // Probably a better way to do this, but for now, 
            // selecting file names explicitly
            var assembly = Assembly.GetExecutingAssembly();
            string resourceBase = "DbToConsolePrinterUnitTESTS.TestScripts.";
            string fileExtension = ".sql";
            string[] testQueriesResourceNames =
            {
                resourceBase + "Select_All_Company" + fileExtension,
                resourceBase + "Select_All_Employees" + fileExtension,
                resourceBase + "Select_All_Position" + fileExtension,
                resourceBase + "Select_All_EmpPosCom" + fileExtension,
            };

            foreach (string resourceName in testQueriesResourceNames)
            {
                _testQueries.Add(ReadEmbeddedFile(resourceName, assembly));
            }

            // adding production query separately 
            /*
            _productionQuery = ReadEmbeddedFile(
                resourceBase + "spGet_Bugs_Daffy" + fileExtension,
                assembly);
            */
        }

        /*    Expected that a table will print to console, properly formatted.
         *    These tests are effectively testing the results of the 
         *    TablePrinter class, ensuring that it behaves as expected
         */

        [TestMethod][Ignore]
        public void TestPrintTable()
        {
            DataSet dataSet = null;

            // must use hard coded params name b/c can't access config file
            string provider = "System.Data.SqlClient";
            string connString = @"Server=(localdb)\mssqllocaldb;Integrated Security=True;
                 AttachDbFilename=|DataDirectory|\DefaultDb.mdf;Trusted_Connection=Yes;";

            try
            {
                // Returning generic Database Provider
                var factory = new DbProvider(provider).Provider;

                // Making connection to appropriate DB based on provider
                var connection = factory.CreateConnection();
                connection.ConnectionString = connString;

                // Data access
                var dbToDataSet = new DbToDataSet(factory, connection)
                {
                    CommandType = CommandType.Text
                };

                Assert.IsNotNull(dbToDataSet);

                // testing printing against different SQL queries
                foreach (string query in _testQueries)
                {
                    dbToDataSet.Query = query;
                    dataSet = new InitTransferDbToDataSet(dbToDataSet)
                        .TransferDataToDataSet();

                    Assert.IsNotNull(dataSet);

                    // Testing various query outputs to console
                    Program.PrintTables(dataSet);
                }

                // testing production query
                //dbToDataSet.Query = _productionQuery;
                dbToDataSet.CommandType = CommandType.StoredProcedure;
                dbToDataSet.Query = "spGet_Bugs_Daffy";

                dataSet = new InitTransferDbToDataSet(dbToDataSet)
                        .TransferDataToDataSet();

                Assert.IsNotNull(dataSet);
                
                Program.PrintTables(dataSet);
            }
            catch (DbException ex)
            {
                Program.ErrorMessage(ex);
                Console.WriteLine("ErrorCode: {0}", ex.ErrorCode);
            }
        }

        // Helper method to read file stream
        private static string ReadEmbeddedFile(string resourceName, Assembly assembly)
        {
            string result = "";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }
    }
}
