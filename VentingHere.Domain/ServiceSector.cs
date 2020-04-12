using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Domain
{
    public class ServiceSector : ServiceBase<Sector>
    {
        public ServiceSector(IRepositorySector repositorySector) : base(repositorySector)
        {

        }
    }
}
