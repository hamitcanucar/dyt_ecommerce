using System;
using dytsenayasar.Util;
using dytsenayasar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dytsenayasar.DataAccess.Entities
{
    public class UserInformation : AEntity
    {
        public string PersonalId { get; set; }
        public DateTime BirthDay { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public GenderType Gender { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }



        public class UserInformationEntityConfiguration : EntityConfiguration<UserInformation>
        {
            public UserInformationEntityConfiguration() : base("user_information")
            {
            }

            public override void Configure(EntityTypeBuilder<UserInformation> builder)
            {
                base.Configure(builder);

                builder.HasIndex(u => u.Gender)
                .HasMethod("hash");

                builder.Property(u => u.PersonalId)
                    .HasColumnName("personal_id")
                    .HasColumnType("varchar(64)")
                    .IsRequired();
                builder.Property(u => u.BirthDay)
                    .HasColumnName("birth_day");
                builder.Property(u => u.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(32)");
                builder.Property(u => u.Gender)
                    .HasColumnName("gender")
                    .HasColumnType("varchar(16)")
                    .HasDefaultValue(GenderType.Male)
                    .HasConversion(
                      g => g.ToString(),
                      g => (GenderType)Enum.Parse(typeof(GenderType), g)
                     );

                builder.HasOne<User>(uc => uc.User)
                    .WithOne(u => u.Information)
                    .HasForeignKey<UserInformation>(uc => uc.UserId);

                builder.Property(uc => uc.UserId);
            }
        }
    }
}