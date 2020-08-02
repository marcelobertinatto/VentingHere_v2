using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Application
{
    public class ServiceAppSubjectIssue : ServiceAppBase<SubjectIssue>, IServiceAppSubjectIssue
    {
        public IServiceSubjectIssue _serviceSubjectIssue;
        public ServiceAppSubjectIssue(IServiceSubjectIssue serviceSubjectIssue): base(serviceSubjectIssue)
        {
            _serviceSubjectIssue = serviceSubjectIssue;
        }
    }
}
