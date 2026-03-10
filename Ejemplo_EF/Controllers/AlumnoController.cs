using Microsoft.AspNetCore.Mvc;
using Ejemplo_EF.Data.Entities;
using Ejemplo_EF.Services.interfaces;

namespace Ejemplo_EF.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlumnoController : ControllerBase
{
    private readonly IAlumnoService _alumnoService;

    public AlumnoController(IAlumnoService alumnoService)
    {
        _alumnoService = alumnoService;
    }

    #region Devolver Lista.
    [HttpGet]
    public async Task<ActionResult<List<Alumno>>> Get()
    {
        return Ok(await _alumnoService.GetAll());
    }
    #endregion

    #region Devolver Uno.
    [HttpGet("{id}")]
    public async Task<ActionResult<Alumno>> GetById(int id)
    {
        var alumno = await _alumnoService.GetById(id);
        if (alumno is null) return NotFound();
        return Ok(alumno);
    }
    #endregion

    #region Insertar.
    [HttpPost]
    public async Task<ActionResult<Alumno>> Post([FromBody] Alumno alumno)
    {
        try
        {
            var nuevo = await _alumnoService.Insert(alumno);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    #endregion

    #region Actualizar.
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Alumno alumno)
    {
        try
        {
            await _alumnoService.Update(alumno);
            return Ok("Alumno actualizado.");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    #endregion

    #region Eliminar.
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _alumnoService.Delete(id);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    #endregion

    #region Asignar Tarea.
    [HttpPost("{alumnoId}/tarea")]
    public async Task<IActionResult> AsignarTarea(int alumnoId, [FromBody] Tarea tarea)
    {
        try
        {
            await _alumnoService.AsignarTarea(alumnoId, tarea);
            return Ok("Tarea asignada correctamente.");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    #endregion

    #region Reasignar Tarea.
    [HttpPut("{nuevoAlumnoId}/reasignar/{tareaId}")]
    public async Task<IActionResult> ReasignarTarea(int tareaId, int nuevoAlumnoId)
    {
        try
        {
            await _alumnoService.ReasignarTarea(tareaId, nuevoAlumnoId);
            return Ok("Tarea reasignada correctamente.");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    #endregion
}