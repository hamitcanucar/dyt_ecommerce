using System;
using dytsenayasar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dytsenayasar.DataAccess.Entities
{
    public enum UserRequestType { PasswordReset, ActivateAccount }
    public class UserRequest : AEntity
    {
        public DateTime ValidityDate { get; set; }
        public UserRequestType RequestType { get; set; }
        public string Token { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }

    public class UserRequestEntityConfiguration : EntityConfiguration<UserRequest>
    {
        public UserRequestEntityConfiguration() : base("user_request")
        {
        }

        public override void Configure(EntityTypeBuilder<UserRequest> builder)
        {
            base.Configure(builder);

            builder.HasIndex(ur => ur.RequestType)
                .HasMethod("hash");
            builder.HasOne<User>(ur => ur.User)
                .WithMany(u => u.Requests)
                .HasForeignKey(ur => ur.UserId);

            builder.Property(ur => ur.ValidityDate)
                .HasColumnName("validity_date");
            builder.Property(ur => ur.Token)
                .HasColumnName("token")
                .HasColumnType("varchar(64)");
            builder.Property(ur => ur.UserId)
                .HasColumnName("user_id");
            builder.Property(ur => ur.RequestType)
                .HasColumnName("request_type")
                .HasColumnType("varchar(16)")
                .HasDefaultValue(UserRequestType.PasswordReset)
                .HasConversion(
                    rt => rt.ToString(),
                    rt => (UserRequestType)Enum.Parse(typeof(UserRequestType), rt)
                );
        }
    }
}