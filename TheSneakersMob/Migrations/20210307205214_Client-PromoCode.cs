using Microsoft.EntityFrameworkCore.Migrations;

namespace TheSneakersMob.Migrations
{
    public partial class ClientPromoCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptCoupons",
                table: "Sells",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PromoCodeId",
                table: "Clients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PromoCodeId",
                table: "Clients",
                column: "PromoCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_PromoCodes_PromoCodeId",
                table: "Clients",
                column: "PromoCodeId",
                principalTable: "PromoCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_PromoCodes_PromoCodeId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_PromoCodeId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AcceptCoupons",
                table: "Sells");

            migrationBuilder.DropColumn(
                name: "PromoCodeId",
                table: "Clients");
        }
    }
}
