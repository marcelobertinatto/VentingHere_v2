using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositoryVent : RepositoryBase<Vent>, IRepositoryVent
    {
        public RepositoryVent(VentingContext ventingContext) : base(ventingContext)
        {
        }
    }
}
