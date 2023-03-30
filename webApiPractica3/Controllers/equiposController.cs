using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiPractica3.Models;
using Microsoft.EntityFrameworkCore;

namespace webApiPractica3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public equiposController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<equipos> listadoEquipo = (from e in _equiposContexto.equipos
                                           select e).ToList();

            if (listadoEquipo.Count() == 0)
            {
                   return NotFound();
            }

            return Ok(listadoEquipo);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get( int id)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos  == id
                               select e).FirstOrDefault();


            if (equipo == null)
            {
                return NotFound();
            }

            return Ok(equipo);
        }

        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByDescription(string filtro)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.descripcion.Contains(filtro)
                               select e).FirstOrDefault();                            

            if (equipo == null)
            {
                return NotFound();
            }

            return Ok(equipo);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] equipos equipo)
        {
            try
            {
                _equiposContexto.equipos.Add(equipo);
                _equiposContexto.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception e){ 
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEquipo(int id, [FromBody] equipos equipoModificar)
        {
            try
            {
                equipos? equipoActual = (from e in _equiposContexto.equipos
                                         where e.id_equipos == id
                                         select e).FirstOrDefault();
                if (equipoActual == null)
                {
                    return NotFound();
                }
                equipoActual.nombre = equipoModificar.nombre;
                equipoActual.descripcion = equipoModificar.descripcion;
                equipoActual.marca_id = equipoModificar.marca_id;
                equipoActual.tipo_equipo_id = equipoModificar.tipo_equipo_id;
                equipoActual.anio_compra = equipoModificar.anio_compra;
                equipoActual.costo = equipoModificar.costo;

                _equiposContexto.Entry(equipoActual).State = EntityState.Modified;
                _equiposContexto.SaveChanges();
                return Ok(equipoModificar);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            try
            {
                equipos? equipo = (from e in _equiposContexto.equipos
                                         where e.id_equipos == id
                                         select e).FirstOrDefault();
                if (equipo == null)
                {
                    return NotFound();
                }

                _equiposContexto.equipos.Attach(equipo);
                _equiposContexto.equipos.Remove(equipo);
                _equiposContexto.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




    }
}
