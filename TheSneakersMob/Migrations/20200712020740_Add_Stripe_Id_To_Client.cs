using Microsoft.EntityFrameworkCore.Migrations;

namespace TheSneakersMob.Migrations
{
    public partial class Add_Stripe_Id_To_Client : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeId",
                table: "Clients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeId",
                table: "Clients");
        }
    }
}
