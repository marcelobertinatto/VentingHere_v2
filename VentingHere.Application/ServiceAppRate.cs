using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Application
{
    public class ServiceAppRate : ServiceAppBase<Rate>, IServiceAppRate
    {
        public ServiceAppRate(IServiceRate serviceRate) : base(serviceRate)
        {
        }
    }
}
