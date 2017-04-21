using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaaS.DataClassImplementations;
using CaaS.Interfaces;
using CaaS.Models;
using CaaS.Models.BVModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebGrease.Css.Extensions;

namespace CaaS.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        private readonly IReportesRepository _reportesRepository;
        private readonly IOngsRepository _ongsRepository;

        public AdminController()
        {
            _reportesRepository = new ReportesRepository();
            _ongsRepository = new OngsRepository();
        }

        // GET: Admin Reportes
        public ActionResult Index()
        {
            var model = _reportesRepository.GetReportes();

            return model.Count() != 0 ? View(model) : View("Empty");
        }

        // GET: Admin Reporte Detalle
        public ActionResult ViewDetailsReporte(string id)
        {
            return View(_reportesRepository.GetReporte(id));
        }

        // GET: Admin Reporte Detalle Edit
        public ActionResult EditDetailsReporte(string id)
        {
            return View(_reportesRepository.GetReporte(id));
        }

        // POST: Admin Reporte Detalle Edit
        [HttpPost]
        public ActionResult EditDetailsReporte(ReportesViewEditModel reporte)
        {
           _reportesRepository.UpdateReporte(reporte);
            return RedirectToAction("Index");
        }

        // POST: Admin Asginar Ong a Reporte
        [HttpPost]
        public ActionResult AsignarOng(string id)
        {
            var ongid = User.Identity.GetUserId();
            //var user = UserManager.Users.FirstOrDefault(x => x.Id == id);
            //getuserID
            

            _reportesRepository.AsignarOng(id, ongid);
            return RedirectToAction("Index");
        }

        // POST: Admin Cerrar Reporte
        [HttpPost]
        public ActionResult CerrarReporte(string id)
        {
            //chequear que el que lo sea sea el usuario que lo abrio

            
            _reportesRepository.CerrarReporte(id);
            return RedirectToAction("Index");
        }

    }

}
