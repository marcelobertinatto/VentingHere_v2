using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Application
{
    public class ServiceAppUser : ServiceAppBase<User>, IServiceAppUser
    {
        private readonly IServiceUser _serviceUser;
        public ServiceAppUser(IServiceUser serviceUser) : base(serviceUser)
        {
            _serviceUser = serviceUser;
        }

        public Task<User> UserSummary(int userId)
        {
            return _serviceUser.UserSummary(userId);
        }
    }
}
