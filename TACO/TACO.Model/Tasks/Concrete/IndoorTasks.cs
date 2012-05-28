using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excolo.Architecture.Core.NHibernate;
using TACO.Model.Domain;
using TACO.Model.Tasks.Abstract;

namespace TACO.Model.Tasks.Concrete
{
    public class IndoorTasks : IIndoorTasks
    {
        private NHibernateRepository<Attraction> repos = new NHibernateRepository<Attraction>();
        private NHibernateRepository<IndoorPOI> reposPOI = new NHibernateRepository<IndoorPOI>();
        private NHibernateRepository<WifiMeassure> reposWifi = new NHibernateRepository<WifiMeassure>();

        public Attraction CreateAttraction(string attractionName, string attractionDescription)
        {
            Attraction attr = new Attraction 
            { 
                AttractionName = attractionName,
                AttractionDescription = attractionDescription
            };

            using (repos.DBContext.BeginTransaction())
            {
                try
                {
                    repos.SaveOrUpdate(attr);
                    repos.DBContext.SubmitChanges();
                    repos.DBContext.CommitTransaction();
                }
                catch
                {
                    repos.DBContext.RollbackTransaction();
                    attr = null;
                }
            }

            return attr;
        }

        public IndoorPOI CreateIndoorPOI(string indoorName, string indoorDesc, Attraction attraction)
        {
            IndoorPOI point = new IndoorPOI
            {
                IndoorPOIName = indoorName,
                IndoorPOIDescription = indoorDesc,
                Attraction = attraction
            };

            using (reposPOI.DBContext.BeginTransaction())
            {
                try
                {
                    reposPOI.SaveOrUpdate(point);
                    reposPOI.DBContext.SubmitChanges();
                    reposPOI.DBContext.CommitTransaction();
                }
                catch
                {
                    reposPOI.DBContext.RollbackTransaction();
                    point = null;
                }
            }

            return point;
        }

        public WifiMeassure CreateWifiMeassure(string BSID, int signalStrength, IndoorPOI indoorPOI)
        {
            WifiMeassure wifiM = new WifiMeassure
            {
                BSID = BSID,
                SignalStrength = signalStrength,
                POI = indoorPOI
            };

            using (reposWifi.DBContext.BeginTransaction())
            {
                try
                {
                    reposWifi.SaveOrUpdate(wifiM);
                    reposWifi.DBContext.SubmitChanges();
                    reposWifi.DBContext.CommitTransaction();
                }
                catch
                {
                    reposWifi.DBContext.RollbackTransaction();
                    wifiM = null;
                }
            }

            return wifiM;
        }


        public Attraction GetAttraction(int id)
        {
            return repos.Get(id);
        }
    }
}
