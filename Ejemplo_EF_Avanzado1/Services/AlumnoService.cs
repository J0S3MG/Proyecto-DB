using Ejemplo_EF_Avanzado1.Data.Utils;
using Ejemplo_EF_Avanzado1.Data.Entities;
using Ejemplo_EF_Avanzado1.Services.Interfaces;
using Ejemplo_EF_Avanzado1.Services.Interfaces.Repositories;

namespace Ejemplo_EF_Avanzado1.Services;

public class AlumnoService : IAlumnoService
{
    private readonly ITareaRepository _tareas;
    private readonly IAlumnoRepository _alumnos;

    public AlumnoService(IAlumnoRepository alumnos, ITareaRepository tareas)
    {
        _tareas = tareas;
        _alumnos = alumnos;
    }

    #region Insertar Alumno.
    public async Task<Alumno> Insert(Alumno a)
    {
        a.Id = 0;
        if (a.Nota < 0 || a.Nota > 10) throw new Exception("La nota debe estar entre 0 y 10.");
        var resultado = await _alumnos.GetByLU(a.LU);
        if (resultado != null) throw new Exception($"Ya existe un alumno con el LU {a.LU}.");
        return await _alumnos.Insert(a);
    }
    #endregion

    #region Devolver Alumnos.
     public async Task<PaginadoResult<Alumno>> GetAll(int pagina = 1, int tamanioPagina = 10)
    {
        if (pagina < 1) throw new Exception("La página debe ser mayor a 0.");
        if (tamanioPagina < 1 || tamanioPagina > 50) throw new Exception("El tamaño de página debe estar entre 1 y 50.");
        return await _alumnos.GetAll(pagina, tamanioPagina);
    }
    #endregion

    #region Devolver un Alumno.
    public async Task<Alumno?> GetById(int id) => await _alumnos.GetById(id);
    #endregion

    #region Actualizar Alumno.
    public async Task Update(Alumno a)
    {
        var existe = await _alumnos.GetById(a.Id);
        if (existe is null) throw new Exception($"No existe un alumno con el Id {a.Id}.");
        if (a.Nota < 0 || a.Nota > 10) throw new Exception("La nota debe estar entre 0 y 10.");
         _alumnos.Update(a);
    }
    #endregion

    #region Eliminar Alumno.
    public async Task<bool> SoftDelete(int id)
    {
        var existe = await _alumnos.GetById(id);
        if (existe is null) throw new Exception($"No existe un alumno con el Id {id}.");
        foreach (var tarea in existe.Tareas) await _tareas.SoftDelete(tarea.Id);
        return await _alumnos.SoftDelete(id);
    }

    public async Task<bool> HardDelete(int id)
    {
        var existe = await _alumnos.GetById(id);
        if (existe is null) throw new Exception($"No existe un alumno con el Id {id}.");
        return await _alumnos.HardDelete(id);
    }
    #endregion
    
    #region Restaurar Alumno.
    public async Task Restore(int id) => await _alumnos.Restore(id);
    #endregion
}