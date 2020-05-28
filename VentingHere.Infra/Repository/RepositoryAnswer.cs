using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositoryAnswer : RepositoryBase<Answer>, IRepositoryAnswer
    {
        public RepositoryAnswer(VentingContext ventingContext) : base(ventingContext)
        {
        }
    }
}
