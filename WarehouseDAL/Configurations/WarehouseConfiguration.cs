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
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(W => W.Id);

            builder.Property(W => W.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(W => W.Location)
                   .IsRequired()
                   .HasMaxLength(200);
        }
    }
}
