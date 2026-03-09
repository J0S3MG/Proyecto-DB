using Ejemplo_ADO.Services.interfaces;
using Ejemplo_ADO.Models;
using Npgsql;

namespace Ejemplo_ADO.DAOs;

public class AlumnoDAO: IAlumnoDAO
{
    private readonly string _context;
    public AlumnoDAO(string onnectionString)
    {
        _context = onnectionString;
    }


    #region Caso Insert.
    public async Task<Alumno?> Insert(Alumno nuevo)
    {
        Alumno? respuesta = null;
        // SQL para PostgreSQL.
        string sqlQuery = "INSERT INTO alumno(nombre, lu, nota) VALUES (@nombre, @lu, @nota) RETURNING Id";
        using var conexion = new NpgsqlConnection(_context); // Nos conectamos a la db.
        await conexion.OpenAsync(); // Abrimos la conexion.

        using var query = new NpgsqlCommand(sqlQuery, conexion); // Mandamos la query.
        // Reemplazamos las variables con los datos del obj.
        query.Parameters.AddWithValue("@nombre", nuevo.Nombre); 
        query.Parameters.AddWithValue("@lu", nuevo.LU);
        query.Parameters.AddWithValue("@nota", nuevo.Nota);

        // ExecuteScalar obtiene el ID que retorna el "RETURNING Id".
        var idResultado = await query.ExecuteScalarAsync();
        if (idResultado != null)
        {   
            var id = Convert.ToInt32(idResultado);
            respuesta = await GetById(id);
            return respuesta;
        }
        return respuesta;
    }
    #endregion


    #region Caso GetAll.
    public async Task<List<Alumno>> GetAll()
    {
        var lista = new List<Alumno>();
        string sqlQuery = "SELECT a.* FROM alumno a"; // Armamos la query.
        using var conexion = new NpgsqlConnection(_context); // Nos conectamos a la db.
        await conexion.OpenAsync(); // Abrimos la conexion.
        // Ejecutamos la query.
        using var query = new NpgsqlCommand(sqlQuery, conexion);
        var reader = await query.ExecuteReaderAsync(); // Obtenemos la respuesta.
        while (await reader.ReadAsync())
        { // Vamos fila a fila transformando cada una en un obj.
            var objeto = LeerUnaFila(reader);
            lista.Add(objeto); // Lo agregamos a la lista.
        }
        return lista;
    }
    #endregion


    #region Caso GetById.
    public async Task<Alumno?> GetById(int id)
    {
        Alumno? objeto = null;
        string sqlQuery = "SELECT a.* FROM alumno a WHERE a.id = @id LIMIT 1";
        using var conexion = new NpgsqlConnection(_context); // Nos conectamos a la db.
        await conexion.OpenAsync(); // Abrimos la conexion.
        // Preparamos el comando con la consulta y la conexión abierta.
        using var query = new NpgsqlCommand(sqlQuery, conexion);
        query.Parameters.AddWithValue("@id", id);
        var reader = await query.ExecuteReaderAsync(); // Ejecutamos y obtenemos el lector (reader).
        if (await reader.ReadAsync())
        {  // Transformamos el resultado a obj.
            objeto = LeerUnaFila(reader);
        }
        return objeto;
    }
    #endregion


    #region Caso Update.
    public async Task<bool> Update(Alumno actualizar)
    {
        string sqlQuery = "UPDATE alumno SET nombre = @nombre, lu = @lu, nota = @nota WHERE id = @id";
        // Usamos 'using' para asegurar que la conexión se cierre al terminar.
        using var conexion = new NpgsqlConnection(_context); 
        await conexion.OpenAsync(); 

        using var query = new NpgsqlCommand(sqlQuery, conexion);
        query.Parameters.AddWithValue("@nombre", actualizar.Nombre);
        query.Parameters.AddWithValue("@lu", actualizar.LU);
        query.Parameters.AddWithValue("@nota", actualizar.Nota);
        query.Parameters.AddWithValue("@id", actualizar.Id);

        // Ejecutamos y guardamos cuantas filas se afectaron.
        int filas = await query.ExecuteNonQueryAsync();
        return filas > 0;
    }
    #endregion


    #region Caso Delete.
    public async Task<bool> Delete(int id)
    {
        string sqlQuery = "DELETE FROM alumno WHERE id = @id";
        using var conexion = new NpgsqlConnection(_context); // Nos conectamos a la db.
        await conexion.OpenAsync(); // Abrimos la conexion.

        using var query = new NpgsqlCommand(sqlQuery, conexion);
        query.Parameters.AddWithValue("@id", id);

        int eliminados = await query.ExecuteNonQueryAsync();
        return eliminados > 0;
    }
    #endregion


    #region Transformar Object <-> Registro. 
    private Alumno LeerUnaFila(NpgsqlDataReader reader)
    { // Tomamos cada fila y leemos sus columnas. 
        int id = reader["id"] != DBNull.Value ? Convert.ToInt32(reader["id"]) : 0;
        int lu = reader["lu"] != DBNull.Value ? Convert.ToInt32(reader["lu"]) : 0;
        decimal nota = reader["nota"] != DBNull.Value ? Convert.ToDecimal(reader["nota"]) : 0;
        string? nombre = reader["nombre"] != DBNull.Value ? Convert.ToString(reader["nombre"]) : "";
        return new Alumno
        {
            Id = id,
            LU = lu,
            Nombre = nombre,
            Nota = nota
        };
    }
    #endregion
}