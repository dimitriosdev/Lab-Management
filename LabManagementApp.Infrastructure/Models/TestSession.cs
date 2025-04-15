using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LabManagementApp.Infrastructure.Models
{
  public enum PassFailStatus
  {
    Pass,
    Fail,
    NA
  }

  public enum TestType
  {
    TuningAndMatching,
    Sensitivity,
    PulseCalibration,
    Homogeneity
  }

  public class TestSession
  {
    public TestSession()
    {
      Tester = string.Empty;
      SoftwareVersion = string.Empty;
      InstrumentUsed = string.Empty;
    }

    [Key]
    [JsonPropertyName("testSessionID")]
    public Guid TestSessionID { get; set; } // Primary Key

    [Required]
    [ForeignKey("Probe")]
    [JsonPropertyName("probeID")]
    public Guid ProbeID { get; set; } // Foreign Key referencing Probe

    [Required]
    [JsonPropertyName("testType")]
    public TestType TestType { get; set; } // e.g., Tuning and Matching, Sensitivity, etc.

    [Required]
    [JsonPropertyName("testDate")]
    public DateTime TestDate { get; set; }

    [Required]
    [StringLength(100)]
    [JsonPropertyName("tester")]
    public string Tester { get; set; } = string.Empty; // Name of the technician performing the test

    [Required]
    [StringLength(50)]
    [JsonPropertyName("softwareVersion")]
    public string SoftwareVersion { get; set; } = string.Empty; // Version of the software used for testing

    [Required]
    [StringLength(100)]
    [JsonPropertyName("instrumentUsed")]
    public string InstrumentUsed { get; set; } = string.Empty; // e.g., Network Analyzer Model, Spectrometer Name

    [Required]
    [JsonPropertyName("passFailStatus")]
    public PassFailStatus PassFailStatus { get; set; } // Pass, Fail, N/A

    [StringLength(500)]
    [JsonPropertyName("notes")]
    public string? Notes { get; set; } // Optional notes about the test session

    // Navigation property for related Probe
    public Probe Probe { get; set; }
  }
}