using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hostel_Management.Services;
using Hostel_Management.Services.Analytics_Service;

namespace Hostel_Management.Controllers
{
    [ApiController]
    [Route("api/analytics")]
    public class AnalyticsController : ControllerBase
    {
        private readonly AnalyticsService _analyticsService;

        public AnalyticsController(AnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("monthly-revenue")]
        public IActionResult GetMonthlyRevenue()
        {
            var revenueData = _analyticsService.GetMonthlyRevenueForPastYear();
            return Ok(revenueData);
        }
    }
}
