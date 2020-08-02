using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositorySubjectIssue : RepositoryBase<SubjectIssue>, IRepositorySubjectIssue
    {
        public RepositorySubjectIssue(VentingContext ventingContext): base(ventingContext)
        {

        }
    }
}
