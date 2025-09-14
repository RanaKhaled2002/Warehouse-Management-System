using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseDAL.Models;

namespace WarehouseDAL.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(I => I.Id);

            builder.Property(I => I.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(I => I.BarCode)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(I => I.BarCode)
                   .IsUnique();
        }
    }
}
