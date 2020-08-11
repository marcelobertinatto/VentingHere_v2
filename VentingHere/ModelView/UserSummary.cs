using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VentingHere.Domain.Entities;

namespace VentingHere.ModelView
{
    public class UserSummary
    {
        public int TotalOfComplaints { get; set; }
        public List<CompanySubjectIssue> ListCompanies { get; set; }

    }
}
