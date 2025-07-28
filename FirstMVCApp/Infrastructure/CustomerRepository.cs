using FirstMVCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstMVCApp.Infrastructure
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersByCountryAsync(string country);

        Task<IEnumerable<Customer>> GetCustomersByCityAsync(string city);

        Task<IEnumerable<Customer>> GetAllCustomersAsync(string criteria); 

    }
    public class CustomerRepository : ICustomerRepository
    {
        NorthwindDbContext _db; 
        ILogger<CustomerRepository> _logger;
        public CustomerRepository(
            ILogger<CustomerRepository> logger, 
            NorthwindDbContext db) 
            => (_logger, _db) = (logger, db);
        

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(string criteria)
        {
            var list = await _db.Customers
                .ToListAsync(); 
            return  list
                .Where(c => c.CompanyName.Contains(criteria, StringComparison.OrdinalIgnoreCase) ||
                             c.ContactName.Contains(criteria) ||
                             c.City.Contains(criteria) ||
                             c.Country.Contains(criteria))
                .ToList();
        }

        public async Task<IEnumerable<Customer>> GetCustomersByCityAsync(string city)
        {
            return await _db.Customers
                .Where(c => c.City.Contains(city))
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomersByCountryAsync(string country)
        {
            return await _db.Customers
                .Where(c => c.Country.ToLower().Contains(country.ToLower()))
                .ToListAsync();
        }
    }
}
