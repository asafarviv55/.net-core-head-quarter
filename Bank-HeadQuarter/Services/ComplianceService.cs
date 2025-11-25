using Bank_HeadQuarter.Models;

namespace Bank_HeadQuarter.Services
{
    public class ComplianceService
    {
        private readonly List<ComplianceReport> _reports = new();

        public ComplianceService()
        {
            InitializeSampleReports();
        }

        private void InitializeSampleReports()
        {
            _reports.Add(new ComplianceReport
            {
                ReportId = 1,
                ReportType = "AML Compliance",
                ReportDate = DateTime.Now.AddDays(-5),
                ReportedBy = "Compliance Officer A",
                BranchId = 1,
                BranchName = "Downtown Main Branch",
                ComplianceStatus = "Compliant",
                Description = "Anti-Money Laundering quarterly review completed",
                RiskLevel = "Low",
                DueDate = DateTime.Now.AddDays(85),
                IsResolved = true,
                ResolutionNotes = "All checks passed successfully"
            });

            _reports.Add(new ComplianceReport
            {
                ReportId = 2,
                ReportType = "KYC Verification",
                ReportDate = DateTime.Now.AddDays(-3),
                ReportedBy = "Compliance Officer B",
                BranchId = 2,
                BranchName = "Westside Branch",
                ComplianceStatus = "Action Required",
                Description = "30 customer records require updated KYC documentation",
                RiskLevel = "Medium",
                DueDate = DateTime.Now.AddDays(14),
                IsResolved = false,
                ResolutionNotes = "Follow-up in progress"
            });
        }

        public List<ComplianceReport> GetAllReports() => _reports;

        public List<ComplianceReport> GetPendingReports() =>
            _reports.Where(r => !r.IsResolved).ToList();

        public List<ComplianceReport> GetReportsByRiskLevel(string riskLevel) =>
            _reports.Where(r => r.RiskLevel.Equals(riskLevel, StringComparison.OrdinalIgnoreCase)).ToList();

        public ComplianceReport? GetReportById(int reportId) =>
            _reports.FirstOrDefault(r => r.ReportId == reportId);

        public bool AddReport(ComplianceReport report)
        {
            report.ReportId = _reports.Any() ? _reports.Max(r => r.ReportId) + 1 : 1;
            report.ReportDate = DateTime.Now;
            _reports.Add(report);
            return true;
        }

        public bool UpdateReportStatus(int reportId, string status, string resolutionNotes)
        {
            var report = GetReportById(reportId);
            if (report == null)
                return false;

            report.ComplianceStatus = status;
            report.ResolutionNotes = resolutionNotes;
            report.IsResolved = status.Equals("Compliant", StringComparison.OrdinalIgnoreCase);
            return true;
        }

        public List<ComplianceReport> GetOverdueReports()
        {
            return _reports
                .Where(r => !r.IsResolved && r.DueDate < DateTime.Now)
                .ToList();
        }

        public Dictionary<string, int> GetComplianceStatistics()
        {
            return new Dictionary<string, int>
            {
                { "Total Reports", _reports.Count },
                { "Pending", GetPendingReports().Count },
                { "Overdue", GetOverdueReports().Count },
                { "High Risk", GetReportsByRiskLevel("High").Count }
            };
        }
    }
}
