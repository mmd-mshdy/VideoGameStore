using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameStore.Application.Transactions.Command.Rental;
using VideoGameStore.Application.Transactions.Command.Rentals;
using VideoGameStore.Domain.Authorization;

[Authorize (Roles=Roles.Customer)]
public sealed class RentalsController : ApiController
{
    private readonly IMediator _mediator;

    public RentalsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("rent")]
    public async Task<IActionResult> Rent(RentalGameCommand command)
    {
        var result = await _mediator.Send(command);
        return FromResult(result);
    }

    [HttpPost("return")]
    public async Task<IActionResult> Return(ReturnGameCommand command)
    {
        var result = await _mediator.Send(command);
        return FromResult(result);
    }
}