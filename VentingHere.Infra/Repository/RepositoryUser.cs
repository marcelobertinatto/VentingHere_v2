using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        private readonly VentingContext _ventingContext;
        public RepositoryUser(VentingContext ventingContext) : base(ventingContext)
        {
            _ventingContext = ventingContext;
        }

        public Task<User> UserSummary(int userId)
        {
            return _ventingContext.Set<User>()
                .Include(x => x.ListRates)
                .Include(x => x.ListVents)      
                .AsNoTracking().SingleAsync(x => x.Id == userId);
        }
    }
}
