using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CaaS.Models
{
    public partial class ApplicationDbContext
    { 
        public DbSet<ReporteModel> Reportes { get; set; }
        public DbSet<OngModel> Ongs { get; set; }
    }

}
