using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema.API.Consume;
using SistemaGFYMP.Modelos;

namespace Sistema.Presentacion_MVC_.Controllers
{
    public class TalleresController : Controller
    {
        // GET: TalleresController
        [Authorize]
        public ActionResult Index()
        {
            var talleres = CRUD<Taller>.GetAll();
            return View(talleres);
        }

        // GET: TalleresController/Details/5
        public ActionResult Details(int id)
        {
            var datos = CRUD<Taller>.GetById(id);
            datos.MantenimientosProgramados = CRUD<MantenimientoProgramado>.GetBy("taller", id);
            return View(datos);
        }

        // GET: TalleresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TalleresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Taller taller)
        {
            try
            {
                CRUD<Taller>.Create(taller);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TalleresController/Edit/5
        public ActionResult Edit(int id)
        {
            var datos = CRUD<Taller>.GetById(id);
            return View(datos);
        }

        // POST: TalleresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Taller taller)
        {
            try
            {
                CRUD<Taller>.Update(id, taller);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TalleresController/Delete/5
        public ActionResult Delete(int id)
        {
            var datos = CRUD<Taller>.GetById(id);
            return View(datos);
        }

        // POST: TalleresController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Taller taller)
        {
            try
            {
                CRUD<Taller>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
