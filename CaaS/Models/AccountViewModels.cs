using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CaaS.Models
{
 

    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo contraseña es obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recordar?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        //TODO: validar espacios
       
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Url(ErrorMessage = "La url no es correcta")]
        [Display(Name = "Web Url")]
        public string WebUrl { get; set; }

        [Required(ErrorMessage = "El campo misión es obligatorio")]
        [Display(Name = "Mision")]
        public string Mision { get; set; }

        [Required(ErrorMessage = "El campo teléfono es obligatorio")]
        [Phone(ErrorMessage = "El teléfono no es correcto")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo mail es obligatorio")]
        [EmailAddress(ErrorMessage = "Dirección de email invalida")]
        [Display(Name = "Mail")]
        public string Mail { get; set; }

        [Display(Name = "Logo de la ONG")]
        public HttpPostedFileBase OngPic { get; set; }

        [Required(ErrorMessage = "El campo contraseña es obligatorio")]
        [StringLength(100, ErrorMessage = "La contraseña debe ser al menos de {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo confirmar contraseña es obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }


    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo contraseña es obligatorio")]
        [StringLength(100, ErrorMessage = "La contraseña debe ser al menos de {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo confirmar contraseña es obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

}
