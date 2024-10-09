using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TA_Apricode.Data;

namespace TA_Apricode.Pages
{
    public class EditModel(GameContext context) : PageModel
    {
        private readonly GameContext _context = context;

        [BindProperty]
        public Game Game { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            Game = game;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var gameToUpdate = await _context.Games.FindAsync(id);
            if (gameToUpdate == null)
            {
                return NotFound();
            }

            gameToUpdate.Title = Game.Title;
            gameToUpdate.Developer = Game.Developer;
            gameToUpdate.Genres = Game.Genres.Select(g => g.Trim()).ToList();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(Game.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}