using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.API.Consume;
using SistemaGFYMP.Modelos;

namespace Sistema.Presentacion_MVC_.Controllers
{
    public class CamionesController : Controller
    {
        // GET: CamionesController
        public ActionResult Index()
        {
            var camiones = CRUD<Camion>.GetAll();
            return View(camiones);
        }

        // GET: CamionesController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Conductores = GetConductores();
            var datos = CRUD<Camion>.GetById(id);
            return View(datos);
        }

        private List<SelectListItem> GetConductores()
        {
            var conductores = CRUD<Conductor>.GetAll();
            if(conductores.Any())
            {
                Console.WriteLine("No se encontraron conductores");
            }
            return conductores.Select(c => new SelectListItem
            {
                Value = c.Codigo.ToString(),
                Text = c.Nombre
            }).ToList();
        }

        // GET: CamionesController/Create
        public ActionResult Create()
        {
            ViewBag.Conductores = GetConductores();
            return View();
        }

        // POST: CamionesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Camion camion)
        {
            try
            {
                CRUD<Camion>.Create(camion);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CamionesController/Edit/5
        public ActionResult Edit(int id)
        {
            var datos = CRUD<Camion>.GetById(id);
            return View(datos);
        }

        // POST: CamionesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Camion camion)
        {
            try
            {
                CRUD<Camion>.Update(id, camion);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CamionesController/Delete/5
        public ActionResult Delete(int id)
        {
            var datos = CRUD<Camion>.GetById(id);
            return View(datos);
        }

        // POST: CamionesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Camion camion)
        {
            try
            {
                CRUD<Camion>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
