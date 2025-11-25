using Bank_HeadQuarter.Models;

namespace Bank_HeadQuarter.Services
{
    public class BranchManagementService
    {
        private readonly List<Branch> _branches = new();

        public BranchManagementService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _branches.Add(new Branch
            {
                BranchId = 1,
                BranchCode = "BR001",
                BranchName = "Downtown Main Branch",
                Region = "Central",
                Address = "123 Main Street",
                City = "New York",
                State = "NY",
                ZipCode = "10001",
                ManagerName = "John Smith",
                PhoneNumber = "212-555-0100",
                IsActive = true,
                OpeningDate = new DateTime(2015, 3, 15),
                MonthlyRevenue = 2500000m,
                TotalEmployees = 45,
                TotalCustomers = 12500
            });

            _branches.Add(new Branch
            {
                BranchId = 2,
                BranchCode = "BR002",
                BranchName = "Westside Branch",
                Region = "West",
                Address = "456 West Ave",
                City = "Los Angeles",
                State = "CA",
                ZipCode = "90001",
                ManagerName = "Sarah Johnson",
                PhoneNumber = "310-555-0200",
                IsActive = true,
                OpeningDate = new DateTime(2017, 7, 20),
                MonthlyRevenue = 1800000m,
                TotalEmployees = 32,
                TotalCustomers = 8900
            });
        }

        public List<Branch> GetAllBranches() => _branches;

        public Branch? GetBranchById(int branchId) => _branches.FirstOrDefault(b => b.BranchId == branchId);

        public List<Branch> GetBranchesByRegion(string region) =>
            _branches.Where(b => b.Region.Equals(region, StringComparison.OrdinalIgnoreCase)).ToList();

        public bool AddBranch(Branch branch)
        {
            if (_branches.Any(b => b.BranchCode == branch.BranchCode))
                return false;

            branch.BranchId = _branches.Max(b => b.BranchId) + 1;
            _branches.Add(branch);
            return true;
        }

        public bool UpdateBranch(Branch branch)
        {
            var existingBranch = GetBranchById(branch.BranchId);
            if (existingBranch == null)
                return false;

            _branches.Remove(existingBranch);
            _branches.Add(branch);
            return true;
        }

        public bool DeactivateBranch(int branchId)
        {
            var branch = GetBranchById(branchId);
            if (branch == null)
                return false;

            branch.IsActive = false;
            return true;
        }

        public Dictionary<string, int> GetBranchStatisticsByRegion()
        {
            return _branches
                .GroupBy(b => b.Region)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
