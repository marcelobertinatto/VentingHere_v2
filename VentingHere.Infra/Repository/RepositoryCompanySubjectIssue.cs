using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositoryCompanySubjectIssue : RepositoryBase<CompanySubjectIssue>, IRepositoryCompanySubjectIssue
    {
        VentingContext _ventingContext;
        public RepositoryCompanySubjectIssue(VentingContext ventingContext) : base(ventingContext)
        {
            _ventingContext = ventingContext;
        }
    }
}
