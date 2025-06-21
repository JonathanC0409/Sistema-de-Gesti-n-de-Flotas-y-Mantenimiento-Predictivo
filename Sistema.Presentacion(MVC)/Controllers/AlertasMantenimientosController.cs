using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema.API.Consume;
using SistemaLogs.Modelos;

namespace Sistema.Presentacion_MVC_.Controllers
{
    public class AlertasMantenimientosController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var datos = CRUD<AlertaMantenimiento>.GetAll();
            return View(datos);
        }

        public IActionResult MantenimietosPendientes()
        {
            var mantenimientosPendientes = CRUD<AlertaMantenimiento>.GetAll("Pendiente");
            return View(mantenimientosPendientes);
        }
         
        
    }
}
