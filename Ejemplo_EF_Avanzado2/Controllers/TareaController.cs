using Microsoft.AspNetCore.Mvc;
using Ejemplo_EF_Avanzado2.Data.Utils;
using Ejemplo_EF_Avanzado2.Data.Entities;
using Ejemplo_EF_Avanzado2.Services.Interfaces;

namespace Ejemplo_EF_Avanzado2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareaController : ControllerBase
{
    private readonly ITareaService _tareaService;

    public TareaController(ITareaService tareaService)
    {
        _tareaService = tareaService;
    }

    #region Devolver Lista.
    [HttpGet]
    public async Task<IActionResult> Get(int pagina = 1, int tamanioPagina = 10, [FromQuery] TareaFiltro? filtro = null)
    {
        try { return Ok(await _tareaService.GetAll(pagina, tamanioPagina, filtro)); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    #endregion

    #region Devolver Uno.
    [HttpGet("{id}")]
    public async Task<ActionResult<Tarea>> GetById(int id)
    {
        var alumno = await _tareaService.GetById(id);
        if (alumno is null) return NotFound();
        return Ok(alumno);
    }
    #endregion

    #region Insertar.
    [HttpPost]
    public async Task<ActionResult<Tarea>> Post([FromBody] Tarea tarea)
    {
        try
        {
            var nuevo = await _tareaService.Insert(tarea);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, nuevo);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    #endregion

    #region Actualizar.
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Tarea tarea)
    {
        try
        {
            await _tareaService.Update(tarea);
            return Ok("Tarea actualizada."); 
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    #endregion

    #region Eliminar.
    [HttpDelete("{id}/soft")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        try
        {
            await _tareaService.SoftDelete(id);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id}/hard")]
    public async Task<IActionResult> HardDelete(int id)
    {
        try
        {
            await _tareaService.HardDelete(id);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    #endregion

    #region Restaurar.
    [HttpPatch("{id}/restore")]
    public async Task<IActionResult> Restore(int id)
    {
        try
        {
            await _tareaService.Restore(id);
            return Ok("Tarea restaurada correctamente.");
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
            await _tareaService.AsignarTarea(alumnoId, tarea);
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
            await _tareaService.ReasignarTarea(tareaId, nuevoAlumnoId);
            return Ok("Tarea reasignada correctamente.");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    #endregion
}