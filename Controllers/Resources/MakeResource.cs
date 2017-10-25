using System.Collections.Generic;
using System.Collections.ObjectModel;
using Vega_ASP.Net_Core.Models;

namespace Vega_ASP.Net_Core.Controllers.Resources
{
    public class MakeResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelResource> Models { get; set; }
        
        public MakeResource()
        {
            Models = new Collection<ModelResource>();
        }
    }
}