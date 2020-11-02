using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Domain
{
    public class ServiceCompanyRate : ServiceBase<CompanyRate>, IServiceCompanyRate
    {
        public ServiceCompanyRate(IRepositoryCompanyRate repositoryCompanyRate) : base(repositoryCompanyRate) 
        {

        }
    }
}
