using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieLibrary.Migrations
{
    public partial class TicketSellerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketSellerId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateTable(
                name: "TicketSeller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSeller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketSeller_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_TicketSellerId",
                table: "Movies",
                column: "TicketSellerId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketSeller_UserId",
                table: "TicketSeller",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_TicketSeller_TicketSellerId",
                table: "Movies",
                column: "TicketSellerId",
                principalTable: "TicketSeller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_TicketSeller_TicketSellerId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "TicketSeller");

            migrationBuilder.DropIndex(
                name: "IX_Movies_TicketSellerId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "TicketSellerId",
                table: "Movies");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);
        }
    }
}
