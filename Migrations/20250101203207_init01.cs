using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_wash.Migrations
{
    /// <inheritdoc />
    public partial class init01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarNumber",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "CarNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CarType",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CarType",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "CarNumber",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
