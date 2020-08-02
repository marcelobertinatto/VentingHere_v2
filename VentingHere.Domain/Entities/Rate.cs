using System;
using System.Collections.Generic;
using System.Text;

namespace VentingHere.Domain.Entities
{
    public class Rate
    {
        public int Id { get; set; }
        public int numStar { get; set; }
        public virtual IEnumerable<CompanyRate> ListCompanyRates { get; set; }
    }
}
