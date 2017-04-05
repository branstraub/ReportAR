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

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

    
        private readonly IReportesRepository _reportesRepository;
        private ApplicationUserManager _userManager;

        public AdminController(IReportesRepository reportesRepository, ApplicationUserManager userManager)
        {
            _reportesRepository = reportesRepository;
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public AdminController()
        {

        }

        // GET: Admin Reportes
        public ActionResult Index()
        {   
            return View(_reportesRepository.GetReportes());
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
            var user = UserManager.Users.FirstOrDefault(x => x.Id == id);
            //getuserID
            

            _reportesRepository.AsignarOng(id, ongid);
            return RedirectToAction("Index");
        }

    }

}
