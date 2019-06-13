using DbToConsolePrinter.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DbToConsolePrinterUnitTESTS")]
namespace DbToConsolePrinter.DriverClassFiles
{
    class InitTransferDbToDataSet
    {
        #region member vars
        IDbToDataSet _dbToDataSet;
        #endregion

        public InitTransferDbToDataSet(IDbToDataSet dbToDataSet)
        {
            _dbToDataSet = dbToDataSet;
        }

        public System.Data.DataSet TransferDataToDataSet()
        {
            return _dbToDataSet.MoveDataToDataSet();
        }
    }
}
