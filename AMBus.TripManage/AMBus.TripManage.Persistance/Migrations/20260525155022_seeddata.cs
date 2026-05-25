using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMBus.TripManage.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FullName", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("cccccccc-0000-0000-0000-000000000001"), 0, "74e0317a-8d53-40f7-bb51-97849f8ed2f0", new DateTime(2026, 5, 25, 15, 50, 18, 918, DateTimeKind.Utc).AddTicks(8962), "driver1@ambus.com", false, "كابتن محمد أحمد", true, false, null, null, null, null, "", false, null, false, null },
                    { new Guid("cccccccc-0000-0000-0000-000000000002"), 0, "de3c77c7-93ac-4667-ad7c-89230245e00f", new DateTime(2026, 5, 25, 15, 50, 18, 918, DateTimeKind.Utc).AddTicks(8969), "driver2@ambus.com", false, "كابتن محمود علي", true, false, null, null, null, null, "", false, null, false, null }
                });

            migrationBuilder.InsertData(
                table: "Buses",
                columns: new[] { "Id", "CreatedBy", "IsActive", "LastModifiedBy", "LastModifiedDate", "Model", "PlateNumber", "TotalSeats", "Type" },
                values: new object[,]
                {
                    { new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "Mercedes-Benz Travego", "أ ب ج 1234", 50, "Standard" },
                    { new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "Volvo 9700", "ق ر س 5678", 45, "Standard" },
                    { new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "Scania Touring", "ط ي ع 9101", 48, "VIP" }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "CreatedBy", "EmergencyContact", "IsAvailable", "LastModifiedBy", "LastModifiedDate", "LicenseExpiry", "LicenseNumber", "UserId" },
                values: new object[,]
                {
                    { new Guid("1bd6a872-4dd8-559c-aa38-f72c6f59f3ef"), null, "01012345678", true, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "د/12345/ج", new Guid("cccccccc-0000-0000-0000-000000000001") },
                    { new Guid("dddddddd-0000-0000-0000-000000000001"), null, "01234567890", true, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "أ/98765/ب", new Guid("cccccccc-0000-0000-0000-000000000002") }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "ArrivalTime", "AvailableSeats", "BasePrice", "BusId", "CreatedBy", "CreatedDate", "DepartureTime", "DriverId", "FromId", "LastModifiedBy", "LastModifiedDate", "Status", "ToId" },
                values: new object[] { new Guid("11111111-2222-3333-4444-555555555555"), new DateTime(2026, 8, 2, 8, 25, 1, 0, DateTimeKind.Unspecified), 0, 5207.25m, new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, new DateTime(2026, 5, 25, 15, 50, 18, 918, DateTimeKind.Utc).AddTicks(3642), new DateTime(2026, 7, 14, 5, 40, 3, 0, DateTimeKind.Unspecified), new Guid("1bd6a872-4dd8-559c-aa38-f72c6f59f3ef"), new Guid("aaaaaaaa-0000-0000-0000-000000000001"), null, null, "Scheduled", new Guid("aaaaaaaa-0000-0000-0000-000000000002") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Buses",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Buses",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555555"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Buses",
                keyColumn: "Id",
                keyValue: new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("1bd6a872-4dd8-559c-aa38-f72c6f59f3ef"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000001"));
        }
    }
}
