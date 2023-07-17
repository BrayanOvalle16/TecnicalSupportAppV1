using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TecnicalSupportAppV1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceState",
                table: "ServiceOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceState",
                table: "ServiceOrders");
        }
    }
}
