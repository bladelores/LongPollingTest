using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modem.Amt.Export.Data
{
    public class Unit: NamedEntity
    {
        public long? BaseUnitId { set; get; }
        public decimal Multiplier { set; get; }
        public decimal Difference { set; get; }
    }
}
