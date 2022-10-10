using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogovaciWeb.Data;

public record Komentar
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Datum { get; set; } = DateTime.Now;

    [DataType(DataType.MultilineText)]
    public string Text { get; set; }


    public BlogItem Blog { get; set; }
    public int BlogId { get; set; }


    public IdentityUser Vlozil { get; set; }
    public string VlozilId { get; set; }

    public Komentar ReakceNa { get; set; }
    public int? ReakceNaId { get; set; }
}
