using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.ModelView;

namespace VentingHere.Helper
{
    public class BogusFakeData
    {
        private readonly UserManager<User> _userManager;
        private readonly IServiceAppCompany _serviceAppCompany;
        private readonly IServiceAppCompanyRate _serviceAppCompanyRate;
        private readonly IServiceAppCompanySubjectIssue _serviceAppCompanySubjectIssue;
        private readonly IMapper _mapper;
        public BogusFakeData(UserManager<User> userManager, IServiceAppCompany serviceAppCompany
            , IServiceAppCompanyRate serviceAppCompanyRate
            , IServiceAppCompanySubjectIssue serviceAppCompanySubjectIssue
            , IMapper mapper)
        {
            _userManager = userManager;
            _serviceAppCompany = serviceAppCompany;
            _serviceAppCompanyRate = serviceAppCompanyRate;
            _serviceAppCompanySubjectIssue = serviceAppCompanySubjectIssue;
            _mapper = mapper;
        }

        public bool CreateFakeUser(int numberOfRows)
        {
            bool resultValue = true;

            var users = new Faker<UserDTO>()
                .RuleFor(x => x.Name, (f,u) => f.Name.FullName())
                .RuleFor(x => x.Surname, (f, u) => f.Name.LastName())
                .RuleFor(x => x.UserName, (f, u) => f.Internet.UserName(u.Name, u.Surname))
                .RuleFor(x => x.Password, (f, u) => f.Internet.Password())
                .RuleFor(x => x.Email, (f, u) => f.Internet.Email(u.Name, u.Surname))
                .RuleFor(x => x.Image, (f, u) => f.Internet.Avatar())
                .RuleFor(x => x.Password, (f, u) => f.Internet.Password())
                .RuleFor(x => x.City, (f, u) => f.Address.City())
                .RuleFor(x => x.County, (f, u) => f.Address.County())
                .RuleFor(x => x.Phone, (f, u) => f.Phone.PhoneNumber())
                .RuleFor(x => x.LastLogin, (f, u) => DateTime.Now.AddDays(-2))
                .RuleFor(x => x.UserFirstRegister, (f, u) => f.Date.Past());

            var fakeData = users.Generate(numberOfRows);

            foreach (var item in fakeData)
            {
                var u = _mapper.Map<User>(item);
                var result = _userManager.CreateAsync(u, item.Password);
            }

            return resultValue;
        }

        public void CreateFakeCompany(int numberOfRows)
        {
            var company = new Faker<CompanyDTO>()
                .RuleFor(x => x.About, (f, u) => f.Lorem.Paragraph())
                .RuleFor(x => x.CompanyName, (f, u) => f.Company.CompanyName())
                .RuleFor(x => x.PhoneNumber, (f, u) => f.Phone.PhoneNumber())
                .RuleFor(x => x.Logo, (f, u) => f.Internet.Avatar())
                .RuleFor(x => x.WebSiteAddress, (f, u) => f.Internet.Url())
                .RuleFor(x => x.Address, (f, u) => f.Address.FullAddress());

            var fakeData = company.Generate(numberOfRows);
            var listCompany = _mapper.Map<List<Company>>(fakeData);

            _serviceAppCompany.AddRange(listCompany);
        }

        public void CreateFakeCompanyRate(int numberOfRows)
        {
            var company = new Faker<CompanyRate>()
                .RuleFor(x => x.CompanyId, (f, u) => f.Random.Number(1, 1000))
                .RuleFor(x => x.RateId, (f, u) => f.Random.Number(1, 5))
                .RuleFor(x => x.UserId, (f, u) => f.Random.Number(1, 18));

            var fakeData = company.Generate(numberOfRows);

            _serviceAppCompanyRate.AddRange(fakeData);
        }

        public void CreateFakeCompanySubjectIssue(int numberOfRows)
        {
            var company = new Faker<CompanySubjectIssue>()
                .RuleFor(x => x.TellUs, (f, u) => f.Lorem.Paragraph())
                .RuleFor(x => x.CompanyId, (f, u) => f.Random.Number(1, 1000))
                .RuleFor(x => x.SubjectId, (f, u) => f.Random.Number(1, 3))
                .RuleFor(x => x.SubjectIssueId, (f, u) => f.Random.Number(1, 3))
                .RuleFor(x => x.UserId, (f, u) => f.Random.Number(1, 18))
                .RuleFor(x => x.DateAndTime, (f, u) => f.Date.Past());

            var fakeData = company.Generate(numberOfRows);
            var listCompanySubjectIssue = _mapper.Map<List<CompanySubjectIssue>>(fakeData);

            _serviceAppCompanySubjectIssue.AddRange(fakeData);
        }

        public String ConvertImageURLToBase64(String url)
        {
            StringBuilder _sb = new StringBuilder();

            Byte[] _byte = this.GetImage(url);

            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

            return _sb.ToString();
        }

        private byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception exp)
            {
                buf = null;
            }

            return (buf);
        }
    }
}
