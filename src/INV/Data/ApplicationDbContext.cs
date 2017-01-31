using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using INV.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace INV.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // ModelBuilder.Entity<ExpertService>().Property(e => e.).HasColumnType("text");
            //builder.Entity<Investment>()
            //    .HasOne<InvestmentRound>()
            //    .WithMany()
            //    //.HasForeignKey(i => i.InvestmentRound)
            //    .OnDelete(DeleteBehavior.Cascade);

                builder.Entity<ApplicationUser>().HasIndex(au => au.UserName).IsUnique();
        
        


    }

        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<InvestmentRound> InvestmentRound { get; set; }
        public DbSet<Invention> Invention { get; set; }
        public DbSet<ExpertService> ExpertService { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Investment> Investment { get; set; }
        public DbSet<ServiceType> ServiceType { get; set; }
    }
}
