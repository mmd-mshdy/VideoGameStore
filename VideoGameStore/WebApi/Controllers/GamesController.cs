using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameStore.Application.Games.Command.Create;
using VideoGameStore.Application.Games.Command.Delete;
using VideoGameStore.Application.Games.Command.Update;
using VideoGameStore.Application.Games.Query.Get;

namespace VideoGameStore.WebUI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class GamesController : Controller
{
    private readonly ISender _sender;

    public GamesController(ISender sender)
    {
        _sender = sender;
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGameCommand dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var id = await _sender.Send(dto);
        return Ok(new { GameId = id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var game = await _sender.Send(new GetGameQuery(id));
        return game == null ? NotFound() : Ok(game);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit( [FromBody] UpdateGameCommand dto)
    {
        await _sender.Send(dto);
        return NoContent();

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _sender.Send(new DeleteGameCommand(id));
        return NoContent();
    }
}
