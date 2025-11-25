using Bank_HeadQuarter.Models;

namespace Bank_HeadQuarter.Services
{
    public class FraudDetectionService
    {
        private readonly List<FraudAlert> _fraudAlerts = new();

        public FraudDetectionService()
        {
            InitializeSampleAlerts();
        }

        private void InitializeSampleAlerts()
        {
            _fraudAlerts.Add(new FraudAlert
            {
                AlertId = 1,
                AlertDateTime = DateTime.Now.AddHours(-2),
                AccountNumber = "1234567890",
                CustomerName = "Michael Brown",
                TransactionAmount = 15000m,
                TransactionType = "Wire Transfer",
                FraudType = "Unusual Transaction Amount",
                RiskScore = "High",
                AlertStatus = "Under Investigation",
                AssignedTo = "Fraud Team Alpha",
                Notes = "Large wire transfer to foreign account",
                IsConfirmedFraud = false
            });

            _fraudAlerts.Add(new FraudAlert
            {
                AlertId = 2,
                AlertDateTime = DateTime.Now.AddDays(-1),
                AccountNumber = "9876543210",
                CustomerName = "Emily Davis",
                TransactionAmount = 5000m,
                TransactionType = "ATM Withdrawal",
                FraudType = "Multiple Failed PIN Attempts",
                RiskScore = "Medium",
                AlertStatus = "Resolved",
                AssignedTo = "Fraud Team Beta",
                Notes = "Customer confirmed legitimate transaction",
                IsConfirmedFraud = false,
                ResolvedDate = DateTime.Now.AddHours(-12)
            });
        }

        public List<FraudAlert> GetAllAlerts() => _fraudAlerts;

        public List<FraudAlert> GetActiveAlerts() =>
            _fraudAlerts.Where(a => !a.IsConfirmedFraud && a.AlertStatus != "Resolved").ToList();

        public List<FraudAlert> GetAlertsByRiskScore(string riskScore) =>
            _fraudAlerts.Where(a => a.RiskScore.Equals(riskScore, StringComparison.OrdinalIgnoreCase)).ToList();

        public FraudAlert? GetAlertById(int alertId) =>
            _fraudAlerts.FirstOrDefault(a => a.AlertId == alertId);

        public bool AddAlert(FraudAlert alert)
        {
            alert.AlertId = _fraudAlerts.Any() ? _fraudAlerts.Max(a => a.AlertId) + 1 : 1;
            alert.AlertDateTime = DateTime.Now;
            _fraudAlerts.Add(alert);
            return true;
        }

        public bool ResolveAlert(int alertId, string resolutionNotes, bool confirmedFraud)
        {
            var alert = GetAlertById(alertId);
            if (alert == null)
                return false;

            alert.AlertStatus = "Resolved";
            alert.IsConfirmedFraud = confirmedFraud;
            alert.ResolvedDate = DateTime.Now;
            alert.Notes += $"\nResolution: {resolutionNotes}";
            return true;
        }

        public Dictionary<string, int> GetAlertStatistics()
        {
            return new Dictionary<string, int>
            {
                { "Total Alerts", _fraudAlerts.Count },
                { "Active Alerts", GetActiveAlerts().Count },
                { "High Risk", GetAlertsByRiskScore("High").Count },
                { "Confirmed Fraud", _fraudAlerts.Count(a => a.IsConfirmedFraud) }
            };
        }
    }
}
