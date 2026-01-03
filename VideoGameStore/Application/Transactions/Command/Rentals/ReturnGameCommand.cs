using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
namespace VideoGameStore.Application.Transactions.Command.Rentals
{
    public record ReturnGameCommand (int id) : ICommand<Result<Domain.Entities.Rental>>;
}
