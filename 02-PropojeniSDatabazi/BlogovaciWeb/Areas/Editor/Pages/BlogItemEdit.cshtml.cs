using BlogovaciWeb.Code;
using BlogovaciWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogovaciWeb.Areas.Editor.Pages
{
    public class BlogItemEditModel : PageModel
    {
        readonly ApplicationDbContext DB;

        [BindProperty(SupportsGet = true)]
        public int ItemId { get; set; }

        [BindProperty]
        public BlogItem Input { get; set; }

        public string Autor { get; set; }


        public BlogItemEditModel(ApplicationDbContext db)
        {
            DB = db;
        }

        public async Task OnGetAsync()
        {
            if (ItemId == 0)
            {
                Input = new BlogItem();
                Autor = User.Identity.Name;
            }
            else
            {
                Input = await DB.Blog.Include(nameof(BlogItem.Autor))
                    .FirstOrDefaultAsync(x => x.Id == ItemId);
                Autor = Input.Autor?.UserName ?? "?";
            }
        }

        public async Task OnPostAsync()
        {
            var currentUser = await DB.Users.AsNoTracking().FirstOrDefaultAsync(
                    x => x.UserName == User.Identity.Name);
            if (ItemId == 0)
            {
                Input.AutorId = currentUser.Id;
            } else
            {
                var item = await DB.Blog
                    .AsNoTracking()
                    .Include(nameof(BlogItem.Autor))
                    .FirstOrDefaultAsync(x => x.Id == ItemId);
                Input.Autor = item.Autor ?? currentUser;
                if (Input.Autor.UserName != currentUser.UserName &&
                    !User.IsInRole(Seed.AdminRoleName))
                {
                    ModelState.AddModelError("Autor", "Autora nelze mìnit");
                    return;
                }
            }
            ModelState.Clear();
            if (Input == null || !TryValidateModel(Input))
                return;

            bool smazat = false;
            if (ItemId == 0)
                await DB.AddAsync(Input);
            else if (Input.Id > 0)
                DB.Update(Input);
            else if (Input.Id < 0)
            {
                Input.Id = ItemId;
                DB.Remove(Input);
                smazat = true;
            }

            await DB.SaveChangesAsync();

            Response.Redirect(smazat ? "/blog" : $"/blog/{Input.Id}");
        }
    }
}
