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
    [Route("api/[controller]/[action]")]
    public class MakesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly VegaDbContext _context;
        
        public MakesController(IMapper mapper, VegaDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("/api/makes")] //wg routy z góry, dostęp przez api/makes/getmakes
        public async Task<IEnumerable<MakesView>> GetMakes() 
        {
            var makesList = await _context.Makes.Include(m => m.Modele).ToListAsync();
            
            return _mapper.Map<IEnumerable<Make>, IEnumerable<MakesView>>(makesList);
        }
    }
}