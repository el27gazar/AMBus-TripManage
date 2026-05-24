using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMBus.TripManage.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RedesignRouteToGovernorates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Routes_RouteId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DistanceKm",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "EstimatedDurationMinutes",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "FromCity",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "Trips",
                newName: "ToId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_RouteId",
                table: "Trips",
                newName: "IX_Trips_ToId");

            migrationBuilder.RenameColumn(
                name: "ToCity",
                table: "Routes",
                newName: "Name");

            migrationBuilder.AddColumn<Guid>(
                name: "FromId",
                table: "Trips",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "CreatedBy", "IsActive", "LastModifiedBy", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000001"), null, true, null, null, "Cairo" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000002"), null, true, null, null, "Alexandria" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000003"), null, true, null, null, "Aswan" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000004"), null, true, null, null, "Minya" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000005"), null, true, null, null, "Luxor" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000006"), null, true, null, null, "Sohag" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000007"), null, true, null, null, "Asyut" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000008"), null, true, null, null, "Hurghada" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000009"), null, true, null, null, "Port Said" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000010"), null, true, null, null, "Ismailia" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000011"), null, true, null, null, "Giza" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000012"), null, true, null, null, "Sharm El-Sheikh" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000013"), null, true, null, null, "Suez" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000014"), null, true, null, null, "Mansoura" },
                    { new Guid("aaaaaaaa-0000-0000-0000-000000000015"), null, true, null, null, "Tanta" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_FromId",
                table: "Trips",
                column: "FromId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Routes_FromId",
                table: "Trips",
                column: "FromId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Routes_ToId",
                table: "Trips",
                column: "ToId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Routes_FromId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Routes_ToId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_FromId",
                table: "Trips");

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-0000-0000-0000-000000000015"));

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "ToId",
                table: "Trips",
                newName: "RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_ToId",
                table: "Trips",
                newName: "IX_Trips_RouteId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Routes",
                newName: "ToCity");

            migrationBuilder.AddColumn<double>(
                name: "DistanceKm",
                table: "Routes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "EstimatedDurationMinutes",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FromCity",
                table: "Routes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Routes_RouteId",
                table: "Trips",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
