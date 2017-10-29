using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vega_ASP.Net_Core.Controllers.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public ContactResource Contact { get; set; }
        public ICollection<int> Features { get; set;}

        public VehicleResource()
        {
            Features = new Collection<int>();
        }
    }
}