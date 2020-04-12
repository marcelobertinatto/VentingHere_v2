using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositoryRate : RepositoryBase<Rate>, IRepositoryRate
    {
        public RepositoryRate(VentingContext ventingContext) : base(ventingContext)
        {
        }
    }
}
