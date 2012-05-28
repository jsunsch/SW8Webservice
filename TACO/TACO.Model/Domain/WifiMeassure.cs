using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excolo.Architecture.Core.Domain;

namespace TACO.Model.Domain
{
    public class WifiMeassure : Entity
    {
        public virtual string BSID { get; set; }
        public virtual int SignalStrength { get; set; }
        public virtual IndoorPOI POI { get; set; }
    }
}
