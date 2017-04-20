using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaaS.DataClassImplementations;
using CaaS.Interfaces;
using CaaS.Models;
using CaaS.Models.BVModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace CaaS.Controllers
{

  
    public class HomeController : Controller
    {
        private readonly IReportesRepository _reportesRepository;
        private readonly IOngsRepository _ongsRepository;

        public HomeController()
        {
            _reportesRepository = new ReportesRepository();
            _ongsRepository = new OngsRepository();
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateIssue(ReporteCreateModel model)
        {
            if (model.Descripcion == null || model.Id == null || model.Lat == null || model.Lon == null)
            {
                return new HttpStatusCodeResult(400);
            }

            var file = Request.Files[0];

            _reportesRepository.CreateReporte(new ReporteModel
            {
                Id = Guid.NewGuid().ToString(),
                Desc = model.Descripcion,
                DateReported = DateTime.Now,
                Direccion = "null", //TODO: llamar a la api de geodecode
                Estado = 0,
                Latitud = (double) model.Lat,
                Longitud = (double) model.Lon,
                ReportedBy = model.Id,
                UrlPic = "null", //TODO: ver donde pija la guardo
            });

            return new HttpStatusCodeResult(201);
        }

        public ActionResult GetIssues(string id)
        {
            var issues = _reportesRepository.GetReportes()
                .Where(x => x.ReportedBy == id)
                .Select(o => new
                {
                    o.Comentario,
                    o.DateReported,
                    o.Direccion,
                    OngAsignada = _ongsRepository.GetOngs()
                    .FirstOrDefault(y => y.Id == o.OngAsignada).Nombre,
                 
                });

            if (!issues.Any())
            {
                return new HttpStatusCodeResult(204);
            }

            return Json(issues, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOng(string id)
        {
            var ong = _ongsRepository.GetOng(id);

            if (ong == null)
            {
                return new HttpStatusCodeResult(204);
            }

            return Json(ong, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOngs()
        {
            var ongs = _ongsRepository.GetOngs();

            if (!ongs.Any())
            {
                return new HttpStatusCodeResult(204);
            }

            return Json(ongs, JsonRequestBehavior.AllowGet);
        }


    }

 
}