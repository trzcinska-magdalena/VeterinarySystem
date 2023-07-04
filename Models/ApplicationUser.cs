using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int Name { get; set; }

        public virtual Vet Vet { get; set; }
    }
}
