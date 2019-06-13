using DbToConsolePrinter.Interfaces;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DbToConsolePrinterUnitTESTS")]
namespace DbToConsolePrinter.ClassFiles
{
    class DbToDataSet : IDbToDataSet
    {
        #region private members
        private readonly DbProviderFactory _factory;
        private readonly DbConnection _conn;
        #endregion

        #region Properties
        public DbCommand Command { get; set; }
        public string Query { get; set; }
        public CommandType CommandType { get; set; }

        #endregion

        public DbToDataSet(DbProviderFactory factory, DbConnection conn)
        {
            _factory = factory;
            _conn = conn;
            Command = _factory.CreateCommand();
            CommandType = CommandType.StoredProcedure;
            Query = "dbo.spGet_Bugs_Daffy";
        }

        public DataSet MoveDataToDataSet()
        {
            using (_conn)
            {
                // Command
                Command.CommandText = Query;
                Command.Connection = _conn;

                // DataAdapter
                DbDataAdapter adapter = _factory.CreateDataAdapter();
                adapter.SelectCommand = Command;

                // DataSet.  For reference, "ACME" is arbitrary.
                DataSet dataSet = new DataSet("ACME");
                adapter.Fill(dataSet);

                return dataSet;
            }
        }
    }
}
