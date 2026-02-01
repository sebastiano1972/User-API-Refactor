
namespace Tests.User.Infrastructure.EntityConfigurations;

internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");
        builder.Property(u => u.Title).HasMaxLength(255);
        builder.Property(u => u.Author).HasMaxLength(255);
    }
}
