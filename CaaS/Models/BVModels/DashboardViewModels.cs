using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaaS.Models.BVModels
{
 

    public class DashboardViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Descripción")]
        public string Desc { get; set; }
        [Display(Name = "Fecha Reportado")]
        public string DateReported { get; set; }
        [Display(Name = "Foto")]
        public string UrlPic { get; set; }
        [Display(Name = "Comentario")]
        public string Comentario { get; set; }
        [Display(Name = "Ong Asignada")]
        public string OngAsignada { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Display(Name = "Latitud")]
        public string Latitud { get; set; }
        [Display(Name = "Longitud")]
        public string Longitud { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }

    }

   
}