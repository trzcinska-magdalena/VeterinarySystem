using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Configuration;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Data
{
    public class VeterinarySystemContext : IdentityDbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentMedicine> AppointmentMedicines { get; set; }
        public DbSet<AppointmentSurgery> AppointmentSurgeries { get; set; }
        public DbSet<AppointmentVet> AppointmentVets { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Specialisation> Specialisations { get; set; }
        public DbSet<Surgery> Surgeries { get; set; }
        public DbSet<TypeOfVaccine> TypeOfVaccines { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<VetSpecialisation> VetSpecialisations { get; set; }
        public DbSet<Weight> Weights { get; set; }
        
        public VeterinarySystemContext()
        {
        }

        public VeterinarySystemContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new AnimalConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentMedicineConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentSurgeryConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentVetConfiguration());
            modelBuilder.ApplyConfiguration(new BreedConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new MedicineConfiguration());
            modelBuilder.ApplyConfiguration(new SpecialisationConfiguration());
            modelBuilder.ApplyConfiguration(new SurgeryConfiguration());
            modelBuilder.ApplyConfiguration(new TypeOfVaccineConfiguration());
            modelBuilder.ApplyConfiguration(new VaccinationConfiguration());
            modelBuilder.ApplyConfiguration(new VetConfiguration());
            modelBuilder.ApplyConfiguration(new VetSpecialisationConfiguration());
            modelBuilder.ApplyConfiguration(new WeightConfiguration());
        }
    }
}