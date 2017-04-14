using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaaS.Models.BVModels
{
 

    public class ReportesViewModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }
        [Display(Name = "Descripcion")]
        public string Desc { get; set; }
        [Display(Name = "Fecha Reportado")]
        public DateTime DateReported { get; set; }
        [Display(Name = "Reportado por")]
        public string ReportedBy { get; set; }
        [Display(Name = "Foto")]
        public string UrlPic { get; set; }
        [Display(Name = "Comentario")]
        public string Comentario { get; set; }
        [Display(Name = "Ong Asignada")]
        public string OngAsignada { get; set; }
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

    }

    public class ReportesViewEditModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }
        [Display(Name = "Comentario")]
        public string Comentario { get; set; }

       
    }

    public class ReporteCreateModel
    {
        public string Descripcion { get; set; }
      
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public string Id { get; set; }
    }


}