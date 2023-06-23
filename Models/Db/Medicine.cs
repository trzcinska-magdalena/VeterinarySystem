namespace VeterinarySystem.Models.Db
{
    public class Medicine
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public virtual ICollection<AppointmentMedicine> AppointmentMedicines { get; set; } = new List<AppointmentMedicine>();
    }
}
