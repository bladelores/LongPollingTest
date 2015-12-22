using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modem.Amt.Export.Data.Mappings
{
    public class StateMapping: EntityMapping<State>
    {
        public StateMapping()
        {
            ToTable("STATE");

            Property(x => x.StateValue).HasColumnName("STATE_VALUE");
            Property(x => x.Color).HasColumnName("COLOR");
            Property(x => x.Importance).HasColumnName("IMPORTANCE");
            Property(x => x.Info).HasColumnName("INFO");
        }
    }
}
