namespace Ejemplo_EF_Avanzado1.Data.Utils;

public class PaginadoResult<T>
{
    public int TotalRegistros { get; set; }
    public int PaginaActual { get; set; }
    public int TamanioPagina { get; set; }
    public int TotalPaginas => (int)Math.Ceiling((double)TotalRegistros / TamanioPagina);
    public List<T> Datos { get; set; } = new List<T>();
}