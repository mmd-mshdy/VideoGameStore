using Microsoft.EntityFrameworkCore;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Infrastructure.Data;
namespace VideoGameStore.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly VideoGamesContext _context;
        public RentalRepository(VideoGamesContext context)
        {
            _context = context;
        }

        public async Task Add(Rental rental)
        {
           await _context.Rentals.AddAsync(rental);
        }

        public async Task<IReadOnlyList<Rental>> GetActiveByCustomerIdAsync(int customerId)
        {
            return await _context.Rentals
                .Where(r => r.CustomerId == customerId && r.ReturnedAt == null)
                .ToListAsync();

        }

        public async Task<Rental?> GetByIdAsync(int id)
        {
            return await _context.Rentals
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IReadOnlyList<Rental>> GetOverdueAsync(DateTime now)
        {
            return await _context.Rentals
                .Where(r =>
                    r.ReturnedAt == null &&
                    r.DueDate < now)
                .ToListAsync();
        }
    
    }
}
