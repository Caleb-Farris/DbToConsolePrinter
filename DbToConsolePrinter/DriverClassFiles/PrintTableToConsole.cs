using DbToConsolePrinter.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DbToConsolePrinterUnitTESTS")]
namespace DbToConsolePrinter.DriverClassFiles
{
    class PrintTableToConsole
    {
        ITablePrinter _tablePrinter;

        public PrintTableToConsole(ITablePrinter tablePrinter)
        {
            _tablePrinter = tablePrinter;
        }

        public void PrintTable()
        {
            _tablePrinter.Print();
        }
    }
}
