using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options) { }

    public DbSet<UserModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.ToTable("UsersMCP");
            entity.HasKey("UserId");
            entity.Property("UserId").ValueGeneratedOnAdd().IsRequired();
        });
    } 
}
