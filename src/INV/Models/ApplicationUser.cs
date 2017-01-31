using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace INV.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Display(Name ="Username")]
        [StringLength(450)]
        public override string UserName { get; set;  }
        [Display(Name = "Phone Number")]
        public override string PhoneNumber { get; set; }

        [Display(Name="First Name")]
        public string FirstName { get; set;  }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "LinkedIn URL")]
        public string LinkedInURL { get; set; }
        [Display(Name = "Youtube Intro URL")]
        public string YouTubeIntro { get; set; }
        [Display(Name = "Web Address")]
        public string WebAddress { get; set;  }
        [Display(Name = "Twitter URL")]
        public string TwitterURL { get; set;  }
        [Column(TypeName = "ntext")]
        [Display(Name = "Bio")]
        public string LongDescription { get; set; }

        public ICollection<Invention> Inventions { get; set; }
        public ICollection<ExpertService> ExpertServices { get; set; }

        public ICollection<Investment> Investments { get; set; }
    }
}
