using System;
using System.Data.Entity;

namespace CaaS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class OngModel
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Mail { get; set; }
        public string PicUrl { get; set; }
        public string WebUrl { get; set; }
        public string Mision { get; set; }
        public bool Locked { get; set; }

    }

}