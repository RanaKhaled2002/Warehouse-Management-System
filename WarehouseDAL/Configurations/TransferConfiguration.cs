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
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.HasKey(T => T.Id);

            builder.Property(T => T.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.HasOne(T => T.Picking)
                   .WithMany(P => P.Transfers)
                   .HasForeignKey(T => T.PickingId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(T => T.SourceWarehouse)
                   .WithMany()
                   .HasForeignKey(T => T.SourceWarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(T => T.DestinationWarehouse)
                   .WithMany()
                   .HasForeignKey(T => T.DestinationWarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
