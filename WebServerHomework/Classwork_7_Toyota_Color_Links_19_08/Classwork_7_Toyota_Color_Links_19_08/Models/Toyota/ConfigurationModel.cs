using System.ComponentModel.DataAnnotations.Schema;

namespace Classwork_7_Toyota_Color_Links_19_08.Models;

public class ConfigurationModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int ModelId { get; set; }
    
    [ForeignKey("ModelId")]
    public ToyotaModel Model { get; set; }

    public List<ConfigurationColorsModel> Colors { get; set; } = new();
}