using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practicaMVC05.Models;
using webApiPractica3.Models;

namespace webApiPractica3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class facultadesController : ControllerBase
    {
        private readonly equiposContext _context;

        public facultadesController(equiposContext context)
        {
            _context = context;
        }

        // GET: api/facultades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<facultades>>> Getfacultades()
        {
          if (_context.facultades == null)
          {
              return NotFound();
          }
            return await _context.facultades.ToListAsync();
        }

        // GET: api/facultades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<facultades>> Getfacultades(int id)
        {
          if (_context.facultades == null)
          {
              return NotFound();
          }
            var facultades = await _context.facultades.FindAsync(id);

            if (facultades == null)
            {
                return NotFound();
            }

            return facultades;
        }

        // PUT: api/facultades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putfacultades(int id, facultades facultades)
        {
            if (id != facultades.facultad_id)
            {
                return BadRequest();
            }

            _context.Entry(facultades).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!facultadesExists(id))
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

        // POST: api/facultades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<facultades>> Postfacultades(facultades facultades)
        {
          if (_context.facultades == null)
          {
              return Problem("Entity set 'equiposContext.facultades'  is null.");
          }
            _context.facultades.Add(facultades);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getfacultades", new { id = facultades.facultad_id }, facultades);
        }

        // DELETE: api/facultades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletefacultades(int id)
        {
            if (_context.facultades == null)
            {
                return NotFound();
            }
            var facultades = await _context.facultades.FindAsync(id);
            if (facultades == null)
            {
                return NotFound();
            }

            _context.facultades.Remove(facultades);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool facultadesExists(int id)
        {
            return (_context.facultades?.Any(e => e.facultad_id == id)).GetValueOrDefault();
        }
    }
}
