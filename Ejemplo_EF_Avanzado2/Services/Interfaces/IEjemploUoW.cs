using Ejemplo_EF_Avanzado2.Services.Interfaces.Repositories;

namespace Ejemplo_EF_Avanzado2.Services.Interfaces;

public interface IEjemploUoW : IDisposable
{
    IAlumnoRepository Alumnos { get; }
    ITareaRepository Tareas { get; }
    Task SaveAsync();             // Persiste todos los cambios trackeados en el DbContext de una sola vez.
    Task BeginTransactionAsync(); // Inicia una transacción explícita en la BD.
    Task CommitAsync();           // Confirma todos los cambios de la transacción en la BD.
    Task RollbackAsync();         // Revierte todos los cambios de la transacción si algo falló.
}