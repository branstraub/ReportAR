using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaaS.Models.BVModels
{
    public class PanelViewModels
    {

        public IEnumerable<LugaresViewModel> Lugares { get; set; }
        public IEnumerable<ServiciosViewModel> Servicios { get; set; }

        public IEnumerable<AnunciosViewModel> Anuncios { get; set; }

    }

  
}