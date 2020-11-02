using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositoryCompanyRate : RepositoryBase<CompanyRate>, IRepositoryCompanyRate
    {
        public RepositoryCompanyRate(VentingContext ventingContext) : base(ventingContext)
        {

        }
    }
}
