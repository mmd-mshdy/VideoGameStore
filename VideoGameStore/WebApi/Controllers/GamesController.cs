using Microsoft.AspNetCore.Mvc;
using VideoGameStore.Application.Dtos;
using VideoGameStore.Application.Interfaces;

namespace VideoGameStore.WebUI.Controllers;

public class GamesController : Controller
{
    private readonly IGameService _service;

    public GamesController(IGameService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var games = await _service.GetAllAsync();
        return View(games);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateGameDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _service.AddAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var game = await _service.GetByIdAsync(id);
        if (game == null) return NotFound();

        return View(new UpdateGameDto
        (
             game.Name,
            game.Genre,
            game.Price,
            DateTime.Now
        ));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, UpdateGameDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
