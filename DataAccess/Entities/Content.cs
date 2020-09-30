using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dytsenayasar.DataAccess.Entities
{
    public enum ContentTypes { Any, Pdf, Epub, Png, Mp4, Doc, Docx }

    public class Content : AEntity, IEntityWithImage, IEntityWithFile
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? Image { get; set; }
        public Guid? File { get; set; }
        public DateTime UploadDate { get; set; }
        public ContentTypes ContentType { get; set; }
        
        public User User { get; set; }
    }

    public class ContentEntityConfiguration : EntityConfiguration<Content>
    {
        public ContentEntityConfiguration() : base("content")
        {
        }

        public override void Configure(EntityTypeBuilder<Content> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Title)
                .HasColumnName("title")
                .HasColumnType("varchar(128)")
                .IsRequired();
            builder.Property(c => c.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(255)");
            builder.Property(c => c.Image)
                .HasColumnName("image");
            builder.Property(c => c.UploadDate)
                .HasColumnName("validity_date");
            builder.Property(c => c.ContentType)
                .HasColumnName("content_type");
            builder.Property(c => c.File)
                .HasColumnName("file");
        }
    }
}