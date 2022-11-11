using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sportzall.Migrations
{
    public partial class addboolAbonement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPay",
                table: "AbonementsUser",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPay",
                table: "AbonementsUser");
        }
    }
}
