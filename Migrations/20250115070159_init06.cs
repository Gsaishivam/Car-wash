using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_wash.Migrations
{
    /// <inheritdoc />
    public partial class init06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Washers_WasherID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WasherID",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
