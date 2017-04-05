using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaaS.Models;

namespace CaaS.Interfaces
{
    public interface ILugaresRepository
    {
        IEnumerable<LugaresModel> GetLugares();

        void CreateLugar(LugaresModel lugar);

        LugaresModel GetLugar(string id);

        void UpdateLugar(LugaresModel lugar);

        void DeleteLugar(string id);
    }
}
