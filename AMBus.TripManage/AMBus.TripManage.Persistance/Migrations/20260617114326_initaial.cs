using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMBus.TripManage.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class initaial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatConversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatConversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatConversations_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatConversations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LicenseExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmergencyContact = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    BusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StationAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StopOrder = table.Column<int>(type: "int", nullable: false),
                    ArrivalOffsetMinutes = table.Column<int>(type: "int", nullable: false),
                    RouteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stops_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatConversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "ChatConversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    FromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Routes_FromId",
                        column: x => x.FromId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Routes_ToId",
                        column: x => x.ToId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BookedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QrCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingSeats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassengerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PassengerIdNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingSeats_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingSeats_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "EGP"),
                    Method = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StripePaymentIntentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StripeClientSecret = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    FawryReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WalletMsisdn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    WalletRedirectUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OtcReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ExternalTransactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefundedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, "Admin", "ADMIN" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, "User", "USER" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, "Driver", "DRIVER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FullName", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("cccccccc-0000-0000-0000-000000000001"), 0, "7c715229-fa5a-4357-9abf-c0371f6240b0", new DateTime(2026, 6, 17, 11, 43, 25, 916, DateTimeKind.Utc).AddTicks(957), "driver1@ambus.com", false, "كابتن محمد أحمد", true, false, null, null, null, null, "", false, null, false, null },
                    { new Guid("cccccccc-0000-0000-0000-000000000002"), 0, "d23f8b66-9a0b-4b8c-8906-2573aff57270", new DateTime(2026, 6, 17, 11, 43, 25, 916, DateTimeKind.Utc).AddTicks(966), "driver2@ambus.com", false, "كابتن محمود علي", true, false, null, null, null, null, "", false, null, false, null }
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TripId",
                table: "Bookings",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSeats_BookingId",
                table: "BookingSeats",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSeats_SeatId",
                table: "BookingSeats",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_PlateNumber",
                table: "Buses",
                column: "PlateNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatConversations_AdminId",
                table: "ChatConversations",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatConversations_UserId",
                table: "ChatConversations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ConversationId",
                table: "ChatMessages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ConversationId_IsRead",
                table: "ChatMessages",
                columns: new[] { "ConversationId", "IsRead" });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_LicenseNumber",
                table: "Drivers",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_UserId",
                table: "Drivers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_FawryReferenceNumber",
                table: "Payments",
                column: "FawryReferenceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ReferenceNumber",
                table: "Payments",
                column: "ReferenceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StripeClientSecret",
                table: "Payments",
                column: "StripeClientSecret");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TripId",
                table: "Reviews",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_BusId",
                table: "Seats",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_RouteId",
                table: "Stops",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_BusId",
                table: "Trips",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DriverId",
                table: "Trips",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_FromId",
                table: "Trips",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ToId",
                table: "Trips",
                column: "ToId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookingSeats");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "ChatConversations");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
