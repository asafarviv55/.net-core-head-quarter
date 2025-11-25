namespace Bank_HeadQuarter.Models
{
    public class PerformanceMetrics
    {
        public int MetricId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public DateTime MetricMonth { get; set; }
        public decimal TotalDeposits { get; set; }
        public decimal TotalWithdrawals { get; set; }
        public decimal TotalLoansIssued { get; set; }
        public int NewAccountsOpened { get; set; }
        public int AccountsClosed { get; set; }
        public decimal CustomerSatisfactionScore { get; set; }
        public int ComplaintsReceived { get; set; }
        public int ComplaintsResolved { get; set; }
        public decimal NetProfit { get; set; }
        public decimal OperatingCosts { get; set; }
        public double EmployeeProductivity { get; set; }
    }
}
