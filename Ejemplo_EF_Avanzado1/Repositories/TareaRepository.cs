using Ejemplo_EF_Avanzado1.Data;
using Ejemplo_EF_Avanzado1.Data.Entities;
using Ejemplo_EF_Avanzado1.Services.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ejemplo_EF_Avanzado1.Repositories;

public class TareaRepository: GenericRepository<Tarea, int>, ITareaRepository
{
    public TareaRepository(EjemploEfContext context) : base(context) { }

    protected override IQueryable<Tarea> BuildQuery(IQueryable<Tarea> query) => query.Include(t => t.Alumno);
}

