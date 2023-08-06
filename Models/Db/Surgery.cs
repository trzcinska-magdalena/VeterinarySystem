using System.ComponentModel.DataAnnotations;

namespace VeterinarySystem.Models.Db
{
    public class Surgery
    {
        public int Id { get; set; }

        [MinLength(3)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<AppointmentSurgery> AppointmentSurgeries { get; set; } = new List<AppointmentSurgery>();

    }
}