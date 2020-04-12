using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VentingHere.Domain.Entities;

namespace VentingHere.Application.Interface
{
    public interface IServiceAppUser : IServiceAppBase<User>
    {
        Task<User> UserSummary(int userId);
    }
}
