using System;
using System.Collections.Generic;
using System.Text;

namespace VentingHere.Domain.Entities
{
    public class Vent
    {
        public int Id { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public string Title { get; set; }
        public string VentDescription { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public virtual User User { get; set; }
        public virtual Company Company { get; set; }
        public virtual IEnumerable<Answer> ListReplies { get; set; }

    }
}
