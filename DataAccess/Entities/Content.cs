using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dytsenayasar.DataAccess.Entities
{

    public class Content : AEntity
    {
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid UserId { get; set; }
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

            builder.Property(e => e.UserId)
                   .HasColumnName("user_id")
                   .IsRequired();
            builder.HasOne(e => e.User)
                   .WithMany(e => e.Contents)
                   .HasForeignKey(e => e.UserId);

            builder.Property(c => c.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(128)")
                .IsRequired();
            builder.Property(c => c.FileType)
                .HasColumnName("file_type")
                .HasColumnType("varchar(255)");
            builder.Property(c => c.CreatedOn)
                .HasColumnName("created_on");
            builder.Property(c => c.DataFiles)
                .HasColumnName("data_files");
        }
    }
}