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
    public class marcasController : ControllerBase
    {
        private readonly equiposContext _context;

        public marcasController(equiposContext context)
        {
            _context = context;
        }

        // GET: api/marcas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<marcas>>> Getmarcas()
        {
          if (_context.marcas == null)
          {
              return NotFound();
          }
            return await _context.marcas.ToListAsync();
        }

        // GET: api/marcas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<marcas>> Getmarcas(int id)
        {
          if (_context.marcas == null)
          {
              return NotFound();
          }
            var marcas = await _context.marcas.FindAsync(id);

            if (marcas == null)
            {
                return NotFound();
            }

            return marcas;
        }

        // PUT: api/marcas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putmarcas(int id, marcas marcas)
        {
            if (id != marcas.id_marcas)
            {
                return BadRequest();
            }

            _context.Entry(marcas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!marcasExists(id))
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

        // POST: api/marcas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<marcas>> Postmarcas(marcas marcas)
        {
          if (_context.marcas == null)
          {
              return Problem("Entity set 'equiposContext.marcas'  is null.");
          }
            _context.marcas.Add(marcas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getmarcas", new { id = marcas.id_marcas }, marcas);
        }

        // DELETE: api/marcas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletemarcas(int id)
        {
            if (_context.marcas == null)
            {
                return NotFound();
            }
            var marcas = await _context.marcas.FindAsync(id);
            if (marcas == null)
            {
                return NotFound();
            }

            _context.marcas.Remove(marcas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool marcasExists(int id)
        {
            return (_context.marcas?.Any(e => e.id_marcas == id)).GetValueOrDefault();
        }
    }
}
