using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pruevaDB1.Components.Model;
using pruevaDB1.Data;

namespace pruevaDB1.Components.Pages.CarreraPages
{
    public class DeleteModel : PageModel
    {
        private readonly pruevaDB1.Data.pruevaDB1Context _context;

        public DeleteModel(pruevaDB1.Data.pruevaDB1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Carrera Carrera { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carrera.FirstOrDefaultAsync(m => m.IdCarrera == id);

            if (carrera == null)
            {
                return NotFound();
            }
            else
            {
                Carrera = carrera;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carrera.FindAsync(id);
            if (carrera != null)
            {
                Carrera = carrera;
                _context.Carrera.Remove(Carrera);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
