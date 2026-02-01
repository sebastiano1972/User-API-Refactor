
namespace Tests.User.Infrastructure.EntityConfigurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
    {
        builder.ToTable("Users");
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);

        builder
           .HasMany(u => u.BorrowedBooks)
           .WithMany(b => b.Users)
           .UsingEntity(b =>
                        {
                            b.ToTable("Borrowings");
                        });
    }
}
