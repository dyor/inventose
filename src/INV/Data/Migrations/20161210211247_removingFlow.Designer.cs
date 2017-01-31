using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using INV.Data;

namespace INV.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161210211247_removingFlow")]
    partial class removingFlow
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("INV.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("LongDescription")
                        .HasColumnType("ntext");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("INV.Models.ExpertService", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<decimal>("Cost");

                    b.Property<DateTime>("DateCreated");

                    b.Property<bool>("IsPublished");

                    b.Property<string>("LongDescription")
                        .HasColumnType("ntext");

                    b.Property<int>("MaxDaysToComplete");

                    b.Property<int>("ServiceTypeID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ServiceTypeID");

                    b.ToTable("ExpertService");
                });

            modelBuilder.Entity("INV.Models.Invention", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<bool>("IsForSale");

                    b.Property<bool>("IsOpenForFunding");

                    b.Property<bool>("IsPublished");

                    b.Property<string>("LongDescription")
                        .HasColumnType("ntext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<decimal>("Valuation");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Invention");
                });

            modelBuilder.Entity("INV.Models.Investment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("DateAccepted");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateReceived");

                    b.Property<DateTime>("DateRejected");

                    b.Property<decimal>("InvestmentAmount");

                    b.Property<int?>("InvestmentRoundID");

                    b.Property<bool>("IsInvestmentAccepted");

                    b.Property<bool>("IsInvestmentReceived");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("InvestmentRoundID");

                    b.ToTable("Investment");
                });

            modelBuilder.Entity("INV.Models.InvestmentRound", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("ExpertServiceID");

                    b.Property<int>("InventionID");

                    b.Property<bool>("IsOpenForFunding");

                    b.Property<decimal>("RaiseAmount");

                    b.Property<int?>("RoundStatusID");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.HasIndex("ExpertServiceID");

                    b.HasIndex("InventionID");

                    b.HasIndex("RoundStatusID");

                    b.ToTable("InvestmentRound");
                });

            modelBuilder.Entity("INV.Models.RoundStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("RoundStatus");
                });

            modelBuilder.Entity("INV.Models.ServiceType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("ServiceType");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("INV.Models.ApplicationRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole");


                    b.ToTable("ApplicationRole");

                    b.HasDiscriminator().HasValue("ApplicationRole");
                });

            modelBuilder.Entity("INV.Models.ExpertService", b =>
                {
                    b.HasOne("INV.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("ExpertServices")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("INV.Models.ServiceType", "ServiceType")
                        .WithMany()
                        .HasForeignKey("ServiceTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("INV.Models.Invention", b =>
                {
                    b.HasOne("INV.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Inventions")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("INV.Models.Investment", b =>
                {
                    b.HasOne("INV.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Investments")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("INV.Models.InvestmentRound", "InvestmentRound")
                        .WithMany("Investments")
                        .HasForeignKey("InvestmentRoundID");
                });

            modelBuilder.Entity("INV.Models.InvestmentRound", b =>
                {
                    b.HasOne("INV.Models.ExpertService", "ExpertService")
                        .WithMany("InvestmentRounds")
                        .HasForeignKey("ExpertServiceID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("INV.Models.Invention", "Invention")
                        .WithMany("InvestmentRounds")
                        .HasForeignKey("InventionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("INV.Models.RoundStatus", "RoundStatus")
                        .WithMany()
                        .HasForeignKey("RoundStatusID");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("INV.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("INV.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("INV.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
