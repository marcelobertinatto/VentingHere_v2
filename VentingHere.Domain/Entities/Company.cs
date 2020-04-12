using System.Collections.Generic;

namespace VentingHere.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string About { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSiteAddress { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public int? RateId { get; set; }
        public virtual IEnumerable<Vent> ListVents { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual Rate Rate { get; set; }

    }
}
