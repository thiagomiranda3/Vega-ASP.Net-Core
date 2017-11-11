using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Controllers.Resources
{
    public class FeatureResource
    {
        [Required]
        [StringLength(255)]
        public string Name   { get; set; }
    }
}