using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TACO.Model.Domain;

namespace TACO.Model.Tasks.Abstract
{
    public interface ITextTasks
    {
        Text CreateText(string textName, string textContent, POI associatedPOI);
    }
}
