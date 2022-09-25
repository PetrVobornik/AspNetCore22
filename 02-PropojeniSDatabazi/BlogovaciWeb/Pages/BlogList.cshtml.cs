using BlogovaciWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogovaciWeb.Pages
{
    public class BlogListModel : PageModel
    {
        readonly ApplicationDbContext DB;

        public List<BlogItem> BlogItems { get; set; }

        public BlogListModel(ApplicationDbContext db)
        {
            DB = db;
        }

        public async Task OnGetAsync()
        {
            BlogItems = await DB.Blog
                .OrderByDescending(x => x.Pripnuto)
                .ThenByDescending(x => x.Datum)
                .ToListAsync();
        }
    }
}
