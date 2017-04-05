using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaaS.Models;

namespace CaaS.Interfaces
{
    public interface IAnunciosRepository
    {
        IEnumerable<AnunciosModel> GetAnuncios();

        void CreateAnuncio(AnunciosModel anuncio);

        AnunciosModel GetAnuncio(string id);

        void UpdateAnuncio(AnunciosModel anuncio);

        void DeleteAnuncio(string id);
    }
}
