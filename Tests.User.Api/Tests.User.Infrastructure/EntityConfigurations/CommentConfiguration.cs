
namespace Tests.User.Infrastructure.EntityConfigurations;

internal sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.Property(u => u.Title).HasMaxLength(255);
        builder.Property(u => u.Content).HasMaxLength(4096);

        builder
           .HasOne<Book>(c => c.Book)
           .WithMany(s => s.Comments);

        builder
           .HasOne<Domain.Entities.User>(c => c.Author);
    }
}
