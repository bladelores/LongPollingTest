using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modem.Amt.Export.Data.Mappings
{
    public class WellboreMapping: NamedEntityMapping<Wellbore>
    {
        public WellboreMapping()
        {
            ToTable("WELLBORE");
        }
    }
}
