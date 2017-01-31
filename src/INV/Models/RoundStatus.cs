using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace INV.Models
{
    public class RoundStatus
    {
        public int ID { get; set; }
        [Display(Name = "Round Status")]

        public string Title { get; set; }
    }
}
