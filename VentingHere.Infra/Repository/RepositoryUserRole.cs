using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositoryUserRole : RepositoryBase<UserRole>, IRepositoryUserRole
    {
        public RepositoryUserRole(VentingContext ventingContext) : base(ventingContext)
        {

        }
    }
}
