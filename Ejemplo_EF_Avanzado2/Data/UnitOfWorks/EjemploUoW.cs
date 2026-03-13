using Ejemplo_EF_Avanzado2.Services.Interfaces;
using Ejemplo_EF_Avanzado2.Services.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ejemplo_EF_Avanzado2.Data.UnitOfWorks;

public class EjemploUoW : IEjemploUoW
{
    private readonly EjemploEfContext _context;
    private IDbContextTransaction? _transaction;

    public IAlumnoRepository Alumnos { get; }
    public ITareaRepository Tareas { get; }

    public EjemploUoW(EjemploEfContext context, IAlumnoRepository alumnos, ITareaRepository tareas)
    {
        _context = context;
        Alumnos = alumnos;
        Tareas = tareas;
    }

    
    // Es el único punto donde se escribe en la BD, garantizando que todas las operaciones anteriores se guarden juntas o ninguna.
    public async Task SaveAsync() => await _context.SaveChangesAsync();

   
    // Necesario cuando tenés múltiples SaveAsync dentro de una misma operacióny necesitás que sean atómicas: o todas se guardan o ninguna.
    public async Task BeginTransactionAsync() => _transaction = await _context.Database.BeginTransactionAsync();

    public async Task CommitAsync() // Solo se ejecuta si todas las operaciones salieron bien.
    {
        if (_transaction is null) throw new Exception("Transacciones realizadas con exito");
        await _transaction.CommitAsync();
    }

    
    public async Task RollbackAsync() // Deja la BD en el estado anterior al BeginTransaction.
    {
        if (_transaction is null) throw new Exception("Rollback: No hay ninguna transaccion activa.");
        await _transaction.RollbackAsync();
    }

    // Libera los recursos del DbContext y la transacción si quedo abierta.
    public void Dispose() // IDisposable garantiza que esto se llame al finalizar el scope,
    {                     // evitando memory leaks y conexiones abiertas innecesariamente.
        _transaction?.Dispose();
        _context.Dispose();
    }
}