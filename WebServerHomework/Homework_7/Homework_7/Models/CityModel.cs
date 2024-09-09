using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_7.Models;

public class CityModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    public int AreaModelId { get; set; } // Внешний ключ для AreaModel
    public AreaModel AreaModel { get; set; } // Навигационное свойство для связи с AreaModel

    // Связь многие ко многим с CityTypeModel
    public ICollection<CityTypeModel> CityTypes { get; set; }

    // Связь один ко многим с улицами
    public ICollection<StreetModel> Streets { get; set; }
}

