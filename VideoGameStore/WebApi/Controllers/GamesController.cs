using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameStore.Application.Dtos;
using VideoGameStore.Application.Services;

namespace VideoGameStore.WebApi.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameService _service;

        public GamesController(GameService service)
        {
            _service = service;
        }

        // GET: Games
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetGameAsync());
        }

    }
}
