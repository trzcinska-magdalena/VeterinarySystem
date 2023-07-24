using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Repository.IRepository
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsWithAllData(int animalId);
    }
}
