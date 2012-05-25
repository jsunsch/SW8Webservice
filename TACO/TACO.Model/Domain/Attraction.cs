using Sstem;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excolo.Architecture.Core.Domain;

namespace TACO.Model.Domain
{
    public class Attraction : Entity
    {
        public virtual string AttractionName { get; set; }
        public virtual string AttractionDescription { get; set; }
    }
}
