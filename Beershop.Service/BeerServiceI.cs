using Beershop.Repositories;
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
    public class BeerServiceI : IService<Beer>
    {
        private IDAO<Beer> _beerDAO;

        public BeerServiceI(IDAO<Beer> beerDAO)  // Dependency Injection
        {
            _beerDAO = beerDAO;
        }

        public async Task Add(Beer entity)
        {
            await _beerDAO.Add(entity);
        }

        public async Task Delete(Beer entity)
        {
            await _beerDAO.Delete(entity);
        }

        public async Task<Beer> FindById(int Id)
        {
            return await _beerDAO.FindById(Id);
        }

        public async Task<IEnumerable<Beer>> GetAll()
        {
            return await _beerDAO.GetAll();
        }

        public async Task Update(Beer entity)
        {
            await _beerDAO.Update(entity);
        }
    }
}
