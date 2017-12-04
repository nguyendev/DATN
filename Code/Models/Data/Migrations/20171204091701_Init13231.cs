using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Data.Migrations
{
    public partial class Init13231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CourseID",
                table: "Enrollment",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "Enrollment",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CourseID",
                table: "Course",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Capacity = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CourseRoom",
                columns: table => new
                {
                    CourseID = table.Column<int>(nullable: false),
                    RoomID = table.Column<int>(nullable: false),
                    CourseID1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRoom", x => new { x.CourseID, x.RoomID });
                    table.ForeignKey(
                        name: "FK_CourseRoom_Course_CourseID1",
                        column: x => x.CourseID1,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseRoom_Room_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Room",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_RoomID",
                table: "Enrollment",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRoom_CourseID1",
                table: "CourseRoom",
                column: "CourseID1");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRoom_RoomID",
                table: "CourseRoom",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Room_RoomID",
                table: "Enrollment",
                column: "RoomID",
                principalTable: "Room",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Room_RoomID",
                table: "Enrollment");

            migrationBuilder.DropTable(
                name: "CourseRoom");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_RoomID",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "Enrollment");

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                table: "Enrollment",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                table: "Course",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
