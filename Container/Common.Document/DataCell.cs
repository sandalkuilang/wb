using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Document
{
    public class DataCell
    {
        public DataCellType Type { get; set; }
        public object Value { get; set; }
        public int ColumnIndex { get; set; }
        //public DataRow Row { get; set; }
    }
}
