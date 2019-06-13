[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DbToConsolePrinterUnitTESTS")]
namespace DbToConsolePrinter.Interfaces
{
    interface IDbToDataSet
    {
        System.Data.DataSet MoveDataToDataSet();
    }
}