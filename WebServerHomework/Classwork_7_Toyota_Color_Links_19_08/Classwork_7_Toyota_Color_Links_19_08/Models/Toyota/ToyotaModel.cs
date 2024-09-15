using System.ComponentModel.DataAnnotations.Schema;

namespace Classwork_7_Toyota_Color_Links_19_08.Models;

public class ToyotaModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }

    public List<ConfigurationModel> Configurations { get; set; } = new();
}