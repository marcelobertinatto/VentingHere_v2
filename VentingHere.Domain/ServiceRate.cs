using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Domain
{
    public class ServiceRate : ServiceBase<Rate>, IServiceRate
    {
        public ServiceRate(IRepositoryRate repositoryRate) : base(repositoryRate)
        {

        }
    }
}
