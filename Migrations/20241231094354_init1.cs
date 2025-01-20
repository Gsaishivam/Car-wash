using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_wash.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Washers_Roles_RolesRoleID",
                table: "Washers");

            migrationBuilder.DropIndex(
                name: "IX_Washers_RolesRoleID",
                table: "Washers");

            migrationBuilder.DropColumn(
                name: "RolesRoleID",
                table: "Washers");

            migrationBuilder.CreateIndex(
                name: "IX_Washers_RoleID",
                table: "Washers",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Washers_Roles_RoleID",
                table: "Washers",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Washers_Roles_RoleID",
                table: "Washers");

            migrationBuilder.DropIndex(
                name: "IX_Washers_RoleID",
                table: "Washers");

            migrationBuilder.AddColumn<int>(
                name: "RolesRoleID",
                table: "Washers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Washers_RolesRoleID",
                table: "Washers",
                column: "RolesRoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Washers_Roles_RolesRoleID",
                table: "Washers",
                column: "RolesRoleID",
                principalTable: "Roles",
                principalColumn: "RoleID");
        }
    }
}
