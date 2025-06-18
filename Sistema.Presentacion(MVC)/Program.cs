using Sistema.API.Consume;
using SistemaGFYMP.Modelos;
using SistemaLogs.Modelos;

namespace Sistema.Presentacion_MVC_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CRUD<Camion>.EndPoint = "https://localhost:7240/api/Camiones";
            CRUD<Conductor>.EndPoint = "https://localhost:7240/api/Conductores";
            CRUD<MantenimientoProgramado>.EndPoint = "https://localhost:7240/api/MantenimientosProgramados";
            CRUD<Taller>.EndPoint = "https://localhost:7240/api/Talleres";
            CRUD<Usuario>.EndPoint = "https://localhost:7240/api/Usuarios";
            CRUD<AlertaPredictiva>.EndPoint = "https://localhost:7220/api/AlertasPredictivas";
            CRUD<LogSensor>.EndPoint = "https://localhost:7220/api/LogsSensores";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
