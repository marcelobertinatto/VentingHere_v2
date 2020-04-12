using System.Threading.Tasks;
using VentingHere.Domain.Entities;

namespace VentingHere.Domain.Repository.Services
{
    public interface IServiceUser : IServiceBase<User>
    {
        Task<User> UserSummary(int userId);
    }
}
