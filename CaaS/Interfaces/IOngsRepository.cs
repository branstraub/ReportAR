using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaaS.Models;
using CaaS.Models.BVModels;

namespace CaaS.Interfaces
{
    public interface IOngsRepository
    {
        IEnumerable<OngModel> GetOngs();

        void CreateOng(OngModel ong);

        OngModel GetOng(string id);

        void DeleteOng(string id);
     
    }
}
