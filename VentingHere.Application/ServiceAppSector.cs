using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Application
{
    public class ServiceAppSector : ServiceAppBase<Sector>, IServiceAppSector
    {
        public ServiceAppSector(IServiceSector serviceSector) : base(serviceSector)
        {
        }
    }
}
