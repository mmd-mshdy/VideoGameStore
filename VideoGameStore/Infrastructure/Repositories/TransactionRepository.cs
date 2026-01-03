using Microsoft.EntityFrameworkCore;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Infrastructure.Data;

namespace VideoGameStore.Infrastructure.Repositories;

public class TransactionRepository : GenericRepository<Transaction>
{
    public TransactionRepository(VideoGamesContext context) : base(context) { }

    public async Task<IEnumerable<Transaction>> GetByCustomerIdAsync(int customerId)
        => await _context.Transactions
            .Where(t => t.CustomerId == customerId)
            .ToListAsync();
}
