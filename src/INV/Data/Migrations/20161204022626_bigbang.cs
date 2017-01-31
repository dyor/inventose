using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace INV.Data.Migrations
{
    public partial class bigbang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invention",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsForSale = table.Column<bool>(nullable: false),
                    IsOpenForFunding = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invention", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Invention_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExpertService",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(nullable: false),
                    MaxDaysToComplete = table.Column<int>(nullable: false),
                    ServiceTypeID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertService", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExpertService_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpertService_ServiceType_ServiceTypeID",
                        column: x => x.ServiceTypeID,
                        principalTable: "ServiceType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentRound",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ExpertServiceID = table.Column<int>(nullable: false),
                    InventionID = table.Column<int>(nullable: false),
                    IsOpenForFunding = table.Column<bool>(nullable: false),
                    RaiseAmount = table.Column<decimal>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentRound", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InvestmentRound_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvestmentRound_ExpertService_ExpertServiceID",
                        column: x => x.ExpertServiceID,
                        principalTable: "ExpertService",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentRound_Invention_InventionID",
                        column: x => x.InventionID,
                        principalTable: "Invention",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Investment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    InvestmentRoundID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Investment_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Investment_InvestmentRound_InvestmentRoundID",
                        column: x => x.InvestmentRoundID,
                        principalTable: "InvestmentRound",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpertService_ApplicationUserId",
                table: "ExpertService",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertService_ServiceTypeID",
                table: "ExpertService",
                column: "ServiceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Invention_ApplicationUserId",
                table: "Invention",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Investment_ApplicationUserId",
                table: "Investment",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Investment_InvestmentRoundID",
                table: "Investment",
                column: "InvestmentRoundID");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentRound_ApplicationUserId",
                table: "InvestmentRound",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentRound_ExpertServiceID",
                table: "InvestmentRound",
                column: "ExpertServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentRound_InventionID",
                table: "InvestmentRound",
                column: "InventionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Investment");

            migrationBuilder.DropTable(
                name: "InvestmentRound");

            migrationBuilder.DropTable(
                name: "ExpertService");

            migrationBuilder.DropTable(
                name: "Invention");

            migrationBuilder.DropTable(
                name: "ServiceType");
        }
    }
}
