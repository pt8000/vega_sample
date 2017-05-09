using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet] //wg routy z góry, dostęp przez api/makes/getmakes
        public IEnumerable<MakesView> GetMakes()
        {
            var makesList = _context.Makes.ToList();
            var model = _mapper.Map<IEnumerable<Makes>, IEnumerable<MakesView>>(makesList);
            return model;
        }
    }
}