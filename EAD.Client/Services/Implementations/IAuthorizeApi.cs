using EAD.Shared;
using EAD.Shared.Models;

namespace EAD.Client.Services.Implementations
{
    public interface IAuthorizeApi
    {
        Task AddCompany(CompanyModel company);
        Task AddCompanyUser(CompanyModel company, UserInfo user);
        Task AddProject(ProjectModel project);
        Task AddProjectUser(ProjectModel project, UserInfo user);
        Task AddTicket(TicketModel ticket);
        Task DeleteCompany(CompanyModel company);
        Task DeleteCompanyUser(CompanyModel company, UserInfo user);
        Task DeleteProject(ProjectModel project);
        Task DeleteProjectUser(ProjectModel project, UserInfo user);
        Task DeleteTicket(TicketModel ticket);
        Task EditProject(ProjectModel project);
        Task EditTicket(TicketModel ticket);
        Task<List<CompanyModel>> GetAllCompanies();
        Task<List<ProjectModel>> GetAllProjects();
        Task<List<TicketModel>> GetAllTickets();
        Task<List<UserInfo>> GetAllUsers();
        Task<UserInfo> GetUserInfo();
        Task Login(LoginParameters loginParameters);
        Task Logout();
        Task Register(RegisterParameters registerParameters);
    }
}