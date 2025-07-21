using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeFileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeFile",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFile", x => new { x.EmployeeID, x.Path });
                    table.ForeignKey(
                        name: "FK_EmployeeFile_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeFile");
        }
    }
}
