using Microsoft.AspNetCore.Identity;

namespace MedicalAppointmentSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
