using BlogovaciWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogovaciWeb.Pages
{
    public class BlogItemModel : PageModel
    {
        readonly ApplicationDbContext DB;

        public BlogItem Blog { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ItemId { get; set; }


        public BlogItemModel(ApplicationDbContext db)
        {
            DB = db;
        }

        public async Task OnGetAsync()
        {
            Blog = await DB.Blog
                .Include(nameof(BlogItem.Autor))
                .FirstOrDefaultAsync(x => x.Id == ItemId);
        }
    }
}
