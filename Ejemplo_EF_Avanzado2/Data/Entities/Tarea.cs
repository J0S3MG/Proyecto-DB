using System.Text.Json.Serialization;

namespace Ejemplo_EF_Avanzado2.Data.Entities;

public class Tarea: BaseEntity
{
    public string? Titulo { get; set; }
    public string? Descripcion { get; set; }
    public DateOnly FechaEntrega { get; set; } 
    public bool Entregada { get; set; }
    public int AlumnoId { get; set; }

    [JsonIgnore]
    public Alumno? Alumno { get; set; }
}