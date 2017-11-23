using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUnitOfWork unitOfWork;

        public FeatureController(IMapper mapper, IFeatureRepository featureRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.featureRepository = featureRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] FeatureResource featureResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var feature = mapper.Map<FeatureResource, Feature>(featureResource);

            featureRepository.Add(feature);
            await unitOfWork.CompleteAsync();

            return Ok(featureResource);
        }
    }
}