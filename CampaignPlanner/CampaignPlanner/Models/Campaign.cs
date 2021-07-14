using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CampaignPlanner.Models
{
    public class Campaign
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public List<Keyword> Keywords { get; set; } = new List<Keyword>();
        [Required]
        public double BidAmount { get; set; }
        [Required]
        public double CampaignFund { get; set; }
        [Required]
        public bool Status { get; set; }
        public Town Town { get; set; }
        [Required]
        public int Radius { get; set; }
    }
}
