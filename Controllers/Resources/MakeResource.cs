using System.Collections.Generic;
using System.Collections.ObjectModel;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Controllers.Resources
{
    public class MakeResource : KeyValuePairResource
    {
        public ICollection<KeyValuePairResource> Models { get; set; }
        
        public MakeResource()
        {
            Models = new Collection<KeyValuePairResource>();
        }
    }
}