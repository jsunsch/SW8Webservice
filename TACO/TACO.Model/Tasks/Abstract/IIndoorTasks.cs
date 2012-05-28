using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TACO.Model.Domain;

namespace TACO.Model.Tasks.Abstract
{
    public interface IIndoorTasks
    {
        Attraction CreateAttraction(string attractionName, string attractionDescription);
        IndoorPOI CreateIndoorPOI(string indoorName, string indoorDesc, Attraction attraction);
        WifiMeassure CreateWifiMeassure(string BSID, int signalStrength, IndoorPOI indoorPOI);

        Attraction GetAttraction(int id);
    }
}
