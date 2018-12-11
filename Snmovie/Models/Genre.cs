using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snmovie.Models
{
  public class Genre
  {
    public byte Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

  }
}
