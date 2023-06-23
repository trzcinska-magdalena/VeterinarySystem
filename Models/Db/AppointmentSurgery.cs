namespace VeterinarySystem.Models.Db
{
    public class AppointmentSurgery
    {
        public int AppointmentId { get; set; }
        public int SurgeryId { get; set; }
        public virtual Appointment Appointment { get; set; } = null!;
        public virtual Surgery Surgery { get; set; } = null!;
    }
}
