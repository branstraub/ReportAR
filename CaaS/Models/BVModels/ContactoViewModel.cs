using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaaS.Models.BVModels
{
 

    public class ContactoViewCreateModel
    {
        [Required(ErrorMessage = "Campo requerido")]
        public string Message { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese un Email válido")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public string Name { get; set; }
       

    }


}