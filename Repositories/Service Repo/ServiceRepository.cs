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
