using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LabManagementApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Probes",
                columns: table => new
                {
                    ProbeID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProbeType = table.Column<string>(type: "TEXT", nullable: false),
                    DesignVersion = table.Column<string>(type: "TEXT", nullable: false),
                    ManufacturingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ManufacturingTechnician = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Probes", x => x.ProbeID);
                });

            migrationBuilder.CreateTable(
                name: "TestSessions",
                columns: table => new
                {
                    TestSessionID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProbeID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TestType = table.Column<string>(type: "TEXT", nullable: false),
                    TestDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Tester = table.Column<string>(type: "TEXT", nullable: false),
                    SoftwareVersion = table.Column<string>(type: "TEXT", nullable: false),
                    InstrumentUsed = table.Column<string>(type: "TEXT", nullable: false),
                    PassFailStatus = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSessions", x => x.TestSessionID);
                    table.ForeignKey(
                        name: "FK_TestSessions_Probes_ProbeID",
                        column: x => x.ProbeID,
                        principalTable: "Probes",
                        principalColumn: "ProbeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Probes",
                columns: new[] { "ProbeID", "DesignVersion", "ManufacturingDate", "ManufacturingTechnician", "Notes", "ProbeType" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "V1.0", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe", "Initial batch", "High-Resolution Liquid Probe" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "V2.1", new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Smith", "Updated design", "Solid-State MAS Probe" }
                });

            migrationBuilder.InsertData(
                table: "TestSessions",
                columns: new[] { "TestSessionID", "InstrumentUsed", "Notes", "PassFailStatus", "ProbeID", "SoftwareVersion", "TestDate", "TestType", "Tester" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), "Network Analyzer Model X", "All parameters within range", "Pass", new Guid("11111111-1111-1111-1111-111111111111"), "1.2.3", new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tuning and Matching", "Alice Brown" });

            migrationBuilder.CreateIndex(
                name: "IX_TestSessions_ProbeID",
                table: "TestSessions",
                column: "ProbeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestSessions");

            migrationBuilder.DropTable(
                name: "Probes");
        }
    }
}
