using Microsoft.AspNetCore.Authentication.Cookies;
using OfficeOpenXml;
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
            CRUD<LecturaSensor>.EndPoint = "https://localhost:7220/api/LecturasSensores";
            CRUD<LogAuditoria>.EndPoint = "https://localhost:7220/api/LogsAuditorias";
            CRUD<AlertaMantenimiento>.EndPoint = "https://localhost:7220/api/AlertasMantenimientos";

         
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.LoginPath = "/Login/Index"; // Ruta para redirigir cuando no esté autenticado
               options.LogoutPath = "/Home/Logout"; // Ruta para logout
               options.SlidingExpiration = true; // Renueva la cookie si el usuario está activo
           });
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

            //app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
