using System;
using System.Collections.Generic;
using System.Text;

namespace VentingHere.Domain.Entities
{
    public class CompanySubjectIssue
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public string TellUs { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public int SubjectIssueId { get; set; }
        public virtual SubjectIssue SubjectIssue { get; set; }
        public int UserId { get; set; }
    }
}
