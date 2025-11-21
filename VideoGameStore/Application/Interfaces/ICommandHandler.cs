using MediatR;

namespace VideoGameStore.Application.Interfaces
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    {
    }
    public  interface ICommandHandler<TCommand , T> : IRequestHandler<TCommand ,T>
        where TCommand : ICommand<T>
    { }
}
