using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using TACO.Model.Domain;

namespace TACO.Model.Mapping
{
    public class WifiMeassureMap : ClassMap<WifiMeassure>
    {
        public WifiMeassureMap()
        {
            Table("WifiMeassure");
            Id(t => t.Id).Column("WifiMeassureID").Not.Nullable().GeneratedBy.Identity();
            Map(t => t.BSID).Column("BSID").Not.Nullable();
            Map(t => t.SignalStrength).Column("SignalStrength").Not.Nullable();
            References(t => t.POI).ForeignKey("IndoorID").Column("IndoorID").Not.Nullable();
        }
    }
}
