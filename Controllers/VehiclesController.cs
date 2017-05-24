using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//folder Core, jako pakiet oddzielajacy do reverse injection principal
//tu gdzie mamy interfejsy, ktore komunikuja sie z pakietem Data (dbcontext, unit of work, implementacja repositoriow)
using VegaApp.Core; 
using VegaApp.Core.Models;
using VegaApp.Resources;

namespace VegaApp.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository vehicleRepo;
        private readonly IUnitOfWork unitOfWork;
        public VehiclesController(IMapper _mapper, IVehicleRepository vehicleRepo, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.vehicleRepo = vehicleRepo;
            this._mapper = _mapper;
        }

        [HttpPost("/api/vehicles/new")]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleRes)
        {
            throw new Exception();
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //dodatkowa walidacja pol ktore sa wymagane itp, zeby nie bylo sql errorow
            //czy to robic czy nie, kwestia wyboru, mozna tez sprawdzac inputy po stronie klienta albo je ograniczac

            /*var model = await _context.Models.FindAsync(vehicleRes.ModelId);
            if(model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid modelId");
                return BadRequest(ModelState);
            }*/

            var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(vehicleRes);
            vehicle.LastUpdate = DateTime.Now;

            vehicleRepo.Add(vehicle);
            // await _context.SaveChangesAsync();
            await unitOfWork.CompleteAsync();


            // var result = _mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            // zmiana zeby pokazywalo w rezultacie pelne dane dodanego auta a nie tylko id jak w savevehicleresource
            // tzeba zresetowac caly obiekt vehicle bo nie bylo w nim pelnych danych
            vehicle = await vehicleRepo.GetVehicle(vehicle.Id);

            var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")] // /api/vehicles/{id}
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleRes)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // var vehicle = await _context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);
            // teraz pelny obiek z nazwami
            var vehicle = await vehicleRepo.GetVehicle(id);

            if (vehicle == null)
                return NotFound();


            _mapper.Map<SaveVehicleResource, Vehicle>(vehicleRes, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            // await _context.SaveChangesAsync();
            await unitOfWork.CompleteAsync();

            // var result = _mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            vehicle = await vehicleRepo.GetVehicle(vehicle.Id);
            var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await vehicleRepo.GetVehicle(id, includeRelated: false); //dla czytelnosci podaje nazwe parametru do czego to false sie odnosi

            if (vehicle == null)
                return NotFound();

            vehicleRepo.Remove(vehicle);
            // await _context.SaveChangesAsync();
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await vehicleRepo.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}