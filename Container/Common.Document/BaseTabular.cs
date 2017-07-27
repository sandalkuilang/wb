using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common.Document
{
    public abstract class BaseTabular
    { 
        protected Stream FileStream { get; private set; }
        public int StartRow { get; set; }
        public int StartHeader { get; set; } 
         
        public BaseTabular(Stream stream)
        {
            this.FileStream = stream;
            this.StartRow = 1;
            this.StartHeader= 0;
        }

        public BaseTabular(Object value)
        { 
            this.StartRow = 1;
            this.StartHeader = 0;
        }

        public abstract IDictionary<int, DataRow> GetRows();
        public abstract DataRow GetRow(int index); 
        public abstract DataCell GetCell(int index);
         
        public abstract void InsertRow(DataCell[] cells);
        public abstract void InsertRow(int index, DataCell[] cells);

        public abstract void RemoveRow(int index);

        //public IValidationConfiguration ValidationConfiguration { get; set; }
    }
}
