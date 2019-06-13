using DbToConsolePrinter.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DbToConsolePrinterUnitTESTS")]
namespace DbToConsolePrinter.DriverClassFiles
{
    class WriteXmlToFile
    {
        IXmlWriter _xmlWriter;

        public WriteXmlToFile(IXmlWriter xmlWriter)
        {
            _xmlWriter = xmlWriter;
        }

        public void Write()
        {
            _xmlWriter.WriteXml();
        }
    }
}
