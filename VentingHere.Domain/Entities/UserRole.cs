using Microsoft.AspNetCore.Identity;

namespace VentingHere.Domain.Entities
{
    public partial class UserRole : IdentityUserRole<int>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
