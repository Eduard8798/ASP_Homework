using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_7.Models;

public class StreetModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } // Название улицы

    public int CityModelId { get; set; } // Внешний ключ для CityModel
    public CityModel CityModel { get; set; } // Навигационное свойство для связи с CityModel
}
