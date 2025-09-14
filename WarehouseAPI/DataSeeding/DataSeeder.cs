using WarehouseDAL.Data;
using WarehouseDAL.Models;

namespace WarehouseAPI.DataSeeding
{
    public class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();

            if (!context.Warehouses.Any())
            {
                context.Warehouses.AddRange(
                    new Warehouse {Name = "Main Warehouse", Location = "Cairo" },
                    new Warehouse {Name = "Alex Warehouse", Location = "Alexandria" },
                    new Warehouse {Name = "Aswan Warehouse", Location = "Aswan" },
                    new Warehouse {Name = "Suez Warehouse", Location = "Suez" },
                    new Warehouse {Name = "Port Said Warehouse", Location = "Port Said" }
                );

                await context.SaveChangesAsync();
            }

            if (!context.Items.Any())
            {
                context.Items.AddRange(
                    new Item {Name = "Phone", BarCode = "123456789" },
                    new Item {Name = "TV", BarCode = "987654321" },
                    new Item {Name = "Laptop", BarCode = "876543219" },
                    new Item {Name = "Headphone", BarCode = "765432198" },
                    new Item {Name = "Microphone", BarCode = "654321987" }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
