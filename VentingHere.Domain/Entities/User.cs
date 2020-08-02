using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace VentingHere.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Phone { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime UserFirstRegister { get; set; }
        public virtual IEnumerable<Vent> ListVents { get; set; }
        public virtual IEnumerable<Answer> ListReplies { get; set; }
        public virtual List<UserRole> ListUserRoles { get; set; }
        public virtual IEnumerable<CompanyRate> ListCompanyRates { get; set; }
    }
}
