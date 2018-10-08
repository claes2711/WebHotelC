using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebHotel.Data.Migrations
{
    public partial class AddClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Room_Room_RoomID",
                table: "Room");*/

            migrationBuilder.DropIndex(
                name: "IX_Room_RoomID",
                table: "Room");

            /*migrationBuilder.DropColumn(
                name: "RoomID",
                table: "Room");*/

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    GivenName = table.Column<string>(nullable: false),
                    Postcode = table.Column<string>(nullable: false),
                    SurName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CheckIn = table.Column<string>(nullable: false),
                    CheckOut = table.Column<string>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    RoomID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Booking_Customer_Email",
                        column: x => x.Email,
                        principalTable: "Customer",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_Room_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Room",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Email",
                table: "Booking",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_RoomID",
                table: "Booking",
                column: "RoomID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "Room",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_RoomID",
                table: "Room",
                column: "RoomID");

            /*migrationBuilder.AddForeignKey(
                name: "FK_Room_Room_RoomID",
                table: "Room",
                column: "RoomID",
                principalTable: "Room",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);*/
        }
    }
}
