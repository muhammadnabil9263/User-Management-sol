using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Management.Migrations
{
    /// <inheritdoc />
    public partial class localUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "localUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    orgnizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_localUsers_Orgnizations_orgnizationId",
                        column: x => x.orgnizationId,
                        principalTable: "Orgnizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_localUsers_orgnizationId",
                table: "localUsers",
                column: "orgnizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "localUsers");
        }
    }
}
