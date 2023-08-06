using System.ComponentModel.DataAnnotations;

namespace VeterinarySystem.Models.Db
{
    public class AppointmentMedicine
    {
        public int AppointmentId { get; set; }
        public int MedicineId { get; set; }
        [Range(1, double.MaxValue)]
        public int Amount { get; set; }
        public virtual Appointment Appointment { get; set; } = null!;
        public virtual Medicine Medicine { get; set; } = null!;
    }
}