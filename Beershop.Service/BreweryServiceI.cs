using Beershop.Repositories.Interfaces;
using Beershop.Service.Interfaces;
using BeerShop.Repositories;
using BeerStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beershop.Service
{
    public class BreweryServiceI : IService<Brewery>
    {
        private IDAO<Brewery> _breweryDAO;

        public BreweryServiceI(IDAO<Brewery> breweryDAO)  // Dependency Injection
        {
            _breweryDAO = breweryDAO;
        }

        public Task Add(Brewery entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Brewery entity)
        {
            throw new NotImplementedException();
        }

        public Task<Brewery> FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Brewery>> GetAll()
        {
            return await _breweryDAO.GetAll();
        }

        public Task Update(Brewery entity)
        {
            throw new NotImplementedException();
        }
    }
}
