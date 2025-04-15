using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabManagementApp.Infrastructure.Models
{
  public class Probe
  {
    [Key]
    [JsonPropertyName("probeID")]
    public Guid ProbeID { get; set; } // Primary Key

    public Probe()
    {
      TestSessions = new List<TestSession>();
    }

    [Required]
    [StringLength(100)]
    [JsonPropertyName("probeType")]
    public string ProbeType { get; set; } // e.g., High-Resolution Liquid Probe, Solid-State MAS Probe

    [Required]
    [StringLength(50)]
    [JsonPropertyName("designVersion")]
    public string DesignVersion { get; set; }

    [Required]
    [JsonPropertyName("manufacturingDate")]
    public DateTime ManufacturingDate { get; set; }

    [Required]
    [StringLength(100)]
    [JsonPropertyName("manufacturingTechnician")]
    public string ManufacturingTechnician { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [JsonPropertyName("probeModel")]
    public string ProbeModel { get; set; } = string.Empty;

    [StringLength(500)]
    [JsonPropertyName("notes")]
    public string? Notes { get; set; } // Optional

    // Navigation property for related TestSessions
    [JsonPropertyName("testSessions")]
    public ICollection<TestSession> TestSessions { get; set; } = new List<TestSession>();
  }
}