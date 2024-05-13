using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.DataSeeding;
using Talabat.Repository.Identity;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Allow DI For DbContext
            builder.Services.AddDbContext<StoreDbContext>(Options =>
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Allow DI For IdentityDbContext
            builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))
            );

            // Add AppplicationServicesExtensions
            builder.Services.AddApplicationServices();

            //Add Configurations To Identity
            builder.Services.ApplicationIdentityServices(builder.Configuration);

            //Allow DI For Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            var app = builder.Build();
            // Update Database
             var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            // Allow CLR Generete object Explicity for StoreDbContext
            var _dbContext = services.GetRequiredService<StoreDbContext>();
            // Allow CLR Generete object Explicity for AppIdentityDbContext
            var _identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
            // Allow CLR Generete object Explicity for UserManager<AppUser>>
            var _userManager = services.GetRequiredService<UserManager<AppUser>>();

            // Craete Object From ILogger
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();
                await _identityDbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbContext);
                await AppIdentityDbContextSeed.SeedUserAsync(_userManager);
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error has been occured during apply migration ");
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleWare>();
                //Add Swagger Extensions
                app.AddSwaggerExtension();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
