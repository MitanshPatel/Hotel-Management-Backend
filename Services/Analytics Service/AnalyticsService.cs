using Hostel_Management.Repositories.Analytics_Repo;

namespace Hostel_Management.Services.Analytics_Service
{
    public class AnalyticsService
    {
        private readonly AnalyticsRepository _analyticsRepository;

        public AnalyticsService(AnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }

        public IEnumerable<MonthlyRevenue> GetMonthlyRevenueForPastYear()
        {
            return _analyticsRepository.GetMonthlyRevenueForPastYear();
        }
    }
}
