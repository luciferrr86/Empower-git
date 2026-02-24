using A4.DAL.Entites.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace A4.DAL.EntityMap
{
    public class BlogMap : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title).HasMaxLength(250).IsRequired();
            builder.Property(b=>b.Content).IsRequired();
            builder.Property(b => b.ShortDescription).IsRequired();
            builder.Property(b=>b.PublishedDate).IsRequired();
            builder.Property(b=>b.IsPublished).HasDefaultValue(false);
            builder.ToTable("Blog");
        }
    }
}
