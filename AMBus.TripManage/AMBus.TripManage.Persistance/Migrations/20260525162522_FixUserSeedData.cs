using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMBus.TripManage.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class FixUserSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "fdbac674-7a81-4b54-902e-52ea35a14952", new DateTime(2026, 5, 25, 16, 25, 21, 588, DateTimeKind.Utc).AddTicks(8200) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "b7fb6a9f-7999-4ade-b0a5-3976a5e4ff39", new DateTime(2026, 5, 25, 16, 25, 21, 588, DateTimeKind.Utc).AddTicks(8208) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555555"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 25, 16, 25, 21, 588, DateTimeKind.Utc).AddTicks(2393));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "74e0317a-8d53-40f7-bb51-97849f8ed2f0", new DateTime(2026, 5, 25, 15, 50, 18, 918, DateTimeKind.Utc).AddTicks(8962) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "de3c77c7-93ac-4667-ad7c-89230245e00f", new DateTime(2026, 5, 25, 15, 50, 18, 918, DateTimeKind.Utc).AddTicks(8969) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555555"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 25, 15, 50, 18, 918, DateTimeKind.Utc).AddTicks(3642));
        }
    }
}
