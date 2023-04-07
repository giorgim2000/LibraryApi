using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class DatabaseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentEnd",
                table: "BookRentalHistory");

            migrationBuilder.RenameColumn(
                name: "RentStart",
                table: "BookRentalHistory",
                newName: "CreationDate");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BookRentalHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "BookRentalHistory");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "BookRentalHistory",
                newName: "RentStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "RentEnd",
                table: "BookRentalHistory",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "94a5af6a-18b9-4f47-ad48-346f28b6887e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "684075db-d8ed-402c-b601-69744f06c73e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3017842f-f614-41b7-a4db-696dfb5e4930", "AQAAAAEAACcQAAAAECnusn1j4y8BlUs10xC/w6A3IsAwl0ZWhFOFORjl+K1mVFBRL0HFbUPiM65cfTiyDg==", "14b4abab-8562-4548-86d7-3a47da82a405" });
        }
    }
}
