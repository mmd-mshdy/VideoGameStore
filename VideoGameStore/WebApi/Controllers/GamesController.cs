using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameStore.Application.Games.Command.Create;
using VideoGameStore.Application.Games.Command.Delete;
using VideoGameStore.Application.Games.Command.Update;
using VideoGameStore.Application.Games.Query.Get;
using VideoGameStore.Application.Games.Query.GetAll;

public sealed class GamesController : ApiController
{
    private readonly IMediator _mediator;

    public GamesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => FromResult(await _mediator.Send(new GetAllGamesQuery()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => FromResult(await _mediator.Send(new GetGameQuery(id)));

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Create(CreateGameCommand command)
        => FromResult(await _mediator.Send(command));

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Update(
        int id,
        UpdateGameCommand command)
        => FromResult(await _mediator.Send(command with { id = id }));

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Delete(int id)
        => FromResult(await _mediator.Send(new DeleteGameCommand(id)));
}