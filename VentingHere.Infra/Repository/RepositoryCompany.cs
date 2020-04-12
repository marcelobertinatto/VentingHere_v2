using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositoryCompany : RepositoryBase<Company>, IRepositoryCompany
    {
        public RepositoryCompany(VentingContext ventingContext) : base(ventingContext)
        {
        }
    }
}
