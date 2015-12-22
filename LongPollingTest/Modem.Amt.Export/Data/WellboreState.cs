using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modem.Amt.Export.Data
{
    public class WellboreState: Entity
    {
        public long WellboreId { set; get; }
        public Wellbore Wellbore { set; get; }
        public DateTime Time { set; get; }
        public long StateId { set; get; }
        public State State { set; get; }
    }
}
