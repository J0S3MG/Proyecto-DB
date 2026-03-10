using Ejemplo_EF.Data.Entities;
using Ejemplo_EF.Data.Utils;

namespace Ejemplo_EF.Services.interfaces;

public interface ITareaRepository
{
    Task<Tarea> Insert(Tarea a);       // Cant de paginas a devolver, tamaño de paginas.
    Task<PaginadoResult<Tarea>> GetAll(int pagina, int tamanioPagina);
    Task<Tarea?> GetById(int lu);
    void Update(Tarea a);
    Task<bool> Delete(int lu);
}