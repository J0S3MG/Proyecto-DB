using Ejemplo_ADO.Models;

namespace Ejemplo_ADO.Services.interfaces;

public interface IAlumnoDAO
{
    Task<Alumno?> Insert(Alumno a);
    Task<List<Alumno>> GetAll();
    Task<Alumno?> GetById(int lu);
    Task<bool> Update(Alumno a);
    Task<bool> Delete(int lu);
}