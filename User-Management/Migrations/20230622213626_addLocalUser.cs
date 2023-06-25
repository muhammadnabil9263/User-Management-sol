using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Management.Migrations
{
    /// <inheritdoc />
    public partial class addLocalUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Orgnizations_orgnizationId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "orgnizationId",
                table: "AspNetUsers",
                newName: "OrgnizationId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_orgnizationId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_OrgnizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Orgnizations_OrgnizationId",
                table: "AspNetUsers",
                column: "OrgnizationId",
                principalTable: "Orgnizations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Orgnizations_OrgnizationId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "OrgnizationId",
                table: "AspNetUsers",
                newName: "orgnizationId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_OrgnizationId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_orgnizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Orgnizations_orgnizationId",
                table: "AspNetUsers",
                column: "orgnizationId",
                principalTable: "Orgnizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
