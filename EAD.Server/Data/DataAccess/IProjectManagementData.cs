using EAD.Shared;
using EAD.Shared.Models;

namespace EAD.Server.Data.DataAccess
{
    public interface IProjectManagementData
    {
        void AddCompany(CompanyModel company);
        void AddCompanyUser(int CompanyID, Guid UserID);
        void AddProject(ProjectModel project);
        void AddProjectUser(int ProjectID, Guid UserID);
        void AddTicket(TicketModel ticket);
        void DeleteCompany(CompanyModel company);
        void DeleteCompanyUser(int CompanyID, Guid UserID);
        void DeleteProject(ProjectModel project);
        void DeleteProjectUser(int ProjectID, Guid UserID);
        void DeleteTicket(TicketModel ticket);
        void EditProject(ProjectModel project);
        void EditTicket(TicketModel ticket);
        List<CompanyModel> LoadAllCompanies(List<UserInfo> allUsers);
        List<ProjectModel> LoadAllProjects(List<UserInfo> allUsers);
        List<TicketModel> LoadAllTickets();
    }
}