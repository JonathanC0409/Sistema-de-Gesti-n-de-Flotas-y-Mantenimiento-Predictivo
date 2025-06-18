using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema.API.Consume;
using SistemaGFYMP.Modelos;

namespace Sistema.Presentacion_MVC_.Controllers
{
    public class ConductoresController : Controller
    {
        // GET: ConductoresController
        public ActionResult Index()
        {
            var conductores = CRUD<Conductor>.GetAll();
            return View(conductores);
        }

        // GET: ConductoresController/Details/5
        public ActionResult Details(int id)
        {
            var datos = CRUD<Conductor>.GetById(id);
            return View(datos);
        }

        // GET: ConductoresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConductoresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Conductor conductor)
        {
            try
            {
                CRUD<Conductor>.Create(conductor);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConductoresController/Edit/5
        public ActionResult Edit(int id)
        {
            var datos = CRUD<Conductor>.GetById(id);
            return View(datos);
        }

        // POST: ConductoresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Conductor conductor)
        {
            try
            {
                CRUD<Conductor>.Update(id, conductor);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConductoresController/Delete/5
        public ActionResult Delete(int id)
        {
            var datos = CRUD<Conductor>.GetById(id);
            return View(datos);
        }

        // POST: ConductoresController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Conductor conductor)
        {
            try
            {
                CRUD<Conductor>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
