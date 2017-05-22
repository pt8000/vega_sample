using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VegaApp.Data;
using VegaApp.Modells;
using VegaApp.Resources;

namespace VegaApp.Controllers
{
    [Route("api/[controller]/[action]")] //zasada ogolna
    public class FeaturesController : Controller
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;
        public FeaturesController(VegaDbContext _context, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._context = _context;
        }

        [HttpGet("/api/features")] //nadpisana przez wlasna sciezke dostepu
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await _context.Feature.ToListAsync();
            
            return _mapper.Map<IEnumerable<Feature>, IEnumerable<KeyValuePairResource>>(features); 
        }
    }
}