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
            var modelissue = _reportesRepository.GetReportes();

            var model = new List<DashboardViewModel>();

            foreach (var issue in modelissue)
            {
                model.Add(new DashboardViewModel
                {
                    Direccion = issue.Direccion,
                    OngAsignada = _ongsRepository.GetOng(issue.OngAsignada)?.Nombre,
                    Id = issue.Id,
                    Latitud = issue.Latitud.ToString(),
                    Longitud = issue.Longitud.ToString(),
                    Desc = issue.Desc,
                    Estado = issue.Estado.ToString(),
                    DateReported = issue.DateReported.ToShortDateString(),
                    Comentario = issue.Comentario
                });
            }
           

            return View(model);
        }

        // GET: Admin Reporte Detalle
        public ActionResult ReportesViewDetails(string id)
        {
           

            var caso = _reportesRepository.GetReporte(id);
          


            if (caso.Estado == 0 || _ongsRepository.GetOng(caso.OngAsignada).Nombre == User.Identity.Name)
            {
                var model = caso.ReporteToViewModel();
                model.OngAsignada = _ongsRepository.GetOng(model.OngAsignada)?.Nombre;
                return View("Reportes/ReportesViewDetails", model);
            }
            else
            {
                return View("NotOwned");
            }

           
        }


        // POST: Asignar ONG
        [HttpPost]
        public ActionResult ReporteAsignar(string casoid, string comentario)
        {

            var ong = _ongsRepository.GetOngs().FirstOrDefault(o => o.Nombre == User.Identity.Name);
            _reportesRepository.AsignarOng(casoid, ong.Id, comentario);

            return RedirectToAction("Index");
        }

        // POST: Admin Reporte Detalle Edit
        [HttpPost]
        public ActionResult EditDetailsReporte(ReportesViewEditModel reporte)
        {
           _reportesRepository.UpdateReporte(reporte);
            return RedirectToAction("Index");
        }

        // POST: Admin Cerrar Reporte
        [HttpPost]
        public ActionResult CerrarReporte(string casoid, string comentario)
        {
           
            var caso = _reportesRepository.GetReporte(casoid);

            if (_ongsRepository.GetOng(caso.OngAsignada).Nombre == User.Identity.Name)
            {
                _reportesRepository.CerrarReporte(casoid, comentario);
            }

            return RedirectToAction("Index");
        }

    }

}
