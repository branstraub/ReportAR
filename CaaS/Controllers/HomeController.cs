﻿using System;
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

  
    public class HomeController : Controller
    {

       
        public ActionResult Index()
        {
            return View();
        }


    }
}