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
    public class IndexModel : PageModel
    {
        private readonly pruevaDB1.Data.pruevaDB1Context _context;

        public IndexModel(pruevaDB1.Data.pruevaDB1Context context)
        {
            _context = context;
        }

        public IList<Atleta> Atleta { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Atleta = await _context.Atleta.ToListAsync();
        }
    }
}
