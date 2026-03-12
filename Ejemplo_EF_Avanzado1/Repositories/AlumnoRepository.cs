using Ejemplo_EF_Avanzado1.Data;
using Ejemplo_EF_Avanzado1.Data.Entities;
using Ejemplo_EF_Avanzado1.Services.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ejemplo_EF_Avanzado1.Repositories;

public class AlumnoRepository: GenericRepository<Alumno, int>, IAlumnoRepository
{
    public AlumnoRepository(EjemploEfContext context) : base(context) { }

    protected override IQueryable<Alumno> BuildQuery(IQueryable<Alumno> query) => query.Include(a => a.Tareas);

    public async Task<Alumno?> GetByLU(int lu) => await  BuildQuery(base._context.Alumno).AsNoTracking().FirstOrDefaultAsync(a => a.LU == lu);
}