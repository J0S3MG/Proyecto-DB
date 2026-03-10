using Ejemplo_EF.Data;
using Ejemplo_EF.Data.Utils;
using Ejemplo_EF.Data.Entities;
using Ejemplo_EF.Services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ejemplo_EF.Repositories;

public class TareaRepository: ITareaRepository
{
    private readonly EjemploEfContext _context;

    public TareaRepository(EjemploEfContext context)
    {
        _context = context;
    }

    public async Task<Tarea> Insert(Tarea t)
    {   
        var result = await _context.Tarea.AddAsync(t); // Insertamos en el DbSet el alumno, nos devuelve un EntityEntry.
        await _context.SaveChangesAsync(); // Guardamos los cambios en la db.
        return result.Entity; // La propiedad Entity devuelve la instancia de la entidad que está siendo seguida por el contexto.  
    }
                                               
    public async Task<PaginadoResult<Tarea>> GetAll(int pagina, int tamanioPagina)
    {
        var total = await _context.Tarea.CountAsync();
        var datos = await _context.Tarea.Include(t => t.Alumno).Skip((pagina - 1) * tamanioPagina).Take(tamanioPagina).ToListAsync();
        return new PaginadoResult<Tarea>
        {
            TotalRegistros = total,
            PaginaActual = pagina,
            TamanioPagina = tamanioPagina,
            Datos = datos
        };
    }

                                                        // Devuelve un alumno con sus tareas.
    public async Task<Tarea?> GetById(int id) => await _context.Tarea.Include(t => t.Alumno).FirstOrDefaultAsync(t => t.Id == id);

    public void Update(Tarea t) 
    {
        _context.Tarea.Update(t);
        _context.SaveChanges();
    }

    public async Task<bool> Delete(int id)
    {
        var alu = await GetById(id);
        if (alu is null) return false;
        _context.Tarea.Remove(alu);
        await _context.SaveChangesAsync();
        return true;
    }
}