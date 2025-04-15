using Microsoft.EntityFrameworkCore;
using LabManagementApp.Infrastructure.Models;
using System;
using Microsoft.Extensions.Configuration;

namespace LabManagementApp.Infrastructure.Contexts
{
  public class LabManagementDbContext : DbContext
  {
    public DbSet<Probe> Probes { get; set; }
    public DbSet<TestSession> TestSessions { get; set; }

    public LabManagementDbContext(DbContextOptions<LabManagementDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("LabManagementDb");
        optionsBuilder.UseSqlite(connectionString);
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Configure one-to-many relationship between Probe and TestSession
      modelBuilder.Entity<TestSession>()
          .HasOne(ts => ts.Probe)
          .WithMany(p => p.TestSessions)
          .HasForeignKey(ts => ts.ProbeID)
          .OnDelete(DeleteBehavior.Cascade);

      // Seed data for Probes
      modelBuilder.Entity<Probe>().HasData(
          new Probe
          {
            ProbeID = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            ProbeModel = "Probe Model A",
            ProbeType = "High-Resolution Liquid Probe",
            DesignVersion = "V1.0",
            ManufacturingDate = new DateTime(2023, 1, 15),
            ManufacturingTechnician = "John Doe",
            Notes = "Initial batch"
          },
          new Probe
          {
            ProbeID = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            ProbeModel = "Probe Model B",
            ProbeType = "Solid-State MAS Probe",
            DesignVersion = "V2.1",
            ManufacturingDate = new DateTime(2024, 6, 10),
            ManufacturingTechnician = "Jane Smith",
            Notes = "Updated design"
          },
          new Probe
          {
            ProbeID = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            ProbeModel = "Probe Model C",
            ProbeType = "High-Resolution Liquid Probe",
            DesignVersion = "V1.1",
            ManufacturingDate = new DateTime(2024, 8, 20),
            ManufacturingTechnician = "Alice Brown",
            Notes = "Minor updates"
          }
      );

      // Seed data for TestSessions
      modelBuilder.Entity<TestSession>().HasData(
          new TestSession
          {
            TestSessionID = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            ProbeID = Guid.Parse("11111111-1111-1111-1111-111111111111"), // Matches the first Probe
            TestType = TestType.TuningAndMatching,
            TestDate = new DateTime(2025, 3, 20),
            Tester = "Alice Brown",
            SoftwareVersion = "1.2.3",
            InstrumentUsed = "Network Analyzer Model X",
            PassFailStatus = PassFailStatus.Pass,
            Notes = "All parameters within range",
          },
          new TestSession
          {
            TestSessionID = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            ProbeID = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            TestType = TestType.Sensitivity,
            TestDate = new DateTime(2025, 4, 15),
            Tester = "Bob White",
            SoftwareVersion = "2.0.0",
            InstrumentUsed = "Spectrometer Model Y",
            PassFailStatus = PassFailStatus.Fail,
            Notes = "Some parameters out of range"
          },
          new TestSession
          {
            TestSessionID = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            ProbeID = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            TestType = TestType.PulseCalibration,
            TestDate = new DateTime(2025, 5, 10),
            Tester = "Charlie Green",
            SoftwareVersion = "1.2.3",
            InstrumentUsed = "Network Analyzer Model X",
            PassFailStatus = PassFailStatus.NA,
            Notes = "Calibration not applicable for this probe type"
          },
          new TestSession
          {
            TestSessionID = Guid.Parse("66666666-6666-6666-6666-666666666666"),
            ProbeID = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            TestType = TestType.Homogeneity,
            TestDate = new DateTime(2025, 6, 5),
            Tester = "Diana Black",
            SoftwareVersion = "2.1.0",
            InstrumentUsed = "Spectrometer Model Y",
            PassFailStatus = PassFailStatus.Pass,
            Notes = "Homogeneity test passed"
          },
          new TestSession
          {
            TestSessionID = Guid.Parse("77777777-7777-7777-7777-777777777777"),
            ProbeID = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            TestType = TestType.TuningAndMatching,
            TestDate = new DateTime(2025, 7, 1),
            Tester = "Eve White",
            SoftwareVersion = "3.0.0",
            InstrumentUsed = "Network Analyzer Model Z",
            PassFailStatus = PassFailStatus.Pass,
            Notes = "New test session added"
          }
      );
    }
  }
}