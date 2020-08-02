using System;
using System.Collections.Generic;
using System.Text;

namespace VentingHere.Domain.Entities
{
    public class SubjectIssue
    {
        public int Id { get; set; }
        public string SubjectIssueText { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual IEnumerable<Company> ListCompanies { get; set; }
        public virtual IEnumerable<CompanySubjectIssue> ListCompanySubjectIssues { get; set; }

    }
}
