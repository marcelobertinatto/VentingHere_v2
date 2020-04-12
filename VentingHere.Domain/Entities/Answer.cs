using System;
using System.Collections.Generic;
using System.Text;

namespace VentingHere.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public DateTime ReplyDateTime { get; set; }
        public string CompanyReply { get; set; }
        public int? UserId { get; set; }
        public int? VentId { get; set; }
        public virtual Vent Vent { get; set; }
        public virtual User User { get; set; }
    }
}
