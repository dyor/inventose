using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace INV.Models
{
    public class Investment
    {

        public int ID { get; set; }
        
        public virtual InvestmentRound InvestmentRound { get; set; }
        [Display(Name = "Investor")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Investment Amount")]
        public decimal InvestmentAmount { get; set; }
        //Basic Logic: investor proposes an investment, inventor accepts or rejects, and if accepted we track investment until inventor receives investment 
        [Display(Name = "Has Investment Been Accepted?")]
        public Boolean IsInvestmentAccepted { get; set; }
        [Display(Name = "Has Investment Been Received?")]
        public Boolean IsInvestmentReceived { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Display(Name = "Date Accepted")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DateAccepted { get; set; }

        [Display(Name = "Date Rejected")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DateRejected { get; set; }

        [Display(Name = "Date Received")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DateReceived { get; set; }



    }
}
