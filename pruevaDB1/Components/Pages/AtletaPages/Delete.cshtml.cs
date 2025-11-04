using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pruevaDB1.Components.Model;
using pruevaDB1.Data;

namespace pruevaDB1.Components.Pages.AtletaPages
{
    public class DeleteModel : PageModel
    {
        private readonly pruevaDB1.Data.pruevaDB1Context _context;

        public DeleteModel(pruevaDB1.Data.pruevaDB1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Atleta Atleta { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atleta = await _context.Atleta.FirstOrDefaultAsync(m => m.IdAtleta == id);

            if (atleta == null)
            {
                return NotFound();
            }
            else
            {
                Atleta = atleta;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atleta = await _context.Atleta.FindAsync(id);
            if (atleta != null)
            {
                Atleta = atleta;
                _context.Atleta.Remove(Atleta);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
