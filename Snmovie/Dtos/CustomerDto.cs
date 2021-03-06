using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Snmovie.Models;


namespace Snmovie.Dtos
{
  public class CustomerDto
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter customer's name.")]
    [StringLength(255)]
    public string Name { get; set; }

    public bool IsSubscribedToNewsletter { get; set; }

    public byte MembershipTypeId { get; set; }

    //[Min18YearsForMembership]
    public DateTime? Birthdate { get; set; }
  }
}
