using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserSegmentation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adduserbirthdaysalaryandsocialscore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "GrossAnnualRevenue",
                table: "User",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SocialScore",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "GrossAnnualRevenue",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SocialScore",
                table: "User");
        }
    }
}
