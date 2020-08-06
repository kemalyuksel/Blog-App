using Blog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mapping
{
    public class TopicMap : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ShortDescription).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Description).HasColumnType("ntext");
            builder.Property(x => x.ImagePath).HasMaxLength(300);

            builder.HasMany(x => x.Comments).WithOne(x => x.Topic).HasForeignKey(x => x.TopicId);
            builder.HasMany(x => x.CategoryTopics).WithOne(x => x.Topic).HasForeignKey(x => x.TopicId);

        }
    }
}
