using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excolo.Architecture.Core.Domain;

namespace TACO.Model.Domain
{
    public class Text : Entity
    {
        public virtual string TextName { get; set; }
        public virtual string TextContent { get; set; }
        public virtual POI POI { get; set; }
    }
}
