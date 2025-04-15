using System;
using System.Collections.Generic;

namespace LabManagementApp.Infrastructure.Models
{
  public class Probe
  {
    public Guid ProbeID { get; set; } // Primary Key
    public string ProbeType { get; set; } // e.g., High-Resolution Liquid Probe, Solid-State MAS Probe
    public string DesignVersion { get; set; }
    public DateTime ManufacturingDate { get; set; }
    public string ManufacturingTechnician { get; set; }
    public string? Notes { get; set; } // Optional

    // Navigation property for related TestSessions
    public ICollection<TestSession> TestSessions { get; set; }
  }
}