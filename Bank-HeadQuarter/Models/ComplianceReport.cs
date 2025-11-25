namespace Bank_HeadQuarter.Models
{
    public class ComplianceReport
    {
        public int ReportId { get; set; }
        public string ReportType { get; set; } = string.Empty;
        public DateTime ReportDate { get; set; }
        public string ReportedBy { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string ComplianceStatus { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string RiskLevel { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool IsResolved { get; set; }
        public string ResolutionNotes { get; set; } = string.Empty;
    }
}
