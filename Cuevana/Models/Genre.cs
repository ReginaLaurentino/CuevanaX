using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cuevana.Models;

public partial class Genre
{
    public int Id { get; set; } = 0;


    [Required(ErrorMessage = "Nombre de Genero es obligatorio")]
    [StringLength(255)]
    [Display(Name = "Genero")]
    public string GenreName { get; set; } = null!;

    public int UserId { get; set; } = 0;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual User? User { get; set; } = null!;
}
