using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Document
{
    public class MemoryCellValidationConfiguration : IValidationConfiguration
    {

        public void Add(ICellValidator validator)
        {
            throw new NotImplementedException();
        }

        public void Remove(string name)
        {
            throw new NotImplementedException();
        }

        public ICellValidator GetValidator(string name)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, ICellValidator> GetValidators()
        {
            throw new NotImplementedException();
        }
    }
}
