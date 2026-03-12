using Microsoft.AspNetCore.Mvc;
using Ejemplo_EF_Avanzado1.Data.Entities;
using Ejemplo_EF_Avanzado1.Services.Interfaces;

namespace Ejemplo_EF_Avanzado1.Controllers;

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
    public async Task<ActionResult<List<Alumno>>> Get(int pagina = 1, int tamanioPagina = 10)
    {
        try
        {
            return Ok(await _alumnoService.GetAll(pagina, tamanioPagina));
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
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
    [HttpDelete("{id}/soft")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        try
        {
            await _alumnoService.SoftDelete(id);
            return NoContent();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id}/hard")]
    public async Task<IActionResult> HardDelete(int id)
    {
        try
        {
            await _alumnoService.HardDelete(id);
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
            await _alumnoService.Restore(id);
            return Ok("Alumno restaurado correctamente.");
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
    #endregion
}