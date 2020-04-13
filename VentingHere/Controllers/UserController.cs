using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VentingHere.Application.Interface;
using VentingHere.AutoMapper;
using VentingHere.Domain.Entities;
using VentingHere.ModelView;

namespace VentingHere.Controllers
{
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        #region INJECTED SERVICES
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IHandleMessage _handleMessage;
        #endregion

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager,
            IMapper mapper,
            IConfiguration config,
            IHandleMessage handleMessage)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
            _handleMessage = handleMessage;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogin([FromBody]UserLoginDTO user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var u = await _userManager.Users.FirstOrDefaultAsync(x => x.Email.ToUpper().Equals(user.Email.ToUpper()));

                    if (u != null)
                    {
                        //verify if the password match the requirements.
                        var outcome = await _signInManager.CheckPasswordSignInAsync(u, user.Password, false);

                        if (outcome.Succeeded)
                        {
                            var userToReturn = _mapper.Map<UserLoginDTO>(u);
                            return Ok(new
                            {
                                token = GenerateJWToken(u).Result,
                                user = u
                            });
                        }
                        else
                        {
                            var result = _handleMessage.Add(Application.Enum.HandleMessageType.Error, "User does not exist!");
                            return Ok(result);
                        }
                    }
                    else
                    {
                        var result = _handleMessage.Add(Application.Enum.HandleMessageType.Error, "User does not exist!");
                        return Ok(result);
                    }
                    
                }
                else
                {
                    var result = _handleMessage.Add(Application.Enum.HandleMessageType.Error, "Problem to log in!");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                var result = _handleMessage.Add(Application.Enum.HandleMessageType.InternalErrors, "Something went wrong: !" + ex.ToString());
                return Ok(result);
            }
        }

        [HttpPost("registeruser")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserDTO user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var u = _mapper.Map<User>(user);
                    var us = await _userManager.Users.FirstOrDefaultAsync(x => x.Email.ToUpper().Equals(user.Email.ToUpper()));
                    if (us != null)
                    {
                        var re1 = _handleMessage.Add(Application.Enum.HandleMessageType.Error, "User already exists!");
                        return Ok(re1);
                    }
                    var result = await _userManager.CreateAsync(u, user.Password);
                    var userToReturn = _mapper.Map<UserDTO>(u);

                    if (result.Succeeded)
                    {
                        //return Created("registeruser", userToReturn);
                        var re = _handleMessage.Add(Application.Enum.HandleMessageType.Success, "User was added successfully!");
                        return Ok(re);
                    }
                    else
                    {
                        string errors = null;
                        if (result.Errors != null)
                        {
                            foreach (var err in result.Errors)
                            {
                                errors = errors + err.Description + "\n";
                            } 
                        }
                        var re1 = _handleMessage.Add(Application.Enum.HandleMessageType.Error, string.IsNullOrEmpty(errors) ? "Something went wron!" : errors);
                        return Ok(re1);
                    }
                }
                else
                {
                    var result = _handleMessage.Add(Application.Enum.HandleMessageType.Error, "User model is not valid!");
                    return Ok(result);
                }
            }
            catch (System.Exception ex)
            {
                var result = _handleMessage.Add(Application.Enum.HandleMessageType.InternalErrors, "Something went wrong !");
                return Ok(result);
            }

        }


        #region PRIVATE METHODS
        private async Task<string> GenerateJWToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(_config.GetSection("AppSettings:Token").Value.ToString()));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}