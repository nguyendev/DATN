using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Data.Migrations
{
    public partial class Initjhnfsak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Image_ImageID",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "ImageID",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Image_ImageID",
                table: "AspNetUsers",
                column: "ImageID",
                principalTable: "Image",
                principalColumn: "ImageID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Image_ImageID",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<long>(
                name: "ImageID",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Image_ImageID",
                table: "AspNetUsers",
                column: "ImageID",
                principalTable: "Image",
                principalColumn: "ImageID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
