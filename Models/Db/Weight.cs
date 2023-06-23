using System.ComponentModel.DataAnnotations;

namespace VeterinarySystem.Models.Db
{
    public class Weight
    {
        public int AnimalId { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public virtual Animal Animal { get; set; } = null!;
    }
}