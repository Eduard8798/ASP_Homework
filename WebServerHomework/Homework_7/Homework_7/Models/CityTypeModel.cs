using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_7.Models;

public class CityTypeModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Type { get; set; } // Тип города, например, "Город" или "Деревня"

    // Связь многие ко многим с CityModel
    public ICollection<CityModel> Cities { get; set; }
}

