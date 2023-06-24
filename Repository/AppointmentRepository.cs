using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Data;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        private VeterinarySystemContext _context;
        public AppointmentRepository(VeterinarySystemContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Appointment> GetAppointmentsWithAllData(int animalId)
        {
            return _context.Appointments
                .Include(e => e.AppointmentMedicines)
                .ThenInclude(e => e.Medicine)
                .Include(e => e.AppointmentSurgeries)
                .ThenInclude(e => e.Surgery)
                .Include(e => e.AppointmentVets)
                .ThenInclude(e => e.Vet)
                .Where(e => e.AnimalId == animalId);
        }
    }
}
