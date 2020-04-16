using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BeautyWebApp.Models
{
    public class Attendee
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name ="Surname")]
        public string LastName { get; set; }
    }
}