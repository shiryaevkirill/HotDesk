using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models.DbModels
{
    public class Role
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        [Remote(action: "CheckRoleName", controller: "Admin", ErrorMessage = "A role with that name already exists!")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter role name!")]
        [DisplayName("Role name")]
        public string RoleName { get; set; }


    }
}
