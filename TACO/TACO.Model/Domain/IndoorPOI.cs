using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excolo.Architecture.Core.Domain;

namespace TACO.Model.Domain
{
    public class IndoorPOI : Entity
    {
        public virtual string IndoorPOIName { get; set; }
        public virtual string IndoorPOIDescription { get; set; }
        public virtual IEnumerable<Text> Texts { get; set; }
        public virtual IEnumerable<WifiMeassure> WifiMeassures { get; set; }
    }
}
