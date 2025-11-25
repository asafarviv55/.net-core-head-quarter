namespace Bank_HeadQuarter.Models
{
    public class Branch
    {
        public int BranchId { get; set; }
        public string BranchCode { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string ManagerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime OpeningDate { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalCustomers { get; set; }
    }
}
