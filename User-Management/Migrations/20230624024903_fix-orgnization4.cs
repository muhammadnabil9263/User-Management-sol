using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Management.Migrations
{
    /// <inheritdoc />
    public partial class fixorgnization4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_localUsers_Orgnizations_OrgnizationId",
                table: "localUsers");

            migrationBuilder.RenameColumn(
                name: "OrgnizationId",
                table: "localUsers",
                newName: "orgnizationId");

            migrationBuilder.RenameIndex(
                name: "IX_localUsers_OrgnizationId",
                table: "localUsers",
                newName: "IX_localUsers_orgnizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_localUsers_Orgnizations_orgnizationId",
                table: "localUsers",
                column: "orgnizationId",
                principalTable: "Orgnizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_localUsers_Orgnizations_orgnizationId",
                table: "localUsers");

            migrationBuilder.RenameColumn(
                name: "orgnizationId",
                table: "localUsers",
                newName: "OrgnizationId");

            migrationBuilder.RenameIndex(
                name: "IX_localUsers_orgnizationId",
                table: "localUsers",
                newName: "IX_localUsers_OrgnizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_localUsers_Orgnizations_OrgnizationId",
                table: "localUsers",
                column: "OrgnizationId",
                principalTable: "Orgnizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
