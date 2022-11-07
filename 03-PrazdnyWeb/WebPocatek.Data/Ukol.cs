using System.ComponentModel.DataAnnotations;

namespace WebPocatek.Data;

public record Ukol
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(140)]
    public string Nazev { get; set; }

    [MaxLength]
    public string? Popis { get; set; }

    public DateTime? Termin { get; set; }

    public bool Hotovo { get; set; } = false;
}