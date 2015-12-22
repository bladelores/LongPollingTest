using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modem.Amt.Export.Data.Mappings
{
    public class ParameterMapping: NamedEntityMapping<Parameter>
    {
        public ParameterMapping()
        {
            ToTable("PARAMETER");

            Property(x => x.Code).HasColumnName("CODE");
            Property(x => x.Multiplier).HasColumnName("MULTIPLIER");
        }
    }
}
