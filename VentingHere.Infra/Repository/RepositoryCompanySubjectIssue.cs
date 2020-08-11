using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositoryCompanySubjectIssue : RepositoryBase<CompanySubjectIssue>, IRepositoryCompanySubjectIssue
    {
        VentingContext _ventingContext;
        public RepositoryCompanySubjectIssue(VentingContext ventingContext) : base(ventingContext)
        {
            _ventingContext = ventingContext;
        }

        public override List<CompanySubjectIssue> Find(Expression<Func<CompanySubjectIssue, bool>> expression)
        {
            var returnedValue = (from csi in _ventingContext.CompanySubjectIssue.Where(expression)                                 
                                 select new CompanySubjectIssue
                                 {
                                     Id = csi.Id,
                                     Company = csi.Company,
                                     Subject = csi.Subject,
                                     SubjectIssue = csi.SubjectIssue,
                                     TellUs = csi.TellUs,
                                     DateAndTime = csi.DateAndTime
                                 }).ToList();

            return returnedValue;
        }
    }
}
