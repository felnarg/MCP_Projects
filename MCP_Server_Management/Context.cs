using MCP_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace MCP_Management;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { } 

    public DbSet<McpServerModel> Servers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<McpServerModel>(entity =>
        {
            entity.ToTable("McpServers");
            entity.HasKey(s => s.IdModel);
            entity.Property(s => s.IdModel).ValueGeneratedOnAdd();
        });
    } 
}
