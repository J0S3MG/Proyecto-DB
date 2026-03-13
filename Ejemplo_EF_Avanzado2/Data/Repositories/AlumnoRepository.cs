using Ejemplo_EF_Avanzado2.Data;
using Ejemplo_EF_Avanzado2.Data.Utils;
using Ejemplo_EF_Avanzado2.Data.Entities;
using Ejemplo_EF_Avanzado2.Services.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ejemplo_EF_Avanzado2.Data.Repositories;

public class AlumnoRepository: GenericRepository<Alumno, int, AlumnoFiltro>, IAlumnoRepository
{
    public AlumnoRepository(EjemploEfContext context) : base(context) { }

    protected override IQueryable<Alumno> BuildQuery(IQueryable<Alumno> query) => query.Include(a => a.Tareas);

    public async Task<Alumno?> GetByLU(int lu) => await  BuildQuery(base._context.Alumno).AsNoTracking().FirstOrDefaultAsync(a => a.LU == lu);

    public override async Task<PaginadoResult<Alumno>> GetAll(int pagina, int tamanioPagina, AlumnoFiltro? filtro = null)
    {
        var query = BuildQuery(_context.Alumno);
        if (filtro != null)
        {
            if (filtro.NotaMinima.HasValue)
                query = query.Where(a => a.Nota >= filtro.NotaMinima.Value);
            if (filtro.NotaMaxima.HasValue)
                query = query.Where(a => a.Nota <= filtro.NotaMaxima.Value);
            if (!string.IsNullOrEmpty(filtro.Nombre))
                query = query.Where(a => a.Nombre!.Contains(filtro.Nombre));
        }
        var total = await query.CountAsync();
        var datos = await query.Skip((pagina - 1) * tamanioPagina).Take(tamanioPagina).ToListAsync();
        return new PaginadoResult<Alumno>
        {
            TotalRegistros = total,
            PaginaActual = pagina,
            TamanioPagina = tamanioPagina,
            Datos = datos
        };
    }
}