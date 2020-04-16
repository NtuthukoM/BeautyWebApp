using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyWebApp.ViewModels
{
    public class PromotionSummaryVM
    {
        public int id { get; set; }
        public string date { get; set; }
        public string venue { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}