using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using TACO.Model.Domain;

namespace TACO.Model.Mapping
{
    public class AttractionMap : ClassMap<Attraction>
    {
        public AttractionMap()
        {
            Table("Attraction");
            Id(p => p.Id).Column("AttractionID").Not.Nullable().GeneratedBy.Identity();
            Map(p => p.AttractionName).Not.Nullable();
            Map(p => p.AttractionDescription);
            HasMany(p => p.IndoorPOIs).KeyColumn("IndoorID").Inverse().Cascade.All();
        }
    }
}
