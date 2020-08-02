using System.Collections.Generic;

namespace VentingHere.Domain.Entities
{
    public class Sector
    {
        public int Id { get; set; }
        public string SectorName { get; set; }        
        public virtual IEnumerable<CompanySector> ListCompanySector { get; set; }
    }
}
