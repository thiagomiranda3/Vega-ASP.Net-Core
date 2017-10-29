using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega_ASP.Net_Core.Controllers.Resources;
using Vega_ASP.Net_Core.Models;
using Vega_ASP.Net_Core.Persistence;

namespace Vega_ASP.Net_Core.Controllers
{
    [Route("/api/makes")]
    public class MakesController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public MakesController(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<MakeResource>> Get()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}