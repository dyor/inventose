using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace INV.Data.Migrations
{
    public partial class roundstatusid2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestmentRound_RoundStatus_RoundStatusID",
                table: "InvestmentRound");

            migrationBuilder.AlterColumn<int>(
                name: "RoundStatusID",
                table: "InvestmentRound",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestmentRound_RoundStatus_RoundStatusID",
                table: "InvestmentRound",
                column: "RoundStatusID",
                principalTable: "RoundStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestmentRound_RoundStatus_RoundStatusID",
                table: "InvestmentRound");

            migrationBuilder.AlterColumn<int>(
                name: "RoundStatusID",
                table: "InvestmentRound",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_InvestmentRound_RoundStatus_RoundStatusID",
                table: "InvestmentRound",
                column: "RoundStatusID",
                principalTable: "RoundStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
