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
    public class LotConfiguration : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.HasKey(L => L.Id);

            builder.Property(L => L.LotCode)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(L => L.LotCode)
                   .IsUnique();

            builder.Property(L => L.ShipmentStatus)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(L => L.LotStatus)
                   .IsRequired()
                   .HasConversion<string>();

            builder.HasMany(L => L.Boxes)
                   .WithOne(B => B.Lot)
                   .HasForeignKey(B => B.LotId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
