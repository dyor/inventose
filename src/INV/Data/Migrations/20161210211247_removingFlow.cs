using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace INV.Data.Migrations
{
    public partial class removingFlow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investment_InvestmentRound_InvestmentRoundID1",
                table: "Investment");
        }
            
         

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvestmentRoundID1",
                table: "Investment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Investment_InvestmentRoundID1",
                table: "Investment",
                column: "InvestmentRoundID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Investment_InvestmentRound_InvestmentRoundID1",
                table: "Investment",
                column: "InvestmentRoundID1",
                principalTable: "InvestmentRound",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
