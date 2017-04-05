using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaaS.Models;
using CaaS.Models.BVModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CaaS.Controllers
{

    [RequireHttps]
    public class HomeController : Controller
    {

        private ApplicationUserManager _userManager;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
          
        }

        [Authorize]
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var user = UserManager.Users.FirstOrDefault(x => x.Id == id);

            var model = new DashboardViewModels
            {
                User = new EditUserViewModel
                {
                    Address = user.Address,
                    Dni = user.Dni,
                    Dob = user.Dob,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Localidad = user.Localidad,
                    Lote = user.Lote,
                    Phone = user.PhoneNumber
                }
            };

            return View(model);
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

    }
}