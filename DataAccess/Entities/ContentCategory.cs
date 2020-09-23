using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dytsenayasar.DataAccess.Entities
{
    public class ContentCategory : AEntity
    {
        public Guid ContentId { get; set; }
        public Content Content { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class ContentCategoryEntityConfiguration : EntityConfiguration<ContentCategory>
    {
        public ContentCategoryEntityConfiguration() : base("content_category")
        {
        }

        public override void Configure(EntityTypeBuilder<ContentCategory> builder)
        {
            base.Configure(builder);

            builder.HasOne<Content>(cc => cc.Content)
                .WithMany(c => c.ContentCategories)
                .HasForeignKey(cc => cc.ContentId);
            builder.HasOne<Category>(cc => cc.Category)
                .WithMany(c => c.ContentCategories)
                .HasForeignKey(cc => cc.CategoryId);

            builder.Property(cc => cc.ContentId)
                .HasColumnName("content_id");
            builder.Property(cc => cc.CategoryId)
                .HasColumnName("category_id");
        }
    }
}