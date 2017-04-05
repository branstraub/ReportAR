using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaaS.Models
{
    public class Dias
    {
        public enum DiasEnum
        {
            [Display(Name = "Lunes")]
            Lunes,
            [Display(Name = "Martes")]
            Martes,
            [Display(Name = "Miercoles")]
            Miercoles,
            [Display(Name = "Jueves")]
            Jueves,
            [Display(Name = "Viernes")]
            Viernes,
            [Display(Name = "Sabado")]
            Sabado,
            [Display(Name = "Domingo")]
            Domingo,
            [Display(Name = "Feriado")]
            Feriado
            
        }

    }
}