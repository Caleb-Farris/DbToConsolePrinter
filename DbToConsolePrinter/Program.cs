using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using DbToConsolePrinter.ClassFiles;
using DbToConsolePrinter.DriverClassFiles;

[assembly: InternalsVisibleTo("DbToConsolePrinterUnitTESTS")]
namespace DbToConsolePrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ensuring DataDirectory knows file location; for using the DB
            SetClientDataDirFileLocation();

            DataSet dataSet = null;

            try
            {
                // Returning generic Database Provider
                var factory = new DbProvider().Provider;

                // Making connection to appropriate DB based on provider
                var connection = factory.CreateConnection();
                connection.ConnectionString = 
                    DbConfigurationSettings.ConnectionString;

                // Data access
                var dbToDataSet = new DbToDataSet(factory, connection);
                dataSet = new InitTransferDbToDataSet(dbToDataSet)
                    .TransferDataToDataSet();
            }
            catch (DbException ex)
            {
                ErrorMessage(ex);
                Console.WriteLine("ErrorCode: {0}", ex.ErrorCode);
            }

            // Printing of DB table to console
            PrintTables(dataSet);

            // Writing the table information as XML to bin file
            PrintXml(dataSet);

            Console.ReadKey();
        }

        /***********************************************************************
        *                    SetClientDataDirFileLocation
        *///********************************************************************
        public static void SetClientDataDirFileLocation()
        {
            string executable =
                System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        /***********************************************************************
        *                           ErrorMessage
        *///********************************************************************
        public static void ErrorMessage(Exception ex)
        {
            Console.WriteLine("GetType: {0}", ex.GetType());
            Console.WriteLine("Source: {0}", ex.Source);
            Console.WriteLine("Message: {0}", ex.Message);
        }

        // Helper static method
        public static void PrintTables(DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                new PrintTableToConsole(
                    new TablePrinter(table)).PrintTable();
            }
        }

        // Helper static method
        public static void PrintXml(DataSet dataSet)
        {
            try
            {
                // default is "output.xml"
                new WriteXmlToFile(
                    new XmlWriter(dataSet)).Write();
            }
            catch (IOException ex)
            {
                ErrorMessage(ex);
            }
        }
    }
}
