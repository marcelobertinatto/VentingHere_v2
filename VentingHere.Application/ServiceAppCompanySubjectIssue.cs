using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Application
{
    public class ServiceAppCompanySubjectIssue : ServiceAppBase<CompanySubjectIssue>, IServiceAppCompanySubjectIssue
    {
        IServiceCompanySubjectIssue _serviceCompanySubjectIssue;
        public ServiceAppCompanySubjectIssue(IServiceCompanySubjectIssue serviceCompanySubjectIssue) : base(serviceCompanySubjectIssue)
        {
            _serviceCompanySubjectIssue = serviceCompanySubjectIssue;
        }
    }
}
