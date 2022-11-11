using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sportzall.Migrations
{
    public partial class removeUserIdfromAbonement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abonement_User_UserId",
                table: "Abonement");

            migrationBuilder.DropIndex(
                name: "IX_Abonement_UserId",
                table: "Abonement");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Abonement");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Abonement",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Abonement_UserId",
                table: "Abonement",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abonement_User_UserId",
                table: "Abonement",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
