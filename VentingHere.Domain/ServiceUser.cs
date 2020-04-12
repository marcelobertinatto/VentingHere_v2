using System.Threading.Tasks;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Domain
{
    public class ServiceUser : ServiceBase<User>, IServiceUser
    {
        private readonly IRepositoryUser _repositoryUser;
        public ServiceUser(IRepositoryUser repositoryUser) : base(repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public Task<User> UserSummary(int userId)
        {
            return _repositoryUser.UserSummary(userId);
        }
    }
}
