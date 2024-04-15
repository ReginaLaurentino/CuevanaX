using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cuevana.Models;

public partial class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nombre de usuario es obligatorio")]
    [StringLength(255)]
    [Display(Name = "Nombre")]    
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Nombre de Uusario es obligatorio")]
    [StringLength(255)]
    [Display(Name = "Usuario")]
    public string UserName { get; set; } = null!;
    [Required(ErrorMessage = "La contraseña es  obligatoria")]
    [StringLength(255)]
    [Display(Name = "Contraseña")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "Rol es obligatorio")]
    [StringLength(255)]
    [Display(Name = "Rol")]
    public string Role { get; set; } = null!;

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
