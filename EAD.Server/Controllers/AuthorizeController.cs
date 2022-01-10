using EAD.Server.Data.DataAccess;
using EAD.Server.Models;
using EAD.Shared;
using EAD.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EAD.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IProjectManagementData _projectManagementData;

        public AuthorizeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IProjectManagementData projectManagementData)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _projectManagementData = projectManagementData;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginParameters parameters)
        {
            var user = await _userManager.FindByNameAsync(parameters.EmailAddress);
            if (user == null) return BadRequest("User does not exist");
            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Password, false);
            if (!singInResult.Succeeded) return BadRequest("Invalid password");

            await _signInManager.SignInAsync(user, new AuthenticationProperties()
            {
                IsPersistent = parameters.RememberMe,

                //for messing with server authentication timeout (cookies)
                //    ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(15)

            });



            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterParameters parameters)
        {
            var user = new ApplicationUser { UserName = parameters.EmailAddress, Email = parameters.EmailAddress, FirstName = parameters.FirstName, LastName = parameters.LastName };

            var result = await _userManager.CreateAsync(user, parameters.Password);
            if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

            return await Login(new LoginParameters
            {
                EmailAddress = parameters.EmailAddress,
                Password = parameters.Password
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        public UserInfo UserInfo()
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            return BuildUserInfo();
        }




        private UserInfo BuildUserInfo()
        {
            if (User.Identity.Name != null)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                return new UserInfo
                {
                    Id = user.Id.ToString(),
                    IsAuthenticated = User.Identity.IsAuthenticated,
                    UserName = User.Identity.Name,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ExposedClaims = User.Claims
                        //Optionally: filter the claims you want to expose to the client
                        //.Where(c => c.Type == "test-claim")
                        .ToDictionary(c => c.Type, c => c.Value)
                };
            }
            else
            {
                return new UserInfo();
            }

        }

        [Authorize]
        [HttpGet]
        public List<UserInfo> AllUsers()
        {
            var userList = new List<UserInfo>();
            var users = _userManager.Users;
            if (users != null)
            {
                foreach (var user in users)
                {
                    userList.Add(new UserInfo
                    {
                        Id = user.Id.ToString(),
                        IsAuthenticated = User.Identity.IsAuthenticated,
                        UserName = User.Identity.Name,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ExposedClaims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
                    });
                }
            }
            return userList;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var allUsers = AllUsers();
            List<CompanyModel> result = null;
            try
            {
                result = _projectManagementData.LoadAllCompanies(allUsers);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteCompanyUser(int CompanyID, Guid Id) //Id (UserID)
        {
            _projectManagementData.DeleteCompanyUser(CompanyID, Id);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddCompanyUser(int CompanyID, Guid Id) //Id (UserID)
        {
            _projectManagementData.AddCompanyUser(CompanyID, Id);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCompany(CompanyModel company) //Id (UserID)
        {
            _projectManagementData.AddCompany(company);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteCompany(CompanyModel company) //Id (UserID)
        {
            try
            {
                _projectManagementData.DeleteCompany(company);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var allUsers = AllUsers();
            List<ProjectModel> result = null;
            try
            {
                result = _projectManagementData.LoadAllProjects(allUsers);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProject(ProjectModel project) //Id (UserID)
        {
            try
            {
                _projectManagementData.AddProject(project);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProject(ProjectModel project) //Id (UserID)
        {
            try
            {
                _projectManagementData.EditProject(project);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteProject(ProjectModel project) //Id (UserID)
        {
            try
            {
                _projectManagementData.DeleteProject(project);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddProjectUser(int ProjectID, Guid UserID) //Id (UserID)
        {
            _projectManagementData.AddProjectUser(ProjectID, UserID);
            return Ok();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteProjectUser(int ProjectID, Guid UserID) //Id (UserID)
        {
            _projectManagementData.DeleteProjectUser(ProjectID, UserID);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            
            List<TicketModel> result = null;
            try
            {
                result = _projectManagementData.LoadAllTickets();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTicket(TicketModel ticket) //Id (UserID)
        {
            try
            {
                _projectManagementData.AddTicket(ticket);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditTicket(TicketModel ticket) //Id (UserID)
        {
            try
            {
                _projectManagementData.EditTicket(ticket);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteTicket(TicketModel ticket) //Id (UserID)
        {
            try
            {
                _projectManagementData.DeleteTicket(ticket);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
