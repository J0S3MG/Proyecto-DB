using System.Text.Json.Serialization;

namespace Ejemplo_EF_Avanzado2.Data.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    [JsonIgnore]
    public bool IsDeleted { get; set; } = false;
    [JsonIgnore]
    public DateTime? DeletedAt { get; set; }
}