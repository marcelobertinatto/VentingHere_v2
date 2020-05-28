using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Application
{
    public class ServiceAppRole : ServiceAppBase<Role>,IServiceAppRole  
    {
        public ServiceAppRole(IServiceRole serviceRole) : base(serviceRole)
        {

        }
    }
}
