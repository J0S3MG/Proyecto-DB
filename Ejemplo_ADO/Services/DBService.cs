using Ejemplo_ADO.Models;
using Ejemplo_ADO.Services.interfaces;

namespace Ejemplo_ADO.Services;

public class DBService: IDBService
{
    private readonly IAlumnoDAO _alumnos;

    public DBService(IAlumnoDAO alumnos)
    {
        _alumnos = alumnos;
    }


    public async Task<Alumno> Insert(Alumno a)
    {
        return await _alumnos.Insert(a);
    }


    public async Task<List<Alumno>> GetAll()
    {
        return await _alumnos.GetAll();
    }


    public async Task<Alumno?> GetById(int id)
    {
        return await _alumnos.GetById(id);
    }


    public async Task<bool> Update(Alumno a)
    {
        return await _alumnos.Update(a);
    }


    public async Task<bool> Delete(int id)
    {
        return await _alumnos.Delete(id);
    }
}