using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_wash.Migrations
{
    /// <inheritdoc />
    public partial class init05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Washers_WashersWasherID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WashersWasherID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WashersWasherID",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Orders",
                table: "Washers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orders",
                table: "Washers");

            migrationBuilder.AddColumn<int>(
                name: "WashersWasherID",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WashersWasherID",
                table: "Orders",
                column: "WashersWasherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Washers_WashersWasherID",
                table: "Orders",
                column: "WashersWasherID",
                principalTable: "Washers",
                principalColumn: "WasherID");
        }
    }
}
