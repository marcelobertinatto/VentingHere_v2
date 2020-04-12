using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositorySector : RepositoryBase<Sector>, IRepositorySector
    {
        public RepositorySector(VentingContext ventingContext) : base(ventingContext)
        {
        }
    }
}
