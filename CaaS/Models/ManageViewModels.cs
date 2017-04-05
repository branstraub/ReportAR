using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace CaaS.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
       
        public bool BrowserRemembered { get; set; }
    }



    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe ser al menos de {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña Actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe ser al menos de {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }


}