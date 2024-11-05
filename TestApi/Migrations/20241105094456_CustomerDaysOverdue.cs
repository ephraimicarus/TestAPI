using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApi.Migrations
{
    /// <inheritdoc />
    public partial class CustomerDaysOverdue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DaysOverdue",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysOverdue",
                table: "Customers");
        }
    }
}
