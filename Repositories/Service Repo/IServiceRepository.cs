using Hostel_Management.Models;

namespace Hostel_Management.Repositories.Service_Repo
{
    public interface IServiceRepository
    {
        IEnumerable<Service> GetAllServices();
        (IEnumerable<Service> items, int totalCount) GetAllFoodServices(int guestId, int pageNumber, int pageSize);
        (IEnumerable<Service> items, int totalCount) GetAllServiceServices(int guestId, int pageNumber, int pageSize);
        IEnumerable<Service> GetServicesByGuestId(int guestId);
        Service GetServiceById(int serviceId);
        void AddService(Service service);
        void UpdateService(Service service);
        void SaveChanges();
    }

}
