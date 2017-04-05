using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaaS.Models;

namespace CaaS.Interfaces
{
    public interface IServiciosRepository
    {
        IEnumerable<ServiciosModel> GetServicios();

        void CreateServicio(ServiciosModel servicio);

        ServiciosModel GetServicio(string id);

        void UpdateServicio(ServiciosModel servicio);

        void DeleteServicio(string id);
    }
}
