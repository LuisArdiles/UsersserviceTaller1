public class Admin
{

        public Guid Id { get; set; } 
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Rol { get; set; } = "Admin";

}