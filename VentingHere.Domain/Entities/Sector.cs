namespace VentingHere.Domain.Entities
{
    public class Sector
    {
        public int Id { get; set; }
        public string SectorName { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
