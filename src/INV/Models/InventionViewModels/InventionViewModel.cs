using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INV.Models.InventionViewModels
{
    public class InventionViewModel
    {
        public Invention Invention {get; set; }
        public ExpertService ExpertService { get; set;  }
        
    }
}
