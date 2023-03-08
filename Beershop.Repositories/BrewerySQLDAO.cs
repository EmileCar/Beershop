using Beershop.Repositories.Interfaces;
using BeerStore.Models.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beershop.Repositories
{
    // DIT IS EEN OUTDATED MANIER VAN DATA OPHALEN
    public class BrewerySQLDAO : IDAO<Brewery>
    {
        private string connstring = "Server=.\\SQL19_VIVES; Database=BierSQL;Trusted_Connection=True;";

        private readonly SqlConnection conn;

        public BrewerySQLDAO()
        {
            conn = new SqlConnection(connstring);
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
            await conn.OpenAsync();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Breweries", conn);
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<Brewery> breweryObjects = new List<Brewery>();
            while (await reader.ReadAsync())
            {
                Brewery brewery = new Brewery();
                brewery.Brouwernr = Convert.ToInt16(reader["Brouwernr"]);
                brewery.Naam = reader["Naam"].ToString() ?? string.Empty; // Null-Coalescing Operator
                //  It will return the value of its left-hand operand if it is not null. If it is null, then it will evaluate the right-hand operand and returns its result.
                brewery.Adres = reader["Adres"].ToString() ?? string.Empty; ;
                brewery.Postcode = reader["Postcode"].ToString() ?? string.Empty; ;
                brewery.Gemeente = reader["Gemeente"].ToString() ?? string.Empty; ;
                breweryObjects.Add(brewery);

            }
            conn.Close();
            return breweryObjects;
        }

        public Task Update(Brewery entity)
        {
            throw new NotImplementedException();
        }
    }
}
