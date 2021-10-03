using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models.AdminModels
{
    public class AddEmployeeModel
    {
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter name!")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter surname!")]
        [DisplayName("Surname")]
        public string Surname { get; set; }

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        [Remote(action: "CheckLogin", controller: "Admin", ErrorMessage = "Login is already taken!")]
        [Required(ErrorMessage = "Enter username!")]
        [DisplayName("Username")]
        public string Login { get; set; }

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        [Required(ErrorMessage = "Enter password!")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }


        [DisplayName("Role")]
        public string Role { get; set; }

        public string[] RoleName { get; set; }
    }
}
