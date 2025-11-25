using Bank_HeadQuarter.Models;

namespace Bank_HeadQuarter.Services
{
    public class LoanManagementService
    {
        private readonly List<LoanApplication> _loanApplications = new();

        public LoanManagementService()
        {
            InitializeSampleLoans();
        }

        private void InitializeSampleLoans()
        {
            _loanApplications.Add(new LoanApplication
            {
                ApplicationId = 1,
                ApplicationNumber = "LN2023001",
                ApplicationDate = DateTime.Now.AddDays(-10),
                CustomerName = "Robert Wilson",
                CustomerAccountNumber = "ACC1001",
                LoanType = "Home Loan",
                RequestedAmount = 350000m,
                ApprovedAmount = 350000m,
                LoanTerm = 360,
                InterestRate = 3.75m,
                ApplicationStatus = "Approved",
                ReviewedBy = "Loan Officer A",
                ReviewDate = DateTime.Now.AddDays(-5),
                BranchId = 1,
                BranchName = "Downtown Main Branch",
                CustomerCreditScore = "750",
                CollateralType = "Property"
            });

            _loanApplications.Add(new LoanApplication
            {
                ApplicationId = 2,
                ApplicationNumber = "LN2023002",
                ApplicationDate = DateTime.Now.AddDays(-7),
                CustomerName = "Lisa Anderson",
                CustomerAccountNumber = "ACC1002",
                LoanType = "Personal Loan",
                RequestedAmount = 25000m,
                ApprovedAmount = 0m,
                LoanTerm = 60,
                InterestRate = 0m,
                ApplicationStatus = "Rejected",
                ReviewedBy = "Loan Officer B",
                ReviewDate = DateTime.Now.AddDays(-3),
                BranchId = 2,
                BranchName = "Westside Branch",
                CustomerCreditScore = "580",
                CollateralType = "None",
                RejectionReason = "Credit score below minimum requirement"
            });

            _loanApplications.Add(new LoanApplication
            {
                ApplicationId = 3,
                ApplicationNumber = "LN2023003",
                ApplicationDate = DateTime.Now.AddDays(-2),
                CustomerName = "David Martinez",
                CustomerAccountNumber = "ACC1003",
                LoanType = "Business Loan",
                RequestedAmount = 500000m,
                ApprovedAmount = 0m,
                LoanTerm = 120,
                InterestRate = 0m,
                ApplicationStatus = "Under Review",
                ReviewedBy = "",
                BranchId = 1,
                BranchName = "Downtown Main Branch",
                CustomerCreditScore = "720",
                CollateralType = "Business Assets"
            });
        }

        public List<LoanApplication> GetAllApplications() => _loanApplications;

        public List<LoanApplication> GetPendingApplications() =>
            _loanApplications.Where(l => l.ApplicationStatus == "Under Review" || l.ApplicationStatus == "Pending").ToList();

        public LoanApplication? GetApplicationById(int applicationId) =>
            _loanApplications.FirstOrDefault(l => l.ApplicationId == applicationId);

        public List<LoanApplication> GetApplicationsByStatus(string status) =>
            _loanApplications.Where(l => l.ApplicationStatus.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

        public bool AddApplication(LoanApplication application)
        {
            application.ApplicationId = _loanApplications.Any() ? _loanApplications.Max(l => l.ApplicationId) + 1 : 1;
            application.ApplicationDate = DateTime.Now;
            _loanApplications.Add(application);
            return true;
        }

        public bool ApproveApplication(int applicationId, decimal approvedAmount, decimal interestRate, string reviewedBy)
        {
            var application = GetApplicationById(applicationId);
            if (application == null)
                return false;

            application.ApplicationStatus = "Approved";
            application.ApprovedAmount = approvedAmount;
            application.InterestRate = interestRate;
            application.ReviewedBy = reviewedBy;
            application.ReviewDate = DateTime.Now;
            return true;
        }

        public bool RejectApplication(int applicationId, string rejectionReason, string reviewedBy)
        {
            var application = GetApplicationById(applicationId);
            if (application == null)
                return false;

            application.ApplicationStatus = "Rejected";
            application.RejectionReason = rejectionReason;
            application.ReviewedBy = reviewedBy;
            application.ReviewDate = DateTime.Now;
            return true;
        }

        public Dictionary<string, object> GetLoanStatistics()
        {
            return new Dictionary<string, object>
            {
                { "Total Applications", _loanApplications.Count },
                { "Pending Review", GetPendingApplications().Count },
                { "Approved", GetApplicationsByStatus("Approved").Count },
                { "Rejected", GetApplicationsByStatus("Rejected").Count },
                { "Total Approved Amount", _loanApplications.Where(l => l.ApplicationStatus == "Approved").Sum(l => l.ApprovedAmount) },
                { "Average Loan Amount", _loanApplications.Where(l => l.ApplicationStatus == "Approved").Any()
                    ? _loanApplications.Where(l => l.ApplicationStatus == "Approved").Average(l => l.ApprovedAmount)
                    : 0m }
            };
        }
    }
}
