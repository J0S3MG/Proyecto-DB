using Ejemplo_EF_Avanzado1.Data.Utils;
using Ejemplo_EF_Avanzado1.Data.Entities;
using Ejemplo_EF_Avanzado1.Services.Interfaces;
using Ejemplo_EF_Avanzado1.Services.Interfaces.Repositories;

namespace Ejemplo_EF_Avanzado1.Services;

public class TareaService : ITareaService
{
    private readonly ITareaRepository _tareas;
    private readonly IAlumnoRepository _alumnos;

    public TareaService(ITareaRepository tareas, IAlumnoRepository alumnos)
    {
        _tareas = tareas;
        _alumnos = alumnos;
    }

    #region Insertar Tarea.
    public async Task<Tarea> Insert(Tarea t)
    {
        t.Id = 0;
        var alumno = await _alumnos.GetById(t.AlumnoId);
        if (alumno is null) throw new Exception($"No existe un alumno con el Id {t.AlumnoId}.");
        if (t.FechaEntrega < DateOnly.FromDateTime(DateTime.UtcNow)) throw new Exception("La fecha de entrega no puede ser una fecha pasada.");
        return await _tareas.Insert(t);
    }
    #endregion

    #region Devolver Tareas.
    public async Task<PaginadoResult<Tarea>> GetAll(int pagina = 1, int tamanioPagina = 10)
    {
        if (pagina < 1) throw new Exception("La página debe ser mayor a 0.");
        if (tamanioPagina < 1 || tamanioPagina > 50) throw new Exception("El tamaño de página debe estar entre 1 y 50.");
        return await _tareas.GetAll(pagina, tamanioPagina);
    }
    #endregion

    #region Devolver una Tarea.
    public async Task<Tarea?> GetById(int id) => await _tareas.GetById(id);
    #endregion

    #region Actualizar Tarea.
    public async Task Update(Tarea t)
    {
        var existe = await _tareas.GetById(t.Id);
        if (existe is null) throw new Exception($"No existe una tarea con el Id {t.Id}.");
        if (t.FechaEntrega < DateOnly.FromDateTime(DateTime.UtcNow)) throw new Exception("La fecha de entrega no puede ser una fecha pasada.");
        if (t.AlumnoId != existe.AlumnoId)
        {
            var alumno = await _alumnos.GetById(t.AlumnoId);
            if (alumno is null) throw new Exception($"No existe un alumno con el Id {t.AlumnoId}.");
        }
        _tareas.Update(t);
    }
    #endregion

    #region Eliminar Tarea.
    public async Task<bool> SoftDelete(int id)
    {
        var existe = await _tareas.GetById(id);
        if (existe is null) throw new Exception($"No existe una tarea con el Id {id}.");
        return await _tareas.SoftDelete(id);
    }

    public async Task<bool> HardDelete(int id)
    {
        var existe = await _tareas.GetById(id);
        if (existe is null) throw new Exception($"No existe una tarea con el Id {id}.");
        return await _tareas.HardDelete(id);
    }
    #endregion

    #region Restaurar Tarea.
    public async Task Restore(int id) => await _tareas.Restore(id);
    #endregion

    #region Asignar Tarea.
    public async Task AsignarTarea(int alumnoId, Tarea t)
    {
        var alumno = await _alumnos.GetById(alumnoId);
        if (alumno is null) throw new Exception($"No existe un alumno con el Id {alumnoId}.");
        if (t.FechaEntrega < DateOnly.FromDateTime(DateTime.UtcNow)) throw new Exception("La fecha de entrega no puede ser una fecha pasada.");

        t.AlumnoId = alumnoId; // Asignamos la FK antes de insertar.
        await _tareas.Insert(t);
    }
    #endregion

    #region Reasignar Tarea.
    public async Task ReasignarTarea(int tareaId, int nuevoAlumnoId)
    {
        var tarea = await _tareas.GetById(tareaId);
        if (tarea is null) throw new Exception($"No existe una tarea con el Id {tareaId}.");
        var alumno = await _alumnos.GetById(nuevoAlumnoId);
        if (alumno is null) throw new Exception($"No existe un alumno con el Id {nuevoAlumnoId}.");

        tarea.AlumnoId = nuevoAlumnoId; // Cambiamos la FK al nuevo alumno.
        // Notificamos al tracker que hubo un cambio.
        _tareas.Update(tarea);
    }
    #endregion
}