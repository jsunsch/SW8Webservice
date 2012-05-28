using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using TACO.Model.Domain;

namespace TACO.Model.Mapping
{
    public class IndoorPOIMap : ClassMap<IndoorPOI>
    {
        public IndoorPOIMap()
        {

            Table("IndoorPOI");
            Id(p => p.Id).Column("IndoorID").Not.Nullable().GeneratedBy.Identity();
            Map(p => p.IndoorPOIName).Not.Nullable();
            Map(p => p.IndoorPOIDescription);
            References(p => p.Attraction).ForeignKey("AttractionID").Column("AttractionID");
            HasMany(p => p.Texts).KeyColumn("PointID").Inverse().Cascade.All();
            HasMany(p => p.WifiMeassures).KeyColumn("IndoorID").Inverse().Cascade.All();
        }
    }
}
