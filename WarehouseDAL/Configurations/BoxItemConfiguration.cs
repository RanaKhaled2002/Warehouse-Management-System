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
    public class BoxItemConfiguration : IEntityTypeConfiguration<BoxItem>
    {
        public void Configure(EntityTypeBuilder<BoxItem> builder)
        {
            builder.HasKey(BI => BI.Id);

            builder.Property(BI => BI.Quantity)
                   .IsRequired();

            builder.HasOne(BI => BI.Box)
                   .WithMany(B => B.BoxItems)
                   .HasForeignKey(BI => BI.BoxId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(BI => BI.Item)
                   .WithMany(I => I.BoxItems)
                   .HasForeignKey(BI => BI.ItemId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
