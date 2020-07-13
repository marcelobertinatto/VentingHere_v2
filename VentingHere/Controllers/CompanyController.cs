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
        private readonly IServiceAppCompany _serviceAppCompany;
        private readonly IMapper _mapper;

        #endregion

        public CompanyController(IHandleMessage<List<CompanyDTO>> handleMessageUser, IServiceAppCompany serviceAppCompany,
            IMapper mapper)
        {
            _handleMessageUser = handleMessageUser;
            _serviceAppCompany = serviceAppCompany;
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
    }
}