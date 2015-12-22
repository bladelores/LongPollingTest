using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modem.Amt.Export.Data.Mappings
{
    public class ContinuousIntervalMapping: EntityMapping<ContinuousInterval>
    {
        public ContinuousIntervalMapping()
        {
            ToTable("CONTINUOUS_INTERVAL");

            Property(x => x.StartTime).HasColumnName("START_TIME");
            Property(x => x.FinishTime).HasColumnName("FINISH_TIME");
            Property(x => x.WellboreId).HasColumnName("WELLBORE_ID");

            HasRequired(x => x.Wellbore).WithMany().HasForeignKey(x => x.WellboreId);
        }
    }
}
