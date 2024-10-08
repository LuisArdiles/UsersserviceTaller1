using System.ComponentModel.DataAnnotations;
public class StudentDTO
{
    [Required]
    public Guid? Id { get; set; } 
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!; 

}