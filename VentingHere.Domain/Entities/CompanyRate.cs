using System;
using System.Collections.Generic;
using System.Text;

namespace VentingHere.Domain.Entities
{
    public class CompanyRate
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int RateId { get; set; }
        public virtual Rate Rate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
