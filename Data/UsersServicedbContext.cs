using Microsoft.EntityFrameworkCore;

public class UsersServicedbContext : DbContext
{
    public UsersServicedbContext(DbContextOptions<UsersServicedbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    public DbSet<Admin> Admins { get; set; }
}