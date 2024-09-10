using System.ComponentModel.DataAnnotations.Schema;

namespace Homeworl_7_Toyota_Color.Models.Toyota;

public class ColorModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Url { get; set; }
    
    public string RGB { get; set; }
    
    public string Code { get; set; }
    
}