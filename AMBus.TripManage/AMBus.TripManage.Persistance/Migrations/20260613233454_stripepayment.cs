using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMBus.TripManage.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class stripepayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymobTransactionId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PaymobTransactionId",
                table: "Payments",
                newName: "StripePaymentIntentId");

            migrationBuilder.RenameColumn(
                name: "PaymobPaymentToken",
                table: "Payments",
                newName: "StripeClientSecret");

            migrationBuilder.RenameColumn(
                name: "PaymobOrderId",
                table: "Payments",
                newName: "ReferenceNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_PaymobOrderId",
                table: "Payments",
                newName: "IX_Payments_ReferenceNumber");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "b10e9e7b-0c8e-4875-b160-616218dcc637", new DateTime(2026, 6, 13, 23, 34, 51, 595, DateTimeKind.Utc).AddTicks(4266) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "668f2226-8429-48a7-9797-c8a5d76a65cc", new DateTime(2026, 6, 13, 23, 34, 51, 595, DateTimeKind.Utc).AddTicks(4273) });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StripeClientSecret",
                table: "Payments",
                column: "StripeClientSecret");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_StripeClientSecret",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "StripePaymentIntentId",
                table: "Payments",
                newName: "PaymobTransactionId");

            migrationBuilder.RenameColumn(
                name: "StripeClientSecret",
                table: "Payments",
                newName: "PaymobPaymentToken");

            migrationBuilder.RenameColumn(
                name: "ReferenceNumber",
                table: "Payments",
                newName: "PaymobOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_ReferenceNumber",
                table: "Payments",
                newName: "IX_Payments_PaymobOrderId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "848d6d55-f4b9-42ca-8f17-6e798ceb7bcf", new DateTime(2026, 6, 8, 16, 51, 43, 997, DateTimeKind.Utc).AddTicks(8614) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "d70c6b63-5a4f-4b4b-8204-e56fdfd0133b", new DateTime(2026, 6, 8, 16, 51, 43, 997, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymobTransactionId",
                table: "Payments",
                column: "PaymobTransactionId");
        }
    }
}
