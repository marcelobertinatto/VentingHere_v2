using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Application
{
    public class ServiceAppSubject : ServiceAppBase<Subject>, IServiceAppSubject
    {
        public IServiceSubject _serviceSubject;
        public ServiceAppSubject(IServiceSubject serviceSubject) : base(serviceSubject)
        {
            _serviceSubject = serviceSubject;
        }
    }
}
