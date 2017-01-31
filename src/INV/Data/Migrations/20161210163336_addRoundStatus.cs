using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace INV.Data.Migrations
{
    public partial class addRoundStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoundStatus",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundStatus", x => x.ID);
                });

            migrationBuilder.AddColumn<int>(
                name: "RoundStatusID",
                table: "InvestmentRound",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentRound_RoundStatusID",
                table: "InvestmentRound",
                column: "RoundStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestmentRound_RoundStatus_RoundStatusID",
                table: "InvestmentRound",
                column: "RoundStatusID",
                principalTable: "RoundStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestmentRound_RoundStatus_RoundStatusID",
                table: "InvestmentRound");

            migrationBuilder.DropIndex(
                name: "IX_InvestmentRound_RoundStatusID",
                table: "InvestmentRound");

            migrationBuilder.DropColumn(
                name: "RoundStatusID",
                table: "InvestmentRound");

            migrationBuilder.DropTable(
                name: "RoundStatus");
        }
    }
}
