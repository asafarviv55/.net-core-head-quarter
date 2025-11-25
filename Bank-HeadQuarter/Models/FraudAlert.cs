namespace Bank_HeadQuarter.Models
{
    public class FraudAlert
    {
        public int AlertId { get; set; }
        public DateTime AlertDateTime { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public decimal TransactionAmount { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public string FraudType { get; set; } = string.Empty;
        public string RiskScore { get; set; } = string.Empty;
        public string AlertStatus { get; set; } = string.Empty;
        public string AssignedTo { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool IsConfirmedFraud { get; set; }
        public DateTime? ResolvedDate { get; set; }
    }
}
