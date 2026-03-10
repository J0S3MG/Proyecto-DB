using Ejemplo_EF.Data.Entities;
using Ejemplo_EF.Services.interfaces;

namespace Ejemplo_EF.Services;

public class AlumnoService : IAlumnoService
{
    private readonly IAlumnoRepository _alumnos;
    private readonly ITareaRepository _tareas;

    public AlumnoService(IAlumnoRepository alumnos, ITareaRepository tareas)
    {
        _alumnos = alumnos;
        _tareas = tareas;
    }

    #region Insertar Alumno.
    public async Task<Alumno> Insert(Alumno a)
    {
        a.Id = 0;
        if (a.Nota < 0 || a.Nota > 10) throw new Exception("La nota debe estar entre 0 y 10.");
        var alumnos = await _alumnos.GetAll();
        bool luRepetido = alumnos.Any(x => x.LU == a.LU);
        if (luRepetido) throw new Exception($"Ya existe un alumno con el LU {a.LU}.");
        return await _alumnos.Insert(a);
    }
    #endregion

    #region Devolver Alumnos.
    public async Task<List<Alumno>> GetAll() => await _alumnos.GetAll();

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
    public async Task<bool> Delete(int id)
    {
        var existe = await _alumnos.GetById(id);
        if (existe is null) throw new Exception($"No existe un alumno con el Id {id}.");
        return await _alumnos.Delete(id);
    }
    #endregion

    #region Asignar Tarea.
    public async Task AsignarTarea(int alumnoId, Tarea t)
    {
        var alumno = await _alumnos.GetById(alumnoId);
        if (alumno is null) throw new Exception($"No existe un alumno con el Id {alumnoId}.");
        if (t.FechaEntrega < DateTime.UtcNow) throw new Exception("La fecha de entrega no puede ser una fecha pasada.");

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
        _alumnos.Update(alumno); 
        _tareas.Update(tarea);
    }
    #endregion
}