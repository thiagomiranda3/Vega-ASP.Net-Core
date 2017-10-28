using System.ComponentModel.DataAnnotations;

namespace Vega_ASP.Net_Core.Models
{
    public class Feature
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name   { get; set; }
        
    }
}