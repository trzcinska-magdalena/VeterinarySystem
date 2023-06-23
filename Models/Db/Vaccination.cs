namespace VeterinarySystem.Models.Db
{
    public class Vaccination
    {
        public int Id { get; set; }

        public int AnimalId { get; set; }
        public int TypeOfVaccineId { get; set; }

        public DateTime Date { get; set; }

        public DateTime ExpiryDate { get; set; }

        public virtual Animal Animal { get; set; } = null!;

        public virtual TypeOfVaccine TypeOfVaccine { get; set; } = null!;
    }
}