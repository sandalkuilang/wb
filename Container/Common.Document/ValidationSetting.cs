using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Document
{
    public class ValidationSetting
    {
        public bool Igonere { get; set; }
        public bool Mandatory { get; set; }
        public string Column { get; set; }
        public string ValidatorName { get; set; }
    }
}
