using Microsoft.AspNetCore.Mvc;
using Sistema.API.Consume;
using SistemaLogs.Modelos;

namespace Sistema.Presentacion_MVC_.Controllers
{
    public class AlertasPredictivasController : Controller
    {
        public IActionResult Index()
        {
            var alertas = CRUD<AlertaPredictiva>.GetAll();
            return View(alertas);
        }
    }
}
