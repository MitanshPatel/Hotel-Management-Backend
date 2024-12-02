using Hostel_Management.Services.Service_Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hostel_Management.Controllers
{
    [ApiController]
    [Route("api/service")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("food")]
        public IActionResult GetFoodServices()
        {
            var services = _serviceService.GetAllServices().Where(s => s.ServiceType.StartsWith("Food"));
            return Ok(services);
        }

        [HttpGet("services")]
        public IActionResult GetOtherServices()
        {
            var services = _serviceService.GetAllServices().Where(s => s.ServiceType.StartsWith("Service"));
            return Ok(services);
        }

        [HttpGet("pending")]
        public IActionResult GetPendingServices()
        {
            var services = _serviceService.GetAllServices().Where(s => s.Status == "Pending");
            return Ok(services);
        }

        [HttpPut("{id}/under-process")]
        public IActionResult UnderProcessService(int id)
        {
            var service = _serviceService.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }

            // Get the HousekeepingId from the JWT token
            var housekeepingId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            service.Status = "Under Process";
            service.HousekeepingId = housekeepingId;
            _serviceService.UpdateService(service);

            return Ok("Service under process");
        }

        [HttpPut("{id}/complete")]
        public IActionResult CompleteService(int id)
        {
            var service = _serviceService.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }

            // Get the HousekeepingId from the JWT token
            var housekeepingId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Ensure that only the assigned housekeeping staff can complete the service
            if (service.HousekeepingId != housekeepingId)
            {
                return Forbid("You are not authorized to complete this service.");
            }

            service.DeliveryTime = DateTime.Now;
            service.Status = "Completed";
            _serviceService.UpdateService(service);

            return Ok("Service completed");
        }
    }
}

