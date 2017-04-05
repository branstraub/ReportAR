using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CaaS.Models
{
    public partial class ApplicationDbContext 
    {
    
        public DbSet<ChoferModel> Choferes { get; set; }
        public DbSet<CombisModel> Combis { get; set; }
        public DbSet<LugaresModel> Lugares { get; set; }
        public DbSet<ServiciosModel> Servicios { get; set; }
        public DbSet<ViajesModel> Viajes { get; set; }
        public DbSet<AnunciosModel> Anuncios { get; set; }
    }

    }
