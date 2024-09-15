using System.ComponentModel.DataAnnotations.Schema;

namespace Homerwork_7_dolly_17_08.Models;

public class DollyClothesModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Url { get; set; }
    
    public string Style { get; set; }
    
   
}