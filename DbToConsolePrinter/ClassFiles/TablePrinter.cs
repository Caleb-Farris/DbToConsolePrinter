using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Runtime.CompilerServices;
using DbToConsolePrinter.Interfaces;

[assembly: InternalsVisibleTo("DbToConsolePrinterUnitTESTS")]
namespace DbToConsolePrinter.ClassFiles
{
    class TablePrinter : ITablePrinter
    {
        #region constants
        private const int maxLineWidth = 80;
        private const int fieldPadding = 4;
        #endregion

        #region Properties
        public DataTable DataTable
        {
            get;
        }
        #endregion

        #region Private members
        private int _lowerIndex;
        private int _upperIndex;
        private List<int> _columnLengths;
        #endregion

        public TablePrinter(DataTable ds)
        {
            DataTable = ds;
            _lowerIndex = _upperIndex = 0;
            _columnLengths = GetMaximumColumnLengths();
            SetIndices();
        }

        private void SetIndices()
        {
            int currentLineWidth = 0;
            _lowerIndex = _upperIndex;

            while (currentLineWidth < maxLineWidth && _upperIndex < _columnLengths.Count)
            {
                currentLineWidth += _columnLengths.ElementAt(_upperIndex);
                ++_upperIndex;
            }

            // we need to move the buffer indice back one space if passed max
            if (currentLineWidth >= maxLineWidth && _upperIndex > 0)
            {
                --_upperIndex;
            }
        }

        public void Print()
        {
            while (_lowerIndex < DataTable.Columns.Count)
            {
                PrintAttributeSection();
                PrintRecordSection();
                SetIndices();
                Console.WriteLine();
            }
        }


        private void PrintAttributeSection()
        {
            for (int i = _lowerIndex; i < _upperIndex; ++i)
            {
                Console.Write("{0,-" + (_columnLengths[i] + fieldPadding) + "}",
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                        DataTable.Columns[i].ToString()));
            }

            Console.WriteLine();
        }

        private void PrintRecordSection()
        {
            foreach (DataRow row in DataTable.Rows)
            {
                for (int j = _lowerIndex; j < _upperIndex; ++j)
                {
                    Console.Write("{0,-" + (_columnLengths[j] + fieldPadding)
                        + "}", row[j]);
                }

                Console.WriteLine();
            }
        }

        private List<int> GetMaximumColumnLengths()
        {
            return Enumerable.Range(0, DataTable.Columns.Count)
                .Select(col => {
                    int rowLen = DataTable.AsEnumerable().Select(
                        row => row[col].ToString()).Max(
                        val => val.Length);
                    int attributeLen = DataTable.Columns[col].ToString().Length;
                    return rowLen > attributeLen ? rowLen : attributeLen;
                }).ToList();
        }
    }
}
