using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common.Document
{
    public abstract class ExcelTabular : BaseTabular
    {

        public string Sheet { get; set; }  
  
        public ExcelTabular(Object value)
            : base(value)
        {

        }

        public abstract void Read();
        public abstract DataRow[] Rows { get; protected set; } 
    }
}
