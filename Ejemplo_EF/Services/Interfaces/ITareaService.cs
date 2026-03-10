using Ejemplo_EF.Data.Entities;
using Ejemplo_EF.Data.Utils;

namespace Ejemplo_EF.Services.interfaces;

public interface ITareaService
{
    Task<Tarea> Insert(Tarea t);
    Task<PaginadoResult<Tarea>> GetAll(int pagina, int tamanioPagina);
    Task<Tarea?> GetById(int id);
    Task Update(Tarea t);
    Task<bool> Delete(int id);
}
