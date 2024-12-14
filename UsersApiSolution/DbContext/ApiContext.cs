using Microsoft.EntityFrameworkCore;
using UsersApiSolution.Models;

namespace UsersApiSolution;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "UsersDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasData(
           new User { Id = Guid.NewGuid(), Nombre = "Juan Pérez", Telefono = "123456789" },
           new User { Id = Guid.NewGuid(), Nombre = "Ana Gómez", Telefono = "987654321" }
        );
    }

    public DbSet<User> Users { get; set; }

}
