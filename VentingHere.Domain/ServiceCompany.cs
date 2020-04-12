using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Domain
{
    public class ServiceCompany : ServiceBase<Company>, IServiceCompany
    {
        public ServiceCompany(IRepositoryCompany repositoryCompany) : base(repositoryCompany)
        {

        }
    }
}
