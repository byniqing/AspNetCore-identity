using Microsoft.AspNetCore.Identity;

namespace IdentityDemo.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string TrueName { get; set; }
        
        public string Address { get; set; }

    }
}
