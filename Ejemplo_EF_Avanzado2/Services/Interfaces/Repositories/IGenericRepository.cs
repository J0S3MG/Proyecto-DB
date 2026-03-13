using Ejemplo_EF_Avanzado2.Data.Utils;
using Ejemplo_EF_Avanzado2.Data.Entities;

namespace Ejemplo_EF_Avanzado2.Services.Interfaces.Repositories;

public interface IGenericRepository<T, TId, TFiltro>  where T : BaseEntity where TFiltro : class
{
    Task<T> Insert(T entity);
    Task<PaginadoResult<T>> GetAll(int pagina, int tamanioPagina, TFiltro? filtro = null);
    Task<T?> GetById(TId id);
    void Update(T entity);
    Task<bool> SoftDelete(TId id);
    Task<bool> HardDelete(TId id);
    Task Restore(TId id);
}