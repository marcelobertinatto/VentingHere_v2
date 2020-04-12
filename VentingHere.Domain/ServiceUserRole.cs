using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Domain
{
    public class ServiceUserRole : ServiceBase<UserRole>, IServiceUserRole
    {
        public ServiceUserRole(IRepositoryUserRole repositoryUserRole) : base(repositoryUserRole)
        {

        }
    }
}
