using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vega_ASP.Net_Core.Core.Models
{
    public class Photo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        
    }
}