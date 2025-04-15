using System;

namespace LabManagementApp.Infrastructure.Models
{
  public class TestSession
  {
    public Guid TestSessionID { get; set; } // Primary Key
    public Guid ProbeID { get; set; } // Foreign Key referencing Probe
    public string TestType { get; set; } // e.g., Tuning and Matching, Sensitivity, etc.
    public DateTime TestDate { get; set; }
    public string Tester { get; set; } // Name of the technician performing the test
    public string SoftwareVersion { get; set; } // Version of the software used for testing
    public string InstrumentUsed { get; set; } // e.g., Network Analyzer Model, Spectrometer Name
    public string PassFailStatus { get; set; } // Pass, Fail, N/A
    public string? Notes { get; set; } // Optional notes about the test session

    // Navigation property for related Probe
    public Probe Probe { get; set; }
  }
}