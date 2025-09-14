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
    public class BoxConfiguration : IEntityTypeConfiguration<Box>
    {
        public void Configure(EntityTypeBuilder<Box> builder)
        {
            builder.HasKey(B => B.Id);

            builder.Property(B => B.Label)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(B => B.Label)
                   .IsUnique();

            builder.HasOne(B => B.Transfer)
                   .WithMany()
                   .HasForeignKey(B => B.TransferId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(B => B.Lot)
                   .WithMany(L => L.Boxes)
                   .HasForeignKey(B => B.LotId)
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
