using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace INV.Models
{
    public class ServiceType
    {
        public int ID { get; set; }
        [Display(Name = "Service Type")]

        public string Title { get; set;  }

    }
}
