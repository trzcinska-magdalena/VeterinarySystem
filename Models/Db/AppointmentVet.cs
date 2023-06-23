namespace VeterinarySystem.Models.Db
{
    public class AppointmentVet
    {
        public int AppointmentId { get; set; }
        public int VetId { get; set; }
        public virtual Appointment Appointment { get; set; } = null!;
        public virtual Vet Vet { get; set; } = null!;
    }
}