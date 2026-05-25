using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMBus.TripManage.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class driver_edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("1bd6a872-4dd8-559c-aa38-f72c6f59f3ef"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "b74d4e32-5bae-43b2-80e4-ac9143f50f8f", new DateTime(2026, 5, 25, 16, 35, 5, 353, DateTimeKind.Utc).AddTicks(4592) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "84372923-dcbf-4091-9d8d-b0452d290c65", new DateTime(2026, 5, 25, 16, 35, 5, 353, DateTimeKind.Utc).AddTicks(4608) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555555"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 25, 16, 35, 5, 351, DateTimeKind.Utc).AddTicks(9790));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "CreatedBy", "EmergencyContact", "IsAvailable", "LastModifiedBy", "LastModifiedDate", "LicenseExpiry", "LicenseNumber", "UserId" },
                values: new object[,]
                {
                    { new Guid("1bd6a872-4dd8-559c-aa38-f72c6f59f3ef"), null, "01012345678", true, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "د/12345/ج", new Guid("cccccccc-0000-0000-0000-000000000001") },
                    { new Guid("dddddddd-0000-0000-0000-000000000001"), null, "01234567890", true, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "أ/98765/ب", new Guid("cccccccc-0000-0000-0000-000000000002") }
                });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555555"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 25, 16, 25, 21, 588, DateTimeKind.Utc).AddTicks(2393));
        }
    }
}
