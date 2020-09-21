using System;
using System.Collections.Generic;
using dytsenayasar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dyt_ecommerce.DataAccess.Entities
{
    public class Content : AEntity, IEntityWithImage, IEntityWithFile
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AgeLimit { get; set; }
        public double BasePrice { get; set; }
        public Guid? Image { get; set; }
        public Guid? File { get; set; }
        public DateTime ValidityDate { get; set; }
        
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
        public ICollection<UserContent> UserContents { get; set; }
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
            builder.Property(c => c.AgeLimit)
                .HasColumnName("age_limit")
                .HasDefaultValue(0);
            builder.Property(c => c.BasePrice)
                .HasColumnName("base_price")
                .HasDefaultValue(0);
            builder.Property(c => c.Image)
                .HasColumnName("image");
            builder.Property(c => c.ValidityDate)
                .HasColumnName("validity_date");
            builder.Property(c => c.CreatorId)
                .HasColumnName("creator_id");
        }
    }
}