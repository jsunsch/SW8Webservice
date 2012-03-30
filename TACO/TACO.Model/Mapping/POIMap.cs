using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate.Spatial.Type;
using TACO.Model.Domain;

namespace TACO.Model.Mapping
{
    public class POIMap : ClassMap<POI>
    {
        public POIMap()
        {
            ImportType<GisSharpBlog.NetTopologySuite.Geometries.Point>();

            EntityName("POI");
            Id(p => p.Id).Column("PointID").Not.Nullable().GeneratedBy.Identity();
            Map(p => p.PointName).Not.Nullable();
            Map(p => p.PointCoord).CustomType(typeof(Wgs84GeographyType));

        }

        public class Wgs84GeographyType : MsSql2008GeographyType
        {
            protected override void SetDefaultSRID(GeoAPI.Geometries.IGeometry geometry)
            {
                geometry.SRID = 4326;
            }
        }
    }
}
