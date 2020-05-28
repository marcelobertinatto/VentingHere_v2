using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Application
{
    public class ServiceAppCompany : ServiceAppBase<Company>, IServiceAppCompany
    {
        public ServiceAppCompany(IServiceCompany serviceCompany) : base(serviceCompany)
        {
        }
    }
}
