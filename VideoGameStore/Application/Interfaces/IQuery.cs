using MediatR;
namespace VideoGameStore.Application.Interfaces
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
