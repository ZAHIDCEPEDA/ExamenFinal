using Clases.Model;
using Clases.ModelosNuevos;
using Clases.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal.Controllers
{
    [Authorize]
    public class TipoVehiculosController : Controller
    {
        private readonly EjercicioEvaluacionContext _contex;
        public TipoVehiculosController(EjercicioEvaluacionContext contex)
        {
            _contex = contex;
        }
        private void Combox()
        {
            ViewData["CodigoVehiculo"] = new SelectList(_contex.Vehiculo.Select(z => new ViewModelVehiculoTipo
            {
                Codigo = z.Codigo,
                Nombre= $"{z.Nombre}",
                Estado = z.Estado
            }).Where(z => z.Estado == 1).ToList(), "Codigo", "Nombre");
        }
        [Authorize(Roles = "Gerente,Persona")]
        // GET: TipoVehiculosController
        public ActionResult Index()
        {
            List<TipoVehiculo> ltsvehiculo = _contex.TipoVehiculo.ToList();
            return View(ltsvehiculo);
        }
        [Authorize(Roles = "Gerente,Persona")]
        // GET: TipoVehiculosController/Details/5
        public ActionResult Details(int id)
        {
            TipoVehiculo tipovehiculo = _contex.TipoVehiculo.Where(z => z.Codigo == id).FirstOrDefault();
            return View(tipovehiculo);
        }
        [Authorize(Roles = "Gerente")]
        // GET: TipoVehiculosController/Create
        public ActionResult Create()
        {
            Combox();
            return View();
        }
        [Authorize(Roles = "Gerente")]
        // POST: TipoVehiculosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoVehiculo tipovehiculo)
        {
            try
            {
                tipovehiculo.Estado = 1;
                _contex.Add(tipovehiculo);
                _contex.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(tipovehiculo);
            }
        }
        [Authorize(Roles = "Gerente")]
        // GET: TipoVehiculosController/Edit/5
        public ActionResult Edit(int id)
        {
            Combox();
            TipoVehiculo tipovehiculo = _contex.TipoVehiculo.Where(z => z.Codigo == id).FirstOrDefault();
            return View(tipovehiculo);
        }
        [Authorize(Roles = "Gerente")]
        // POST: TipoVehiculosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TipoVehiculo tipovehiculo)
        {
            if (id != tipovehiculo.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _contex.SaveChanges();
                _contex.Update(tipovehiculo);
                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(tipovehiculo);
            }
        }
        [Authorize(Roles = "Gerente")]
        public ActionResult Activar(int id)
        {
            TipoVehiculo tipovehiculo = _contex.TipoVehiculo    .Where(z => z.Codigo == id).FirstOrDefault();
            tipovehiculo.Estado = 1;
            _contex.SaveChanges();
            _contex.Update(tipovehiculo);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Gerente")] 
        public ActionResult Desactivar(int id)
        {
            TipoVehiculo tipovehiculo = _contex.TipoVehiculo.Where(z => z.Codigo == id).FirstOrDefault();
            tipovehiculo.Estado = 0;
            _contex.SaveChanges();
            _contex.Update(tipovehiculo);
            return RedirectToAction("Index");
        }
    }
}

