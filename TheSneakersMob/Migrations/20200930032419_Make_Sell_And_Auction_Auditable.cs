using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheSneakersMob.Migrations
{
    public partial class Make_Sell_And_Auction_Auditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Sells",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Sells",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Auctions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Auctions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Sells");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Sells");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Auctions");
        }
    }
}
