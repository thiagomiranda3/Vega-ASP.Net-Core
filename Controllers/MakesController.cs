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
    [Route("/api/makes")]
    public class MakesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IMakeRepository makeRepository;

        public MakesController(IMapper mapper, IMakeRepository makeRepository)
        {
            this.makeRepository = makeRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<MakeResource>> Get()
        {
            var makes = await makeRepository.GetAllAsync();
            return mapper.Map<ICollection<Make>, ICollection<MakeResource>>(makes);
        }
    }
}