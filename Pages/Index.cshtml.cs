using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TA_Apricode.Data;

namespace TA_Apricode.Pages
{
    public class IndexModel(GameContext context) : PageModel
    {
        private readonly GameContext _context = context;

        public IList<Game> Games { get; set; } = default!;
        public Game NewGame { get; set; } = new Game();

        public async Task OnGetAsync()
        {
            Games = await _context.Games.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Games.Add(NewGame);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}