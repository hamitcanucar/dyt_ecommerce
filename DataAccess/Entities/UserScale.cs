using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dytsenayasar.DataAccess.Entities
{
    public class UserScale : AEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public float Weigt { get; set; }
        public int Cheest { get; set; }
        public int Waist { get; set; }
        public int UpperArm { get; set; }
        public int Hip { get; set; }
        public int Leg { get; set; }
    }

    public class UserScaleClientEntityConfiguration : EntityConfiguration<UserScale>
    {
        public UserScaleClientEntityConfiguration() : base("user_scale")
        {
        }

        public override void Configure(EntityTypeBuilder<UserScale> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.UserId)
               .HasColumnName("user_id")
               .IsRequired();
            builder.HasOne(e => e.User)
                   .WithMany(e => e.UserScales)
                   .HasForeignKey(e => e.UserId);
            builder.Property(e => e.Hip)
                .HasColumnName("hip");
            builder.Property(e => e.Leg)
                .HasColumnName("leg");
            builder.Property(e => e.UpperArm)
                .HasColumnName("upper_arm");
            builder.Property(e => e.Waist)
                .HasColumnName("waist");
            builder.Property(e => e.Weigt)
                .HasColumnName("weight");
        }
    }
}