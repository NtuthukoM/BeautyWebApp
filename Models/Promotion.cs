using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyWebApp.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public int PromotionCreatorId { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Venue { get; set; }
        public string PromotionType { get; set; }
        public string Cost { get; set; }
        public DateTime PromotionDate { get; set; }

        public virtual List<PromotionCreator> PromotionCreators { get; set; }
    }
}