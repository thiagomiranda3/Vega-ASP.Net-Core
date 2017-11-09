using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega_ASP.Net_Core.Controllers.Resources;
using Vega_ASP.Net_Core.Core;
using Vega_ASP.Net_Core.Core.Models;
using Vega_ASP.Net_Core.Persistence;

namespace Vega_ASP.Net_Core.Controllers
{
    [Route("/api/features")]
    public class FeatureController : Controller
    {
        private readonly IMapper mapper;
        private readonly IFeatureRepository featureRepository;

        public FeatureController(IMapper mapper, IFeatureRepository featureRepository)
        {
            this.featureRepository = featureRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<KeyValuePairResource>> Get()
        {
            var features = await featureRepository.GetAllAsync();
            return mapper.Map<ICollection<Feature>, ICollection<KeyValuePairResource>>(features);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vehicle = await featureRepository.GetAsync(id);
            if (vehicle == null)
                return NotFound();

            return Ok(mapper.Map<Feature, KeyValuePairResource>(vehicle));
        }
    }
}