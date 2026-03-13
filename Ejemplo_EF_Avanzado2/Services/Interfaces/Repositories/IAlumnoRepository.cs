using Ejemplo_EF_Avanzado2.Data.Utils;
using Ejemplo_EF_Avanzado2.Data.Entities;

namespace Ejemplo_EF_Avanzado2.Services.Interfaces.Repositories;

public interface IAlumnoRepository: IGenericRepository<Alumno, int, AlumnoFiltro>
{
  Task<Alumno?> GetByLU(int lu);
}