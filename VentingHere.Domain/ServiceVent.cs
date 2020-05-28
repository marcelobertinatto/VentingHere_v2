using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Domain
{
    public class ServiceVent : ServiceBase<Vent>, IServiceVent
    {

        public ServiceVent(IRepositoryVent repositoryVent) : base(repositoryVent)
        {

        }
    }
}
