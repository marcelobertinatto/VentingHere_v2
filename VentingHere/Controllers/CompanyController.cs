using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VentingHere.Application.Interface;
using VentingHere.Domain.Entities;

namespace VentingHere.Controllers
{
    [Route("api/[Controller]")]
    public class CompanyController : Controller
    {
        #region INJECTED SERVICES

        private readonly IHandleMessage<List<string>> _handleMessageUser;
        private readonly IServiceAppCompany _serviceAppCompany;

        #endregion
        public CompanyController(IHandleMessage<List<string>> handleMessageUser, IServiceAppCompany serviceAppCompany)
        {
            _handleMessageUser = handleMessageUser;
            _serviceAppCompany = serviceAppCompany;
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
                    var company = _serviceAppCompany.Find(x => x.CompanyName.Equals(searchTerm)).Select(x => x.CompanyName).ToList();
                    if (company.Count > 0)
                    {
                        var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Success, "Company found", company);
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
    }
}