using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Management.Migrations
{
    /// <inheritdoc />
    public partial class userrelationwithorgnization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "orgnizationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_orgnizationId",
                table: "AspNetUsers",
                column: "orgnizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Orgnizations_orgnizationId",
                table: "AspNetUsers",
                column: "orgnizationId",
                principalTable: "Orgnizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Orgnizations_orgnizationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_orgnizationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "orgnizationId",
                table: "AspNetUsers");
        }
    }
}
