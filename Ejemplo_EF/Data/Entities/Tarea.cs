using System.Text.Json.Serialization;

namespace Ejemplo_EF.Data.Entities;

public class Tarea
{
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public string? Descripcion { get; set; }
    public DateTime FechaEntrega { get; set; }
    public bool Entregada { get; set; }

    public int AlumnoId { get; set; }
    [JsonIgnore]
    public Alumno? Alumno { get; set; }
}