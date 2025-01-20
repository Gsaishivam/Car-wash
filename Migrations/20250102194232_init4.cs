using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_wash.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Washers_WasherID",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "WasherID",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "WasherID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Washers_WasherID",
                table: "Orders",
                column: "WasherID",
                principalTable: "Washers",
                principalColumn: "WasherID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
