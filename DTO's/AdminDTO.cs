using System.ComponentModel.DataAnnotations;
public class AdminDTO
{
    [Required]
    public Guid? Id { get; set; } 
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!; 
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string Rol { get; set; } = "Admin";


}