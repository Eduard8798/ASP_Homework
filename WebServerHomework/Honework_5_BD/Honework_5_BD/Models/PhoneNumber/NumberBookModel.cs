using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Honework_5_BD.Models.PhoneNumber;

public class NumberBookModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int Age { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Phone { get; set; }
    
    
}
