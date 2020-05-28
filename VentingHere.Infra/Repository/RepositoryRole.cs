using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositoryRole : RepositoryBase<Role>, IRepositoryRole
    {
        public RepositoryRole(VentingContext ventingContext) : base(ventingContext)
        {

        }
    }
}
