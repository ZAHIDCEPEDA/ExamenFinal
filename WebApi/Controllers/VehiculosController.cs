using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clases.Model;
using Clases.ModelosNuevos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly EjercicioEvaluacionContext _context;

        public VehiculosController(EjercicioEvaluacionContext context)
        {
            _context = context;
        }

        // GET: api/Vehiculos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculo()
        {
            return await _context.Vehiculo.ToListAsync();
        }

        // GET: api/Vehiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehiculo>> GetVehiculo(int id)
        {
            var vehiculo = await _context.Vehiculo.FindAsync(id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return vehiculo;
        }

        // PUT: api/Vehiculos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiculo(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(vehiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vehiculos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehiculo>> PostVehiculo(Vehiculo vehiculo)
        {
            _context.Vehiculo.Add(vehiculo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VehiculoExists(vehiculo.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVehiculo", new { id = vehiculo.Codigo }, vehiculo);
        }

        // DELETE: api/Vehiculos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculo(int id)
        {
            var vehiculo = await _context.Vehiculo.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            _context.Vehiculo.Remove(vehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehiculoExists(int id)
        {
            return _context.Vehiculo.Any(e => e.Codigo == id);
        }
    }
}
