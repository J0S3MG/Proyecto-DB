using Ejemplo_EF_Avanzado2.Data;
using Ejemplo_EF_Avanzado2.Data.Utils;
using Ejemplo_EF_Avanzado2.Data.Entities;
using Ejemplo_EF_Avanzado2.Services.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ejemplo_EF_Avanzado2.Data.Repositories;

public class TareaRepository: GenericRepository<Tarea, int, TareaFiltro>, ITareaRepository
{
    public TareaRepository(EjemploEfContext context) : base(context) { }

    protected override IQueryable<Tarea> BuildQuery(IQueryable<Tarea> query) => query.Include(t => t.Alumno);

    public override async Task<PaginadoResult<Tarea>> GetAll(int pagina, int tamanioPagina, TareaFiltro? filtro = null)
    {
        var query = BuildQuery(_context.Set<Tarea>());
        if (filtro != null)
        {
            if (filtro.Entregada.HasValue)
                query = query.Where(t => t.Entregada == filtro.Entregada.Value);
            if (filtro.FechaDesde.HasValue)
                query = query.Where(t => t.FechaEntrega >= filtro.FechaDesde.Value);
            if (filtro.FechaHasta.HasValue)
                query = query.Where(t => t.FechaEntrega <= filtro.FechaHasta.Value);
            if (filtro.AlumnoId.HasValue)
                query = query.Where(t => t.AlumnoId == filtro.AlumnoId.Value);
        }
        var total = await query.CountAsync();
        var datos = await query.Skip((pagina - 1) * tamanioPagina).Take(tamanioPagina).ToListAsync();
        return new PaginadoResult<Tarea>
        {
            TotalRegistros = total,
            PaginaActual = pagina,
            TamanioPagina = tamanioPagina,
            Datos = datos
        };
    }
}

