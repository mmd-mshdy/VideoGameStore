using MediatR;
namespace VideoGameStore.Application.Interfaces
{
    public interface ICommand :IRequest
    {
    }
    public interface ICommand<T>: IRequest<T>
    {

    }
}
