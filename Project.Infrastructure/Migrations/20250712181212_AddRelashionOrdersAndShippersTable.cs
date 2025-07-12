using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelashionOrdersAndShippersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipVia",
                table: "Orders",
                column: "ShipVia");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shippers_ShipVia",
                table: "Orders",
                column: "ShipVia",
                principalTable: "Shippers",
                principalColumn: "ShipperID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shippers_ShipVia",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShipVia",
                table: "Orders");
        }
    }
}
