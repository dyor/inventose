using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace INV.Data.Migrations
{
    public partial class phoo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkedInURL",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterURL",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebAddress",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YouTubeIntro",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkedInURL",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TwitterURL",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WebAddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "YouTubeIntro",
                table: "AspNetUsers");
        }
    }
}
