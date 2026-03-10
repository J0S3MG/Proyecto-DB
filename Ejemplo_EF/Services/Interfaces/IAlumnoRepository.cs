using Ejemplo_EF.Data.Entities;

namespace Ejemplo_EF.Services.interfaces;

public interface IAlumnoRepository
{
    Task<Alumno> Insert(Alumno a);
    Task<List<Alumno>> GetAll();
    Task<Alumno?> GetById(int lu);
    void Update(Alumno a);
    Task<bool> Delete(int lu);
}