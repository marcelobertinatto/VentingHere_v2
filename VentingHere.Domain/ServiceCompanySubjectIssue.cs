using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Domain
{
    public class ServiceCompanySubjectIssue : ServiceBase<CompanySubjectIssue>, IServiceCompanySubjectIssue
    {
        IRepositoryCompanySubjectIssue _repositoryCompanySubjectIssue;
        public ServiceCompanySubjectIssue(IRepositoryCompanySubjectIssue repositoryCompanySubjectIssue) : base(repositoryCompanySubjectIssue)
        {
            _repositoryCompanySubjectIssue = repositoryCompanySubjectIssue;
        }
    }
}
