using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogovaciWeb.Data;

public class BlogUser : IdentityUser<int>
{
    [Required]
    [MaxLength(50)]
    public string NickName { get; set; }
}

public class BlogRole : IdentityRole<int>
{
    public BlogRole() { }
    public BlogRole(string name) : base(name) { }
}
