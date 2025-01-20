using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_wash.Migrations
{
    /// <inheritdoc />
    public partial class init04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Washers_WasherID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WasherID",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "WashCompletedStatus",
                table: "Orders",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Washers_WashersWasherID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WashersWasherID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WashCompletedStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WashersWasherID",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WasherID",
                table: "Orders",
                column: "WasherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Washers_WasherID",
                table: "Orders",
                column: "WasherID",
                principalTable: "Washers",
                principalColumn: "WasherID");
        }
    }
}
