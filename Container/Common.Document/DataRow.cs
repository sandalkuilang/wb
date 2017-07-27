using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Document
{
    public class DataRow
    {
        public int Index { get; set; }
        public IDictionary<int, DataCell> Cells { get; set; }
    }
}
