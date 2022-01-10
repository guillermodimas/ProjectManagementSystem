using EAD.Client.Pages;
using EAD.Server.Data.Internal;
using EAD.Shared;
using EAD.Shared.Models;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EAD.Server.Data.DataAccess
{
    public class ProjectManagementData : IProjectManagementData
    {
        private readonly ISQLDataAccess _sQLDataAccess;
        private readonly ILogger<ProjectManagementData> _logger;

        public ProjectManagementData(ISQLDataAccess sQLDataAccess, ILogger<ProjectManagementData> logger)
        {
            _sQLDataAccess = sQLDataAccess;
            _logger = logger;
        }

        public List<CompanyModel> LoadAllCompanies(List<UserInfo> allUsers)
        {
            //show how to group related models into a single model
            var companies = _sQLDataAccess.LoadData<CompanyModel, dynamic>("dbo.spLoadAllCompanies", new { }, "DefaultConnection");


            foreach (var company in companies)
            {


                //Load CompanyUser Per Company
                var users = _sQLDataAccess.LoadData<Guid, dynamic>("dbo.spLoadCompanyUsersByCompanyID", new { company.CompanyID }, "DefaultConnection");

                List<UserInfo> companyUsers = new List<UserInfo>();
                foreach (var user in users)
                {
                    companyUsers.Add(allUsers.Where(x => x.Id == user.ToString()).FirstOrDefault());
                }
                foreach (var compUser in companyUsers)
                {
                    if (companies.Where(x => x.CompanyID == company.CompanyID).First().CompanyUserID == null)
                    {
                        companies.Where(x => x.CompanyID == company.CompanyID).First().CompanyUserID = new List<UserInfo>() { compUser };
                    }
                    else
                    {
                        companies.Where(x => x.CompanyID == company.CompanyID).First().CompanyUserID.Add(compUser);
                    }
                }




            }
            return companies;
        }

        public void DeleteCompanyUser(int CompanyID, Guid UserID)
        {
            try
            {
                _sQLDataAccess.SaveData("dbo.spDeleteCompanyUser", new { CompanyID, UserID }, "DefaultConnection");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public void AddCompanyUser(int CompanyID, Guid UserID)
        {
            try
            {
                _sQLDataAccess.SaveData("dbo.spAddCompanyUser", new { CompanyID, UserID }, "DefaultConnection");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void AddCompany(CompanyModel company)
        {
            try
            {
                _sQLDataAccess.SaveData("dbo.spAddCompany", new { company.CompanyName }, "DefaultConnection");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public void DeleteCompany(CompanyModel company)
        {
            try
            {
                _sQLDataAccess.SaveData("dbo.spDeleteCompany", new { company.CompanyID }, "DefaultConnection");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public List<ProjectModel> LoadAllProjects(List<UserInfo> allUsers)
        {
            //show how to group related models into a single model

            //display ALL projects
            var projects = _sQLDataAccess.LoadData<ProjectModel, dynamic>("dbo.spLoadAllProjects", new { }, "DefaultConnection");
            foreach (var proj in projects)
            {
                var users = _sQLDataAccess.LoadData<Guid, dynamic>("dbo.spLoadProjectUsersByProjectID", new { proj.ProjectID }, "DefaultConnection");
                List<UserInfo> projectUsers = new List<UserInfo>();
                foreach (var user in users)
                {
                    projectUsers.Add(allUsers.Where(x => x.Id == user.ToString()).FirstOrDefault());
                }
                foreach (var projUser in projectUsers)
                {
                    if (projects.Where(x => x.ProjectID == proj.ProjectID).First().ProjectAssignedUser == null)
                    {
                        projects.Where(x => x.ProjectID == proj.ProjectID).First().ProjectAssignedUser = new List<UserInfo>() { projUser };
                    }
                    else
                    {
                        projects.Where(x => x.ProjectID == proj.ProjectID).First().ProjectAssignedUser.Add(projUser);
                    }
                }
            }
            return projects;
        }
        public void AddProject(ProjectModel project)
        {
            try
            {
                _sQLDataAccess.SaveData("dbo.spAddProject", new { project.ProjectTitle, project.ProjectOverview, project.ProjectDueDate, project.AssignedCompanyID }, "DefaultConnection");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public void EditProject(ProjectModel project)
        {
            try
            {
                _sQLDataAccess.SaveData("dbo.spEditProject", new { project.ProjectID, project.ProjectTitle, project.ProjectOverview, project.ProjectDueDate, project.AssignedCompanyID }, "DefaultConnection");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public void DeleteProject(ProjectModel project)
        {
            try
            {
                _sQLDataAccess.SaveData("dbo.spDeleteProject", new { project.ProjectID }, "DefaultConnection");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public void AddProjectUser(int ProjectID, Guid UserID)
        {
            try
            {
                _sQLDataAccess.SaveData("dbo.spAddProjectUser", new { ProjectID, UserID }, "DefaultConnection");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public void DeleteProjectUser(int ProjectID, Guid UserID)
        {
            try
            {
                _sQLDataAccess.SaveData("dbo.spDeleteProjectUser", new { ProjectID, UserID }, "DefaultConnection");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public List<TicketModel> LoadAllTickets()
        {
            return _sQLDataAccess.LoadData<TicketModel, dynamic>("dbo.spLoadAllTickets", new { }, "DefaultConnection");
        }

        public void AddTicket(TicketModel ticket)
        {
            try
            {
                _sQLDataAccess.SaveData("dbo.spAddTicket", new { Status = Convert.ToInt32(ticket.Status), ticket.Title, ticket.Description, ticket.AssignedProjectID, ticket.AssignedUserID }, "DefaultConnection");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        public void EditTicket(TicketModel ticket)
        {
            try
            {
                _sQLDataAccess.SaveData("dbo.spEditTicket", new { ticket.TicketID, Status = Convert.ToInt32(ticket.Status), ticket.Title, ticket.Description, ticket.AssignedProjectID, ticket.AssignedUserID }, "DefaultConnection");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public void DeleteTicket(TicketModel ticket)
        {
            try
            {
                _sQLDataAccess.SaveData("dbo.spDeleteTicket", new { ticket.TicketID }, "DefaultConnection");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

    }
}
