using System.ComponentModel.DataAnnotations.Schema;
using Vega_ASP.Net_Core.Models;

namespace Vega_ASP.Net_Core.Models
{
    [Table("VehicleFeatures")]
    public class VehicleFeature
    {
        public int VehicleId { get; set; }
        public int FeatureId { get; set; }
        public Vehicle Vehicle { get; set; }
        public Feature Feature { get; set; }
    }
}