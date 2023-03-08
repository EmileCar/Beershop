using Beershop.Repositories.Interfaces;
using BeerStore.Models.Data;
using BeerStore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beershop.Repositories
{
    public class BreweryDAOI : IDAO<Brewery>
    {
        private readonly BeerDbContext _dbContext;  // Namespace using BierSQL.Domein.Entities; toevoegen bovenaan

        public BreweryDAOI()
        {
            _dbContext = new BeerDbContext();
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
            try
            {
                return await _dbContext.Breweries.ToListAsync(); // volgende Namespaces toevoegen bovenaan using System.Linq; using Microsoft.EntityFrameworkCore;
            }
            catch (Exception ex)
            { 
                throw; 
            }
        }

        public Task Update(Brewery entity)
        {
            throw new NotImplementedException();
        }
    }
}
