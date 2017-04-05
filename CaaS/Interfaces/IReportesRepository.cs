using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaaS.Models;
using CaaS.Models.BVModels;

namespace CaaS.Interfaces
{
    public interface IReportesRepository
    {
        IEnumerable<ReporteModel> GetReportes();

        void CreateReporte(ReporteModel reporte);

        ReporteModel GetReporte(string id);

        void UpdateReporte(ReportesViewEditModel reporte);

        void AsignarOng(string reporteId, string ongId);

        void DeleteReporte(string id);
    }
}
