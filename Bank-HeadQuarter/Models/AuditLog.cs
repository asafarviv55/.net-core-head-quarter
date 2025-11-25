namespace Bank_HeadQuarter.Models
{
    public class AuditLog
    {
        public int AuditId { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public int? BranchId { get; set; }
    }
}
