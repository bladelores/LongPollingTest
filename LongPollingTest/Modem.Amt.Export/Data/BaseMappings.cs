using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Modem.Amt.Export.Data
{
    public class EntityMapping<T> : EntityTypeConfiguration<T> where T : Entity
    {
        public EntityMapping()
        {
            Property(x => x.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasKey(x => x.Id);
        }
    }

    public class NamedEntityMapping<T> : EntityMapping<T> where T : NamedEntity
    {
        public NamedEntityMapping()
        {
            Property(x => x.Name).HasColumnName("NAME");
        }
    }
}
