using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Application
{
    public class ServiceAppAnswer : ServiceAppBase<Answer>, IServiceAppAnswer
    {
        public ServiceAppAnswer(IServiceAnswer serviceAnswer) : base(serviceAnswer)
        {
        }
    }
}
