using Ejemplo_EF_Avanzado2.Data.Utils;
using Ejemplo_EF_Avanzado2.Data.Entities;

namespace Ejemplo_EF_Avanzado2.Services.Interfaces.Repositories;

public interface ITareaRepository: IGenericRepository<Tarea, int, TareaFiltro> { }