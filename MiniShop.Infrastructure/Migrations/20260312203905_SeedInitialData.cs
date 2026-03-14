using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserName", "Role" },
                values: new object[]
                {
                    new Guid("4f2c9c41-9f5c-4a73-bb92-6e7a4a9b1f6d"),
                    "admin",
                    1
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[]
                {
                    "ProductId",
                    "Name",
                    "Price",
                    "Stock",
                    "DiscountType",
                    "DiscountValue"
                },
                values: new object[]
                {
                    new Guid("8b91e2d3-5c6a-4f8b-a0d9-1e3c7f4b6a92"),
                    "Laptop",
                    1200,
                    10,
                    1,
                    10.0
                });    

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("4f2c9c41-9f5c-4a73-bb92-6e7a4a9b1f6d")
            );

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("8b91e2d3-5c6a-4f8b-a0d9-1e3c7f4b6a92")
            );
        }
    }
}
