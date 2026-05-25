using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMBus.TripManage.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class removeseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555555"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "403b05ec-d406-47f1-afbc-642f199f2fb4", new DateTime(2026, 5, 25, 16, 37, 31, 780, DateTimeKind.Utc).AddTicks(1159) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "ca2a41c2-388d-4144-b092-8e283b8801d7", new DateTime(2026, 5, 25, 16, 37, 31, 780, DateTimeKind.Utc).AddTicks(1166) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "ArrivalTime", "AvailableSeats", "BasePrice", "BusId", "CreatedBy", "CreatedDate", "DepartureTime", "DriverId", "FromId", "LastModifiedBy", "LastModifiedDate", "Status", "ToId" },
                values: new object[] { new Guid("11111111-2222-3333-4444-555555555555"), new DateTime(2026, 8, 2, 8, 25, 1, 0, DateTimeKind.Unspecified), 0, 5207.25m, new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, new DateTime(2026, 5, 25, 16, 35, 5, 351, DateTimeKind.Utc).AddTicks(9790), new DateTime(2026, 7, 14, 5, 40, 3, 0, DateTimeKind.Unspecified), new Guid("1bd6a872-4dd8-559c-aa38-f72c6f59f3ef"), new Guid("aaaaaaaa-0000-0000-0000-000000000001"), null, null, "Scheduled", new Guid("aaaaaaaa-0000-0000-0000-000000000002") });
        }
    }
}
