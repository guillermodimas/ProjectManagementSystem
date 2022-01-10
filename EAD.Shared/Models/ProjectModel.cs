using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD.Shared.Models
{
    public class ProjectModel
    {
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Project title cannot be blank")]
        public string ProjectTitle { get; set; }

        [Required(ErrorMessage = "Project overview cannot be blank")]
        public string ProjectOverview { get; set; }

        [Required(ErrorMessage = "Project due date required")]
        public DateTime ProjectDueDate { get; set; }

        public int? AssignedCompanyID { get; set; }

        public List<UserInfo> ProjectAssignedUser { get; set; } //assigned list of users assigned to the project
    }
}

