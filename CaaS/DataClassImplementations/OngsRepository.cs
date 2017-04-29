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
    public class OngsRepository : IOngsRepository
    {
        public IEnumerable<OngModel> GetOngs()
        {
            using (var context = new ApplicationDbContext())
            {
                var ongs = context.Ongs.ToArray();
                return ongs;
            }
        }

        public void CreateOng(OngModel ong)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Ongs.Add(ong);
                context.SaveChanges();
            }
        }

        public OngModel GetOng(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                var ong = context.Ongs.FirstOrDefault(x => x.Id == id && x.Locked == false);
                return ong;
            }
        }

     
        public void DeleteOng(string id)
        {
            
            using (var context = new ApplicationDbContext())
            {
                var ong = context.Ongs.FirstOrDefault(x => x.Id == id);
                context.Entry(ong).State = EntityState.Deleted;
                context.SaveChanges();
            }

        }

   
    }
    
    }
