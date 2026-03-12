using Ejemplo_EF_Avanzado1.Data.Entities;
using Ejemplo_EF_Avanzado1.Data.Utils;

namespace Ejemplo_EF_Avanzado1.Services.Interfaces;

public interface ITareaService
{
    Task<Tarea> Insert(Tarea t);
    Task<PaginadoResult<Tarea>> GetAll(int pagina, int tamanioPagina);
    Task<Tarea?> GetById(int id);
    Task Update(Tarea t);
    Task<bool> SoftDelete(int id);
    Task<bool> HardDelete(int id);
    Task Restore(int id);
    Task AsignarTarea(int alumnoId, Tarea t);
    Task ReasignarTarea(int tareaId, int nuevoAlumnoId);
}
