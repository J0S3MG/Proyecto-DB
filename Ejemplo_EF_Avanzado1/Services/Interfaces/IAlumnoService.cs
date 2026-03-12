using Ejemplo_EF_Avanzado1.Data.Entities;
using Ejemplo_EF_Avanzado1.Data.Utils;

namespace Ejemplo_EF_Avanzado1.Services.Interfaces;

public interface IAlumnoService
{
    Task<Alumno> Insert(Alumno a);
    Task<PaginadoResult<Alumno>> GetAll(int pagina, int tamanioPagina);
    Task<Alumno?> GetById(int lu);
    Task Update(Alumno a);
    Task<bool> SoftDelete(int id);
    Task<bool> HardDelete(int id);
    Task Restore(int id);
}