using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebPocatek.Data;

namespace WebPocatek.Pages
{
    public class UkolyModel : PageModel
    {
        readonly ToDoDbContext DB;

        public UkolyModel(ToDoDbContext db)
        {
            DB = db;
        }

        public IList<Ukol> Ukoly { get; set; }

        public async Task OnGetAsync()
        {
            Ukoly = await DB.Ukoly
                .OrderBy(x => x.Hotovo)
                .ThenBy(x => x.Termin ?? DateTime.MaxValue)
                .ThenBy(x => x.Id)
                .ToListAsync();
        }
    }
}
