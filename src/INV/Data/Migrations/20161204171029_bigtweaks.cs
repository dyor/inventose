using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace INV.Data.Migrations
{
    public partial class bigtweaks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateAcceted",
                table: "Investment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Investment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateReceived",
                table: "Investment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRejected",
                table: "Investment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "InvestmentAmount",
                table: "Investment",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsInvestmentAccepted",
                table: "Investment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInvestmentReceived",
                table: "Investment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Invention",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Valuation",
                table: "Invention",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ExpertService",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "ExpertService",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Invention",
                maxLength: 100,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ExpertService",
                maxLength: 100,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAcceted",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "DateReceived",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "DateRejected",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "InvestmentAmount",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "IsInvestmentAccepted",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "IsInvestmentReceived",
                table: "Investment");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Invention");

            migrationBuilder.DropColumn(
                name: "Valuation",
                table: "Invention");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ExpertService");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "ExpertService");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Invention",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ExpertService",
                nullable: true);
        }
    }
}
