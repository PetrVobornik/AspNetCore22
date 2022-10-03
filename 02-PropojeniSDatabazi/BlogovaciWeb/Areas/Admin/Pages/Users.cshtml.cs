using BlogovaciWeb.Code;
using BlogovaciWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogovaciWeb.Areas.Admin.Pages
{
    public class UsersModel : PageModel
    {
        readonly ApplicationDbContext DB;
        readonly UserManager<IdentityUser> userManager;
        readonly RoleManager<IdentityRole> roleManager;

        public List<UserDataForAdmin> UsersList { get; set; }

        public UsersModel(ApplicationDbContext db,
                    UserManager<IdentityUser> userManager,
                    RoleManager<IdentityRole> roleManager)
        {
            DB = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task OnGetAsync()
        {
            await LoadUsers();
        }

        public async Task<IActionResult> OnPostRoleAsync(string user, string role)
        {
            var userDb = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == user);
            if (userDb != null)
            {
                bool hasRole = await userManager.IsInRoleAsync(userDb, role);
                if (hasRole)
                {
                    if (role != Seed.AdminRoleName || (await userManager.GetUsersInRoleAsync(role)).Count > 1)
                    await userManager.RemoveFromRoleAsync(userDb, role);
                } else 
                    await userManager.AddToRoleAsync(userDb, role);
            }
            await LoadUsers();
            return Page();
        }

        public async Task<IActionResult> OnPostBanAsync(string user, string ban)
        {
            var userDb = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == user);
            if (userDb != null)
            {
                var end = userDb.LockoutEnd;
                switch (ban)
                {
                    case "kick": end = (userDb.LockoutEnd ?? DateTimeOffset.Now).AddDays(1); break;
                    case "ban": end = DateTimeOffset.MaxValue; break;
                    case "cancel": end = null; break;
                }
                await userManager.SetLockoutEndDateAsync(userDb, end);
                if (end != null && end > DateTimeOffset.Now)
                    await userManager.UpdateSecurityStampAsync(userDb);
            }
            await LoadUsers();
            return Page();
        }

        private async Task LoadUsers()
        {
            var roles = roleManager.Roles.Select(x => x.Name);
            var usersInRole = new Dictionary<string, string[]>();
            foreach (var role in roles)
                usersInRole.Add(role, (await userManager.GetUsersInRoleAsync(role))
                    .Select(x => x.UserName).ToArray());

            UsersList = await userManager.Users.Select(x => new UserDataForAdmin()
            {
                UserName = x.UserName,
                Email = x.Email,
                BanEnds = x.LockoutEnd
            }).ToListAsync();

            foreach (var user in UsersList)
                user.Roles = usersInRole.Where(x => x.Value.Contains(user.UserName))
                    .Select(x => x.Key).ToArray();
        }

        public class UserDataForAdmin
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public DateTimeOffset? BanEnds { get; set; }
            public string[] Roles { get; set; }
        }
    }
}
