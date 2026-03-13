namespace Ejemplo_EF_Avanzado2.Data.Utils;

public class TareaFiltro
{
    public bool? Entregada { get; set; }
    public DateOnly? FechaDesde { get; set; }
    public DateOnly? FechaHasta { get; set; }
    public int? AlumnoId { get; set; }
}