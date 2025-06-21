using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.API.Consume;
using SistemaGFYMP.Modelos;

namespace Sistema.Presentacion_MVC_.Controllers
{
    public class MantenimientosProgramadosController : Controller
    {
        // GET: MantenimientosProgramadosController
        [Authorize]
        public ActionResult Index()
        {
            var mantenimientos = CRUD<MantenimientoProgramado>.GetAll();
            return View(mantenimientos);
        }

        // GET: MantenimientosProgramadosController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Talleres = GetTalleres();
            ViewBag.Camiones = GetCamiones();
            var datos = CRUD<MantenimientoProgramado>.GetById(id);
            return View(datos);
        }

        private List<SelectListItem> GetTalleres()
        {
            var talleres = CRUD<Taller>.GetAll();
            if (talleres.Any())
            {
                Console.WriteLine("No se encontraron conductores");
            }
            return talleres.Select(t => new SelectListItem
            {
                Value = t.Codigo.ToString(),
                Text = t.Nombre
            }).ToList();
        }

        private List<SelectListItem> GetCamiones()
        {
            var camion = CRUD<Camion>.GetAll();
            if (camion.Any())
            {
                Console.WriteLine("No se encontraron conductores");
            }
            return camion.Select(t => new SelectListItem
            {
                Value = t.Codigo.ToString(),
                Text = t.Marca
            }).ToList();
        }

        // GET: MantenimientosProgramadosController/Create
        public ActionResult Create()
        {
            ViewBag.Talleres = GetTalleres();
            ViewBag.Camiones = GetCamiones();
            return View();
        }

        // POST: MantenimientosProgramadosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MantenimientoProgramado mantenimiento)
        {
            try
            {
                var taller = CRUD<Taller>.GetById(mantenimiento.TallerCodigo);
                if (mantenimiento.Fecha < DateTime.Now)
                {
                    ModelState.AddModelError("Fecha", "La fecha no puede ser anterior a la fecha actual.");
                    return View(mantenimiento);
                }
                if(taller.CapacidadMaximaReparaciones <= taller.MantenimientosProgramados.Count)
                {
                    ModelState.AddModelError("TallerCodigo", "El taller ha alcanzado su capacidad máxima de reparaciones.");
                    return View(mantenimiento);
                }
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
            ViewBag.Talleres = GetTalleres();
            ViewBag.Camiones = GetCamiones();
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
                var taller = CRUD<Taller>.GetById(mantenimiento.TallerCodigo);

                if (mantenimiento.Fecha < DateTime.Now)
                {
                    ModelState.AddModelError("Fecha", "La fecha no puede ser anterior a la fecha actual.");
                    return View();
                }
                if (taller.CapacidadMaximaReparaciones <= taller.MantenimientosProgramados.Count)
                {
                    ModelState.AddModelError("TallerCodigo", "El taller ha alcanzado su capacidad máxima de reparaciones.");
                    return View(mantenimiento);
                }
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
