using Clases.Model;
using Clases.ModelosNuevos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal.Controllers
{
    [Authorize]
    public class VehiculosController : Controller
    {
        private readonly EjercicioEvaluacionContext _contex;
        public VehiculosController(EjercicioEvaluacionContext contex)
        {
            _contex = contex;
        }
        [Authorize(Roles="Gerente,Persona")]
        // GET: VehiculosController
        public ActionResult Index()
        {
            List<Vehiculo> ltsvehiculo = _contex.Vehiculo.ToList();
            return View(ltsvehiculo);
        }
        [Authorize(Roles = "Gerente,Persona")]
        // GET: VehiculosController/Details/5
        public ActionResult Details(int id)
        {
            Vehiculo vehiculo = _contex.Vehiculo.Where(z => z.Codigo == id).FirstOrDefault();
            return View(vehiculo);
        }
        [Authorize(Roles = "Gerente")]
        // GET: VehiculosController/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Gerente")]
        // POST: VehiculosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehiculo vehiculo)
        {
            try
            {
                vehiculo.Estado = 1;
                _contex.Add(vehiculo);
                _contex.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(vehiculo);
            }
        }
        [Authorize(Roles = "Gerente")]
        // GET: VehiculosController/Edit/5
        public ActionResult Edit(int id)
        {
            Vehiculo vehiculo = _contex.Vehiculo.Where(z => z.Codigo == id).FirstOrDefault();
            return View(vehiculo);
        }
        [Authorize(Roles = "Gerente")]
        // POST: VehiculosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _contex.SaveChanges();
                _contex.Update(vehiculo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(vehiculo);
            }
        }
        [Authorize(Roles = "Gerente")]
        public ActionResult Activar(int id)
        {
            Vehiculo vehiculo = _contex.Vehiculo.Where(z => z.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 1;
            _contex.SaveChanges();
            _contex.Update(vehiculo);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Gerente")]
        public ActionResult Desactivar(int id)
        {
            Vehiculo vehiculo = _contex.Vehiculo.Where(z => z.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 0;
            _contex.SaveChanges();
            _contex.Update(vehiculo);
            return RedirectToAction("Index");
        }
    }
}
