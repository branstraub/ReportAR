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
    public class AnunciosRepository : IAnunciosRepository
    {
        public IEnumerable<AnunciosModel> GetAnuncios()
        {
            using (var context = new ApplicationDbContext())
            {
                var anuncios = context.Anuncios.ToArray();
                return anuncios;
            }
        }

        public void CreateAnuncio(AnunciosModel anuncio)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Anuncios.Add(anuncio);
                context.SaveChanges();

            }
        }

        public AnunciosModel GetAnuncio(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                var anuncio = context.Anuncios.FirstOrDefault(x => x.Id == id);
                return anuncio;
            }
        }

        public void UpdateAnuncio(AnunciosModel anuncio)
        {
            AnunciosModel anuncioEntity;

            using (var context = new ApplicationDbContext())
            {
                anuncioEntity = context.Anuncios.FirstOrDefault(x => x.Id == anuncio.Id);
            }

            anuncioEntity.Description = anuncio.Description;
            anuncioEntity.Name = anuncio.Name;

            using (var context = new ApplicationDbContext())
            {
                context.Entry(anuncioEntity).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteAnuncio(string id)
        {

            using (var context = new ApplicationDbContext())
            {
                var anuncioEntity = context.Anuncios.FirstOrDefault(x => x.Id == id);
                context.Entry(anuncioEntity).State = EntityState.Deleted;
                context.SaveChanges();
            }

        }
    }

    public static class AnunciosRepositoryExtensions
    {
        public static AnunciosViewModel AnunciosToViewModel(this AnunciosModel model)
        {

            return new AnunciosViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Date = model.Date
            };
        }
        
    }
}