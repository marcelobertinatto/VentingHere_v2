using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VentingHere.ModelView
{
    public class CompanySubjectTellUsDTO
    {
        public string CompanyName { get; set; }
        public string WebSiteAddress { get; set; }
        public string Address { get; set; }
        public string TellUs { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectDescribed { get; set; }
        public int SubjectIssueId { get; set; }
        public string SubjectIssueDescribed { get; set; }


    }
}
