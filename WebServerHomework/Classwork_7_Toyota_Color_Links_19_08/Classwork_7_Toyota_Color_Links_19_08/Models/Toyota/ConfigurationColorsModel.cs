using System.ComponentModel.DataAnnotations.Schema;

namespace Classwork_7_Toyota_Color_Links_19_08.Models;

public class ConfigurationColorsModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string MainImageUrl { get; set; }
    
    public int ColorId { get; set; }
    
    [ForeignKey("ColorId")] 
    
    public ColorModel Color { get; set; }
    
    public int ConfigurationId { get; set; }

    [ForeignKey("ConfigurationId")] 
    
    
    public ConfigurationModel Configuration { get; set; }

}
