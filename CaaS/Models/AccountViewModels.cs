using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaaS.Models
{
 

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recordar?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        //TODO: validar espacios
       
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

      
        [DataType(DataType.Url)]
        [Display(Name = "Web Url")]
        public string WebUrl { get; set; }

        [Required]
        [Display(Name = "Mision")]
        public string Mision { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Mail")]
        public string Mail { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe ser al menos de {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }


    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe ser al menos de {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

}
