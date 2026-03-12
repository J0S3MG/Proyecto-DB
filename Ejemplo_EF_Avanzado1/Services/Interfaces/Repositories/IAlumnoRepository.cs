using Ejemplo_EF_Avanzado1.Data.Entities;

namespace Ejemplo_EF_Avanzado1.Services.Interfaces.Repositories;

public interface IAlumnoRepository: IGenericRepository<Alumno, int>
{
  Task<Alumno?> GetByLU(int lu);
}