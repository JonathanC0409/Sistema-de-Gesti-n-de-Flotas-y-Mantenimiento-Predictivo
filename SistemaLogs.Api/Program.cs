using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SistemaLogs.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<App2DbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));

            builder.Services.AddDbContext<App1DbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext.sql")));

            // Add services to the container.

            // Configurar CORS para permitir solicitudes desde el frontend
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    builder => builder.WithOrigins("https://localhost:7135")  // Frontend URL
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });


            builder.Services
                    .AddControllers()
                    .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                     );

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sistema Logs API", Version = "v1" });
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); // Resolver el conflicto
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Usar la política CORS configurada
            app.UseCors("AllowFrontend");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
