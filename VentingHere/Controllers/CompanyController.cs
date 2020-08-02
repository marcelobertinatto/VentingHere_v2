using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;
using VentingHere.ModelView;

namespace VentingHere.Controllers
{
    [Route("api/[Controller]")]
    public class CompanyController : Controller
    {
        #region INJECTED SERVICES

        private readonly IHandleMessage<List<CompanyDTO>> _handleMessageUser;
        private readonly IHandleMessage<CompanySubjectTellUsDTO> _handleMessageCompanySubjectTellUs;
        private readonly IHandleMessage<List<SubjectDTO>> _handleMessageSubject;
        private readonly IHandleMessage<List<SubjectIssueDTO>> _handleMessageSubjectIssue;
        private readonly IServiceAppCompany _serviceAppCompany;
        private readonly IServiceAppSubject _serviceAppSubject;
        private readonly IServiceAppSubjectIssue _serviceAppSubjectIssue;
        private readonly IServiceAppCompanySubjectIssue _serviceAppCompanySubjectIssue;
        private readonly IMapper _mapper;

        #endregion

        public CompanyController(IHandleMessage<List<CompanyDTO>> handleMessageUser,
            IHandleMessage<CompanySubjectTellUsDTO> handleMessageCompanySubjectTellUs,
            IHandleMessage<List<SubjectDTO>> handleMessageSubject,
            IHandleMessage<List<SubjectIssueDTO>> handleMessageSubjectIssue,
            IServiceAppCompany serviceAppCompany,
            IServiceAppSubject serviceAppSubject,
            IServiceAppSubjectIssue serviceAppSubjectIssue,
            IServiceAppCompanySubjectIssue serviceAppCompanySubjectIssue,
            IMapper mapper)
        {
            _handleMessageUser = handleMessageUser;
            _handleMessageCompanySubjectTellUs = handleMessageCompanySubjectTellUs;
            _handleMessageSubject = handleMessageSubject;
            _handleMessageSubjectIssue = handleMessageSubjectIssue;
            _serviceAppCompany = serviceAppCompany;
            _serviceAppSubject = serviceAppSubject;
            _serviceAppSubjectIssue = serviceAppSubjectIssue;
            _serviceAppCompanySubjectIssue = serviceAppCompanySubjectIssue;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet("getcompany/{searchTerm}")]
        public IActionResult GetCompany(string searchTerm)
        {
            try
            {
                if (!String.IsNullOrEmpty(searchTerm))
                {
                    var company = _serviceAppCompany.Find(x => x.CompanyName.ToUpper().Contains(searchTerm.ToUpper()))
                        .Select(x => x).ToList();
                    if (company.Count > 0)
                    {
                        var companyMapper = _mapper.Map<List<CompanyDTO>>(company);
                        var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Success, "Company found", companyMapper);
                        return Ok(result);
                    }
                    else
                    {
                        var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Success, "Company not found", null);
                        return Ok(result);
                    }                    
                }
                else
                {
                    var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Error, "Company was not find!", null);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.InternalErrors, "Something went wrong: !" + ex.ToString(), null);
                return Ok(result);
            }
        }

        [HttpPost("savecompany")]
        public IActionResult SaveCompany([FromBody] CompanyDTO companyDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var co = _serviceAppCompany.Find(x => x.CompanyName.ToUpper().Equals(companyDTO.CompanyName.ToUpper())).FirstOrDefault();

                    if (co != null)
                    {
                        var re1 = _handleMessageUser.Add(Application.Enum.HandleMessageType.Error, "Company already exists!", null);
                        return Ok(re1);
                    }
                    else
                    {
                        var company = _mapper.Map<Company>(companyDTO);
                        _serviceAppCompany.Add(company);
                        var re = _handleMessageUser.Add(Application.Enum.HandleMessageType.Success, "Company was added successfully!", null);
                        return Ok(re);
                    }
                }
                else
                {
                    var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Error, "Problem to register company!", null);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.InternalErrors, "Something went wrong: !" + ex.ToString(), null);
                return Ok(result);
            }
        }

        [HttpPost("savecompanytellus")]
        public IActionResult SaveCompany([FromBody] CompanySubjectTellUsDTO companySubjectTellUsDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (companySubjectTellUsDTO.SubjectId != 0 && companySubjectTellUsDTO.SubjectIssueId != 0)
                    {
                        var companySubjectIssueTellUs = _mapper.Map<CompanySubjectIssue>(companySubjectTellUsDTO);
                        _serviceAppCompanySubjectIssue.Add(companySubjectIssueTellUs);
                        var re = _handleMessageUser.Add(Application.Enum.HandleMessageType.Success, "Complaint registered successfully!", null);
                        return Ok(re);
                    }
                    else
                    {
                        var subject = _mapper.Map<Subject>(companySubjectTellUsDTO);
                        var subjectIssue = _mapper.Map<SubjectIssue>(companySubjectTellUsDTO);

                        var subId = _serviceAppSubject.Find(x => x.SubjectText.ToUpper().Equals(subject.SubjectText))
                                    .FirstOrDefault().Id;

                        var subIssueId = _serviceAppSubjectIssue.Find(x => x.SubjectIssueText.ToUpper().Equals(subjectIssue.SubjectIssueText))
                                    .FirstOrDefault().Id;

                        if (subId == 0 && subIssueId == 0)
                        {
                            subjectIssue.Subject = subject;
                            _serviceAppSubjectIssue.Add(subjectIssue); 
                        }

                        companySubjectTellUsDTO.SubjectId = subjectIssue.SubjectId;
                        companySubjectTellUsDTO.SubjectIssueId = subjectIssue.Id;
                        
                        if (companySubjectTellUsDTO.CompanyId == 0)
                        {
                            var co = _serviceAppCompany.Find(x => x.CompanyName.ToUpper().Equals(companySubjectTellUsDTO.CompanyName)).FirstOrDefault();

                            if (co == null)
                            {
                                var company = _mapper.Map<Company>(companySubjectTellUsDTO);
                                _serviceAppCompany.Add(company);
                                companySubjectTellUsDTO.CompanyId = company.Id; 

                                var companySubjectIssueTellUs = _mapper.Map<CompanySubjectIssue>(companySubjectTellUsDTO);
                                _serviceAppCompanySubjectIssue.Add(companySubjectIssueTellUs);
                            }
                            else
                            {
                                companySubjectTellUsDTO.CompanyId = co.Id;

                                var companySubjectIssueTellUs = _mapper.Map<CompanySubjectIssue>(companySubjectTellUsDTO);
                                _serviceAppCompanySubjectIssue.Add(companySubjectIssueTellUs);
                            }
                        }
                        else
                        {
                            companySubjectTellUsDTO.CompanyId = companySubjectTellUsDTO.CompanyId;
                            var companySubjectIssueTellUs = _mapper.Map<CompanySubjectIssue>(companySubjectTellUsDTO);

                            var companySubjectIssue = _serviceAppCompanySubjectIssue.Find(x => x.SubjectId == companySubjectIssueTellUs.SubjectId
                            && x.SubjectIssueId == companySubjectIssueTellUs.SubjectIssueId && x.CompanyId == companySubjectIssueTellUs.CompanyId
                            && x.UserId == companySubjectIssueTellUs.UserId).FirstOrDefault();

                            if (companySubjectIssue == null)
                            {
                                _serviceAppCompanySubjectIssue.Add(companySubjectIssueTellUs);
                            }
                            else
                            {
                                var result = _handleMessageCompanySubjectTellUs.Add(Application.Enum.HandleMessageType.Success, "Complaint already exists!", companySubjectTellUsDTO);
                                return Ok(result);
                            }
                        }

                        var re = _handleMessageUser.Add(Application.Enum.HandleMessageType.Success, "Complaint registered successfully!", null);
                        return Ok(re);
                    }
                }
                else
                {
                    var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Error, "Problem to register your complaint!", null);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.InternalErrors, "Something went wrong: !" + ex.ToString(), null);
                return Ok(result);
            }
        }

        [AllowAnonymous]
        [HttpGet("getsubject")]
        public IActionResult GetSubject()
        {
            try
            {
                var subject = _serviceAppSubject.GetAll().ToList();
                if (subject.Count > 0)
                {
                    var subjectMapper = _mapper.Map<List<SubjectDTO>>(subject);
                    var result = _handleMessageSubject.Add(Application.Enum.HandleMessageType.Success, "Subject found", subjectMapper);
                    return Ok(result);
                }
                else
                {
                    var result = _handleMessageSubject.Add(Application.Enum.HandleMessageType.Success, "Subject not found", null);
                    return Ok(result);
                }                
            }
            catch (Exception ex)
            {
                var result = _handleMessageSubject.Add(Application.Enum.HandleMessageType.InternalErrors, "Something went wrong: !" + ex.ToString(), null);
                return Ok(result);
            }
        }

        [AllowAnonymous]
        [HttpGet("getsubjectissue/{id}")]
        public IActionResult GetSubjectIssue(string id)
        {
            try
            {
                var subjectIssues = _serviceAppSubjectIssue.Find(x => x.SubjectId == int.Parse(id)).ToList();
                if (subjectIssues.Count > 0)
                {
                    var subjectIssueMapper = _mapper.Map<List<SubjectIssueDTO>>(subjectIssues);
                    var result = _handleMessageSubjectIssue.Add(Application.Enum.HandleMessageType.Success, "Subject found", subjectIssueMapper);
                    return Ok(result);
                }
                else
                {
                    var result = _handleMessageSubjectIssue.Add(Application.Enum.HandleMessageType.Success, "Subject not found", null);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                var result = _handleMessageSubjectIssue.Add(Application.Enum.HandleMessageType.InternalErrors, "Something went wrong: !" + ex.ToString(), null);
                return Ok(result);
            }
        }
    }
}