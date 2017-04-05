using System;
using System.Data.Entity;

namespace CaaS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class LugaresModel
    {
        public string Id { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
    }

}