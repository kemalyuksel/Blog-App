using Blog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mapping
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Description).HasMaxLength(300).IsRequired();
            builder.Property(x => x.AuthorName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.AuthorEmail).HasMaxLength(100).IsRequired();


            builder.HasOne(x => x.Topic).WithMany(x => x.Comments).HasForeignKey(x => x.TopicId);

            builder.HasMany(x => x.SubComments).WithOne(x => x.ParentComment).HasForeignKey(x => x.ParentCommentId);
        }
    }
}
