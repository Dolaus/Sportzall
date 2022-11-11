using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sportzall.Migrations
{
    public partial class AddModelAbonements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abonement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abonement_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "trener" });

            migrationBuilder.CreateIndex(
                name: "IX_Abonement_UserId",
                table: "Abonement",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abonement");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
