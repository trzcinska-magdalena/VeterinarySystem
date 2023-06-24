using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Repository.IRepository
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        IEnumerable<Animal> GetAllWithData(string? whereString);
        Animal? GetWithAllData(int id);
    }
}
