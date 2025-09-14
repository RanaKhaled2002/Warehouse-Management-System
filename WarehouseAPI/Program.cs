
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WarehouseAPI.DataSeeding;
using WarehouseAPI.Middleware;
using WarehouseBLL.Interfaces;
using WarehouseBLL.Services;
using WarehouseDAL.Data;
using WarehouseDAL.Interfaces;

namespace WarehouseAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<WarehouseDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IPickingService, PickingService>();
            builder.Services.AddScoped<ITransferService, TransferService>();
            builder.Services.AddScoped<IBoxService, BoxService>();
            builder.Services.AddScoped<ILotService, LotService>();

            var app = builder.Build();

            #region Update Database
            using var scope = app.Services.CreateScope();

            var service = scope.ServiceProvider;

            var context = service.GetRequiredService<WarehouseDbContext>();

            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync();
                await DataSeeder.SeedAsync(service);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There Are Problems During Apply Migrations !!");
            }

            #endregion

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
