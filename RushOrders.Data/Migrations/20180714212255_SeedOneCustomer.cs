using Microsoft.EntityFrameworkCore.Migrations;

namespace RushOrders.Data.Migrations
{
    public partial class SeedOneCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { 1, "andersonom@gmail.com", "Anderson Martins" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
