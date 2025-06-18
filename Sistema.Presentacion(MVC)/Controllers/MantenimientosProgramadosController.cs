using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema.API.Consume;
using SistemaGFYMP.Modelos;

namespace Sistema.Presentacion_MVC_.Controllers
{
    public class MantenimientosProgramadosController : Controller
    {
        // GET: MantenimientosProgramadosController
        public ActionResult Index()
        {
            var mantenimientos = CRUD<MantenimientoProgramado>.GetAll();
            return View(mantenimientos);
        }

        // GET: MantenimientosProgramadosController/Details/5
        public ActionResult Details(int id)
        {
            var datos = CRUD<MantenimientoProgramado>.GetById(id);
            return View(datos);
        }

        // GET: MantenimientosProgramadosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MantenimientosProgramadosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MantenimientoProgramado mantenimiento)
        {
            try
            {
                CRUD<MantenimientoProgramado>.Create(mantenimiento);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MantenimientosProgramadosController/Edit/5
        public ActionResult Edit(int id)
        {
            var datos = CRUD<MantenimientoProgramado>.GetById(id);
            return View(datos);
        }

        // POST: MantenimientosProgramadosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MantenimientoProgramado mantenimiento)
        {
            try
            {
                CRUD<MantenimientoProgramado>.Update(id, mantenimiento);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MantenimientosProgramadosController/Delete/5
        public ActionResult Delete(int id)
        {
            var datos = CRUD<MantenimientoProgramado>.GetById(id);
            return View(datos);
        }

        // POST: MantenimientosProgramadosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, MantenimientoProgramado mantenimiento)
        {
            try
            {
                CRUD<MantenimientoProgramado>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
