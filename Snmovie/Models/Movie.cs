using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snmovie.Models
{
  public class Movie
  {
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    public Genre Genre { get; set; }

    [Display(Name = "Genre")]
    [Required]
    public byte GenreId { get; set; }

    [Display(Name ="DateAdded")]
    public DateTime DateAdded { get; set; }

    [Display(Name ="Relese Date")]
    public DateTime ReleaseDate { get; set; }

    [Display(Name ="Number In Stock")]
    [Range(1, 20)]
    public byte NumberInStock { get; set; }

  }
}
