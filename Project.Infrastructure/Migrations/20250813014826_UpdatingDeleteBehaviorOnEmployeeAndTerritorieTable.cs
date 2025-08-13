using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingDeleteBehaviorOnEmployeeAndTerritorieTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTerritories_Employees_EmployeeID",
                table: "EmployeeTerritories");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTerritories_Territorie_TerritoryID",
                table: "EmployeeTerritories");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTerritories_Employees_EmployeeID",
                table: "EmployeeTerritories",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTerritories_Territorie_TerritoryID",
                table: "EmployeeTerritories",
                column: "TerritoryID",
                principalTable: "Territorie",
                principalColumn: "TerritoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTerritories_Employees_EmployeeID",
                table: "EmployeeTerritories");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTerritories_Territorie_TerritoryID",
                table: "EmployeeTerritories");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTerritories_Employees_EmployeeID",
                table: "EmployeeTerritories",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTerritories_Territorie_TerritoryID",
                table: "EmployeeTerritories",
                column: "TerritoryID",
                principalTable: "Territorie",
                principalColumn: "TerritoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
