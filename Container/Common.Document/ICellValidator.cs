using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Document
{
    public interface ICellValidator
    {
        string GetName();
        ValidationResult Validate(DataCell value);
    }
}
