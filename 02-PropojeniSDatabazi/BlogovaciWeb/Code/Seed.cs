using BlogovaciWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogovaciWeb.Code
{
    public class Seed
    {
        public const string EditorRoleName = "editor";
        public const string AdminRoleName = "admin";

        readonly ApplicationDbContext DB;
        readonly UserManager<IdentityUser> userManager;
        readonly RoleManager<IdentityRole> roleManager;

        public Seed(ApplicationDbContext db,
                    UserManager<IdentityUser> userManager,
                    RoleManager<IdentityRole> roleManager)
        {
            DB = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void Initialize()
        {
            DB.Database.MigrateAsync().Wait();
            InitUsers().Wait();
        }

        async Task InitUsers()
        {
            // Přidání rolí
            var adminRole = await FindOrAddRole(AdminRoleName);
            await FindOrAddRole(EditorRoleName);

            // Přidání výchozího uživatele
            var adminUser = await userManager.FindByNameAsync("admin@neco.cz");
            if (adminUser == null)
            {
                adminUser = new IdentityUser()
                {
                    UserName = "admin@neco.cz",
                    Email = "admin@neco.cz",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "123456");
            }

            // Přidání role admina uživateli
            bool isAdmin = await userManager.IsInRoleAsync(adminUser, AdminRoleName);
            if (!isAdmin)
                await userManager.AddToRoleAsync(adminUser, AdminRoleName);
        }

        async Task<IdentityRole> FindOrAddRole(string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                await roleManager.CreateAsync(role);
            }
            return role;
        }
    }
}
