using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Domain
{
    public class ServiceSubject : ServiceBase<Subject>, IServiceSubject
    {
        public ServiceSubject(IRepositorySubject repositorySubject) : base(repositorySubject)
        {

        }
    }
}
