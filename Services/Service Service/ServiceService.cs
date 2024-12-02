using Hostel_Management.Models;
using Hostel_Management.Repositories.Service_Repo;

namespace Hostel_Management.Services.Service_Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public IEnumerable<Service> GetAllServices()
        {
            return _serviceRepository.GetAllServices();
        }

        public IEnumerable<Service> GetServicesByGuestId(int guestId)
        {
            return _serviceRepository.GetServicesByGuestId(guestId);
        }

        public Service GetServiceById(int serviceId)
        {
            return _serviceRepository.GetServiceById(serviceId);
        }

        public void AddService(Service service)
        {
            _serviceRepository.AddService(service);
            _serviceRepository.SaveChanges();
        }

        public void UpdateService(Service service)
        {
            _serviceRepository.UpdateService(service);
            _serviceRepository.SaveChanges();
        }
    }

}
