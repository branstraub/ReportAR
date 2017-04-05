using System;
using System.Data.Entity;

namespace CaaS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ReporteModel
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public DateTime DateReported { get; set; }
        public string ReportedBy { get; set; }
        public string UrlPic { get; set; }
        public string Comentario { get; set; }
        public string OngAsignada { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Direccion { get; set; }

    }

}