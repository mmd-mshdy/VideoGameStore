namespace VideoGameStore.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Games {get;}
        Task<int> CompleteAsync();
    }
}
