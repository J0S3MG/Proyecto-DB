using Ejemplo_EF.Data;
using Ejemplo_EF.Data.Entities;
using Ejemplo_EF.Services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ejemplo_EF.Repositories;

public class AlumnoRepository: IAlumnoRepository
{
    private readonly EjemploEfContext _context;

    public AlumnoRepository(EjemploEfContext context)
    {
        _context = context;
    }

    public async Task<Alumno> Insert(Alumno a)
    {   
        var result = await _context.Alumno.AddAsync(a); // Insertamos en el DbSet el alumno, nos devuelve un EntityEntry.
        await _context.SaveChangesAsync(); // Guardamos los cambios en la db.
        return result.Entity; // La propiedad Entity devuelve la instancia de la entidad que está siendo seguida por el contexto.  
    }
                                                    // Devuelve la lista de alumnos con sus tareas.
    public async Task<List<Alumno>> GetAll() => await _context.Alumno.Include(a => a.Tareas).ToListAsync();

                                                        // Devuelve un alumno con sus tareas.
    public async Task<Alumno?> GetById(int id) => await _context.Alumno.Include(a => a.Tareas).FirstOrDefaultAsync(a => a.Id == id);

    public void Update(Alumno a) 
    {
        _context.Alumno.Update(a);
        _context.SaveChanges();
    }

    public async Task<bool> Delete(int id)
    {
        var alu = await GetById(id);
        if (alu is null) return false;
        _context.Alumno.Remove(alu);
        await _context.SaveChangesAsync();
        return true;
    }
}