using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace INV.Models
{
    public class Invention 
    {

        public int ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        [Display(Name = "Invention Title")]
        public string Title { get; set; }
        [Display(Name = "Valuation")]
        [DataType(DataType.Currency)]
        public decimal Valuation { get; set; }
        //Once it is for sale, list it on inventions for sale
        [Display(Name = "Is Invention for Sale?")]
        public Boolean IsForSale { get; set; }
        [Display(Name = "Is Open for Funding?")]
        public Boolean IsOpenForFunding {
            get; set;
        }

        

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        [Display(Name = "Is Published?")]
        public Boolean IsPublished { get; set; }
        [Column(TypeName = "ntext")]
        [Display(Name = "Plan to Refine then Sell")]
        public string LongDescription { get; set; }
        public ICollection<InvestmentRound> InvestmentRounds { get; set; }
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }


}
