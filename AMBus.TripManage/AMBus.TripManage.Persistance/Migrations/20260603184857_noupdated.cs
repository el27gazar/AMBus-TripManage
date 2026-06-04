using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMBus.TripManage.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class noupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("b2d690ce-3456-4cf2-0987-654321fedcba"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "7b213cb0-82c7-4910-9b89-082ee2da856c", new DateTime(2026, 6, 3, 18, 48, 56, 445, DateTimeKind.Utc).AddTicks(7426) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "69c948c0-0ea3-42d5-880e-7016cadcf094", new DateTime(2026, 6, 3, 18, 48, 56, 445, DateTimeKind.Utc).AddTicks(7433) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "276ef040-90e0-4a17-8544-61cb2eeec534", new DateTime(2026, 6, 3, 18, 42, 31, 583, DateTimeKind.Utc).AddTicks(9264) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "252cf736-2b46-4c2d-9617-e6567b579223", new DateTime(2026, 6, 3, 18, 42, 31, 583, DateTimeKind.Utc).AddTicks(9269) });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "EmergencyContact", "IsAvailable", "LastModifiedBy", "LastModifiedDate", "LicenseExpiry", "LicenseNumber", "UserId" },
                values: new object[] { new Guid("b2d690ce-3456-4cf2-0987-654321fedcba"), "System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "01000000000", true, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DL-EGYPT-123456", new Guid("c1a589bd-2345-4bf1-9876-543210abcdef") });
        }
    }
}
