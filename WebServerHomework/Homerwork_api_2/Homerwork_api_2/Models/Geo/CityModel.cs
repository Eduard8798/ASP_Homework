namespace Homerwork_api_2.Models.Geo;
using System.ComponentModel.DataAnnotations.Schema;

public class CityModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int AreaId { get; set; }
    [ForeignKey("AreaId")]
    public AreaModel? Area { get; set; }
}