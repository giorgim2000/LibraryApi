using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class RentalHistoryModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "90384433-e62f-40c5-8e2d-7104aa2da5e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "41ba6216-a8c7-47b1-9f5e-2aa848db030d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e5cd3d7-44bd-4f94-a56d-6117307d2836", "AQAAAAEAACcQAAAAEBx8rYFmI8ebFGC6VWohyhSHd74UAev93lQQabQIGsX1IAKYZaazS38E08wFw+KhJQ==", "4b8f7f4f-904e-4471-a9d8-e9c2186c1973" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7cc25f2a-bccc-47ff-bcf9-99a846d7ab13");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c8f7a301-2577-4505-916f-c66aff18c9d1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd12186b-bf79-40db-9551-530c9426138c", "AQAAAAEAACcQAAAAEE18QVhxMXLK/YnjcPxw1pj9i4w4yuAQPmROYkySfC0QW14qOoGpOyg8oK8yhjEgRA==", "1f40d9f8-fe2f-4602-87f9-0c56ee88927d" });
        }
    }
}
