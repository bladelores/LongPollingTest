using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modem.Amt.Export.Data
{
    public class Entity
    {
        public long Id { set; get; }
    }

    public class NamedEntity : Entity
    {
        public string Name { set; get; }
    }
}
