using VideoGameStore.Domain.Entities;
namespace VideoGameStore.Application.Interfaces
{
    public interface IGenericRepository<T>
       
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync (T t);
        Task UpdateAsync(T t);
        Task DeleteAsync(int id);
        
    }
}
