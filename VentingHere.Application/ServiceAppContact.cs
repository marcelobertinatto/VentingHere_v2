using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Application
{
    public class ServiceAppContact : ServiceAppBase<Contact>, IServiceAppContact
    {
        private readonly IServiceContact _serviceUser;
        public ServiceAppContact(IServiceContact serviceUser) : base(serviceUser)
        {
            _serviceUser = serviceUser;
        }
    }
}
