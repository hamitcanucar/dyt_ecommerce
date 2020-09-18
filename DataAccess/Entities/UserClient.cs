using System;
using dyt_ecommerce.Util;
using dytsenayasar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dyt_ecommerce.DataAccess.Entities
{
    public class UserClient : AEntity
    {
        private string clientId;

        public Guid UserId { get; set; }
        public User User { get; set; }

        public byte[] ClientIdHash { get; set; }
        public string ClientId
        {
            get { return clientId; }
            set
            {
                clientId = value;
                ClientIdHash = clientId.HashToSha1AsByte();
            }
        }

        public class UserClientEntityConfiguration : EntityConfiguration<UserClient>
        {
            public UserClientEntityConfiguration() : base("user_client")
            {
            }

            public override void Configure(EntityTypeBuilder<UserClient> builder)
            {
                base.Configure(builder);

                builder.HasIndex(uc => uc.ClientIdHash)
                    .HasMethod("hash");
                builder.HasOne<User>(uc => uc.User)
                    .WithOne(u => u.Client)
                    .HasForeignKey<UserClient>(uc => uc.UserId);

                builder.Property(uc => uc.UserId)
                    .HasColumnName("user_id");
                builder.Property(uc => uc.ClientId)
                    .HasColumnName("clientid");
                builder.Property(uc => uc.ClientIdHash)
                    .HasColumnName("clientid_hash");
            }
        }
    }
}