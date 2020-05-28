using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Domain
{
    public class ServiceRole : ServiceBase<Role>, IServiceRole
    {
        public ServiceRole(IRepositoryRole repositoryRole) : base(repositoryRole)
        {
        }
    }
}
