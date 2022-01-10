using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD.Shared.Models
{
    public class CompanyModel
    {
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "Company name cannot be blank")]
        public string CompanyName { get; set; }

        public List<UserInfo> CompanyUserID { get; set; } //assigned company users (can be null)
    }
}
