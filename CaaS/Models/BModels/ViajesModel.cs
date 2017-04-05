using System;
using System.Data.Entity;

namespace CaaS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ViajesModel
    {
        public string Id { get; set; }
        public int Cantidad { get; set; }
        public string Dni { get; set; }
        public string CombiId { get; set; }
        public DateTime Dia { get; set; }
        public int Calificacion { get; set; }
    }

}