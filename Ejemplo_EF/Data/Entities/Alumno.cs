namespace Ejemplo_EF.Data.Entities;

public class Alumno
{
    public int Id { get; set; }
    public int LU {  get; set; }
    public string? Nombre { get; set; }
    public decimal Nota { get; set; }
    public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}