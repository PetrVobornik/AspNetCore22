﻿using System.ComponentModel.DataAnnotations;

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

}
