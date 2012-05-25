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
        ITextTasks textTasks = null;

        public HomeController(IPointTasks pointTasks, ITextTasks textTasks)
        {
            this.pointTasks = pointTasks;
            this.textTasks = textTasks;
        }

        [HttpGet]
        public JsonResult AllPoints()
        {
            var obj = pointTasks.GetAllPOI();

            return Json(GetJSONList(obj), JsonRequestBehavior.AllowGet);
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

        [HttpGet]
        public JsonResult GetPoint(int id)
        {
            return Json(GetJSONSingle(pointTasks.GetPointByID(id)), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTexts(int id)
        {
            var poi = pointTasks.GetPointByID(id);

            return Json(GetJSONList(poi.Texts), JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private List<object> GetJSONList(IEnumerable<POI> obj)
        {
        
            List<object> list = new List<object>();
            foreach (var poi in obj)
                list.Add(GetJSONSingle(poi));

            return list;
        }
        [NonAction]
        private List<object> GetJSONList(IEnumerable<Text> obj)
        {

            List<object> list = new List<object>();
            foreach (var text in obj)
                list.Add(new { Id = text.Id, Name = text.TextName, Text = text.TextContent, PointID = text.POI.Id });

            return list;
        }
        [NonAction]
        private string ConCatTexts(IEnumerable<Text> obj)
        {
            String output = "";

            foreach (var text in obj)
                output += text.TextContent + "<;&>||#";
            return output;
        }

        [NonAction]
        private object GetJSONSingle(POI poi)
        {

            object single = new { 
                Id = poi.Id, 
                Name = poi.PointName, 
                Longitude = poi.PointCoord.X, 
                Latitude = poi.PointCoord.Y, 
                Description = poi.PointDescription,
                Texts = GetJSONList(poi.Texts)
            };

            return single;
        }

        [HttpGet]
        public JsonResult CreatePOI(string name, double lon, double lat, string description = null)
        {
            var poi = pointTasks.CreatePoint(name, description, new Point(lon, lat));

            return Json(GetJSONSingle(poi), JsonRequestBehavior.AllowGet);
        }

        public string ResetData()
        {
           
            using (ISession session = NHibernateSession.GetSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.CreateQuery("delete POI f").ExecuteUpdate();

                transaction.Commit();
            }

            var p1 = pointTasks.CreatePoint("TestA", new Point(10, 10));
            var p2 = pointTasks.CreatePoint("TestB", new Point(4, 8));
            var p3 = pointTasks.CreatePoint("TestC", new Point(-1, 6));
            var p4 = pointTasks.CreatePoint("TestD", new Point(7, 5));
            var p5 = pointTasks.CreatePoint("TestE", new Point(-2, 3));
            var p6 = pointTasks.CreatePoint("TestF", new Point(4, 2));
            var p7 = pointTasks.CreatePoint("TestG", new Point(7, 1));
            var p8 = pointTasks.CreatePoint("TestH", new Point(-3, -2));
            var p9 = pointTasks.CreatePoint("TestI", new Point(3, -3));

            textTasks.CreateText("Test text", "This is a text associated with TestA", p1);
            textTasks.CreateText("Test text 2", "This is another text associated with TestA", p1);
            textTasks.CreateText("WOOO awesome", "This is the last text associated with TestA", p1);


            return "Mjello";
        }

    }
}
