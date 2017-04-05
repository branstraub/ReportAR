using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaaS.Models.BVModels
{
    public class LugaresViewCreateModel
    {

        [Required]
        [Display(Name = "Lugar")]
        public string Place { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }

    }

    public class LugaresViewModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Display(Name = "Lugar")]
        public string Place { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }

    }
}