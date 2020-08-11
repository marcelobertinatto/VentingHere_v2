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
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace VentingHere.Controllers
{
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        #region INJECTED SERVICES
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IServiceAppUser _serviceAppUser;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IHandleMessage<UserDetailsDTO> _handleMessageUser;
        private readonly IHandleMessage<UserSummary> _handleMessageUserSummary;
        private readonly IServiceAppCompanySubjectIssue _serviceAppCompanySubjectIssue;
        #endregion

        public UserController(IServiceAppUser serviceAppUser, UserManager<User> userManager, SignInManager<User> signInManager,
            IMapper mapper,
            IConfiguration config,
            IServiceAppCompanySubjectIssue serviceAppCompanySubjectIssue,
            IHandleMessage<UserSummary> handleMessageUserSummary,
            IHandleMessage<UserDetailsDTO> handleMessageUser)
        {
            _serviceAppUser = serviceAppUser;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
            _serviceAppCompanySubjectIssue = serviceAppCompanySubjectIssue;
            _handleMessageUserSummary = handleMessageUserSummary;
            _handleMessageUser = handleMessageUser;
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
                    var u = await _userManager.Users.FirstOrDefaultAsync(x => x.Email.ToUpper().Equals(user.Email.ToUpper()) && x.Password.Equals(user.Password));

                    if (u != null)
                    {
                        ////verify if the password match the requirements.
                        //var outcome = await _signInManager.CheckPasswordSignInAsync(u, user.Password, false);

                        //if (outcome.Succeeded)
                        //{
                            var userToReturn = _mapper.Map<UserLoginDTO>(u);
                            u.LastLogin = DateTime.Now;
                            var update = await _userManager.UpdateAsync(u);
                            if (update.Succeeded)
                            {
                                return Ok(new
                                {
                                    token = GenerateJWToken(u).Result,
                                    user = u,
                                    Application.Enum.HandleMessageType.Error
                                });
                            }
                            else
                            {
                                var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Error, "Problem to log in!",null);
                                return Ok(result);
                            }                            
                        //}
                        //else
                        //{
                        //    var result = _handleMessage.Add(Application.Enum.HandleMessageType.Error, "User does not exist!");
                        //    return Ok(result);
                        //}
                    }
                    else
                    {
                        var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Error, "User does not exist!",null);
                        return Ok(result);
                    }
                    
                }
                else
                {
                    var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Error, "Problem to log in!",null);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.InternalErrors, "Something went wrong: !" + ex.ToString(),null);
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
                        var re1 = _handleMessageUser.Add(Application.Enum.HandleMessageType.Error, "User already exists!",null);
                        return Ok(re1);
                    }
                    u.UserFirstRegister = DateTime.Now;
                    if (string.IsNullOrEmpty(u.Image))
                    {
                        u.Image = "R0lGODlhGAEYAZEAAP///+Xl5ZmZmQAAACwAAAAAGAEYAQAC/4yPqcvtD6OctNqLs968+w+G4kiW5omm6sq27gvH8kzX9o3n+s73/g8MCofEovGITCqXzKbzCY1Kp9Sq9YrNarfcrvcLVgrG4bJxjE6r1+y1+b1ry+dzuD1Gz+vV975pDxjo5kfIIXiIKFC4OJHo+KjIKBkAWVk5SWip6Yh5t/mZ2PkGSnooGlaaCnjKpeoayGr1OgsbO0WLq2crldtbtwvlKzwI3DR8jFZsjMysLMYM7fwDTc0m7VOdnXbNo+0dyW3zPQ4eLkP+bQ6Dzq7Owg5f7k4SXz//Vx9/P5Jvvx/Sz9+/DgHzDTRUUOBBDAkVLrTQUN9DhhHhTbxQ0eJFCv8ZNW6U0NHjxwch242EUBLdSZQpx61s0NLkywQxVc5EUNPmTQM5dd7sSW4nT6AudxItOvNo0J9KvRlt6jQpVG1Mp0Z7arVZ1azIsHLtKvUr2JViqb0sa3Yk2rQb17Kd6LZa27hX4dLVavfusbl6h13sO3Yh4L15B/cqbDgX4sS0/jJWvPixKseSZ/GtPJky5lSXN5Pq7PnTx9CfR5MWffK0JrKqL6VuDYk1bE6vZ4eSbVvQ2dy6w/LO4/X3L9/C5QQvbm0rcmLEl2877pyM0OjMmzsXOpS69Onat3PXjj179PDir5PvTp4S9fTqx6cHfx4+dPPK5Vt3X3/9fPy7uyf/y68fgPTdtxx2/nlHIHIG+hfegQ0yuCB6EdonYHEPSvhdgBMOmCGH+/32HoUf8haihh0WWKKHI9rGnogngsheewrGKKNwNNZI4o04sqjjjD3CqOOOrQV5AJBE5khkkbMlucCQTCqg2pNQkiZlk55VaWVlWDKA2ZZcaunllI+FmSVjZJZp2JlipqkmTWa26SabcCo52JxxAmYnTnXmqWdffPap15903iXooHQVWp5biCa61qJCiuUoo5BGemikj2Zl6aVWZVoppXFx+qmlnTo66qKlInpqoakKuuqfrfL5ap6x2jnrnLXCeWubuaq565m9kvlrmMF6GaqoioLaKLJo6WWqKVTMFutpsqQSqqqctI5pa5e8hiZslFjyyCSSP9p4o4orLhmfi7ipq9aB8tTmYH/uvhuZu6DN+9w/+K7izr63SeOvJbsEDMopBGdWyMGN+aHwYaM0LAwqEAemxcRvyWJxNlVk7NMyHHeMxMf9hCxyQkSU3NDJKBek8sojD+FyQC3HLJEQNBsE8801B6HzzkD0LNLPQIOMzdBL2Ww0UjwnnU7OTFPl9NNyRS11XUtXffE0WGdd9NZ4Xe01YUiHLTbYZPsy89kOj6322ma3vTDbcMf99tyupG333VTnjXDdfJcm99+lkFAAADs=";
                    }
                    var result = await _userManager.CreateAsync(u, user.Password);
                    //var userToReturn = _mapper.Map<UserDTO>(u);

                    if (result.Succeeded)
                    {
                        //return Created("registeruser", userToReturn);
                        var re = _handleMessageUser.Add(Application.Enum.HandleMessageType.Success, "User was added successfully!",null);
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
                        var re1 = _handleMessageUser.Add(Application.Enum.HandleMessageType.Error, string.IsNullOrEmpty(errors) ? "Something went wron!" : errors,null);
                        return Ok(re1);
                    }
                }
                else
                {
                    var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Error, "User model is not valid!",null);
                    return Ok(result);
                }
            }
            catch (System.Exception ex)
            {
                var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.InternalErrors, "Something went wrong !",null);
                return Ok(result);
            }

        }

        [HttpPost("saveuserdetails")]
        public async Task<IActionResult> SaveUserDetails([FromBody]UserDetailsDTO user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var u = await _userManager.Users.FirstOrDefaultAsync(x => x.Email.Equals(user.Email));
                    if (u != null)
                    {
                        var userChecked = await _userManager.Users.FirstOrDefaultAsync(x => x.Email.ToUpper().Equals(user.Email.ToUpper()) && x.Password.Equals(user.CurrentPassword));
                        if (userChecked != null)
                        {
                            if (user.Userimage != null)
                            {
                                u.Image = user.Userimage; 
                            }
                            u.Password = user.NewPassword;
                            u.Name = user.Fullname;
                            _serviceAppUser.Update(u);
                            var userMapper = _mapper.Map<UserDetailsDTO>(u);
                            //userMapper.SecurityStamp = u.SecurityStamp;
                            //userMapper.Id = u.Id;
                            //_serviceAppUser.Update(userMapper);
                            //var updatedUserResult = await _userManager.UpdateAsync(userMapper);
                            //if (updatedUserResult.Succeeded)
                            //{
                                var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Success, "User updated successfully!", userMapper);
                                return Ok(result); 
                            //}
                            //else
                            //{
                            //    var result = _handleMessage.Add(Application.Enum.HandleMessageType.Error, "Problem to update user!");
                            //    return Ok(result);
                            //}
                        }
                        else
                        {
                            var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Error, "Current password does not match!",null);
                            return Ok(result);
                        }                                              
                    }
                    else
                    {
                        var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Error, "User was not find!",null);
                        return Ok(result);
                    }
                }
                else
                {
                    var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.Error, "Problem to update user!",null);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                var result = _handleMessageUser.Add(Application.Enum.HandleMessageType.InternalErrors, "Something went wrong: !" + ex.ToString(),null);
                return Ok(result);
            }
        }

        [HttpPost("getusercomplaints")]
        public IActionResult GetUserComplaints([FromBody] int userId)
        {
            try
            {
                if (userId != 0)
                {
                    var u = _serviceAppCompanySubjectIssue.Find(x => x.UserId == userId).Distinct().ToList();

                    if (u != null)
                    {
                        var userSummary = new UserSummary();
                        userSummary.TotalOfComplaints = u.Count;
                        userSummary.ListCompanies = u;

                        var result = _handleMessageUserSummary.Add(Application.Enum.HandleMessageType.Success, "List of complaints found!", userSummary);
                        return Ok(result);
                    }
                    else
                    {
                        var result = _handleMessageUserSummary.Add(Application.Enum.HandleMessageType.Error, "List of complaints not found!", null);
                        return Ok(result);
                    }
                }
                else
                {
                    var result = _handleMessageUserSummary.Add(Application.Enum.HandleMessageType.Error, "Problem to find a list of complaints!", null);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                var result = _handleMessageUserSummary.Add(Application.Enum.HandleMessageType.InternalErrors, "Something went wrong: !" + ex.ToString(), null);
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