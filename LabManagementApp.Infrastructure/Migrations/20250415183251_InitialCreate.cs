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
                    ProbeType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DesignVersion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ManufacturingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ManufacturingTechnician = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ProbeModel = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
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
                    TestType = table.Column<int>(type: "INTEGER", nullable: false),
                    TestDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Tester = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SoftwareVersion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    InstrumentUsed = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PassFailStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
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
                columns: new[] { "ProbeID", "DesignVersion", "ManufacturingDate", "ManufacturingTechnician", "Notes", "ProbeModel", "ProbeType" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "V1.0", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe", "Initial batch", "Probe Model A", "High-Resolution Liquid Probe" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "V2.1", new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Smith", "Updated design", "Probe Model B", "Solid-State MAS Probe" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "V1.1", new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alice Brown", "Minor updates", "Probe Model C", "High-Resolution Liquid Probe" }
                });

            migrationBuilder.InsertData(
                table: "TestSessions",
                columns: new[] { "TestSessionID", "InstrumentUsed", "Notes", "PassFailStatus", "ProbeID", "SoftwareVersion", "TestDate", "TestType", "Tester" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Network Analyzer Model X", "All parameters within range", 0, new Guid("11111111-1111-1111-1111-111111111111"), "1.2.3", new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Alice Brown" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Spectrometer Model Y", "Some parameters out of range", 1, new Guid("22222222-2222-2222-2222-222222222222"), "2.0.0", new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Bob White" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Network Analyzer Model X", "Calibration not applicable for this probe type", 2, new Guid("11111111-1111-1111-1111-111111111111"), "1.2.3", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Charlie Green" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Spectrometer Model Y", "Homogeneity test passed", 0, new Guid("22222222-2222-2222-2222-222222222222"), "2.1.0", new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Diana Black" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Network Analyzer Model Z", "New test session added", 0, new Guid("11111111-1111-1111-1111-111111111111"), "3.0.0", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Eve White" }
                });

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
