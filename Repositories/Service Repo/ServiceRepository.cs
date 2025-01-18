using Hostel_Management.Data;
using Hostel_Management.Models;

namespace Hostel_Management.Repositories.Service_Repo
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Service> GetAllServices()
        {
            return _context.Services.ToList();
        }

        public (IEnumerable<Service> items, int totalCount) GetAllFoodServices(int guestId, int pageNumber, int pageSize)
        {
            var query = _context.Services
                                .Where(s => s.GuestId == guestId && s.ServiceType.StartsWith("Food"))
                                .OrderByDescending(s => s.RequestTime);
            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return (items, totalCount);
        }

        public (IEnumerable<Service> items, int totalCount) GetAllServiceServices(int guestId, int pageNumber, int pageSize)
        {
            var query = _context.Services
                                .Where(s => s.GuestId == guestId && s.ServiceType.StartsWith("Service"))
                                .OrderByDescending(s => s.RequestTime);
            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return (items, totalCount);
        }



        public IEnumerable<Service> GetServicesByGuestId(int guestId)
        {
            return _context.Services.Where(s => s.GuestId == guestId).ToList();
        }

        public Service GetServiceById(int serviceId)
        {
            return _context.Services.Find(serviceId);
        }

        public void AddService(Service service)
        {
            _context.Services.Add(service);
        }

        public void UpdateService(Service service)
        {
            _context.Services.Update(service);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

}
