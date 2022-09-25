using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication2.Pages
{
    public class DruhaModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Jmeno { get; set; }


        public void OnGet()
        {
            
        }
    }
}
