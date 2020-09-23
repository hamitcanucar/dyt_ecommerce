using System;
using dytsenayasar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dytsenayasar.DataAccess.Entities
{
    public enum DeliveryType { Optional, Mandatory }

    public class UserContent : AEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public DateTime ValidityDate { get; set; }

        public Guid ContentId { get; set; }
        public Content Content { get; set; }
    }


    public class UserContentEntityConfiguration : EntityConfiguration<UserContent>
    {
        public UserContentEntityConfiguration() : base("user_content")
        {
        }

        public override void Configure(EntityTypeBuilder<UserContent> builder)
        {
            base.Configure(builder);

            builder.HasIndex(uc => uc.DeliveryType)
                .HasMethod("hash");

            builder.HasOne<User>(uc => uc.User)
                .WithMany(u => u.UserContents)
                .HasForeignKey(uc => uc.UserId);
            builder.HasOne<Content>(uc => uc.Content)
                .WithMany(c => c.UserContents)
                .HasForeignKey(uc => uc.ContentId);

            builder.Property(c => c.ValidityDate)
                .HasColumnName("validity_date");
            builder.Property(uc => uc.UserId)
                .HasColumnName("user_id");
            builder.Property(uc => uc.ContentId)
                .HasColumnName("content_id");
            builder.Property(uc => uc.DeliveryType)
                .HasColumnName("delivery_type")
                .HasColumnType("varchar(16)")
                .HasDefaultValue(DeliveryType.Optional)
                .HasConversion(
                    d => d.ToString(),
                    d => (DeliveryType)Enum.Parse(typeof(DeliveryType), d)
                );
        }
    }
}