using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excolo.Architecture.Core.NHibernate;
using GisSharpBlog.NetTopologySuite.Geometries;
using NHibernate;
using TACO.Model.Domain;
using TACO.Model.Tasks.Abstract;

namespace TACO.Controllers
{
    public class HomeController : Controller
    {
        IPointTasks pointTasks = null;

        public HomeController(IPointTasks pointTasks)
        {
            this.pointTasks = pointTasks;
        }

        public ActionResult AllPoints(int id = 3)
        {
            var obj = pointTasks.GetKNearestPOI(10, new Point(7, 3), (double)id);

            return View(obj);
        }


        [HttpGet]
        public JsonResult KNearestPOI(double lon, double lat, int number, double radius = 0)
        {
            IEnumerable<POI> obj;

            if(radius<=0)
                obj = pointTasks.GetKNearestPOI(number, new Point(lon, lat));
            else if(number<0)
                obj = pointTasks.GetKNearestPOI(new Point(lon, lat), radius);
            else
                obj = pointTasks.GetKNearestPOI(number, new Point(lon, lat), radius);

            return Json(GetJSONList(obj), JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private List<object> GetJSONList(IEnumerable<POI> obj)
        { 
        
            List<object> list = new List<object>();
            foreach (var poi in obj)
                list.Add(new { Id = poi.Id, Name = poi.PointName, Longitude = poi.PointCoord.X, Latitude = poi.PointCoord.Y });

            return list;
        }

        public string ResetData()
        {
           
            using (ISession session = NHibernateSession.GetSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.CreateQuery("delete POI f").ExecuteUpdate();

                transaction.Commit();
            }

            pointTasks.CreatePoint("TestA", new Point(10, 10));
            pointTasks.CreatePoint("TestB", new Point(4, 8));
            pointTasks.CreatePoint("TestC", new Point(-1, 6));
            pointTasks.CreatePoint("TestD", new Point(7, 5));
            pointTasks.CreatePoint("TestE", new Point(-2, 3));
            pointTasks.CreatePoint("TestF", new Point(4, 2));
            pointTasks.CreatePoint("TestG", new Point(7, 1));
            pointTasks.CreatePoint("TestH", new Point(-3, -2));
            pointTasks.CreatePoint("TestI", new Point(3, -3));

            return "Mjello";
        }

    }
}
