using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using pruevaDB1.Components.Model;
using pruevaDB1.Data;

namespace pruevaDB1.Components.Pages.CarreraPages
{
    public class CreateModel : PageModel
    {
        private readonly pruevaDB1.Data.pruevaDB1Context _context;

        public CreateModel(pruevaDB1.Data.pruevaDB1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Carrera Carrera { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Carrera.Add(Carrera);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
