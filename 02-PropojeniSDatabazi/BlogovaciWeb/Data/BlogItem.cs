using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogovaciWeb.Data;

public record BlogItem
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(140)]
    public string Nadpis { get; set; }

    [Required]
    public DateTime Datum { get; set; } = DateTime.Now;

    [MaxLength(255)]
    public string Perex { get; set; }

    [DataType(DataType.MultilineText), Display(Name = "Text příspěvku")]
    public string Prispevek { get; set; }

    [Display(Name = "Připnuto")]
    public bool Pripnuto { get; set; }


    public IdentityUser Autor { get; set; }
    
    public string AutorId { get; set; }

    public ICollection<Komentar> Komentare { get; set; }

}
