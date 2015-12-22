using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modem.Amt.Export.Data
{
    public class ContinuousInterval: Entity
    {
        public long WellboreId { set; get; }
        public Wellbore Wellbore { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime FinishTime { set; get; }
    }
}
