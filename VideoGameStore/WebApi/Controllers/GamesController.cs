using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameStore.Application.Dtos;
using VideoGameStore.Infrastructure.Data;

namespace VideoGameStore.WebApi.Controllers
{
    public class GamesController : Controller
    {
        private readonly VideoGamesContext _context;

        public GamesController(VideoGamesContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameDtos.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameDto = await _context.GameDtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameDto == null)
            {
                return NotFound();
            }

            return View(gameDto);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Genre,Price,ReleaseDate")] GameDto gameDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameDto);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameDto = await _context.GameDtos.FindAsync(id);
            if (gameDto == null)
            {
                return NotFound();
            }
            return View(gameDto);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Genre,Price,ReleaseDate")] GameDto gameDto)
        {
            if (id != gameDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameDtoExists(gameDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gameDto);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameDto = await _context.GameDtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameDto == null)
            {
                return NotFound();
            }

            return View(gameDto);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameDto = await _context.GameDtos.FindAsync(id);
            if (gameDto != null)
            {
                _context.GameDtos.Remove(gameDto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameDtoExists(int id)
        {
            return _context.GameDtos.Any(e => e.Id == id);
        }
    }
}
