using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sportzall.Migrations
{
    public partial class addCascadeDeleteforAbonementsUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbonementsUser_User_UserId",
                table: "AbonementsUser");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AbonementsUser",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AbonementsUser_User_UserId",
                table: "AbonementsUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbonementsUser_User_UserId",
                table: "AbonementsUser");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AbonementsUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AbonementsUser_User_UserId",
                table: "AbonementsUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
