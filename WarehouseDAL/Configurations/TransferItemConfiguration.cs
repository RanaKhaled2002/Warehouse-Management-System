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
    public class TransferItemConfiguration : IEntityTypeConfiguration<TransferItem>
    {
        public void Configure(EntityTypeBuilder<TransferItem> builder)
        {
            builder.HasKey(TI => TI.Id);

            builder.Property(TI => TI.RequestedQty)
                  .IsRequired();

            builder.Property(TI => TI.ScannedQty)
                   .IsRequired();

            builder.HasOne(TI => TI.Transfer)
                   .WithMany(T => T.TransferItems)
                   .HasForeignKey(TI => TI.TransferId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(TI => TI.Item)
                   .WithMany(I => I.TransferItems)
                   .HasForeignKey(TI => TI.ItemId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
