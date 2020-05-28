using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Application
{
    public class ServiceAppVent : ServiceAppBase<Vent>, IServiceAppVent
    {
        public ServiceAppVent(IServiceVent serviceVent) : base(serviceVent)
        {
        }
    }
}
