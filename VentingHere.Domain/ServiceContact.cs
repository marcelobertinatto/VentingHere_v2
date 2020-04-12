using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Domain
{
    public class ServiceContact : ServiceBase<Contact>, IServiceContact
    {
        private readonly IRepositoryContact _repositoryContact;
        public ServiceContact(IRepositoryContact repositoryContact) : base(repositoryContact)
        {
            _repositoryContact = repositoryContact;
        }    
    }
}
