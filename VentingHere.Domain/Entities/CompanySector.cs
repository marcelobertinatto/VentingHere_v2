using System;
using System.Collections.Generic;
using System.Text;

namespace VentingHere.Domain.Entities
{
    public class CompanySector
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int SectorId { get; set; }
        public virtual Sector Sector { get; set; }
    }
}
