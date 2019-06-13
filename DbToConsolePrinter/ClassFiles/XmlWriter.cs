using DbToConsolePrinter.Interfaces;
using System.Data;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DbToConsolePrinterUnitTESTS")]
namespace DbToConsolePrinter.ClassFiles
{
    class XmlWriter : IXmlWriter
    {
        #region Private members
        private DataSet _dataSet;
        #endregion

        #region Properties
        public string OutputFileName
        {
            get;
        }
        #endregion

        public XmlWriter(DataSet dataSet, string outputFileName = "output.xml")
        {
            _dataSet = dataSet;
            OutputFileName = outputFileName;
        }

        public void WriteXml()
        {
            if (_dataSet == null) { return; }

            _dataSet.WriteXml(OutputFileName);
        }
    }
}
