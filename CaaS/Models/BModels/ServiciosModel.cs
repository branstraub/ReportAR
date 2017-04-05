using System;
using System.Data.Entity;

namespace CaaS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ServiciosModel
    {
        public string Id { get; set; }
        public Dias.DiasEnum DayOfWeek { get; set; }
        public DateTime HourOfDay { get; set; }
        public string PlaceOfDeparture { get; set; }
        public string PlaceOfArrival { get; set; }

    }

}