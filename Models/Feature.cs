using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vega_ASP.Net_Core.Models
{
    [Table("Features")]
    public class Feature
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name   { get; set; }
        public ICollection<VehicleFeature> Vehicles { get; set;}

        public Feature()
        {
            Vehicles = new Collection<VehicleFeature>();
        }
    }
}