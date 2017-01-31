using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace INV.Data.Migrations
{
    public partial class updateApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestmentRound_AspNetUsers_ApplicationUserId",
                table: "InvestmentRound");

            migrationBuilder.DropIndex(
                name: "IX_InvestmentRound_ApplicationUserId",
                table: "InvestmentRound");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "InvestmentRound");

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "AspNetUsers",
                type: "ntext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "InvestmentRound",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentRound_ApplicationUserId",
                table: "InvestmentRound",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestmentRound_AspNetUsers_ApplicationUserId",
                table: "InvestmentRound",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
