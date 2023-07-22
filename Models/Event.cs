namespace VeterinarySystem.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; } = null!;
        public string Start { get; set; } = null!;
        public string? End { get; set; }
        public bool? AllDay { get; set; }
    }
}
