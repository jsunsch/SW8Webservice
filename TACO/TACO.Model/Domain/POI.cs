using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excolo.Architecture.Core.Domain;
using GeoAPI.Geometries;

namespace TACO.Model.Domain
{
    public class POI : Entity
    {
        public virtual String PointName { get; set; }
        public virtual IPoint PointCoord { get; set; }
        public virtual String PointDescription { get; set; }
        public virtual IEnumerable<Text> Texts { get; set; }
        
        public POI()
        {
            Texts = new List<Text>();
        }
    }
}
