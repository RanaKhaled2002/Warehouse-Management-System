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
    public class PickingConfiguration : IEntityTypeConfiguration<Picking>
    {
        public void Configure(EntityTypeBuilder<Picking> builder)
        {
            builder.HasKey(P => P.Id);

            builder.Property(P => P.Description)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(P => P.PickingDate)
                   .IsRequired();

            builder.Property(P => P.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.HasOne(P => P.Warehouse)
                   .WithMany()
                   .HasForeignKey(P => P.WarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
