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
    public class LugaresRepository : ILugaresRepository
    {
        public IEnumerable<LugaresModel> GetLugares()
        {
            using (var context = new ApplicationDbContext())
            {
                var lugares = context.Lugares.ToArray();
                return lugares;
            }
        }

        public void CreateLugar(LugaresModel lugar)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Lugares.Add(lugar);
                context.SaveChanges();

            }
        }

        public LugaresModel GetLugar(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                var lugar = context.Lugares.FirstOrDefault(x => x.Id == id);
                return lugar;
            }
        }

        public void UpdateLugar(LugaresModel lugar)
        {
            LugaresModel lugarEntity;

            using (var context = new ApplicationDbContext())
            {
                lugarEntity = context.Lugares.FirstOrDefault(x => x.Id == lugar.Id);
            }

            lugarEntity.Description = lugar.Description;
            lugarEntity.Place = lugar.Place;

            using (var context = new ApplicationDbContext())
            {
                context.Entry(lugarEntity).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteLugar(string id)
        {

            using (var context = new ApplicationDbContext())
            {
                var lugarEntity = context.Lugares.FirstOrDefault(x => x.Id == id);
                context.Entry(lugarEntity).State = EntityState.Deleted;
                context.SaveChanges();
            }

        }
    }

    public static class LugaresRepositoryExtensions
    {
        public static LugaresViewModel LugaresToViewModel(this LugaresModel model)
        {

            return new LugaresViewModel
            {
                Id = model.Id,
                Place = model.Place,
                Description = model.Description
            };
        }
        
    }
}