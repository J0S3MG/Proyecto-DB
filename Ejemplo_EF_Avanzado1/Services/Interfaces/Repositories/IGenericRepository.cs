using Ejemplo_EF_Avanzado1.Data.Utils;
using Ejemplo_EF_Avanzado1.Data.Entities;

namespace Ejemplo_EF_Avanzado1.Services.Interfaces.Repositories;

public interface IGenericRepository<T, TId>  where T : BaseEntity // En este caso T siempre hereda de baseEntity.
{
    Task<T> Insert(T entity);
     Task<PaginadoResult<T>> GetAll(int pagina, int tamanioPagina);
    Task<T?> GetById(TId id);
    void Update(T entity);
    Task<bool> SoftDelete(TId id);
    Task<bool> HardDelete(TId id);
    Task Restore(TId id);
}