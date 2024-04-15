using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cuevana.Models;

public partial class Movie
{
    public int Id { get; set; } = 0; 

    [Display(Name = "Titulo")]
    public string ?Title { get; set; } = null!;
    public DateTime ?ReleaseYear { get; set; }= DateTime.MinValue;

    [Display(Name = "Imagen")]
    public string ?Movieimage { get; set; } = null!;

    [Display(Name = "Reparto")]
    public string ?Casting { get; set; } = null!;
  
    [Display(Name = "Director")]
    public string ?Director { get; set; } = null!;

    [Display(Name = "Genero")]
    public int ?GenreId { get; set; } = 0;

    public string ?Link { get; set; } = null!;
    public int ?UserId { get; set; } = 0;
    public virtual Genre? Genre { get; set; } = null!;
    public virtual User? User { get; set; } = null!;
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
