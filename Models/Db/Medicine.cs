using System.ComponentModel.DataAnnotations;

namespace VeterinarySystem.Models.Db
{
    public class Medicine
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public virtual ICollection<AppointmentMedicine> AppointmentMedicines { get; set; } = new List<AppointmentMedicine>();
    }
}
