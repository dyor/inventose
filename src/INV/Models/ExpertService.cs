using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace INV.Models
{
    public class ExpertService
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Service Title cannot be longer than 100 characters.")]
        [Display(Name = "Service Title")]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }


        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }
        [Display(Name = "Max Days to Complete")]
        public int MaxDaysToComplete { get; set; }
        public ICollection<InvestmentRound> InvestmentRounds { get; set; }
        [Display(Name = "Is Published?")]
        public Boolean IsPublished { get; set; }
        [Display(Name = "Expert")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        [Display(Name = "Service Type")]
        public ServiceType ServiceType { get; set; }
        public int ServiceTypeID { get; set;  }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DateCreated { get; set; } = DateTime.Now;


    }
}
