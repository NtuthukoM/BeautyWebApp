using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyWebApp.Models
{
    public class PromotionAttendee
    {
        public int Id { get; set; }
        public int PromotionId { get; set; }
        public int AttendeeId { get; set; }
    }
}