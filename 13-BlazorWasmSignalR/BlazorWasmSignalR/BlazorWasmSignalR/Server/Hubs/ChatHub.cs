using BlazorWasmSignalR.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BlazorWasmSignalR.Server.Hubs;

[Authorize]
public class ChatHub : Hub
{
    readonly UserManager<ApplicationUser> userManager;

    public ChatHub(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task OdeslatZpravu(string adresat, string zprava)
    {
        var odesilatelId = Context.UserIdentifier;
        var odesilatelUsr = await userManager.FindByIdAsync(odesilatelId);

        var adresati = adresat.ToLower().Split(';')
            .Select(x => x.Trim())
            .Where(x => !String.IsNullOrEmpty(x));
        var adresatiId = await userManager.Users
            .Where(x => adresati.Contains(x.Email))
            .Select(x => x.Id).ToListAsync();
        adresatiId.Add(odesilatelId);

        await Clients.Users(adresatiId)
            .SendAsync("PrijmoutZpravu", odesilatelUsr?.Email, zprava);
    }
}
