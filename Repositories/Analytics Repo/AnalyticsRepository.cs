using Hostel_Management.Data;

namespace Hostel_Management.Repositories.Analytics_Repo
{
    public class AnalyticsRepository
    {
        private readonly ApplicationDbContext _context;

        public AnalyticsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<MonthlyRevenue> GetMonthlyRevenueForPastYear()
        {
            var pastYear = DateTime.Now.AddMonths(-12);
            var revenueData = _context.Payments
                .Where(p => p.PaymentDate >= pastYear)
                .GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(p => p.Amount)
                })
                .OrderBy(mr => mr.Year)
                .ThenBy(mr => mr.Month)
                .ToList();

            return revenueData.Select(rd => new MonthlyRevenue
            {
                Month = $"{rd.Year}-{rd.Month:00}",
                Revenue = rd.Revenue
            });
        }
    }

    public class MonthlyRevenue
    {
        public string Month { get; set; }
        public decimal Revenue { get; set; }
    }
}
