using Hostel_Management.Models;

namespace Hostel_Management.Services.Service_Service
{
    public interface IServiceService
    {
        IEnumerable<Service> GetAllServices();
        IEnumerable<Service> GetServicesByGuestId(int guestId);
        Service GetServiceById(int serviceId);
        void AddService(Service service);
        void UpdateService(Service service);
    }

}
