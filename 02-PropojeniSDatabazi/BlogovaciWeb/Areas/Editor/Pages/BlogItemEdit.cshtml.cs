using BlogovaciWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogovaciWeb.Areas.Editor.Pages
{
    public class BlogItemEditModel : PageModel
    {
        readonly ApplicationDbContext DB;

        [BindProperty(SupportsGet = true)]
        public int ItemId { get; set; }

        [BindProperty]
        public BlogItem Input { get; set; }


        public BlogItemEditModel(ApplicationDbContext db)
        {
            DB = db;
        }

        public async Task OnGetAsync()
        {
            if (ItemId == 0)
                Input = new BlogItem();
            else
                Input = await DB.Blog.FindAsync(ItemId);
        }

        public async Task OnPostAsync()
        {
            if (Input == null || !TryValidateModel(Input))
                return;

            if (ItemId == 0)
                await DB.AddAsync(Input);
            else if (Input.Id > 0)
                DB.Update(Input);
            else if (Input.Id < 0)
            {
                Input.Id = ItemId;
                DB.Remove(Input);
            }

            await DB.SaveChangesAsync();

            Response.Redirect("/blog");
        }
    }
}
