using Ejemplo_EF_Avanzado2.Data.Utils;
using Ejemplo_EF_Avanzado2.Data.Entities;
using Ejemplo_EF_Avanzado2.Services.Interfaces;
using Ejemplo_EF_Avanzado2.Services.Interfaces.Repositories;

namespace Ejemplo_EF_Avanzado2.Services;

public class AlumnoService : IAlumnoService
{
    private readonly IEjemploUoW _uow;
    public AlumnoService(IEjemploUoW uow)
    {
        _uow = uow;
    }

    #region Insertar Alumno.
    public async Task<Alumno> Insert(Alumno a)
    {
        a.Id = 0;
        if (a.Nota < 0 || a.Nota > 10) throw new Exception("La nota debe estar entre 0 y 10.");
        var resultado = await _uow.Alumnos.GetByLU(a.LU);
        if (resultado != null) throw new Exception($"Ya existe un alumno con el LU {a.LU}.");
        Alumno alu = await _uow.Alumnos.Insert(a);
        await _uow.SaveAsync();
        return alu;
    }
    #endregion

    #region Devolver Alumnos.
     public async Task<PaginadoResult<Alumno>> GetAll(int pagina = 1, int tamanioPagina = 10, AlumnoFiltro? filtro = null)
    {
        if (pagina < 1) throw new Exception("La página debe ser mayor a 0.");
        if (tamanioPagina < 1 || tamanioPagina > 50) throw new Exception("El tamaño de página debe estar entre 1 y 50.");
        return await _uow.Alumnos.GetAll(pagina, tamanioPagina, filtro);
    }
    #endregion

    #region Devolver un Alumno.
    public async Task<Alumno?> GetById(int id) => await _uow.Alumnos.GetById(id);
    #endregion

    #region Actualizar Alumno.
    public async Task Update(Alumno a)
    {
        var existe = await _uow.Alumnos.GetById(a.Id);
        if (existe is null) throw new Exception($"No existe un alumno con el Id {a.Id}.");
        if (a.Nota < 0 || a.Nota > 10) throw new Exception("La nota debe estar entre 0 y 10.");
        _uow.Alumnos.Update(a);
        await _uow.SaveAsync();
    }
    #endregion

    #region Eliminar Alumno.
    public async Task<bool> SoftDelete(int id)
    {
        var existe = await _uow.Alumnos.GetById(id);
        if (existe is null) throw new Exception($"No existe un alumno con el Id {id}.");
        await _uow.BeginTransactionAsync();
        try
        {
            foreach (var tarea in existe.Tareas) await _uow.Tareas.SoftDelete(tarea.Id);
            var result = await _uow.Alumnos.SoftDelete(id);
            await _uow.SaveAsync();
            await _uow.CommitAsync();
            return result;
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> HardDelete(int id)
    {
        var existe = await _uow.Alumnos.GetById(id);
        if (existe is null) throw new Exception($"No existe un alumno con el Id {id}.");
        var result = await _uow.Alumnos.HardDelete(id);
        await _uow.SaveAsync();
        return result;
    }
    #endregion
    
    #region Restaurar Alumno.
    public async Task Restore(int id)
    {
        await _uow.Alumnos.Restore(id);
        await _uow.SaveAsync();
    }
    #endregion
}