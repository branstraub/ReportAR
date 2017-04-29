using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.WindowsAzure.Storage;
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

        [HttpPost]
        public ActionResult CreateIssue(ReporteCreateModel model)
        {
            if (model.Descripcion == null || model.Id == null || model.Lat == null || model.Lon == null)
            {
                return new HttpStatusCodeResult(400);
            }

           
            var urlpic = "";
            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0];

                var storageAccount =
                    CloudStorageAccount.Parse(
                        "DefaultEndpointsProtocol=https;AccountName=abrigarpics;AccountKey=cWkQ94z1J22ZCZ4AT+17nOjXUPmE48qhqJREN4RJhM8giIfzP6hjeSr7DKgOcmK6rJ+lplF+av1Dqt2mp72CHA==;EndpointSuffix=core.windows.net");
                var blobClient = storageAccount.CreateCloudBlobClient();
                var container = blobClient.GetContainerReference("issuepic");

                urlpic = model.Id + "-" + Guid.NewGuid() + "." + file.FileName.Split('.')[1];
                var blockBlob = container.GetBlockBlobReference(urlpic);
                using (var fileStream = file.InputStream)
                {
                    blockBlob.UploadFromStream(fileStream);
                }
            }
            
            var address = "";

            try
            {

                var html = "";
                var url = @"http://dev.virtualearth.net/REST/v1/Locations/{0},{1}?key=AvnNFjDsnWvQb6le63wIjKsNKdvu2PqntR1-vph6KkzXiAS2gj491wfkaoqinn1f";

                url = url.Replace("{0}", model.Lat.ToString().Replace(",", ".")).Replace("{1}", model.Lon.ToString().Replace(",", "."));
                var request = (HttpWebRequest)WebRequest.Create(url);
               
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                dynamic jsonDecoded = JsonConvert.DeserializeObject<dynamic>(html);
                address = jsonDecoded.resourceSets[0].resources[0].address.formattedAddress;
            }
            catch (Exception e)
            {
                address = "";
            }

            _reportesRepository.CreateReporte(new ReporteModel
            {
                Id = Guid.NewGuid().ToString(),
                Desc = model.Descripcion,
                DateReported = DateTime.Now,
                Direccion = address,
                Estado = 0,
                Latitud = (double) model.Lat,
                Longitud = (double) model.Lon,
                ReportedBy = model.Id,
                UrlPic = urlpic, 
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
                    OngAsignada = _ongsRepository.GetOng(o.OngAsignada).Nombre,
                 
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
            var ongs = _ongsRepository.GetOngs().Where(o => o.Locked == false);

            if (!ongs.Any())
            {
                return new HttpStatusCodeResult(204);
            }

            return Json(ongs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact(ContactoViewCreateModel model) { 


        if(!ModelState.IsValid)
        {
            return View("Index", model);
        }

        //todo: send message

        return View("Index");
        }


    }

 
}