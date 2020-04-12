using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositoryContact : RepositoryBase<Contact>, IRepositoryContact
    {
        private readonly VentingContext _ventingContext;
        public RepositoryContact(VentingContext ventingContext) : base(ventingContext)
        {
            _ventingContext = ventingContext;
        }
    }
}
