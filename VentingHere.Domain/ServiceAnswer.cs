using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Domain
{
    public class ServiceAnswer : ServiceBase<Answer>, IServiceAnswer
    {
        public ServiceAnswer(IRepositoryBase<Answer> repositoryBase) : base(repositoryBase)
        {
        }
    }
}
