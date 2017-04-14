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
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(402);
            }

            //Request.Files

            _reportesRepository.CreateReporte(new ReporteModel
            {
                Id = Guid.NewGuid().ToString(),
                Desc = model.Descripcion,
                DateReported = DateTime.Now,
                Direccion = "null", //TODO: llamar a la api de geodecode
                Estado = 0,
                Latitud = model.Lat,
                Longitud = model.Lon,
                ReportedBy = model.Id,
                UrlPic = "null", //TODO: ver donde pija la guardo
            });

            return new HttpStatusCodeResult(200);
        }

        public ActionResult Getissues(string id)
        {
            var issues = _reportesRepository.GetReportes()
                .Where(x => x.Id == id)
                .Select(o => new
                {
                    o.Comentario,
                    o.DateReported,
                    o.Direccion,
                    o.OngAsignada,
                 
                });

            if (!issues.Any())
            {
                return new HttpStatusCodeResult(402);
            }

            return Json(issues, JsonRequestBehavior.AllowGet);
        }


    }

 
}