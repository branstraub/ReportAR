using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CaaS.Interfaces;
using CaaS.Models;
using CaaS.Models.BVModels;

namespace CaaS.DataClassImplementations
{
    public class ServiciosRepository : IServiciosRepository
    {
        public IEnumerable<ServiciosModel> GetServicios()
        {
            using (var context = new ApplicationDbContext())
            {
                var servicios = context.Servicios.ToArray();
                return servicios;
            }
        }

        public void CreateServicio(ServiciosModel servicio)
        {

            if (GetServicios()
                    .FirstOrDefault(
                        x =>
                            x.DayOfWeek == servicio.DayOfWeek && x.HourOfDay == servicio.HourOfDay &&
                            x.PlaceOfArrival == servicio.PlaceOfArrival &&
                            x.PlaceOfDeparture == servicio.PlaceOfDeparture)
                == null)
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Servicios.Add(servicio);
                    context.SaveChanges();
                }
                
            }
            else
            {
                //TODO: ya existe, avisar?
            }
        }

        public ServiciosModel GetServicio(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                var servicio = context.Servicios.FirstOrDefault(x => x.Id == id);
                return servicio;
            }
        }

        public void UpdateServicio(ServiciosModel servicio)
        {
            ServiciosModel servicioEntity;

            using (var context = new ApplicationDbContext())
            {
                servicioEntity = context.Servicios.FirstOrDefault(x => x.Id == servicio.Id);
            }

            servicioEntity.DayOfWeek = servicio.DayOfWeek;
            servicioEntity.HourOfDay = servicio.HourOfDay;
            servicioEntity.PlaceOfArrival = servicio.PlaceOfArrival;
            servicioEntity.PlaceOfDeparture = servicio.PlaceOfDeparture;

            using (var context = new ApplicationDbContext())
            {
                context.Entry(servicioEntity).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteServicio(string id)
        {

            //borrar las combis asociadas al servicio

            using (var context = new ApplicationDbContext())
            {
                var serviciosEntity = context.Servicios.FirstOrDefault(x => x.Id == id);
                context.Entry(serviciosEntity).State = EntityState.Deleted;
                context.SaveChanges();
            }

        }
    }

    public static class ServiciosRepositoryExtensions
    {
        public static ServiciosViewModel ServiciosToViewModel(this ServiciosModel model)
        {

            return new ServiciosViewModel
            {
                Id = model.Id,
                DayOfWeek = model.DayOfWeek,
                PlaceOfDeparture = model.PlaceOfDeparture,
                PlaceOfArrival = model.PlaceOfArrival,
                HourOfDay = model.HourOfDay
            };
        }
        
    }
}