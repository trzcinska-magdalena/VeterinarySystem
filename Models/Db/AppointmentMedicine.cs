namespace VeterinarySystem.Models.Db
{
    public class AppointmentMedicine
    {
        public int AppointmentId { get; set; }
        public int MedicineId { get; set; }
        public int Amount { get; set; }
        public virtual Appointment Appointment { get; set; } = null!;
        public virtual Medicine Medicine { get; set; } = null!;
    }
}