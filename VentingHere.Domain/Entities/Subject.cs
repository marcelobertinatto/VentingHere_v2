using System;
using System.Collections.Generic;
using System.Text;

namespace VentingHere.Domain.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectText { get; set; }
        public virtual IEnumerable<SubjectIssue> SubjectIssue { get; set; }
        public virtual IEnumerable<Company> ListCompanies { get; set; }
        public virtual IEnumerable<CompanySubjectIssue> ListCompanySubjectIssues { get; set; }
    }
}
