using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolaHugo
{
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