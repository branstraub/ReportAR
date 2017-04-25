﻿using System;
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
            return View("Reportes/ReportesViewDetails",_reportesRepository.GetReporte(id).ReporteToViewModel());
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
