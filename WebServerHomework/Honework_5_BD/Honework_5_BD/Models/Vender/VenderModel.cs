using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Honework_5_BD.Models.Vender;

public class VenderModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    
    public int Id { get; set; }
    
    [Required]
    
    public string Name { get; set; }
    
    public string Contact { get; set; }
    
    public string Address { get; set; }
}