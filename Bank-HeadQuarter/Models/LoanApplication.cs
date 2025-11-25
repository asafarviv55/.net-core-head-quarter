namespace Bank_HeadQuarter.Models
{
    public class LoanApplication
    {
        public int ApplicationId { get; set; }
        public string ApplicationNumber { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerAccountNumber { get; set; } = string.Empty;
        public string LoanType { get; set; } = string.Empty;
        public decimal RequestedAmount { get; set; }
        public decimal ApprovedAmount { get; set; }
        public int LoanTerm { get; set; } // in months
        public decimal InterestRate { get; set; }
        public string ApplicationStatus { get; set; } = string.Empty;
        public string ReviewedBy { get; set; } = string.Empty;
        public DateTime? ReviewDate { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string CustomerCreditScore { get; set; } = string.Empty;
        public string CollateralType { get; set; } = string.Empty;
        public string RejectionReason { get; set; } = string.Empty;
    }
}
