using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dytsenayasar.DataAccess.Entities
{

    public class UserFile : AEntity
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }

    public class UserFileEntityConfiguration : EntityConfiguration<UserFile>
    {
        public UserFileEntityConfiguration() : base("user_file")
        {
        }

        public override void Configure(EntityTypeBuilder<UserFile> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.UserId)
                   .HasColumnName("user_id")
                   .IsRequired();
            builder.HasOne(e => e.User)
                   .WithMany(e => e.UserFiles)
                   .HasForeignKey(e => e.UserId);

            builder.Property(c => c.FileName)
                .HasColumnName("file_name")
                .HasColumnType("varchar(128)")
                .IsRequired();
            builder.Property(c => c.FileType)
                .HasColumnName("file_type")
                .HasColumnType("varchar(255)");
            builder.Property(c => c.CreatedOn)
                .HasColumnName("created_on");
        }
    }
}