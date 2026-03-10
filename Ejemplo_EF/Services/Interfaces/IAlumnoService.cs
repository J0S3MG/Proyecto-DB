using Ejemplo_EF.Data.Entities;

namespace Ejemplo_EF.Services.interfaces;

public interface IAlumnoService
{
    Task<Alumno> Insert(Alumno a);
    Task<List<Alumno>> GetAll();
    Task<Alumno?> GetById(int lu);
    Task Update(Alumno a);
    Task<bool> Delete(int lu);
    Task AsignarTarea(int alumnoId, Tarea t);
    Task ReasignarTarea(int tareaId, int nuevoAlumnoId);
}