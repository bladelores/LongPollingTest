using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modem.Amt.Export.Data
{
    public class State: Entity
    {
        public string StateValue { set; get; }
        public string Color { set; get; }
        public long Importance { set; get; }
        public string Info { set; get; }
    }
}
