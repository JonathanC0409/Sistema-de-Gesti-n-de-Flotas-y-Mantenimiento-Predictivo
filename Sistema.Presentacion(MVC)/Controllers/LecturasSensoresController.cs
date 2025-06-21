using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema.API.Consume;
using SistemaLogs.Modelos;

namespace Sistema.Presentacion_MVC_.Controllers
{
    public class LecturasSensoresController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var datos = CRUD<LecturaSensor>.GetAll();
            return View(datos);
        }
    }
}
