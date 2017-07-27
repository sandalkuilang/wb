using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Document
{
    public class ValidationResult
    {
        public string Message { get; set; }
        public int Index { get; set; }
        public DataCell Cell { get; set; }
        public ValidationStatus Status { get; set; }
    }
}
