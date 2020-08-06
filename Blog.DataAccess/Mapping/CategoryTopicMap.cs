using Blog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mapping
{
    public class CategoryTopicMap : IEntityTypeConfiguration<CategoryTopic>
    {
        public void Configure(EntityTypeBuilder<CategoryTopic> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasIndex(x => new { x.TopicId, x.CategoryId }).IsUnique();
        }
    }
}
