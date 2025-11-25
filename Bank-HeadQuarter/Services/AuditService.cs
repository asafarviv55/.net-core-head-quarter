using Bank_HeadQuarter.Models;

namespace Bank_HeadQuarter.Services
{
    public class AuditService
    {
        private readonly List<AuditLog> _auditLogs = new();

        public void LogAction(string userId, string userName, string action, string module,
            string details, bool isSuccessful, string errorMessage = "", int? branchId = null)
        {
            var log = new AuditLog
            {
                AuditId = _auditLogs.Any() ? _auditLogs.Max(a => a.AuditId) + 1 : 1,
                Timestamp = DateTime.Now,
                UserId = userId,
                UserName = userName,
                Action = action,
                Module = module,
                Details = details,
                IpAddress = "127.0.0.1", // In real app, get actual IP
                IsSuccessful = isSuccessful,
                ErrorMessage = errorMessage,
                BranchId = branchId
            };

            _auditLogs.Add(log);
        }

        public List<AuditLog> GetAllLogs() => _auditLogs.OrderByDescending(l => l.Timestamp).ToList();

        public List<AuditLog> GetLogsByUser(string userId) =>
            _auditLogs.Where(l => l.UserId == userId).OrderByDescending(l => l.Timestamp).ToList();

        public List<AuditLog> GetLogsByModule(string module) =>
            _auditLogs.Where(l => l.Module.Equals(module, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(l => l.Timestamp).ToList();

        public List<AuditLog> GetLogsByDateRange(DateTime startDate, DateTime endDate) =>
            _auditLogs.Where(l => l.Timestamp >= startDate && l.Timestamp <= endDate)
                .OrderByDescending(l => l.Timestamp).ToList();

        public List<AuditLog> GetFailedActions() =>
            _auditLogs.Where(l => !l.IsSuccessful).OrderByDescending(l => l.Timestamp).ToList();

        public Dictionary<string, int> GetAuditStatistics()
        {
            return new Dictionary<string, int>
            {
                { "Total Actions", _auditLogs.Count },
                { "Successful", _auditLogs.Count(l => l.IsSuccessful) },
                { "Failed", _auditLogs.Count(l => !l.IsSuccessful) },
                { "Today", _auditLogs.Count(l => l.Timestamp.Date == DateTime.Today) }
            };
        }

        public Dictionary<string, int> GetActionsByModule()
        {
            return _auditLogs
                .GroupBy(l => l.Module)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
