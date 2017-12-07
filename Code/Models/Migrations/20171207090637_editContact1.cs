using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class editContact1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Contact",
                newName: "WhereIdentityCard");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Contact",
                newName: "MobilePhone");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateIdentityCard",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Landline",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "Contact",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateIdentityCard",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Landline",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "WhereIdentityCard",
                table: "Contact",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "MobilePhone",
                table: "Contact",
                newName: "City");
        }
    }
}
