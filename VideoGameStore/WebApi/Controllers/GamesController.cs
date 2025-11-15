using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameStore.Application.Games.Command.Create;
using VideoGameStore.Application.Games.Command.Delete;
using VideoGameStore.Application.Games.Command.Update;
using VideoGameStore.Application.Games.Query.Get;

namespace VideoGameStore.WebUI.Controllers;
[Authorize]
public class GamesController : Controller
{
    private readonly ISender _sender;

    public GamesController(ISender sender)
    {
        _sender = sender;
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateGameCommand dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _sender.Send(dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> GetById(GetGameQuery query)
    {
        var game = await _sender.Send(query);
        if (game == null) return NotFound();

        return View(new GameDto
        (
             game.Name,
            game.Genre,
            game.Price,
            DateTime.Now
        ));
    }

    [HttpPut]
    public async Task<IActionResult> Edit( UpdateGameCommand dto)
    {
        await _sender.Send(dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteGameCommand command)
    {
        await _sender.Send(command);
        return RedirectToAction(nameof(Index));
    }
}
