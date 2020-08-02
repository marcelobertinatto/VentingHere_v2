using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositorySubject : RepositoryBase<Subject>, IRepositorySubject
    {
        private readonly VentingContext _ventingContext;
        public RepositorySubject(VentingContext ventingContext): base(ventingContext)
        {
            _ventingContext = ventingContext;
        }
    }
}
