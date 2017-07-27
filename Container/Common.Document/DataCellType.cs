using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Document
{
    public enum DataCellType
    {
        String = 1,
        Numeric,
        Boolean,
        Formula,
        Blank,
        Error,
        Warning
    }
}
