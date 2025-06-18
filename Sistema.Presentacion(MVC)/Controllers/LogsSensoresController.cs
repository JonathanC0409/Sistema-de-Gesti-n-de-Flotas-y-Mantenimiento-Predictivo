using Microsoft.AspNetCore.Mvc;
using Sistema.API.Consume;
using SistemaLogs.Modelos;

namespace Sistema.Presentacion_MVC_.Controllers
{
    public class LogsSensoresController : Controller
    {
        public IActionResult Index()
        {
            var datos = CRUD<LogSensor>.GetAll();
            return View(datos);
        }
    }
}
