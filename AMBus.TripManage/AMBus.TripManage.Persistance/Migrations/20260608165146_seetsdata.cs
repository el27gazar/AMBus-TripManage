using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMBus.TripManage.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class seetsdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "BusId", "CreatedBy", "IsAvailable", "LastModifiedBy", "LastModifiedDate", "SeatNumber" },
                values: new object[,]
                {
                    { new Guid("035c2aea-4e19-869b-3a4a-8ebff0c3ecca"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "26" },
                    { new Guid("05677b5d-59c4-ac62-7b8b-e15a0bf36520"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "22" },
                    { new Guid("0b150199-187d-77c2-9160-56f75bc6d8d3"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "43" },
                    { new Guid("0c8b3464-c503-ec5a-25ca-a2eba6a0b104"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "17" },
                    { new Guid("0d4e6f7f-5a56-6ecb-96ba-99d26ee348af"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "33" },
                    { new Guid("0d902d04-ad60-fb64-d1ce-cc935e801204"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "7" },
                    { new Guid("0da8e991-a20b-3a4d-b5fe-6ee391959a4a"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "16" },
                    { new Guid("0e46ffdc-ee6e-6840-1393-f61dfd22777f"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "37" },
                    { new Guid("0eaee7e1-d260-560e-f25b-3dde5b52dc9b"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "47" },
                    { new Guid("0f505ad8-25e9-92a1-d93a-0302e8d28d82"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "39" },
                    { new Guid("11089b5d-ccc7-f542-b225-eae62343703c"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "47" },
                    { new Guid("119bbb2e-846e-0572-f974-3674198e7a3f"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "23" },
                    { new Guid("121a8fef-e2b3-737b-fa05-97812c63a239"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "6" },
                    { new Guid("1492ce3e-b9a2-eb49-cd8d-c3a48eda2c67"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "32" },
                    { new Guid("16b1a9ca-6b20-4802-79f2-fe3a78d15b75"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "4" },
                    { new Guid("1c97c820-d62a-73f2-2baa-9fddf7bd7fff"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "25" },
                    { new Guid("1ffeac7b-02ec-e57a-d62d-b51b46006fed"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "26" },
                    { new Guid("22dae416-de17-bb82-805d-d160e37ffa16"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "5" },
                    { new Guid("2323c0e4-dfba-a657-4c3c-a52a768e7788"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "27" },
                    { new Guid("245d861b-b459-63ea-3afc-37cbcb98b80f"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "23" },
                    { new Guid("27f5f39c-d74a-ac88-43f9-e7d3519003d9"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "5" },
                    { new Guid("2b6a4a2b-9a6d-1a17-fe74-e5bb827eacaf"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "14" },
                    { new Guid("2b86af15-76aa-7cfa-e6af-c14e9fa3d45e"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "31" },
                    { new Guid("2dfe5f1a-effb-f3fc-bb73-15481193ce15"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "6" },
                    { new Guid("2f451571-02f4-dd09-384e-80046bc8ecc9"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "29" },
                    { new Guid("3047edff-e4a9-b39f-bf51-765fa61c2d1b"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "24" },
                    { new Guid("3049f56a-33b9-e27c-8541-839ca839b928"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "40" },
                    { new Guid("32f189cb-3203-538c-4e22-ff53f652b269"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "19" },
                    { new Guid("35117c04-8e25-d880-cae5-3ccbd7aa7e1d"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "10" },
                    { new Guid("35e2ca2d-7d2d-91d1-1544-25b8c7e07444"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "14" },
                    { new Guid("36de3c2b-e765-2c1b-28f1-ca1f138391ea"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "15" },
                    { new Guid("3757b8f2-fbc3-9a72-3625-3f8abfbf5e51"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "15" },
                    { new Guid("377fef42-6470-071d-72a6-53f68595adf0"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "12" },
                    { new Guid("39638c1e-64b7-ba88-9548-fb24309e72f6"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "21" },
                    { new Guid("399036c6-eb87-f359-6600-89ccc233c06f"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "17" },
                    { new Guid("3d778329-3347-ff12-049a-0d807ea44e3b"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "10" },
                    { new Guid("3e9c924a-025b-f2f5-5db6-84cb6bc50b63"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "32" },
                    { new Guid("3fdf9140-7d84-b797-71ff-02791f6f71b1"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "13" },
                    { new Guid("40ce4720-8c42-1fe9-4e87-7aef97df998b"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "20" },
                    { new Guid("42d1b0e5-0165-cad9-2bb1-e269ffc63c8e"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "31" },
                    { new Guid("46cccd7c-6603-11d1-fe28-24825efb8349"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "8" },
                    { new Guid("475e7fa4-b1fb-a21d-ebdf-6ea91f0ac171"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "21" },
                    { new Guid("47e6d7a5-2bf9-310b-d25c-0299a664499d"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "35" },
                    { new Guid("4b2a3fd0-1cb3-396a-ac4f-c854ae45bef6"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "9" },
                    { new Guid("4b6402ba-fbcb-3c00-fb76-a36c8363773e"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "10" },
                    { new Guid("4bdd4e99-a97b-da27-13c3-17260ce70504"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "46" },
                    { new Guid("4e21acdf-2a58-78c0-37a5-fc80303918b5"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "19" },
                    { new Guid("4f433cb3-f947-1dc1-e124-cb350bb6e611"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "34" },
                    { new Guid("4fd975b6-ae2d-3f54-f7fd-e0e60c8c9d40"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "36" },
                    { new Guid("51fdce15-adc1-54af-f299-23f27391508e"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "1" },
                    { new Guid("5276f34b-13bd-e0a0-148a-9397fe30dc46"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "3" },
                    { new Guid("54e02fd2-1b6c-91bf-ca3a-7fa0b1986627"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "11" },
                    { new Guid("55ef05de-c53c-d50d-5e06-aceba50e2ebe"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "38" },
                    { new Guid("5a424bf2-eebf-6c57-4041-4968d307b7f6"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "34" },
                    { new Guid("5b82496b-5e19-e2ff-9e64-7539c8b76249"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "12" },
                    { new Guid("5ceb3e87-590c-543f-355f-5812efd7f878"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "39" },
                    { new Guid("614882ee-37c2-ea70-3884-309f8671befb"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "3" },
                    { new Guid("619446bb-a73e-be5e-ba7a-7b13ceac7243"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "31" },
                    { new Guid("61dd7bde-bf77-6ee2-61ec-c84b68eaeb52"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "25" },
                    { new Guid("63047a43-6b31-4735-ce53-dda17eb45ea2"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "24" },
                    { new Guid("63a24996-bb8b-8c4e-3623-1abccea17a59"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "18" },
                    { new Guid("66ad4764-086c-8212-0dca-56956e1bacaf"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "40" },
                    { new Guid("670e0be0-97fc-f841-58f3-6637c10af835"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "38" },
                    { new Guid("675f4812-f959-3bfe-6dde-98e9f29da07c"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "45" },
                    { new Guid("691df5db-185c-3c2e-5bf0-945ceab37092"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "2" },
                    { new Guid("698708f3-dbba-4050-6687-bd57df07fbdb"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "9" },
                    { new Guid("69b6afd0-cf65-6441-8d3c-7fb1f5f6687d"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "7" },
                    { new Guid("6f5cbf2d-a096-ddb0-1462-430f27963dd9"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "16" },
                    { new Guid("70d0bb0e-c5c1-4653-6035-a43eb8fcb470"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "2" },
                    { new Guid("70ee4dc7-339f-37ef-b328-c8bceb5a3f2e"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "8" },
                    { new Guid("711a609e-3c0b-05c3-bc0c-d348bcdd41cd"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "42" },
                    { new Guid("718b3b8f-770e-6a3e-651a-4f2b1b556851"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "28" },
                    { new Guid("76299b28-678b-e3b4-63f8-bfa536ed9822"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "35" },
                    { new Guid("76f9e26d-36cc-c258-c566-ec9cbada17d2"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "42" },
                    { new Guid("7706a439-5631-0ab5-164c-e64099237890"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "37" },
                    { new Guid("7894ab69-e84d-f3b8-d7f0-f08df51ba919"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "5" },
                    { new Guid("78acc64c-16f3-7495-b724-107f64a2907b"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "17" },
                    { new Guid("78c22159-21fe-36c6-73bd-8e3a29aa64af"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "35" },
                    { new Guid("7f6bc73e-73e4-768d-06ac-b90c97372143"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "41" },
                    { new Guid("800f10b4-6771-dc6d-2d4c-82d5ec47d70d"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "44" },
                    { new Guid("808b6928-2471-a30b-aa10-bd985352c940"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "43" },
                    { new Guid("825a4ebe-f0da-bbf3-54e8-a98771d84147"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "25" },
                    { new Guid("83502f8b-936f-5da3-0a65-ac76ebc51d0c"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "13" },
                    { new Guid("848b8d8a-c6f5-38c1-d724-fab244c7efb6"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "11" },
                    { new Guid("878f7c01-d9a1-85ad-0550-fb9540f0d42c"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "38" },
                    { new Guid("88d014fd-0197-2198-4021-4988a010261b"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "7" },
                    { new Guid("8bb9129d-c54c-d708-a3b8-9b6b5fcb5c52"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "33" },
                    { new Guid("903c9f3e-b631-ba85-79ee-ee94f04bc1ed"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "16" },
                    { new Guid("925e6fcb-979e-9adf-4c05-286b408c4367"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "44" },
                    { new Guid("936d51ac-d8c7-2213-a51b-b465492b6386"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "13" },
                    { new Guid("9569f343-4deb-a71c-b187-75225342fd61"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "4" },
                    { new Guid("963975d1-28fc-ba0b-680f-17c683626dd8"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "36" },
                    { new Guid("9661ffa1-c9ba-cfe0-dae3-256e041f60ce"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "46" },
                    { new Guid("97397422-bb0d-caa2-7630-86091502c8ae"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "20" },
                    { new Guid("9f669a8d-c8ce-989f-50d2-ed5892246d42"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "37" },
                    { new Guid("a43e9de8-979a-cb24-eea1-c07ec6d1e77f"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "33" },
                    { new Guid("a91bd7ca-db5e-9a4c-6a60-79740cdd2e48"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "30" },
                    { new Guid("aace95d3-4983-48d7-b89d-84f4c3bd8df6"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "41" },
                    { new Guid("af5267c9-5c84-2a28-c36e-0c3b9a9c19e3"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "29" },
                    { new Guid("afe9f5e3-b489-7304-3008-2c5de58dc2c9"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "36" },
                    { new Guid("b198f716-f30d-9f26-67db-7c2e2f80b176"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "50" },
                    { new Guid("b1fc00ff-ec33-41a6-72d5-ef25579bef41"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "42" },
                    { new Guid("b2fe9aef-e57e-3764-46e8-12de613e1057"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "15" },
                    { new Guid("b39ddf6f-39e5-1cc9-5966-ffbb214be937"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "12" },
                    { new Guid("b4455302-b212-4520-ff84-5f3e4bd4cc3b"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "22" },
                    { new Guid("b5d80338-4e61-fdf5-faa0-b8a69c3651e0"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "28" },
                    { new Guid("b61a8a9b-d428-7436-e7c3-e3e485e35d1f"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "21" },
                    { new Guid("b6bd0875-064d-983b-de52-ad6eb04ade56"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "4" },
                    { new Guid("b6f913c2-8945-491d-c7d2-39bffa5e5503"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "23" },
                    { new Guid("ba2948a3-524b-ee6a-9f2a-439d1b3bdf76"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "45" },
                    { new Guid("bb504df4-9115-2fbb-13b8-f3f692ecf2f2"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "18" },
                    { new Guid("bc1c0533-c9c7-fb9a-a90b-6ea363127940"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "14" },
                    { new Guid("be6cc5fe-42b2-35ea-560d-e71cf6cd1151"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "48" },
                    { new Guid("c259fbe2-2d77-774c-fe84-afbbc202af16"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "29" },
                    { new Guid("c25c6983-4c9f-0dbd-e8ae-e451a34b5a91"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "49" },
                    { new Guid("c53889e3-90af-702d-4b19-5b94c88388a4"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "43" },
                    { new Guid("c91ec45a-4ab1-401d-252a-44d9b6f091bc"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "30" },
                    { new Guid("cc7dcc30-ba74-3299-8bcc-2928a0ee3086"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "3" },
                    { new Guid("d0bf4361-7b77-bd9f-c110-ec55fcbad383"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "2" },
                    { new Guid("d1497c23-ab85-09c7-8414-8a25311075be"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "24" },
                    { new Guid("d177ee7d-3ad7-7f58-8733-b525b4339356"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "44" },
                    { new Guid("d94a86bc-e609-a476-6864-44fba882cd5d"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "26" },
                    { new Guid("daf55a61-83bc-c73d-7226-e1dafe4ad08c"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "1" },
                    { new Guid("df120787-c0ae-2cce-25b2-148432432e96"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "27" },
                    { new Guid("e7ba7f62-a24c-3b23-d2c0-2919e0fb316c"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "11" },
                    { new Guid("e89c935d-6787-73c7-f7f3-00576daa4209"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "27" },
                    { new Guid("e8c9725b-23e1-76b2-9a1e-f14d72ec40aa"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "22" },
                    { new Guid("e92aba20-a85a-f5bc-19c2-c65804346d24"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "34" },
                    { new Guid("ebea7e80-12f2-a120-c89a-eca85e612805"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "28" },
                    { new Guid("ec68c30b-9439-60bc-f0ce-788a8aedfeb3"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "19" },
                    { new Guid("f153e2c6-c43c-d5cc-9702-4013345fd6a8"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "32" },
                    { new Guid("f1fc2118-c377-ec8e-2b11-997d1a0b89bd"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "8" },
                    { new Guid("f23234f4-64fe-9873-db42-cfb449020f0b"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "20" },
                    { new Guid("f5162fc2-e360-db7b-8395-73fb18c2c6c3"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "18" },
                    { new Guid("f5bd96db-129a-9a2d-cedc-0d2476cb1499"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "48" },
                    { new Guid("f979b868-a9b1-b14b-0afb-16675233ffdc"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "9" },
                    { new Guid("fa756733-8a6a-7e31-cfe8-8146911147b2"), new Guid("bbbbbbbb-0000-0000-0000-000000000002"), null, true, null, null, "30" },
                    { new Guid("facd4eb5-53c6-08b8-378d-fc3e6c8f4e0a"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "45" },
                    { new Guid("fae5f569-4fed-1f6f-b722-db6c510accdd"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "6" },
                    { new Guid("fc165bbf-4de8-5c55-8cf2-2d288d08e866"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "40" },
                    { new Guid("fc8b0a7e-da81-71f4-98e0-d8d3b906b4db"), new Guid("a2e34b73-475b-ab84-1236-b77bc807d26f"), null, true, null, null, "41" },
                    { new Guid("ff63b66e-3e46-0085-8aac-d6e7d8a09b76"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "1" },
                    { new Guid("fff99fcf-efbc-8270-7e9c-d1da1efe01e5"), new Guid("bbbbbbbb-0000-0000-0000-000000000001"), null, true, null, null, "39" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("035c2aea-4e19-869b-3a4a-8ebff0c3ecca"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("05677b5d-59c4-ac62-7b8b-e15a0bf36520"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("0b150199-187d-77c2-9160-56f75bc6d8d3"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("0c8b3464-c503-ec5a-25ca-a2eba6a0b104"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("0d4e6f7f-5a56-6ecb-96ba-99d26ee348af"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("0d902d04-ad60-fb64-d1ce-cc935e801204"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("0da8e991-a20b-3a4d-b5fe-6ee391959a4a"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("0e46ffdc-ee6e-6840-1393-f61dfd22777f"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("0eaee7e1-d260-560e-f25b-3dde5b52dc9b"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("0f505ad8-25e9-92a1-d93a-0302e8d28d82"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("11089b5d-ccc7-f542-b225-eae62343703c"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("119bbb2e-846e-0572-f974-3674198e7a3f"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("121a8fef-e2b3-737b-fa05-97812c63a239"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("1492ce3e-b9a2-eb49-cd8d-c3a48eda2c67"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("16b1a9ca-6b20-4802-79f2-fe3a78d15b75"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("1c97c820-d62a-73f2-2baa-9fddf7bd7fff"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("1ffeac7b-02ec-e57a-d62d-b51b46006fed"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("22dae416-de17-bb82-805d-d160e37ffa16"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("2323c0e4-dfba-a657-4c3c-a52a768e7788"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("245d861b-b459-63ea-3afc-37cbcb98b80f"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("27f5f39c-d74a-ac88-43f9-e7d3519003d9"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("2b6a4a2b-9a6d-1a17-fe74-e5bb827eacaf"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("2b86af15-76aa-7cfa-e6af-c14e9fa3d45e"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("2dfe5f1a-effb-f3fc-bb73-15481193ce15"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("2f451571-02f4-dd09-384e-80046bc8ecc9"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("3047edff-e4a9-b39f-bf51-765fa61c2d1b"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("3049f56a-33b9-e27c-8541-839ca839b928"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("32f189cb-3203-538c-4e22-ff53f652b269"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("35117c04-8e25-d880-cae5-3ccbd7aa7e1d"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("35e2ca2d-7d2d-91d1-1544-25b8c7e07444"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("36de3c2b-e765-2c1b-28f1-ca1f138391ea"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("3757b8f2-fbc3-9a72-3625-3f8abfbf5e51"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("377fef42-6470-071d-72a6-53f68595adf0"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("39638c1e-64b7-ba88-9548-fb24309e72f6"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("399036c6-eb87-f359-6600-89ccc233c06f"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("3d778329-3347-ff12-049a-0d807ea44e3b"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("3e9c924a-025b-f2f5-5db6-84cb6bc50b63"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("3fdf9140-7d84-b797-71ff-02791f6f71b1"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("40ce4720-8c42-1fe9-4e87-7aef97df998b"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("42d1b0e5-0165-cad9-2bb1-e269ffc63c8e"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("46cccd7c-6603-11d1-fe28-24825efb8349"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("475e7fa4-b1fb-a21d-ebdf-6ea91f0ac171"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("47e6d7a5-2bf9-310b-d25c-0299a664499d"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("4b2a3fd0-1cb3-396a-ac4f-c854ae45bef6"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("4b6402ba-fbcb-3c00-fb76-a36c8363773e"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("4bdd4e99-a97b-da27-13c3-17260ce70504"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("4e21acdf-2a58-78c0-37a5-fc80303918b5"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("4f433cb3-f947-1dc1-e124-cb350bb6e611"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("4fd975b6-ae2d-3f54-f7fd-e0e60c8c9d40"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("51fdce15-adc1-54af-f299-23f27391508e"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("5276f34b-13bd-e0a0-148a-9397fe30dc46"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("54e02fd2-1b6c-91bf-ca3a-7fa0b1986627"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("55ef05de-c53c-d50d-5e06-aceba50e2ebe"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("5a424bf2-eebf-6c57-4041-4968d307b7f6"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("5b82496b-5e19-e2ff-9e64-7539c8b76249"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("5ceb3e87-590c-543f-355f-5812efd7f878"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("614882ee-37c2-ea70-3884-309f8671befb"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("619446bb-a73e-be5e-ba7a-7b13ceac7243"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("61dd7bde-bf77-6ee2-61ec-c84b68eaeb52"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("63047a43-6b31-4735-ce53-dda17eb45ea2"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("63a24996-bb8b-8c4e-3623-1abccea17a59"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("66ad4764-086c-8212-0dca-56956e1bacaf"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("670e0be0-97fc-f841-58f3-6637c10af835"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("675f4812-f959-3bfe-6dde-98e9f29da07c"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("691df5db-185c-3c2e-5bf0-945ceab37092"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("698708f3-dbba-4050-6687-bd57df07fbdb"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("69b6afd0-cf65-6441-8d3c-7fb1f5f6687d"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("6f5cbf2d-a096-ddb0-1462-430f27963dd9"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("70d0bb0e-c5c1-4653-6035-a43eb8fcb470"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("70ee4dc7-339f-37ef-b328-c8bceb5a3f2e"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("711a609e-3c0b-05c3-bc0c-d348bcdd41cd"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("718b3b8f-770e-6a3e-651a-4f2b1b556851"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("76299b28-678b-e3b4-63f8-bfa536ed9822"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("76f9e26d-36cc-c258-c566-ec9cbada17d2"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("7706a439-5631-0ab5-164c-e64099237890"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("7894ab69-e84d-f3b8-d7f0-f08df51ba919"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("78acc64c-16f3-7495-b724-107f64a2907b"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("78c22159-21fe-36c6-73bd-8e3a29aa64af"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("7f6bc73e-73e4-768d-06ac-b90c97372143"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("800f10b4-6771-dc6d-2d4c-82d5ec47d70d"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("808b6928-2471-a30b-aa10-bd985352c940"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("825a4ebe-f0da-bbf3-54e8-a98771d84147"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("83502f8b-936f-5da3-0a65-ac76ebc51d0c"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("848b8d8a-c6f5-38c1-d724-fab244c7efb6"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("878f7c01-d9a1-85ad-0550-fb9540f0d42c"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("88d014fd-0197-2198-4021-4988a010261b"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("8bb9129d-c54c-d708-a3b8-9b6b5fcb5c52"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("903c9f3e-b631-ba85-79ee-ee94f04bc1ed"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("925e6fcb-979e-9adf-4c05-286b408c4367"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("936d51ac-d8c7-2213-a51b-b465492b6386"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("9569f343-4deb-a71c-b187-75225342fd61"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("963975d1-28fc-ba0b-680f-17c683626dd8"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("9661ffa1-c9ba-cfe0-dae3-256e041f60ce"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("97397422-bb0d-caa2-7630-86091502c8ae"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("9f669a8d-c8ce-989f-50d2-ed5892246d42"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("a43e9de8-979a-cb24-eea1-c07ec6d1e77f"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("a91bd7ca-db5e-9a4c-6a60-79740cdd2e48"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("aace95d3-4983-48d7-b89d-84f4c3bd8df6"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("af5267c9-5c84-2a28-c36e-0c3b9a9c19e3"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("afe9f5e3-b489-7304-3008-2c5de58dc2c9"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("b198f716-f30d-9f26-67db-7c2e2f80b176"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("b1fc00ff-ec33-41a6-72d5-ef25579bef41"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("b2fe9aef-e57e-3764-46e8-12de613e1057"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("b39ddf6f-39e5-1cc9-5966-ffbb214be937"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("b4455302-b212-4520-ff84-5f3e4bd4cc3b"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("b5d80338-4e61-fdf5-faa0-b8a69c3651e0"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("b61a8a9b-d428-7436-e7c3-e3e485e35d1f"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("b6bd0875-064d-983b-de52-ad6eb04ade56"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("b6f913c2-8945-491d-c7d2-39bffa5e5503"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("ba2948a3-524b-ee6a-9f2a-439d1b3bdf76"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("bb504df4-9115-2fbb-13b8-f3f692ecf2f2"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("bc1c0533-c9c7-fb9a-a90b-6ea363127940"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("be6cc5fe-42b2-35ea-560d-e71cf6cd1151"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("c259fbe2-2d77-774c-fe84-afbbc202af16"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("c25c6983-4c9f-0dbd-e8ae-e451a34b5a91"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("c53889e3-90af-702d-4b19-5b94c88388a4"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("c91ec45a-4ab1-401d-252a-44d9b6f091bc"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("cc7dcc30-ba74-3299-8bcc-2928a0ee3086"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("d0bf4361-7b77-bd9f-c110-ec55fcbad383"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("d1497c23-ab85-09c7-8414-8a25311075be"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("d177ee7d-3ad7-7f58-8733-b525b4339356"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("d94a86bc-e609-a476-6864-44fba882cd5d"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("daf55a61-83bc-c73d-7226-e1dafe4ad08c"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("df120787-c0ae-2cce-25b2-148432432e96"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("e7ba7f62-a24c-3b23-d2c0-2919e0fb316c"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("e89c935d-6787-73c7-f7f3-00576daa4209"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("e8c9725b-23e1-76b2-9a1e-f14d72ec40aa"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("e92aba20-a85a-f5bc-19c2-c65804346d24"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("ebea7e80-12f2-a120-c89a-eca85e612805"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("ec68c30b-9439-60bc-f0ce-788a8aedfeb3"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("f153e2c6-c43c-d5cc-9702-4013345fd6a8"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("f1fc2118-c377-ec8e-2b11-997d1a0b89bd"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("f23234f4-64fe-9873-db42-cfb449020f0b"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("f5162fc2-e360-db7b-8395-73fb18c2c6c3"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("f5bd96db-129a-9a2d-cedc-0d2476cb1499"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("f979b868-a9b1-b14b-0afb-16675233ffdc"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("fa756733-8a6a-7e31-cfe8-8146911147b2"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("facd4eb5-53c6-08b8-378d-fc3e6c8f4e0a"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("fae5f569-4fed-1f6f-b722-db6c510accdd"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("fc165bbf-4de8-5c55-8cf2-2d288d08e866"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("fc8b0a7e-da81-71f4-98e0-d8d3b906b4db"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("ff63b66e-3e46-0085-8aac-d6e7d8a09b76"));

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("fff99fcf-efbc-8270-7e9c-d1da1efe01e5"));

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
    }
}
