using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dytsenayasar.DataAccess.Entities
{
    public class Category
    { 
        public int ID { get; set; }
        public string Name_en { get; set; }
        public string Name_tr { get; set; }

        public ICollection<ContentCategory> ContentCategories { get; set; }
    }

    public class CategoryEntityConfigration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("category");
            builder.HasKey(e => e.ID);

            builder.Property(e => e.ID)
                .HasColumnName("id");
            builder.Property(e => e.Name_en)
                .HasColumnName("name_en")
                .HasColumnType("varchar(64)")
                .IsRequired();
            builder.Property(e => e.Name_tr)
                .HasColumnName("name_tr")
                .HasColumnType("varchar(64)")
                .IsRequired();
        }
    }
}