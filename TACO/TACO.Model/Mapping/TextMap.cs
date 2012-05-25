using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using TACO.Model.Domain;

namespace TACO.Model.Mapping
{
    public class TextMap : ClassMap<Text>
    {
        public TextMap()
        {
            Table("Text");
            Id(t => t.Id).Column("TextID").Not.Nullable().GeneratedBy.Identity();
            Map(t => t.TextName).Column("TextName").Not.Nullable();
            Map(t => t.TextContent).Column("Text").Not.Nullable();
            References(t => t.POI).ForeignKey("PointID").Column("PointID").Not.Nullable();
        }
    }
}
