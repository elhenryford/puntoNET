using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pruevaDB1.Components.Model;
using pruevaDB1.Data;

namespace pruevaDB1.Components.Pages.AtletaPages
{
    public class EditModel : PageModel
    {
        private readonly pruevaDB1.Data.pruevaDB1Context _context;

        public EditModel(pruevaDB1.Data.pruevaDB1Context context)
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

            var atleta =  await _context.Atleta.FirstOrDefaultAsync(m => m.IdAtleta == id);
            if (atleta == null)
            {
                return NotFound();
            }
            Atleta = atleta;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Atleta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtletaExists(Atleta.IdAtleta))
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

        private bool AtletaExists(int id)
        {
            return _context.Atleta.Any(e => e.IdAtleta == id);
        }
    }
}
