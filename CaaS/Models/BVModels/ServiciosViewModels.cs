using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaaS.Models.BVModels
{
    public class ServiciosViewCreateModel
    {

    [Display(Name = "Dia de la semana")]
    public DiasHabilitadosModel DayOfWeek { get; set; }

    [Required]
    [Display(Name = "Hora de salida")]
    public DateTime HourOfDay { get; set; }

    [Required]
    [Display(Name = "Lugar de salida")]
    public string PlaceOfDeparture { get; set; }

    [Required]
    [Display(Name = "Lugar de llegada")]
    public string PlaceOfArrival { get; set; }

    }

    public class ServiciosViewModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Display(Name = "Dia de la semana")]
        public Dias.DiasEnum DayOfWeek { get; set; }
       
        [Display(Name = "Hora de salida")]
        public DateTime HourOfDay { get; set; }
   
        [Display(Name = "Lugar de salida")]
        public string PlaceOfDeparture { get; set; }

        [Display(Name = "Lugar de llegada")]
        public string PlaceOfArrival { get; set; }


    }

    public class DiasHabilitadosModel
    {
        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
        public bool Feriado { get; set; }
    }
}