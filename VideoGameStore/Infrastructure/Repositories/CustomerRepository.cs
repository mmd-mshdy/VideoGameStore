using Microsoft.EntityFrameworkCore;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Infrastructure.Data;

namespace VideoGameStore.Infrastructure.Repositories
{
    public class CustomerRepository : IGenericRepository<Customer>
    {
        private readonly VideoGamesContext _context;
        public CustomerRepository(VideoGamesContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer t)
        {
            await _context.Customers.AddAsync(t);

        }

        public async Task DeleteAsync(int id)
        {
          var customer= await _context.Customers.FirstOrDefaultAsync(x=>x.Id==id);
            if (customer is null) throw new InvalidOperationException("invalid");
            _context.Customers.Remove(customer);

        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
         return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            return Task.CompletedTask;
        }
    }
}
