using Hostel_Management.Services.Service_Service;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Housekeeping")]
        [HttpGet("food")]
        public IActionResult GetFoodServices()
        {
            var services = _serviceService.GetAllServices().Where(s => s.ServiceType.StartsWith("Food"));
            return Ok(services);
        }

        [Authorize(Roles = "Housekeeping")]
        [HttpGet("services")]
        public IActionResult GetOtherServices()
        {
            var services = _serviceService.GetAllServices().Where(s => s.ServiceType.StartsWith("Service"));
            return Ok(services);
        }

        [Authorize(Roles = "Housekeeping")]
        [HttpGet("pending")]
        public IActionResult GetPendingServices()
        {
            var services = _serviceService.GetAllServices().Where(s => s.Status == "Pending");
            return Ok(services);
        }

        [Authorize(Roles = "Housekeeping")]
        [HttpGet("pending-food")]
        public IActionResult GetPendingFoodServices()
        {
            var services = _serviceService.GetAllServices().Where(s => s.ServiceType.StartsWith("Food") && s.Status == "Pending");
            return Ok(services);
        }

        [Authorize(Roles = "Housekeeping")]
        [HttpGet("pending-services")]
        public IActionResult GetPendingOtherServices()
        {
            var services = _serviceService.GetAllServices().Where(s => s.ServiceType.StartsWith("Service") && s.Status == "Pending");
            return Ok(services);
        }

        [Authorize(Roles = "Housekeeping")]
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

            return Ok(new { msg = "Service under process" });
        }

        [Authorize(Roles = "Housekeeping")]
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

            return Ok(new { msg = "Service completed" });
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("my-food")]
        public IActionResult GetMyFoodServices([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var guestIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (guestIdClaim == null)
            {
                return Unauthorized();
            }
            var guestId = int.Parse(guestIdClaim);
            var (items, totalCount) = _serviceService.GetAllFoodServices(guestId, pageNumber, pageSize);
            return Ok(new { items, totalCount });
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("my-services")]
        public IActionResult GetMyOtherServices([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var guestIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (guestIdClaim == null)
            {
                return Unauthorized();
            }
            var guestId = int.Parse(guestIdClaim);
            var (items, totalCount) = _serviceService.GetAllServiceServices(guestId, pageNumber, pageSize);
            return Ok(new { items, totalCount });
        }
    }
}

