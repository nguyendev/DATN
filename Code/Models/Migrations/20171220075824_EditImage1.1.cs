using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EditImage11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Image",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_OwnerID",
                table: "Image",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_AspNetUsers_OwnerID",
                table: "Image",
                column: "OwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_AspNetUsers_OwnerID",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_OwnerID",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Image");
        }
    }
}
