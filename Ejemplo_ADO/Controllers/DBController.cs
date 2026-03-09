using Microsoft.AspNetCore.Mvc;
using Ejemplo_ADO.Models;
using Ejemplo_ADO.Services.interfaces;

namespace Ejemplo_ADO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DBController : ControllerBase
    {
        private readonly IDBService _dbService;

        public DBController(IDBService dbService)
        {
            _dbService = dbService;
        }

        #region Devolver Lista.
        [HttpGet]
        public async Task<ActionResult<List<Alumno>>> Get()
        {
            return Ok(await _dbService.GetAll());
        }
        #endregion

        #region Devolver Uno.
        [HttpGet("{id}")]
        public async Task<ActionResult<Alumno>> GetById(int id)
        {
            var alumno = await _dbService.GetById(id);
            if (alumno == null) return NotFound();
            return Ok(alumno);
        }
        #endregion

        #region Insertar.
        [HttpPost]
        public async Task<ActionResult<Alumno>> Post([FromBody] Alumno alumno)
        {
            var nuevoAlumno = await _dbService.Insert(alumno);
            return CreatedAtAction(nameof(GetById), new { id = nuevoAlumno.Id }, nuevoAlumno);
        }
        #endregion

        #region Actualizar.
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Alumno alumno)
        {
            var respuesta = await _dbService.Update(alumno);
            if(!respuesta) return NotFound();
            return Ok("Alumno actualizado");
        }
        #endregion

        #region Eliminar.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _dbService.Delete(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
        #endregion
    }
}