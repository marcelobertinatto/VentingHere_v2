using System;
using System.Collections.Generic;
using System.Text;

namespace VentingHere.Domain.Entities
{
    public class Rate
    {
        public int Id { get; set; }
        public int numStar { get; set; }
        public int? UserId { get; set; }
        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
    }
}
