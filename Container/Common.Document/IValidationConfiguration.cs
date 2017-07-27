using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Document
{
    public interface IValidationConfiguration
    {
        void Add(ICellValidator validator);
        void Remove(string name);

        ICellValidator GetValidator(string name);
        Dictionary<string, ICellValidator> GetValidators();
    }
}
