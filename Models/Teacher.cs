public class Teacher
{

        public Guid Id { get; set; } 
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = "Teacher";

}