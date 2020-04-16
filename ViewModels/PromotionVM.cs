using BeautyWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyWebApp.ViewModels
{
    public class PromotionVM
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int? Duration { get; set; }
        [Required]
        public string Venue { get; set; }
        [Required]
        [Display(Name = "Promotion Type")]
        public string PromotionType { get; set; }
        [Required]
        public string Cost { get; set; }
        [Required]
        public DateTime? PromotionDate { get; set; }

        //Promotion Creator:
        public int? PromotionCreatorId { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }

        public List<Attendee> Attendees { get; set; }

    }
}