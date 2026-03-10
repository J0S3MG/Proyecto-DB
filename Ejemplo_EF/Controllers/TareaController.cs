using Microsoft.AspNetCore.Mvc;
using Ejemplo_EF.Data.Entities;
using Ejemplo_EF.Services.interfaces;

namespace Ejemplo_EF.Controllers;

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
    public async Task<ActionResult<List<Tarea>>> Get(int pagina = 1, int tamanioPagina = 10)
    {
        try
        {
            return Ok(await _tareaService.GetAll(pagina, tamanioPagina));
        }
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _tareaService.Delete(id);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    #endregion
}