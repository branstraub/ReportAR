using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaaS.DataClassImplementations;
using CaaS.Interfaces;
using CaaS.Models;
using CaaS.Models.BVModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Extensions;

namespace CaaS.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly ILugaresRepository _lugaresRepository;
        private readonly IAnunciosRepository _anunciosRepository;
        private readonly IServiciosRepository _serviciosRepository;

        public AdminController(ILugaresRepository lugaresRepository, IAnunciosRepository anunciosRepository, IServiciosRepository serviciosRepository)
        {
            _lugaresRepository = lugaresRepository;
            _anunciosRepository = anunciosRepository;
            _serviciosRepository = serviciosRepository;
        }

        public AdminController() : this(new LugaresRepository(), new AnunciosRepository(), new ServiciosRepository())
        {

        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(new PanelViewModels
            {
                Lugares = _lugaresRepository.GetLugares().Select(x => x.LugaresToViewModel()),
                Anuncios = _anunciosRepository.GetAnuncios().Select(x => x.AnunciosToViewModel())
            });
        }

        public ActionResult LugaresCreate()
        {
            return View("Lugares/LugaresCreate");
        }

        [HttpPost]
        public ActionResult LugaresCreate(LugaresViewCreateModel model)
        {

            _lugaresRepository.CreateLugar(new LugaresModel
            {
                Id = Guid.NewGuid().ToString(),
                Place = model.Place,
                Description = model.Description
            });

            return RedirectToAction("Index");
        }

        public ActionResult LugaresEdit(string id)
        {
            var lugar = _lugaresRepository.GetLugar(id).LugaresToViewModel();
            return View("Lugares/LugaresEdit", lugar);
        }

        [HttpPost]
        public ActionResult LugaresEdit(LugaresViewModel model)
        {
            _lugaresRepository.UpdateLugar(new LugaresModel
            {
                Description = model.Description,
                Id = model.Id,
                Place = model.Place
            });

            return RedirectToAction("Index");
        }


        public ActionResult LugaresDelete(string id)
        {
            _lugaresRepository.DeleteLugar(id);
            return RedirectToAction("Index");
        }


        public ActionResult AnunciosCreate()
        {
            return View("Anuncios/AnunciosCreate");
        }

        [HttpPost]
        public ActionResult AnunciosCreate(AnunciosViewCreateModel model)
        {
            _anunciosRepository.CreateAnuncio(new AnunciosModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Description = model.Description,
                Date = DateTime.Now
            });

            return RedirectToAction("Index");
        }

        public ActionResult AnunciosEdit(string id)
        {
            var anuncio = _anunciosRepository.GetAnuncio(id).AnunciosToViewModel();
            return View("Anuncios/AnunciosEdit", anuncio);
        }

        [HttpPost]
        public ActionResult AnunciosEdit(AnunciosViewModel model)
        {
            _anunciosRepository.UpdateAnuncio(new AnunciosModel
            {
                Description = model.Description,
                Id = model.Id,
                Name = model.Name,
                Date = model.Date
            });

            return RedirectToAction("Index");
        }

        public ActionResult AnunciosDelete(string id)
        {
            _anunciosRepository.DeleteAnuncio(id);
            return RedirectToAction("Index");
        }

        public ActionResult ServiciosCreate()
        {

            ViewBag.DestinosLista = GetDestinos();
            return View("Servicios/ServiciosCreate", new ServiciosViewCreateModel
            {
                DayOfWeek = new DiasHabilitadosModel()
            });
        }

        [HttpPost]
        public ActionResult ServiciosCreate(ServiciosViewCreateModel model)
        {
            if (model.PlaceOfArrival == model.PlaceOfDeparture)
            {
                ViewBag.DestinosLista = GetDestinos();
                ModelState.AddModelError("", "El lugar de origen no puede ser igual al de destino");

                model.DayOfWeek.Lunes = false;
                model.DayOfWeek.Martes = false;
                model.DayOfWeek.Miercoles = false;
                model.DayOfWeek.Jueves = false;
                model.DayOfWeek.Viernes = false;
                model.DayOfWeek.Sabado = false;
                model.DayOfWeek.Domingo = false;
                model.DayOfWeek.Feriado = false;

                return View("Servicios/ServiciosCreate", model);
            }

            //revisar que ya no haya un servicio igual, mismo origen, destino, dia y horario

            #region CreateServicios

            if (model.DayOfWeek.Lunes)
            {
                _serviciosRepository.CreateServicio(new ServiciosModel
                {
                    Id = Guid.NewGuid().ToString(),
                    PlaceOfDeparture = model.PlaceOfDeparture,
                    PlaceOfArrival = model.PlaceOfArrival,
                    DayOfWeek = Dias.DiasEnum.Lunes,
                    HourOfDay = model.HourOfDay
                });
            }

            if (model.DayOfWeek.Martes)
            {
                _serviciosRepository.CreateServicio(new ServiciosModel
                {
                    Id = Guid.NewGuid().ToString(),
                    PlaceOfDeparture = model.PlaceOfDeparture,
                    PlaceOfArrival = model.PlaceOfArrival,
                    DayOfWeek = Dias.DiasEnum.Martes,
                    HourOfDay = model.HourOfDay
                });
            }

            if (model.DayOfWeek.Miercoles)
            {
                _serviciosRepository.CreateServicio(new ServiciosModel
                {
                    Id = Guid.NewGuid().ToString(),
                    PlaceOfDeparture = model.PlaceOfDeparture,
                    PlaceOfArrival = model.PlaceOfArrival,
                    DayOfWeek = Dias.DiasEnum.Miercoles,
                    HourOfDay = model.HourOfDay
                });
            }

            if (model.DayOfWeek.Jueves)
            {
                _serviciosRepository.CreateServicio(new ServiciosModel
                {
                    Id = Guid.NewGuid().ToString(),
                    PlaceOfDeparture = model.PlaceOfDeparture,
                    PlaceOfArrival = model.PlaceOfArrival,
                    DayOfWeek = Dias.DiasEnum.Jueves,
                    HourOfDay = model.HourOfDay
                });
            }

            if (model.DayOfWeek.Viernes)
            {
                _serviciosRepository.CreateServicio(new ServiciosModel
                {
                    Id = Guid.NewGuid().ToString(),
                    PlaceOfDeparture = model.PlaceOfDeparture,
                    PlaceOfArrival = model.PlaceOfArrival,
                    DayOfWeek = Dias.DiasEnum.Viernes,
                    HourOfDay = model.HourOfDay
                });
            }

            if (model.DayOfWeek.Sabado)
            {
                _serviciosRepository.CreateServicio(new ServiciosModel
                {
                    Id = Guid.NewGuid().ToString(),
                    PlaceOfDeparture = model.PlaceOfDeparture,
                    PlaceOfArrival = model.PlaceOfArrival,
                    DayOfWeek = Dias.DiasEnum.Sabado,
                    HourOfDay = model.HourOfDay
                });
            }

            if (model.DayOfWeek.Domingo)
            {
                _serviciosRepository.CreateServicio(new ServiciosModel
                {
                    Id = Guid.NewGuid().ToString(),
                    PlaceOfDeparture = model.PlaceOfDeparture,
                    PlaceOfArrival = model.PlaceOfArrival,
                    DayOfWeek = Dias.DiasEnum.Domingo,
                    HourOfDay = model.HourOfDay
                });
            }

            if (model.DayOfWeek.Feriado)
            {
                _serviciosRepository.CreateServicio(new ServiciosModel
                {
                    Id = Guid.NewGuid().ToString(),
                    PlaceOfDeparture = model.PlaceOfDeparture,
                    PlaceOfArrival = model.PlaceOfArrival,
                    DayOfWeek = Dias.DiasEnum.Feriado,
                    HourOfDay = model.HourOfDay
                });
            }

            #endregion

            return RedirectToAction("Index");

          
        }

        private static SelectList GetDias()
        {
            return new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Selected = false,
                        Text = Dias.DiasEnum.Lunes.ToString(),
                        Value = ((int) Dias.DiasEnum.Lunes).ToString()
                    },
                    new SelectListItem
                    {
                        Selected = false,
                        Text = Dias.DiasEnum.Martes.ToString(),
                        Value = ((int) Dias.DiasEnum.Martes).ToString()
                    },
                    new SelectListItem
                    {
                        Selected = false,
                        Text = Dias.DiasEnum.Miercoles.ToString(),
                        Value = ((int) Dias.DiasEnum.Miercoles).ToString()
                    },
                    new SelectListItem
                    {
                        Selected = false,
                        Text = Dias.DiasEnum.Jueves.ToString(),
                        Value = ((int) Dias.DiasEnum.Jueves).ToString()
                    },
                    new SelectListItem
                    {
                        Selected = false,
                        Text = Dias.DiasEnum.Viernes.ToString(),
                        Value = ((int) Dias.DiasEnum.Viernes).ToString()
                    },
                    new SelectListItem
                    {
                        Selected = false,
                        Text = Dias.DiasEnum.Sabado.ToString(),
                        Value = ((int) Dias.DiasEnum.Sabado).ToString()
                    },
                    new SelectListItem
                    {
                        Selected = false,
                        Text = Dias.DiasEnum.Domingo.ToString(),
                        Value = ((int) Dias.DiasEnum.Domingo).ToString()
                    },
                    new SelectListItem
                    {
                        Selected = false,
                        Text = Dias.DiasEnum.Feriado.ToString(),
                        Value = ((int) Dias.DiasEnum.Feriado).ToString()
                    },
                }, "Value", "Text", 1);
        }

        private List<SelectListItem> GetDestinos()
        {
            var lugares = _lugaresRepository.GetLugares();

            return lugares.Select(lugar => new SelectListItem
            {
                Selected = false, Text = lugar.Place, Value = lugar.Id
            }).ToList();
        }
    }

}
