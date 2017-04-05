using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaaS.Models.BVModels
{
    public class AnunciosViewCreateModel
    {

        [Required]
        [Display(Name = "Titulo")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }

    }

    public class AnunciosViewModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Titulo")]
        public string Name { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }
    }
}