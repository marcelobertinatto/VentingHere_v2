using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Application
{
    public class ServiceAppCompanyRate : ServiceAppBase<CompanyRate>, IServiceAppCompanyRate
    {
        public ServiceAppCompanyRate(IServiceCompanyRate serviceCompanyRate) : base(serviceCompanyRate)
        {

        }
    }
}
