namespace VeterinarySystem.Models.Db
{
    public class Surgery
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public virtual ICollection<AppointmentSurgery> AppointmentSurgeries { get; set; } = new List<AppointmentSurgery>();

    }
}