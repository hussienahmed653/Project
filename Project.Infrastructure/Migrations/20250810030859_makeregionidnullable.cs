using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class makeregionidnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territorie_Regions_RegionID",
                table: "Territorie");

            migrationBuilder.AlterColumn<int>(
                name: "RegionID",
                table: "Territorie",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Territorie_Regions_RegionID",
                table: "Territorie",
                column: "RegionID",
                principalTable: "Regions",
                principalColumn: "RegionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territorie_Regions_RegionID",
                table: "Territorie");

            migrationBuilder.AlterColumn<int>(
                name: "RegionID",
                table: "Territorie",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Territorie_Regions_RegionID",
                table: "Territorie",
                column: "RegionID",
                principalTable: "Regions",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
