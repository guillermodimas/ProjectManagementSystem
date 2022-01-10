using EAD.Client.Services;
using EAD.Shared;
using EAD.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EAD.Client.Services.Implementations
{
    public class AuthorizeApi : IAuthorizeApi
    {
        private readonly HttpClient _httpClient;

        public AuthorizeApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Login(LoginParameters loginParameters)
        {
            //var stringContent = new StringContent(JsonSerializer.Serialize(loginParameters), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsJsonAsync("api/Authorize/Login", loginParameters);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync("api/Authorize/Logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegisterParameters registerParameters)
        {
            //var stringContent = new StringContent(JsonSerializer.Serialize(registerParameters), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsJsonAsync("api/Authorize/Register", registerParameters);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<UserInfo> GetUserInfo()
        {
            var result = await _httpClient.GetFromJsonAsync<UserInfo>("api/Authorize/UserInfo");
            return result;
        }

        public async Task<List<UserInfo>> GetAllUsers()
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserInfo>>("api/Authorize/AllUsers");
            return result;
        }
        public async Task<List<CompanyModel>> GetAllCompanies()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CompanyModel>>("api/Authorize/GetCompanies");
            return result;
        }

        public async Task DeleteCompanyUser(CompanyModel company, UserInfo user)
        {
            var result = await _httpClient.GetAsync($"api/Authorize/DeleteCompanyUser?CompanyID={company.CompanyID}&Id={user.Id}");
            result.EnsureSuccessStatusCode();
        }

        public async Task AddCompanyUser(CompanyModel company, UserInfo user)
        {
            var result = await _httpClient.GetAsync($"api/Authorize/AddCompanyUser?CompanyID={company.CompanyID}&Id={user.Id}");
            result.EnsureSuccessStatusCode();
        }

        public async Task AddCompany(CompanyModel company)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/Authorize/AddCompany", company);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task DeleteCompany(CompanyModel company)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/Authorize/DeleteCompany", company);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<List<ProjectModel>> GetAllProjects()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ProjectModel>>("api/Authorize/GetProjects");
            return result;
        }

        public async Task AddProject(ProjectModel project)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/Authorize/AddProject", project);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
        public async Task AddProjectUser(ProjectModel project, UserInfo user)
        {
            var result = await _httpClient.GetAsync($"api/Authorize/AddProjectUser?ProjectID={project.ProjectID}&UserID={user.Id}");
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
        public async Task DeleteProjectUser(ProjectModel project, UserInfo user)
        {
            var result = await _httpClient.GetAsync($"api/Authorize/DeleteProjectUser?ProjectID={project.ProjectID}&UserID={user.Id}");
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task EditProject(ProjectModel project)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/Authorize/EditProject", project);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
        public async Task DeleteProject(ProjectModel project)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/Authorize/DeleteProject", project);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
        public async Task<List<TicketModel>> GetAllTickets()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TicketModel>>("api/Authorize/GetTickets");
            return result;
        }

        public async Task AddTicket(TicketModel ticket)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/Authorize/AddTicket", ticket);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
        public async Task EditTicket(TicketModel ticket)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/Authorize/EditTicket", ticket);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
        public async Task DeleteTicket(TicketModel ticket)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/Authorize/DeleteTicket", ticket);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
    }
}
