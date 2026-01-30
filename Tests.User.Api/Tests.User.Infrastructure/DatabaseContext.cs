namespace Tests.User.Infrastructure;

internal class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Must stay as in memory database
        optionsBuilder.UseInMemoryDatabase("Tests.User.Api");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
           .Entity<Tests.User.Domain.Entities.User>()
           .ToTable("Users");

        modelBuilder
           .Entity<Tests.User.Domain.Entities.User>()
           .Property(u => u.FirstName).HasMaxLength(255);

        modelBuilder
           .Entity<Tests.User.Domain.Entities.User>()
           .Property(u => u.LastName).HasMaxLength(255);
    }
}