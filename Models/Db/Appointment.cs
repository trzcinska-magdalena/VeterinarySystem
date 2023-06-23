namespace VeterinarySystem.Models.Db
{
    public class Appointment
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; } = null!;

        public virtual Animal Animal { get; set; } = null!;

        public virtual ICollection<AppointmentMedicine> AppointmentMedicines { get; set; } = new List<AppointmentMedicine>();

        public virtual ICollection<AppointmentSurgery> AppointmentSurgeries { get; set; } = new List<AppointmentSurgery>();
        public virtual ICollection<AppointmentVet> AppointmentVets { get; set; } = new List<AppointmentVet>();

    }
}