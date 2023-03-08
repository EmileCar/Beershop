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
    public class BeerDAOI : IDAO<Beer>
    {
        private readonly BeerDbContext _dbContext;  // Namespace using BierSQL.Domein.Entities; toevoegen bovenaan

        public BeerDAOI()
        {
            _dbContext = new BeerDbContext();
        }
        public async Task Add(Beer entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            try
            {

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task Delete(Beer entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Beer> FindById(int Id)
        {
            try
            {
                return await _dbContext.Beers.Where(b => b.Biernr == Id).Include(b => b.BrouwernrNavigation).Include(b => b.SoortnrNavigation).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { 
                throw new Exception("error DAO beer"); 
            }
        }

        public async Task<IEnumerable<Beer>> GetAll()
        {
            try
            {// select * from Bieren
                return await _dbContext.Beers
                    .Include(b => b.BrouwernrNavigation)
                    .Include(b => b.SoortnrNavigation)
                    .ToListAsync(); // volgende Namespaces toevoegen bovenaan using System.Linq; using Microsoft.EntityFrameworkCore;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in DAO");
                throw;

            }
        }

        public async Task Update(Beer entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
