using Bank_HeadQuarter.Models;

namespace Bank_HeadQuarter.Services
{
    public class PerformanceAnalyticsService
    {
        private readonly List<PerformanceMetrics> _metrics = new();

        public PerformanceAnalyticsService()
        {
            InitializeSampleMetrics();
        }

        private void InitializeSampleMetrics()
        {
            _metrics.Add(new PerformanceMetrics
            {
                MetricId = 1,
                BranchId = 1,
                BranchName = "Downtown Main Branch",
                MetricMonth = new DateTime(2023, 10, 1),
                TotalDeposits = 15000000m,
                TotalWithdrawals = 8000000m,
                TotalLoansIssued = 5000000m,
                NewAccountsOpened = 250,
                AccountsClosed = 45,
                CustomerSatisfactionScore = 4.5m,
                ComplaintsReceived = 12,
                ComplaintsResolved = 11,
                NetProfit = 850000m,
                OperatingCosts = 450000m,
                EmployeeProductivity = 92.5
            });

            _metrics.Add(new PerformanceMetrics
            {
                MetricId = 2,
                BranchId = 2,
                BranchName = "Westside Branch",
                MetricMonth = new DateTime(2023, 10, 1),
                TotalDeposits = 10000000m,
                TotalWithdrawals = 5500000m,
                TotalLoansIssued = 3200000m,
                NewAccountsOpened = 180,
                AccountsClosed = 30,
                CustomerSatisfactionScore = 4.3m,
                ComplaintsReceived = 8,
                ComplaintsResolved = 7,
                NetProfit = 620000m,
                OperatingCosts = 380000m,
                EmployeeProductivity = 88.0
            });
        }

        public List<PerformanceMetrics> GetAllMetrics() => _metrics;

        public PerformanceMetrics? GetMetricsByBranchAndMonth(int branchId, DateTime month)
        {
            return _metrics.FirstOrDefault(m =>
                m.BranchId == branchId &&
                m.MetricMonth.Year == month.Year &&
                m.MetricMonth.Month == month.Month);
        }

        public List<PerformanceMetrics> GetMetricsByBranch(int branchId) =>
            _metrics.Where(m => m.BranchId == branchId).OrderByDescending(m => m.MetricMonth).ToList();

        public bool AddMetrics(PerformanceMetrics metrics)
        {
            metrics.MetricId = _metrics.Any() ? _metrics.Max(m => m.MetricId) + 1 : 1;
            _metrics.Add(metrics);
            return true;
        }

        public decimal CalculateNetProfitMargin(PerformanceMetrics metrics)
        {
            var totalRevenue = metrics.TotalDeposits - metrics.TotalWithdrawals + metrics.TotalLoansIssued;
            return totalRevenue > 0 ? (metrics.NetProfit / totalRevenue) * 100 : 0;
        }

        public double CalculateCustomerRetentionRate(PerformanceMetrics metrics)
        {
            var totalAccounts = metrics.NewAccountsOpened + metrics.AccountsClosed;
            return totalAccounts > 0 ? ((double)(totalAccounts - metrics.AccountsClosed) / totalAccounts) * 100 : 0;
        }

        public Dictionary<string, object> GetBranchPerformanceSummary(int branchId)
        {
            var branchMetrics = GetMetricsByBranch(branchId);
            if (!branchMetrics.Any())
                return new Dictionary<string, object>();

            var latestMetric = branchMetrics.First();
            return new Dictionary<string, object>
            {
                { "Branch", latestMetric.BranchName },
                { "Total Deposits", latestMetric.TotalDeposits },
                { "Net Profit", latestMetric.NetProfit },
                { "Profit Margin", $"{CalculateNetProfitMargin(latestMetric):F2}%" },
                { "Customer Satisfaction", latestMetric.CustomerSatisfactionScore },
                { "Retention Rate", $"{CalculateCustomerRetentionRate(latestMetric):F2}%" },
                { "Employee Productivity", $"{latestMetric.EmployeeProductivity:F1}%" }
            };
        }

        public List<PerformanceMetrics> GetTopPerformingBranches(int count)
        {
            return _metrics
                .GroupBy(m => m.BranchId)
                .Select(g => g.OrderByDescending(m => m.MetricMonth).First())
                .OrderByDescending(m => m.NetProfit)
                .Take(count)
                .ToList();
        }
    }
}
