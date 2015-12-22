using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modem.Amt.Export.Data
{
    public class Parameter: NamedEntity
    {
        public string Code { set; get; }
        public decimal Multiplier { get; set; }
    }
}
