using Hostel_Management.Models;

namespace Hostel_Management.Repositories.Service_Repo
{
    public interface IServiceRepository
    {
        IEnumerable<Service> GetAllServices();
        IEnumerable<Service> GetServicesByGuestId(int guestId);
        Service GetServiceById(int serviceId);
        void AddService(Service service);
        void UpdateService(Service service);
        void SaveChanges();
    }

}
