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
    public class ReportesRepository : IReportesRepository
    {
        public IEnumerable<ReporteModel> GetReportes()
        {
            using (var context = new ApplicationDbContext())
            {
                var reportes = context.Reportes.ToArray();
                return reportes;
            }
        }

        public void CreateReporte(ReporteModel servicio)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Reportes.Add(servicio);
                context.SaveChanges();
            }
        }

        public ReporteModel GetReporte(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                var reporte = context.Reportes.FirstOrDefault(x => x.Id == id);
                return reporte;
            }
        }

        public void UpdateReporte(ReportesViewEditModel reporte)
        {
            ReporteModel reporteEntity;

            using (var context = new ApplicationDbContext())
            {
                reporteEntity = context.Reportes.FirstOrDefault(x => x.Id == reporte.Id);
            }

            reporteEntity.Comentario = reporte.Comentario;
           
            using (var context = new ApplicationDbContext())
            {
                context.Entry(reporteEntity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void AsignarOng(string reporteId, string ongId, string comentario)
        {
            ReporteModel reporteEntity;

            using (var context = new ApplicationDbContext())
            {
                reporteEntity = context.Reportes.FirstOrDefault(x => x.Id == reporteId);

            }

            reporteEntity.OngAsignada = ongId;
            reporteEntity.Estado = 1;
            reporteEntity.Comentario = comentario;

            using (var context = new ApplicationDbContext())
            {
                context.Entry(reporteEntity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteReporte(string id)
        {

          
            using (var context = new ApplicationDbContext())
            {
                var reporteEntity = context.Reportes.FirstOrDefault(x => x.Id == id);
                context.Entry(reporteEntity).State = EntityState.Deleted;
                context.SaveChanges();
            }

        }

        public void CerrarReporte(string id)
        {
            ReporteModel reporteEntity;

            using (var context = new ApplicationDbContext())
            {
                reporteEntity = context.Reportes.FirstOrDefault(x => x.Id == id);
            }

            reporteEntity.Estado = 1;

            using (var context = new ApplicationDbContext())
            {
                context.Entry(reporteEntity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }

    public static class ReportesRepositoryExtensions
    {
        public static ReportesViewModel ReporteToViewModel(this ReporteModel model)
        {

            return new ReportesViewModel
            {
                Id = model.Id,
                OngAsignada = model.OngAsignada,
                Comentario = model.Comentario,
                DateReported = model.DateReported,
                Desc = model.Desc,
                ReportedBy = model.ReportedBy,
                UrlPic = model.UrlPic,
                Latitud = model.Latitud.ToString(),
                Longitud = model.Longitud.ToString(),
                Direccion = model.Direccion
            };
        }
        
    }
}