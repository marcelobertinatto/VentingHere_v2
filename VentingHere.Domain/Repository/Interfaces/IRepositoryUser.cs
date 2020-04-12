using System.Threading.Tasks;
using VentingHere.Domain.Entities;

namespace VentingHere.Domain.Repository.Interfaces
{
    public interface IRepositoryUser : IRepositoryBase<User>
    {
        Task<User> UserSummary(int userId);
    }
}
