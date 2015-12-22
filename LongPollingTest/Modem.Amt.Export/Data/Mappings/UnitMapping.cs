using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modem.Amt.Export.Data.Mappings
{
    public class UnitMapping: NamedEntityMapping<Unit>
    {
        public UnitMapping()
        {
            ToTable("UNIT");

            Property(x => x.BaseUnitId).HasColumnName("BASE_UNIT_ID");
            Property(x => x.Multiplier).HasColumnName("MULTIPLIER");
            Property(x => x.Difference).HasColumnName("DIFFERENCE");
        }
    }
}
