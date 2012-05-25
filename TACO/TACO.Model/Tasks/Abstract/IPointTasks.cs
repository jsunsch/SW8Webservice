using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoAPI.Geometries;
using TACO.Model.Domain;

namespace TACO.Model.Tasks.Abstract
{
    public interface IPointTasks
    {
        IEnumerable<POI> GetKNearestPOI(int k, IPoint coord);
        IEnumerable<POI> GetKNearestPOI(int k, IPoint coord, double radius);
        IEnumerable<POI> GetKNearestPOI(IPoint coord, double radius);
        IEnumerable<POI> GetAllPOI();
        POI GetPointByID(int id);

        POI CreatePoint(string name, IPoint coords);
        POI CreatePoint(string name, string description, IPoint coords);
    }
}
