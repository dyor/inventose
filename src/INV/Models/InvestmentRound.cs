using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace INV.Models
{
    public class InvestmentRound
    {
        public int ID { get; set;  }
        public string Title { get; set;  }
        public virtual Invention Invention { get; set; }
        public int InventionID { get; set; }
        [Display(Name = "Expert Service")]
        public virtual ExpertService ExpertService { get; set;  }
        public int ExpertServiceID { get; set;  }
        
        public virtual ICollection<Investment> Investments { get; set; }
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Display(Name ="Is Open for Funding?")]
        public Boolean IsOpenForFunding { get; set; }

        public virtual RoundStatus RoundStatus { get; set;  }

        public int RoundStatusID { get; set;  }

        //flow is : Open, Subscribed (enough cash has been pledged), FundingSettled (full amount of capital received), ServiceSettled (capital sent to expert after completion of service)

        [DataType(DataType.Currency)]
        public Decimal RaiseAmount { get; set;  }

    }
}
