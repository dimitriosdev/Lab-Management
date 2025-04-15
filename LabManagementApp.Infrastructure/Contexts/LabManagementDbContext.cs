using Microsoft.EntityFrameworkCore;
using LabManagementApp.Infrastructure.Models;
using System;

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
        optionsBuilder.UseSqlite("Data Source=d:/repos/playground/dotnet/LabManagementApp/LabManagementApp.Infrastructure/LabManagement.db");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Seed data for Probes
      modelBuilder.Entity<Probe>().HasData(
          new Probe
          {
            ProbeID = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            ProbeType = "High-Resolution Liquid Probe",
            DesignVersion = "V1.0",
            ManufacturingDate = new DateTime(2023, 1, 15),
            ManufacturingTechnician = "John Doe",
            Notes = "Initial batch"
          },
          new Probe
          {
            ProbeID = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            ProbeType = "Solid-State MAS Probe",
            DesignVersion = "V2.1",
            ManufacturingDate = new DateTime(2024, 6, 10),
            ManufacturingTechnician = "Jane Smith",
            Notes = "Updated design"
          }
      );

      // Seed data for TestSessions
      modelBuilder.Entity<TestSession>().HasData(
          new TestSession
          {
            TestSessionID = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            ProbeID = Guid.Parse("11111111-1111-1111-1111-111111111111"), // Matches the first Probe
            TestType = "Tuning and Matching",
            TestDate = new DateTime(2025, 3, 20),
            Tester = "Alice Brown",
            SoftwareVersion = "1.2.3",
            InstrumentUsed = "Network Analyzer Model X",
            PassFailStatus = "Pass",
            Notes = "All parameters within range"
          }
      );
    }
  }
}