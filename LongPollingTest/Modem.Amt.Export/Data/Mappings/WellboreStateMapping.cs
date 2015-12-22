using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modem.Amt.Export.Data.Mappings
{
    public class WellboreStateMapping: EntityMapping<WellboreState>
    {
        public WellboreStateMapping()
        {
            ToTable("WELLBORE_STATE");

            Property(x => x.Time).HasColumnName("TIME");
            Property(x => x.StateId).HasColumnName("STATE_ID");
            Property(x => x.WellboreId).HasColumnName("WELLBORE_ID");

            HasRequired(x => x.State).WithMany().HasForeignKey(x => x.StateId);
            HasRequired(x => x.Wellbore).WithMany().HasForeignKey(x => x.WellboreId);
        }
    }
}
