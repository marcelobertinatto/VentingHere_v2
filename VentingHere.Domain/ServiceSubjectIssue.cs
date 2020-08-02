using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Domain
{
    public class ServiceSubjectIssue : ServiceBase<SubjectIssue>, IServiceSubjectIssue
    {
        public ServiceSubjectIssue(IRepositorySubjectIssue repositorySubjectIssue): base(repositorySubjectIssue)
        {

        }
    }
}
