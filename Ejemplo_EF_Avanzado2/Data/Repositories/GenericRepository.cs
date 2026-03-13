using Ejemplo_EF_Avanzado2.Data;
using Ejemplo_EF_Avanzado2.Data.Utils;
using Ejemplo_EF_Avanzado2.Data.Entities;
using Ejemplo_EF_Avanzado2.Services.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ejemplo_EF_Avanzado2.Data.Repositories;

public abstract class GenericRepository<T, TId, TFiltro> : IGenericRepository<T, TId, TFiltro> where T : BaseEntity where TFiltro : class
{
    protected readonly EjemploEfContext _context;

    protected GenericRepository(EjemploEfContext context)
    {
        _context = context;
    }

    protected virtual IQueryable<T> BuildQuery(IQueryable<T> query) => query;

    public async Task<T> Insert(T entity)
    {
        EntityEntry<T> insertedValue = await _context.Set<T>().AddAsync(entity);
        return insertedValue.Entity;
    }
    
    public virtual async Task<PaginadoResult<T>> GetAll(int pagina, int tamanioPagina, TFiltro? filtro = null)
    {
        var query = BuildQuery(_context.Set<T>());
        var total = await query.CountAsync();
        var datos = await query.Skip((pagina - 1) * tamanioPagina).Take(tamanioPagina).ToListAsync();
        return new PaginadoResult<T>
        {
            TotalRegistros = total,
            PaginaActual = pagina,
            TamanioPagina = tamanioPagina,
            Datos = datos
        };
    }

    public async Task<T?> GetById(TId id) => await BuildQuery(_context.Set<T>()).AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
    
    public void Update(T entity) 
    {
        _context.Set<T>().Update(entity);
    }

    public async Task<bool> SoftDelete(TId id)
    {
        T? entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id.Equals(id));
        if (entity is null) return false;
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        return true;
    }

    public async Task<bool> HardDelete(TId id)
    {
        T? entity = await GetById(id);
        if (entity is null) return false;
        _context.Set<T>().Remove(entity);
        return true;
    }
    
    public async Task Restore(TId id)
    {
        var entity = await _context.Set<T>().IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id.Equals(id));
        if (entity is null) throw new Exception($"No existe una entidad con el Id {id}.");
        if (!entity.IsDeleted) throw new Exception($"La entidad con Id {id} no está eliminada.");
        entity.IsDeleted = false;
        entity.DeletedAt = null;
    }
}