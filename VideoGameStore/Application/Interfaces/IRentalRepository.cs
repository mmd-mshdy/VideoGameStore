using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental?> GetByIdAsync(int id);
        Task<IReadOnlyList<Rental>> GetActiveByCustomerIdAsync(int customerId);
        Task<IReadOnlyList<Rental>> GetOverdueAsync(DateTime now);
        Task Add(Rental rental);
    }
}
