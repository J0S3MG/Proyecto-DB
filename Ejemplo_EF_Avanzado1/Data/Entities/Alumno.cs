namespace Ejemplo_EF_Avanzado1.Data.Entities;

public class Alumno: BaseEntity
{
    public int LU {  get; set; }
    public string? Nombre { get; set; }
    public decimal Nota { get; set; }
    public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}