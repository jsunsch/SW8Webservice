using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excolo.Architecture.Core.NHibernate;
using GeoAPI.Geometries;
using NHibernate.Spatial;
using GisSharpBlog.NetTopologySuite.Geometries;
using TACO.Model.Domain;
using TACO.Model.Tasks.Abstract;
using NHibernate.Spatial.Type;
using NHibernate;
using System.Diagnostics;

namespace TACO.Model.Tasks.Concrete
{
    public class PointTasks : IPointTasks
    {

        private NHibernateRepository<POI> repos = new NHibernateRepository<POI>();

        public IEnumerable<Domain.POI> GetKNearestPOI(int k, GeoAPI.Geometries.IPoint coord, double radius)
        {
            IEnumerable<POI> output = NHibernateSession.GetSession().CreateQuery(@"select p from POI p 
                                            where NHSP.Distance(p.PointCoord, :coord) <= :maxDistance 
                                            order by NHSP.Distance(p.PointCoord, :coord)")
                                        .SetParameter("coord", coord,
                                            NHibernateUtil.Custom(typeof(TACO.Model.Mapping.POIMap.Wgs84GeographyType)))
                                        .SetParameter<double>("maxDistance", radius)
                                        .SetMaxResults(k)
                                        .Enumerable<POI>();

            return output;
        }

        public IEnumerable<POI> GetKNearestPOI(int k, IPoint coord)
        {
            IEnumerable<POI> output = NHibernateSession.GetSession().CreateQuery(@"select p from POI p 
                                            order by NHSP.Distance(p.PointCoord, :coord)")
                                        .SetParameter("coord", coord,
                                            NHibernateUtil.Custom(typeof(TACO.Model.Mapping.POIMap.Wgs84GeographyType)))
                                        .SetMaxResults(k)
                                        .Enumerable<POI>();

            return output;
        }

        public IEnumerable<POI> GetKNearestPOI(IPoint coord, double radius)
        {
            IEnumerable<POI> output = NHibernateSession.GetSession().CreateQuery(@"select p from POI p 
                                            where NHSP.Distance(p.PointCoord, :coord) <= :maxDistance 
                                            order by NHSP.Distance(p.PointCoord, :coord)")
                                        .SetParameter("coord", coord,
                                            NHibernateUtil.Custom(typeof(TACO.Model.Mapping.POIMap.Wgs84GeographyType)))
                                        .SetParameter<double>("maxDistance", radius)
                                        .Enumerable<POI>();

            return output;
        }


        public POI CreatePoint(string name, IPoint coords)
        {
            coords.SRID = 4326;
            POI point = new POI { PointName = name, PointCoord = coords };

            using (repos.DBContext.BeginTransaction())
            {
                try
                {
                    repos.SaveOrUpdate(point);
                    repos.DBContext.SubmitChanges();
                    repos.DBContext.CommitTransaction();
                }
                catch 
                {
                    repos.DBContext.RollbackTransaction();
                    point = null;
                }
            }

            return point;
        }
    }
}
