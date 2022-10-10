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

        async Task LoadData()
        {
            Blog = await DB.Blog
                .Include(x => x.Autor)
                .Include(x => x.Komentare)
                .ThenInclude(x => x.Vlozil)
                .FirstOrDefaultAsync(x => x.Id == ItemId);
        }


        public async Task OnGetAsync()
        {
            await LoadData();
        }

        public async Task OnPostCommentAsync(string reakce, string text)
        {
            var currentUser = await DB.Users.AsNoTracking().FirstOrDefaultAsync(
                   x => x.UserName == User.Identity.Name);
            var komentar = new Komentar()
            {
                BlogId = ItemId,
                Datum = DateTime.Now,
                Text = text,
                VlozilId = currentUser.Id,
            };
            if (int.TryParse(reakce, out int reakceId))
                komentar.ReakceNaId = reakceId;

            await DB.AddAsync(komentar);
            await DB.SaveChangesAsync();

            await LoadData();
        }

        public async Task OnPostDeleteAsync(string id)
        {
            if (int.TryParse(id, out int idKomentare))
            {
                var toDelete = new List<Komentar>();
                var komentar = await DB.Komentare.FindAsync(idKomentare);
                toDelete.Add(komentar);

                var reakce = await DB.Komentare
                    .Where(x => x.ReakceNaId == komentar.Id)
                    .ToListAsync();
                while (reakce.Count > 0)
                {
                    toDelete.AddRange(reakce);
                    var lastIds = reakce.Select(x => x.Id).ToArray();
                    reakce = await DB.Komentare
                        .Where(x => lastIds.Contains(x.ReakceNaId??-1))
                        .ToListAsync();
                }

                DB.Komentare.RemoveRange(toDelete);
                await DB.SaveChangesAsync();
            }

            await LoadData();
        }
    }
}
